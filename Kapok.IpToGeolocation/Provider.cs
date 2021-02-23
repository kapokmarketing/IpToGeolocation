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
        IpGeolocationIo = 4,
        AbstractApi = 5,
        IpRegistry = 6,
        BrowserSet = 255
    }
}