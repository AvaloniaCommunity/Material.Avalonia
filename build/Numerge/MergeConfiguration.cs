using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Numerge
{
    [DataContract]
    public class MergeConfiguration
    {
        [DataMember]
        public List<PackageContfiguration> Packages { get; set; }
        
        public static MergeConfiguration Load(Stream s)
        {
            var serializer = new DataContractJsonSerializer(typeof(MergeConfiguration), new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true 
            });
            return (MergeConfiguration) serializer.ReadObject(s);
        }

        public static MergeConfiguration LoadFile(string path)
        {
            using (var s = File.OpenRead(path))
                return Load(s);
        }
    }

    [DataContract]
    public class PackageContfiguration
    {
        [DataMember]
        public string Id { get; set; }
        
        [DataMember]
        public string IncomingIncludeAssetsOverride { get; set; }
        
        [DataMember]
        public bool MergeAll { get; set; }
        
        [DataMember]
        public List<string> Exclude { get; set; } = new List<string>();

        [DataMember]
        public List<PackageMergeConfiguration> Merge { get; set; } =
            new List<PackageMergeConfiguration>();
    }

    [DataContract]
    public class PackageMergeConfiguration
    {
        [DataMember]
        public string Id { get; set; }
        
        [DataMember]
        public bool IgnoreMissingFrameworkBinaries { get; set; }
        
        [DataMember]
        public bool IgnoreMissingFrameworkDependencies { get; set; }
               
        [DataMember]
        public bool DoNotMergeDependencies { get; set; }
    }
}
