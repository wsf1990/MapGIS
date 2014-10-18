using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public interface IMapHelper
    {
        PointLatLng GetLocation(string address, out GeoCoderStatusCode code);

        Placemark GetAddress(PointLatLng point, out GeoCoderStatusCode code);
    }
}
