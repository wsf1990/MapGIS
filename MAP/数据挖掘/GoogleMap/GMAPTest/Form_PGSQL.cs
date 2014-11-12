using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMAPTest.PgSQL;

namespace GMAPTest
{
    public partial class Form_PGSQL : Form
    {
        public Form_PGSQL()
        {
            InitializeComponent();
        }

        private void Form_PGSQL_Load(object sender, EventArgs e)
        {
            //PgSQLHelper.GetTableList();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            PgSQLHelper.Insert();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            PgSQLHelper.Update();
        }

        private void btn_GetAllTable_Click(object sender, EventArgs e)
        {
            var tables = PgSQLHelper.GetTableList();
            tables.ForEach(s=>MessageBox.Show(s));
        }


    }
}
