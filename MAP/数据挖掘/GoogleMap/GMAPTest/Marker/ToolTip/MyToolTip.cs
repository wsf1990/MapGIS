using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.Marker.ToolTip
{
    /// <summary>
    /// 自定义ToolTip显示方式
    /// </summary>
    public class MyToolTip : GMapToolTip
    {
        public MyToolTip(GMapMarker marker) : base(marker)
        {
            Stroke = new System.Drawing.Pen(Color.Red);
            Stroke.Width = 1;
            Stroke.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
            Fill = Brushes.Red;
            Font = new Font(FontFamily.GenericSerif, 8, FontStyle.Regular);
            Foreground = Brushes.Red;
        }

        public override void OnRender(Graphics g)
        {
            System.Drawing.Size st = g.MeasureString(Marker.ToolTipText, Font).ToSize();
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Marker.ToolTipPosition.X, Marker.ToolTipPosition.Y - st.Height, st.Width + TextPadding.Width, st.Height + TextPadding.Height);
            Offset.X = -st.Width / 2;
            Offset.Y = 30;
            rect.Offset(Offset.X, Offset.Y);

            g.DrawString(Marker.ToolTipText, Font, Foreground, rect, Format);
            g.DrawLine(Stroke, rect.X, rect.Y +rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
        }
    }
}
