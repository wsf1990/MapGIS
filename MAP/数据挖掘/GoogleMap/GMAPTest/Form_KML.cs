using GMAPTest.KML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAPTest
{
    public partial class Form_KML : Form
    {
        public Form_KML()
        {
            InitializeComponent();
        }
        string path = "kml/test.kml";
        private void button1_Click(object sender, EventArgs e)
        {
            //string 
            var kml = KMLHelper.ReadKMLFile(path);
        }

        private void btn_WriteKML_Click(object sender, EventArgs e)
        {
            var kml = KMLHelper.ReadKMLFile(path);
            KMLHelper.WriteKML("kml/write.kml", kml);
        }
    }
}
