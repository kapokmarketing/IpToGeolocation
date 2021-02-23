using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapok.IpToGeolocation
{
    public enum Provider : byte
    {
        Unknown = 0,
        None = 1,
        IpStack = 2,
        IpGeolocationApi = 3,
        AbstractApi = 4,
        IpRegistry = 5,
        BrowserSet = 255
    }
}