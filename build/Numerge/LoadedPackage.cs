using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using static Numerge.Constants;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace Numerge
{
    class LoadedPackage
    {
        public string FileName { get; }
        public Dictionary<string, byte[]> BinaryContents { get;  } = new Dictionary<string, byte[]>();
        public LoadedNuspec Spec { get; }
        public ContentTypes ContentTypes { get; }
        readonly string _nuspecName;

        public LoadedPackage(string fileName, byte[] package)
        {
            FileName = fileName;
            var arch = new ZipArchive(new MemoryStream(package));
            foreach (var e in arch.Entries)
            {
                using (var s = e.Open())
                {
                    var ms = new MemoryStream();
                    s.CopyTo(ms);
                    BinaryContents.Add(e.FullName.Replace("\\", "/"), ms.ToArray());
                }
            }

            var nuspecItem = BinaryContents.First(c => c.Key.EndsWith("nuspec"));
            _nuspecName = nuspecItem.Key;
            
            Spec = new LoadedNuspec(nuspecItem.Value);
            BinaryContents.Remove(_nuspecName);
            
            ContentTypes = new ContentTypes(BinaryContents[ContentTypesFileName]);
            BinaryContents.Remove(ContentTypesFileName);
        }

        public LoadedPackage(string path) : this(Path.GetFileName(path), File.ReadAllBytes(path))
        {
            
        }

        public void ResolveBinaryDependencies(Dictionary<string, LoadedPackage> pkgs)
        {
            foreach (var grp in Spec.Dependencies)
            {
                foreach (var d in grp.Value.ToList())
                {
                    if (pkgs.TryGetValue(d.Id, out var bindep))
                        grp.Value.Replace(d.Id, bindep);
                }
            }
        }

        public void MergeContents(INumergeLogger logger, LoadedPackage victim,
            PackageMergeConfiguration config)
        {
            var ignoredPrefixes = new[] {"lib/", "_rels/", "package/"};
            foreach (var item in victim.BinaryContents.Where(x =>
                x.Key.Contains("/") && !ignoredPrefixes.Any(p => x.Key.StartsWith(p))))
            {
                if (BinaryContents.ContainsKey(item.Key))
                    logger.Warning($"{Spec.Id}: Refusing to replace item {item.Key} with item from {victim.Spec.Id}");
                else
                {
                    BinaryContents[item.Key] = item.Value;
                }
            }

            var libs = victim.BinaryContents.Where(x => x.Key.StartsWith("lib/"))
                .Select(x => new {sp = x.Key.Split(new[] {'/'}, 3), data = x.Value})
                .Select(x => new {Tfm = x.sp[1], File = x.sp[2], Data = x.data}).GroupBy(x => x.Tfm)
                .ToDictionary(x => x.Key, x => x.ToList());

            var ourFrameworks = BinaryContents.Where(x => x.Key.StartsWith("lib/")).Select(x => x.Key.Split('/')[1])
                .Distinct().ToList();

            foreach (var foreignTfm in libs)
                if (!ourFrameworks.Contains(foreignTfm.Key))
                    throw new MergeAbortedException(
                        $"Error merging {victim.Spec.Id}: Package {Spec.Id} doesn't have target framework {foreignTfm.Key}");
            
            //TODO: Actually detect compatibility with .NET Standard
            libs.TryGetValue("netstandard2.0", out var netstandardLibs);

            foreach (var framework in ourFrameworks)
            {
                libs.TryGetValue(framework, out var frameworkLibs);
                frameworkLibs = frameworkLibs ?? netstandardLibs;
                if (frameworkLibs == null)
                {
                    if (!config.IgnoreMissingFrameworkBinaries)
                        throw new MergeAbortedException(
                            $"Unable to merge {victim.Spec.Id} to {Spec.Id}: {victim.Spec.Id} doesn't support {framework} or netstandard2.0");
                }
                else

                    foreach (var lib in frameworkLibs)
                    {
                        var targetPath = $"lib/{framework}/{lib.File}";
                        if (BinaryContents.ContainsKey(targetPath))
                            logger.Warning(
                                $"{Spec.Id}: Refusing to replace item {targetPath} with item from {victim.Spec.Id}");
                        else
                            BinaryContents[targetPath] = lib.Data;
                    }
            }

           

            if (!config.DoNotMergeDependencies)
            {
                //TODO: Actually detect compatibility with .NET Standard
                if (!victim.Spec.Dependencies.TryGetValue(".NETStandard2.0", out var netstandardDeps))
                    victim.Spec.Dependencies.TryGetValue("netstandard2.0", out netstandardDeps);
                
                var handledDepFrameworks = new HashSet<string>();
                foreach (var group in victim.Spec.Dependencies)
                {
                    Spec.Dependencies.GetOrCreateGroup(group.Key)
                        .AddRange(group.Value);
                    handledDepFrameworks.Add(group.Key);
                }

                foreach (var ourGroup in Spec.Dependencies)
                {
                    // Merge deps
                    if (!handledDepFrameworks.Contains(ourGroup.Key))
                    {
                        if (netstandardDeps == null)
                        {
                            if (!config.IgnoreMissingFrameworkDependencies)
                                throw new MergeAbortedException(
                                    $"Unable to merge dependencies from {victim.Spec.Id} to {Spec.Id}: {victim.Spec.Id} doesn't have deps for {ourGroup.Key} or netstandard2.0");
                        }
                        else
                        {
                            ourGroup.Value.AddRange(netstandardDeps);
                        }
                    }
                }
            }

            foreach (var ct in victim.ContentTypes)
                if (!ContentTypes.ContainsKey(ct.Key))
                    ContentTypes[ct.Key] = ct.Value;

        }
        
        public void ReplaceDeps(string from, LoadedPackage to)
        {
            Spec.Dependencies.Replace(from, to);
        }

        public void Save(Stream stream)
        {
            var contents = BinaryContents.ToDictionary(x => x.Key, x => x.Value);
            contents[_nuspecName] = Spec.Serialize();
            contents[ContentTypesFileName] = ContentTypes.Serialize();
            using (var arch = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                foreach (var c in contents)
                {
                    var e = arch.CreateEntry(c.Key, CompressionLevel.Optimal);
                    using (var es = e.Open())
                        es.Write(c.Value, 0, c.Value.Length);
                }
            }
        }

        public void SaveToDirectory(string dirPath)
        {
            Directory.CreateDirectory(dirPath);
            var path = Path.Combine(dirPath, FileName);
            using (var s = File.Create(path))
                Save(s);
        }
        
    }


    class LoadedNuspec
    {
        readonly byte[] _data;
        public string Id { get; }
        public string Version { get; }
        readonly string _xmlns;
        
        public XName NugetName(string name) => XName.Get(name, _xmlns);
        
        public Dependencies Dependencies { get; } = new Dependencies();

        public LoadedNuspec(byte[] data)
        {
            
            _data = data;
            var doc = XDocument.Load(new MemoryStream(data));
            _xmlns = doc.Root.Name.Namespace.ToString();
            
            var deps = doc.Root.Descendants(NugetName("dependencies")).First();
            foreach (var group in deps.Elements(NugetName("group")))
            {
                var tfm = group.Attribute("targetFramework").Value;
                var groupList = Dependencies[tfm] = new DependencyGroup();
                foreach (var dep in group.Elements())
                    groupList.Add(new ExternalDependency(dep));
            }

            var metadata = doc.Root.Element(NugetName("metadata"));
            Id = metadata.Element(NugetName("id")).Value;
            Version = metadata.Element(NugetName("version")).Value;
        }

        public byte[] Serialize()
        {
            RemoveSelfDependency();
            var doc = XDocument.Load(new MemoryStream(_data));
            var deps = doc.Root.Descendants(NugetName("dependencies")).First();
            deps.RemoveAll();
            foreach (var group in Dependencies)
            {
                var el = new XElement(NugetName("group"));
                el.SetAttributeValue("targetFramework", group.Key);
                deps.Add(el);
                foreach (var dep in group.Value)
                    el.Add(dep.Serialize(_xmlns));
            }
            var ms = new MemoryStream();
            doc.Save(ms, SaveOptions.OmitDuplicateNamespaces);
            return ms.ToArray();
        }

        public void RemoveSelfDependency() => Dependencies.RemoveDependency(Id);
    }

    class Dependencies : Dictionary<string, DependencyGroup>
    {
        public DependencyGroup GetOrCreateGroup(string tfm)
        {
            if (!TryGetValue(tfm, out var grp))
                grp = this[tfm] = new DependencyGroup();
            return grp;
        }
        
        public void Add(string tfm, IDependency dependency)
        {
            GetOrCreateGroup(tfm).Add(dependency);
        }

        public void Replace(string what, LoadedPackage with)
        {
            foreach (var g in Values)
                g.Replace(what, with);
        }

        public void RemoveDependency(string id)
        {
            foreach (var g in Values)
                g.Remove(id);
        }
    }

    class DependencyGroup : IEnumerable<IDependency>
    {
        List<IDependency> _list = new List<IDependency>();
        public IEnumerator<IDependency> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        static string MergeExcludes(string first, string second)
        {
            var sl = (first ?? "").Split(',');
            var s2 = (second ?? "").Split(',');
            var a = sl.Where(e => s2.Contains(e)).ToArray();
            if (a.Length == 0)
                return null;
            return string.Join(",", a);
        }
        
        public void Add(IDependency dependency)
        {
            for (var c = 0; c < _list.Count; c++)
            {
                var d = _list[c];
                if (d.Id == dependency.Id)
                { 
                    var target = d is BinaryDependency ? d : dependency is BinaryDependency ? dependency : d;
                    target.ExcludeAssets = MergeExcludes(d.ExcludeAssets, dependency.ExcludeAssets);
                    _list[c] = target;
                    return;
                }
            }
            _list.Add(dependency);
        }

        public void AddRange(IEnumerable<IDependency> deps)
        {
            foreach (var d in deps)
                Add(d);
        }

        public void Replace(string what, LoadedPackage package)
        {
            var dep = _list.FirstOrDefault(d => d.Id == what);
            if (dep == null)
                return;
            var newDep = new BinaryDependency(dep, package);
            _list.Remove(dep);
            Add(newDep);
        }

        public void Remove(string id)
        {
            var dep = _list.FirstOrDefault(d => d.Id == id);
            if (dep != null)
                _list.Remove(dep);
        }
    }
    
    interface IDependency
    {
        string Id { get; }
        string ExcludeAssets { get; set; }
        XElement Serialize(string xmlna);
    }

    class ExternalDependency : IDependency
    {
        private readonly XElement _el;

        public ExternalDependency(XElement el)
        {
            _el = el;
            Id = el.Attribute("id").Value;
            ExcludeAssets = el.Attribute("exclude")?.Value;
        }

        public string Id { get; }
        public string ExcludeAssets { get; set; }
        public XElement Serialize(string xmlns)
        {
            var rv = XElement.Load(_el.CreateReader());
            if (!string.IsNullOrWhiteSpace(ExcludeAssets))
                rv.SetAttributeValue("exclude", ExcludeAssets);
            else
                rv.Attribute("exclude")?.Remove();
            
            foreach (var e in rv.DescendantsAndSelf())
            {
                e.Name = XName.Get(rv.Name.LocalName, xmlns);
                foreach (var a in e.Attributes().ToList())
                {
                    a.Remove();
                    e.SetAttributeValue(XName.Get(a.Name.LocalName), a.Value);
                }
            }

            return rv;
        }
    }

    class BinaryDependency : IDependency
    {
        public LoadedPackage Package { get; }
        public string Id => Package.Spec.Id;
        public string ExcludeAssets { get; set; }

        public BinaryDependency(IDependency original, LoadedPackage package)
        {
            Package = package;
            ExcludeAssets = original.ExcludeAssets;
        }
        
        public XElement Serialize(string xmlns)
        {
            var rv = new XElement(XName.Get("dependency", xmlns));
            rv.SetAttributeValue("id", Id);
            rv.SetAttributeValue("version", Package.Spec.Version);
            if (!string.IsNullOrWhiteSpace(ExcludeAssets))
                rv.SetAttributeValue("exclude", ExcludeAssets);
            return rv;
        }
    }

    class ContentTypes : Dictionary<string, string>
    {
        public ContentTypes(byte[] data)
        {
            var doc = XDocument.Load(new MemoryStream(data));
            foreach (var el in doc.Root.Elements())
                this[el.Attribute("Extension").Value] =
                    el.Attribute("ContentType").Value;
        }

        public byte[] Serialize()
        {
            var types = new XElement(ContentTypesName("Types"));
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", ""), types);

            foreach (var kp in this)
            {
                var el = new XElement(ContentTypesName("Default"));
                el.SetAttributeValue("Extension", kp.Key);
                el.SetAttributeValue("ContentType", kp.Value);

                types.Add(el);
            }
            var ms = new MemoryStream();
            doc.Save(ms);
            return ms.ToArray();
        }
    }
}
