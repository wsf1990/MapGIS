﻿using System;
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
        GMapHelper helper;

        PointLatLng startPos = new PointLatLng(0, 0);
        PointLatLng endPos = new PointLatLng(0, 0);

        bool isDrawLine = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            helper = new GMapHelper(gMapControl1);
            helper.InitMapBox(BaiduMapProvider.Instance);//GMapProviders.GoogleTerrainMap);
            
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isDrawLine && startPos == new PointLatLng(0, 0))
                startPos = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            else if (startPos != new PointLatLng(0, 0))
            {
                endPos = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                helper.DrawLine(startPos, endPos);
                startPos = new PointLatLng(0, 0);
                endPos = new PointLatLng(0, 0);
            }
            if (e.Clicks == 2)
                return;
            if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Left)
                helper.AddMarker(gMapControl1.FromLocalToLatLng(e.X, e.Y));
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

        private void gMapControl1_Click(object sender, EventArgs e)
        {
            int a = 1;
        }

        private void btn_DrawLine_Click(object sender, EventArgs e)
        {
            isDrawLine = !isDrawLine;
        }

        private void txt_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var list = helper.SearchAddress(txt_City.Text, txt_Address.Text);
                
            }
        }
    }
}
