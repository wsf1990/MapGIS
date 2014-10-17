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
            this.Size = new System.Drawing.Size(20, 20);//设置Size属性后可以实现对Marker的单击等控制。
            this.Offset = new Point(-10, -10);//设置Offest属性可以使LocalPosition直接进行偏移
            brush = new SolidBrush(Color.BlueViolet);
            IsHitTestVisible = true;
        }

        public override void OnRender(System.Drawing.Graphics g)
        {
            var pos = new Point(LocalPosition.X, LocalPosition.Y);
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
