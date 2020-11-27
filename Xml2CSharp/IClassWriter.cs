using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2CSharp
{
    public interface IClassWriter
    {
        string Write(IEnumerable<Class> classInfo, string rootName);

        string Comment { get; }
        List<string> ReservedKeywords { get; }
    }
}
