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
            gMapControl1.BackColor = Color.CadetBlue;
            gMapControl1.Position = new PointLatLng(30.981178, 105.351914);

            //gMapControl1.MapProvider.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.BoundsOfMap = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            //gMapControl1.Manager.Mode = AccessMode.CacheOnly;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Zoom = 13;

            //gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            gMapControl1.MapProvider = GMapProviders.GoogleChinaMap;
            gMapControl1.DragButton = MouseButtons.Left;
            
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;
        }
    }
}
