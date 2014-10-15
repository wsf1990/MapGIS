using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BruTile;
using BruTile.Cache;
using BruTile.Web;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap.Layers;

namespace GoogleMap
{
    public partial class Frm_MapTest : Form
    {
        SharpMap.Layers.ILayer MapLayer = null;
        public Frm_MapTest()
        {
            InitializeComponent();
        }

        private void DisMapBox()
        {
            mapBox1.Map.Layers.Clear();
            mapBox1.Map.Layers.Add(MapLayer);
            mapBox1.Map.ZoomToExtents();
            mapBox1.Refresh();
        }

        private void ShowTools()
        {
            mapBox1.ActiveTool = MapBox.Tools.ZoomWindow;
        }

        private SharpMap.Map map;

        private void Frm_MapTest_Load(object sender, EventArgs e)
        {
            ShowTools();
            //BruTile.Cache.ITileCache<byte[]> tiles = new MemoryCache<byte[]>();
            //ITileProvider provider = new WebTileProvider(,);
            //ITileSource tileSource = new TileSource();
            //SharpMap.Layers.TileLayer layer = new TileLayer(tileSource, "tile");

            MapLayer = MapHelper.AddShpLayer("shp/bou2_4p.shp");

            //var layer = OSMHelper.AddOSMLayer();
            //mapBox1.Map.Layers.Add(VLayer);
            DisMapBox();
            map = mapBox1.Map;
        }

        /// <summary>
        /// 单击，使其单击的点置于图像中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(mapBox1.ActiveTool != MapBox.Tools.ZoomWindow)
            mapBox1.Map.Center = MapHelper.GetCoordinate(map, new PointF(e.X, e.Y));
            mapBox1.Refresh();
        }

        private void list_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(list_Source.SelectedItem.ToString() == "OSM")
            {
                MapLayer = OSMHelper.AddOSMLayer();
                DisMapBox();
            }
            else if (list_Source.SelectedItem.ToString() == "GoogleEarth")
            { 
                
            }
        }
    }
}
