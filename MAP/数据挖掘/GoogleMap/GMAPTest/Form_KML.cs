using GMAPTest.Common;
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
        string path = @"C:\Users\JSJZX\Desktop\kmz\123.kmz";
        private void button1_Click(object sender, EventArgs e)
        {
            //ZipHelper.Zip(@"C:\Users\JSJZX\Desktop\kmz\123", @"C:\Users\JSJZX\Desktop\kmz\123.zip");
            //ZipHelper.UnZip(@"C:\Users\JSJZX\Desktop\kmz\美国空军机场.kmz", @"C:\Users\JSJZX\Desktop\kmz");
            var kml = KMLHelper.ReadKMZ(path);
        }

        private void btn_WriteKML_Click(object sender, EventArgs e)
        {
            var kml = KMLHelper.ReadKMZ(@"C:\Users\JSJZX\Desktop\kmz\全球十大航天发射基地.kmz");
            KMLHelper.WriteKMZ(@"C:\Users\JSJZX\Desktop\kmz\123.kmz", kml);
        }
    }
}
