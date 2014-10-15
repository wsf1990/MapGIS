namespace GoogleMap
{
    partial class Frm_MapTest
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
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.list_Source = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(118, 0);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.White;
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = false;
            this.mapBox1.Size = new System.Drawing.Size(504, 382);
            this.mapBox1.TabIndex = 0;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapBox1_MouseClick);
            // 
            // list_Source
            // 
            this.list_Source.Dock = System.Windows.Forms.DockStyle.Left;
            this.list_Source.FormattingEnabled = true;
            this.list_Source.ItemHeight = 12;
            this.list_Source.Items.AddRange(new object[] {
            "OSM",
            "GoogleEarth",
            "BaiDu"});
            this.list_Source.Location = new System.Drawing.Point(0, 0);
            this.list_Source.Name = "list_Source";
            this.list_Source.Size = new System.Drawing.Size(120, 382);
            this.list_Source.TabIndex = 1;
            this.list_Source.SelectedIndexChanged += new System.EventHandler(this.list_Source_SelectedIndexChanged);
            // 
            // Frm_MapTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 382);
            this.Controls.Add(this.list_Source);
            this.Controls.Add(this.mapBox1);
            this.Name = "Frm_MapTest";
            this.Text = "GIS - 魏守峰 - 1.0";
            this.Load += new System.EventHandler(this.Frm_MapTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SharpMap.Forms.MapBox mapBox1;
        private System.Windows.Forms.ListBox list_Source;
    }
}