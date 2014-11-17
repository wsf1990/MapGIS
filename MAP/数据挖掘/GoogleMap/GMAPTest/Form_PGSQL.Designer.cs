namespace GMAPTest
{
    partial class Form_PGSQL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Query = new System.Windows.Forms.Button();
            this.btn_GetAllTable = new System.Windows.Forms.Button();
            this.btn_QueryOne = new System.Windows.Forms.Button();
            this.txt_sql = new System.Windows.Forms.TextBox();
            this.btn_DoSQL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_PlanetName_Query = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Translate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(22, 22);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "插入数据";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(115, 22);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Update.TabIndex = 1;
            this.btn_Update.Text = "更新数据";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(209, 22);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除数据";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.Location = new System.Drawing.Point(311, 22);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(75, 23);
            this.btn_Query.TabIndex = 3;
            this.btn_Query.Text = "查询数据";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // btn_GetAllTable
            // 
            this.btn_GetAllTable.Location = new System.Drawing.Point(22, 76);
            this.btn_GetAllTable.Name = "btn_GetAllTable";
            this.btn_GetAllTable.Size = new System.Drawing.Size(75, 23);
            this.btn_GetAllTable.TabIndex = 4;
            this.btn_GetAllTable.Text = "获取所有表";
            this.btn_GetAllTable.UseVisualStyleBackColor = true;
            this.btn_GetAllTable.Click += new System.EventHandler(this.btn_GetAllTable_Click);
            // 
            // btn_QueryOne
            // 
            this.btn_QueryOne.Location = new System.Drawing.Point(115, 76);
            this.btn_QueryOne.Name = "btn_QueryOne";
            this.btn_QueryOne.Size = new System.Drawing.Size(94, 23);
            this.btn_QueryOne.TabIndex = 5;
            this.btn_QueryOne.Text = "查询某条数据";
            this.btn_QueryOne.UseVisualStyleBackColor = true;
            this.btn_QueryOne.Click += new System.EventHandler(this.btn_QueryOne_Click);
            // 
            // txt_sql
            // 
            this.txt_sql.Location = new System.Drawing.Point(22, 128);
            this.txt_sql.Multiline = true;
            this.txt_sql.Name = "txt_sql";
            this.txt_sql.Size = new System.Drawing.Size(422, 105);
            this.txt_sql.TabIndex = 6;
            // 
            // btn_DoSQL
            // 
            this.btn_DoSQL.Location = new System.Drawing.Point(373, 239);
            this.btn_DoSQL.Name = "btn_DoSQL";
            this.btn_DoSQL.Size = new System.Drawing.Size(71, 23);
            this.btn_DoSQL.TabIndex = 7;
            this.btn_DoSQL.Text = "执行";
            this.btn_DoSQL.UseVisualStyleBackColor = true;
            this.btn_DoSQL.Click += new System.EventHandler(this.btn_DoSQL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "SQL语句:";
            // 
            // btn_PlanetName_Query
            // 
            this.btn_PlanetName_Query.Location = new System.Drawing.Point(22, 287);
            this.btn_PlanetName_Query.Name = "btn_PlanetName_Query";
            this.btn_PlanetName_Query.Size = new System.Drawing.Size(71, 23);
            this.btn_PlanetName_Query.TabIndex = 9;
            this.btn_PlanetName_Query.Text = "查询";
            this.btn_PlanetName_Query.UseVisualStyleBackColor = true;
            this.btn_PlanetName_Query.Click += new System.EventHandler(this.btn_PlanetName_Query_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "地名表:";
            // 
            // btn_Translate
            // 
            this.btn_Translate.Location = new System.Drawing.Point(115, 287);
            this.btn_Translate.Name = "btn_Translate";
            this.btn_Translate.Size = new System.Drawing.Size(71, 23);
            this.btn_Translate.TabIndex = 11;
            this.btn_Translate.Text = "逐条翻译";
            this.btn_Translate.UseVisualStyleBackColor = true;
            this.btn_Translate.Click += new System.EventHandler(this.btn_Translate_Click);
            // 
            // Form_PGSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 352);
            this.Controls.Add(this.btn_Translate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_PlanetName_Query);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_DoSQL);
            this.Controls.Add(this.txt_sql);
            this.Controls.Add(this.btn_QueryOne);
            this.Controls.Add(this.btn_GetAllTable);
            this.Controls.Add(this.btn_Query);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Add);
            this.Name = "Form_PGSQL";
            this.Text = "Form_PGSQL";
            this.Load += new System.EventHandler(this.Form_PGSQL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.Button btn_GetAllTable;
        private System.Windows.Forms.Button btn_QueryOne;
        private System.Windows.Forms.TextBox txt_sql;
        private System.Windows.Forms.Button btn_DoSQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_PlanetName_Query;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Translate;
    }
}