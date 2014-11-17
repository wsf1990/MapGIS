using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMAPTest.Common;
using GMAPTest.OSM;
using GMAPTest.PgSQL;
using GMap.NET;
using Npgsql;
using GMAPTest.OSM_Name241.BLL;

namespace GMAPTest
{
    public partial class Form_PGSQL : Form
    {
        public Form_PGSQL()
        {
            InitializeComponent();
        }

        OSMBLL bll = new OSMBLL();

        private void Form_PGSQL_Load(object sender, EventArgs e)
        {
            //PgSQLHelper.GetTableList();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            var osm = new OSM.OSM(){ Name = "wsf"};
            bll.Save(osm);
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            var osm = new OSM.OSM() { ID = 15, Name = "wsf" };
            bll.Update(osm);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            bll.Delete(12);
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            bll.Query();
            //var query = PgSQLHelper.Query();
            //query.ForEach(s => MessageBox.Show(s));
        }

        private void btn_GetAllTable_Click(object sender, EventArgs e)
        {
            var tables = new PGHelper().GetTableList();
            tables.ForEach(s => MessageBox.Show(s));
        }

        private void btn_QueryOne_Click(object sender, EventArgs e)
        {
            var osm = bll.GetByID(23);
            MessageBox.Show("查询到的数据为：" + osm.ID + osm.Name);
        }

        private void btn_DoSQL_Click(object sender, EventArgs e)
        {
            string sql = txt_sql.Text;
            if(string.IsNullOrWhiteSpace(sql))
                return;
            new PGHelper().ExecuteNonQueryText(sql);
        }

        private PlanetNameBLL bll2 = new PlanetNameBLL();
        private void btn_PlanetName_Query_Click(object sender, EventArgs e)
        {
            var list = bll2.GetPageData(3, 2);
        }

        private void btn_Translate_Click(object sender, EventArgs e)
        {
            var count = bll2.GetCount();
            for (int i = 1; i <= count; i++)
            {
                var name = new PlanetNameBLL().GetOneWithNoCH(i);
                if (name != null)
                {
                    var addresses = GoogleHelper.GetAddress(new PointLatLng(name.Latitude, name.Longitude));
                    if (addresses != null && addresses.Count > 0)
                    {
                        var address = addresses.FirstOrDefault();
                        name.NameCH = address.Address_components.FirstOrDefault().Long_name; //翻译
                        bll2.UpdateNameCH(name);
                    }
                }
            }
            
        }

    }
}
