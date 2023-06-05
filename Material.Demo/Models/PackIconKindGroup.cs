using System;
using System.Collections.Generic;
using System.Linq;
using Material.Demo.ViewModels;
using Material.Icons;

namespace Material.Demo.Models
{
    public class MaterialIconKindGroup
    {
        private readonly IconsDemoViewModel _parent;
        
        public MaterialIconKindGroup(IconsDemoViewModel parent, IEnumerable<string> kinds)
        {
            _parent = parent;
            
            if (kinds is null) throw new ArgumentNullException(nameof(kinds));
            var allValues = kinds.ToList();
            if (!allValues.Any()) throw new ArgumentException($"{nameof(kinds)} must contain at least one value");
            Kind = allValues.First();
            KindValue = Enum.Parse<MaterialIconKind>(Kind);
            Aliases = allValues
                .OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase)
                .ToArray();
        }

        public IconsDemoViewModel Parent => _parent;
        
        public string Kind { get; }
        public string KindToCopy => $"<avalonia:MaterialIcon Kind=\"{Kind}\" />";
        public MaterialIconKind KindValue { get; }
        public string[] Aliases { get; }
    }
}