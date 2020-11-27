using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2CSharp
{
    public interface ISettings
    {
        Dictionary<string,object> Settings { get; set; }
    }
}
