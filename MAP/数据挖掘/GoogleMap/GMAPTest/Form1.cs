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
using GMAPTest.SHP;

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
            ShpFileContent Con = new ShpFileContent("shp/bou2_4p.shp");
            var lines = Con.PolyLines;
            helper = new GMapHelper(gMapControl1, map_Eagle);
            helper.InitMapBox(TencentTransptationProvider.Instance);//GMapProviders.GoogleTerrainMap);
            var point = helper.GetAddressPoint("天安门,北京");
            if (point.HasValue)
            {
                helper.DrawAddress(point.Value);
                helper.GetPlaceName(point.Value);
            }
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng pos = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            var add = helper.GetPlaceName(pos);
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
                helper.DrawCircle(pos, add);
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
        bool IsLeftDown = false;
        private void gMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            IsLeftDown = false;
        }

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            IsLeftDown = true;
        }

        private void gMapControl1_OnPositionChanged(PointLatLng point)
        {
            try
            {
                if (IsLeftDown && !point.IsEmpty)
                    map_Eagle.Position = point;
            }
            catch(StackOverflowException ex)
            {
                
            }
        }

        private void gMapControl1_OnMapZoomChanged()
        {
            var zoom = gMapControl1.Zoom - 5;
            if (zoom < 4)
                zoom = 4;
            map_Eagle.Zoom = zoom;
            map_Eagle.Position = gMapControl1.Position;
        }

        private void map_Eagle_OnMapZoomChanged()
        {
            //MessageBox.Show(map_Eagle.Zoom.ToString());
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            map_Eagle.Top = this.Height - map_Eagle.Height - 20;
            map_Eagle.Left = this.Width - map_Eagle.Width;
        }

        private void map_Eagle_OnPositionChanged(PointLatLng point)
        {
            if (!IsLeftDown)//防止出现循环变化
                gMapControl1.Position = map_Eagle.Position;
        }
    }
}
