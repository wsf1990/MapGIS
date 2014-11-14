using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMAPTest.OSM;
using GMAPTest.PgSQL;
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

        private void btn_PlanetName_Query_Click(object sender, EventArgs e)
        {
            var list = new PlanetNameBLL().GetPageData(3, 2);
        }
    }
}
