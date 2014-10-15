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
            GMapControl mapControl = new GMapControl();
            mapControl.BackColor = Color.CadetBlue;
            mapControl.Dock = DockStyle.Fill;
            this.Controls.Add(mapControl);

            mapControl.Position = new PointLatLng(30, 120);

            mapControl.MapProvider.Area = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            mapControl.BoundsOfMap = new RectLatLng(30.981178, 105.351914, 2.765142, 4.120995);
            mapControl.Manager.Mode = AccessMode.CacheOnly;
            mapControl.MapProvider = new BaiduMapProvider();
            mapControl.DragButton = MouseButtons.Left;
            mapControl.Zoom = 13;
            mapControl.MinZoom = 8;
            mapControl.MaxZoom = 24;
        }
    }
}
