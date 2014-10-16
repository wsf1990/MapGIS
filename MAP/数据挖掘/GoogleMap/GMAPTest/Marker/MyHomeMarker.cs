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
    /// <summary>
    /// 自定义的Marker用于显示住址信息等
    /// </summary>
    public class MyHomeMarker : GMapMarker
    {
        #region 1. 要显示的标记图片
        private Image image;

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                if (value != null)
                {
                    this.Size = new Size(value.Width, value.Height);
                    this.Offset = new Point(-Size.Width / 2, -Size.Height / 2);
                    image = value;
                }
            }
        }
        #endregion

        float angle = 20;

        public MyHomeMarker(PointLatLng latlon)
            : base(latlon)
        {
            //this.latlon = latlon;
            Image = new Bitmap("Snow.png");
        }

        public override void OnRender(System.Drawing.Graphics g)
        {
            g.DrawImageUnscaled(RotateImage(image, angle), new Point(){ X = LocalPosition.X, Y = LocalPosition.Y });
        }

        /// <summary>
        /// 旋转图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private Bitmap RotateImage(Image image, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            const double pi2 = Math.PI / 2.0;
            double oldWidth = (double)image.Width;
            double oldHeight = (double)image.Height;
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;

            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight;

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;
            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);

            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {

                Point[] points;


                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[] {   
                                             new Point( (int) oppositeBottom, 0 ),   
                                             new Point( nWidth, (int) oppositeTop ),  
                                             new Point( 0, (int) adjacentBottom )  
                                         };

                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[] {   
                                             new Point( nWidth, (int) oppositeTop ),  
                                             new Point( (int) adjacentTop, nHeight ),  
                                             new Point( (int) oppositeBottom, 0 )                           
                                         };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[] {   
                                             new Point( (int) adjacentTop, nHeight ),   
                                             new Point( 0, (int) adjacentBottom ),  
                                             new Point( nWidth, (int) oppositeTop )  
                                         };
                }
                else
                {
                    points = new Point[] {   
                                             new Point( 0, (int) adjacentBottom ),   
                                             new Point( (int) oppositeBottom, 0 ),  
                                             new Point( (int) adjacentTop, nHeight )          
                                         };
                }

                g.DrawImage(image, points);
            }

            return rotatedBmp;
        }

    }
}
