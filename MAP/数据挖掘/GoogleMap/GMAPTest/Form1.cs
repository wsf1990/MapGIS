using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMAPTest.MapProvider.Baidu;

namespace GMAPTest
{
    public partial class Form1 : Form
    {
        GMapHelper helper;

        PointLatLng startPos = new PointLatLng(0, 0);
        PointLatLng endPos = new PointLatLng(0, 0);

        bool isDrawLine = false;

        BackgroundWorker woeker = new BackgroundWorker();//后台多线程工作
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            helper = new GMapHelper(gMapControl1);
            helper.InitMapBox(GDMapProvider.Instance);//GMapProviders.GoogleTerrainMap);
            var point = helper.GetAddressPoint("天安门");
            if (point.HasValue)
            {
                helper.DrawAddress(point.Value);
                helper.GetPlaceName(point.Value);
            }
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng pos = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            //MessageBox.Show(helper.GetPlaceName(pos));
            if (isDrawLine && startPos == new PointLatLng(0, 0))
                startPos = pos;
            else if (startPos != new PointLatLng(0, 0))
            {
                endPos = pos;
                //helper.DrawLine(startPos, endPos);
                //helper.FindRoute(startPos, endPos);

                helper.DrawBox(startPos, endPos);
                var dis = Utility.GetDistance(startPos, endPos);
                MessageBox.Show(dis.ToString());
                startPos = new PointLatLng(0, 0);
                endPos = new PointLatLng(0, 0);
            }
            if (e.Clicks == 2)
                return;
            if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Left)
                //helper.AddMarker(pos);
                helper.DrawCircle(pos);
        }

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标移动 显示当前位置经纬度
            var latlon = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            lb_Lon.Text = latlon.Lng.ToString();
            lb_Lat.Text = latlon.Lat.ToString();
            lb_Zoom.Text = gMapControl1.Zoom.ToString();
        }

        private void gMapControl1_DoubleClick(object sender, EventArgs e)
        {
            gMapControl1.Zoom++;
            //MessageBox.Show(gMapControl1.Zoom.ToString());
        }

        private void btn_SaveImage_Click(object sender, EventArgs e)
        {
            using (var image = gMapControl1.ToImage())
                image.Save("jt.png");
        }

        private void gMapControl1_Click(object sender, EventArgs e)
        {
            int a = 1;
        }

        private void btn_DrawLine_Click(object sender, EventArgs e)
        {
            isDrawLine = !isDrawLine;
            if (isDrawLine)
                lb_Status.Text = "路径查找";
            else
            {
                lb_Status.Text = "Normal";
            }
        }

        private void txt_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var list = helper.SearchAddress(txt_Address.Text);
            }
        }

        private void txt_End_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var route = helper.FindRoute(txt_Start.Text, txt_End.Text);
                helper.DrawRoute(route);
            }
        }
    }
}
