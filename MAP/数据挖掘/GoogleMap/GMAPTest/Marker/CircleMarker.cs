using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public class CircleMarker : GMapMarker
    {
        SolidBrush brush;
        public CircleMarker(PointLatLng pos) : base(pos)
        {
            brush = new SolidBrush(Color.BlueViolet);
            IsHitTestVisible = true;
        }

        public override void OnRender(System.Drawing.Graphics g)
        {
            var pos = new Point(LocalPosition.X - 10, LocalPosition.Y - 10);
            g.FillEllipse(brush, new RectangleF() { Location = pos, Size = new SizeF(20, 20) });
        }

        public override void Dispose()
        {
            if (brush != null)
                brush.Dispose();
            base.Dispose();
        }
    }
}
