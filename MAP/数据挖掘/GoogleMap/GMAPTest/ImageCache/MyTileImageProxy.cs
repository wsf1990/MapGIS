using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.ImageCache
{
    public class MyTileImageProxy : PureImageProxy
    {
        public override PureImage FromStream(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public override bool Save(System.IO.Stream stream, PureImage image)
        {
            throw new NotImplementedException();
        }
    }
}
