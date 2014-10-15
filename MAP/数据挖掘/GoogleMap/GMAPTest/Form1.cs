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

namespace GMAPTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GMAPHelper.InitMapBox(gMapControl1, GMapProviders.GoogleTerrainMap);//GMapProviders.GoogleTerrainMap);
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标移动 显示当前位置经纬度
            var latlon = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            lb_Lon.Text = latlon.Lng.ToString();
            lb_Lat.Text = latlon.Lat.ToString();
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
    }
}
