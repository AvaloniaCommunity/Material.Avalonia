using System.Collections.Generic;
using System.Xml.Linq;

namespace Numerge
{
    static class Constants
    {
        public const string ContentTypesFileName = "[Content_Types].xml";

        public const string ContentTypesXmlns = "http://schemas.openxmlformats.org/package/2006/content-types";
        public static XName ContentTypesName(string name) => XName.Get(name, ContentTypesXmlns);
    }
}