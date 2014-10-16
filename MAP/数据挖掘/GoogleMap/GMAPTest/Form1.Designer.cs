namespace GMAPTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lb_Londis = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_Lon = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_Lat = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_DrawLine = new System.Windows.Forms.ToolStripButton();
            this.btn_SaveImage = new System.Windows.Forms.ToolStripButton();
            this.txt_City = new System.Windows.Forms.ToolStripTextBox();
            this.txt_Address = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ShowTileGridLines = true;
            this.gMapControl1.Size = new System.Drawing.Size(573, 367);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.Click += new System.EventHandler(this.gMapControl1_Click);
            this.gMapControl1.DoubleClick += new System.EventHandler(this.gMapControl1_DoubleClick);
            this.gMapControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseClick);
            this.gMapControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_Londis,
            this.lb_Lon,
            this.toolStripStatusLabel1,
            this.lb_Lat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 345);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(573, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lb_Londis
            // 
            this.lb_Londis.Name = "lb_Londis";
            this.lb_Londis.Size = new System.Drawing.Size(32, 17);
            this.lb_Londis.Text = "经度";
            // 
            // lb_Lon
            // 
            this.lb_Lon.Name = "lb_Lon";
            this.lb_Lon.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "维度";
            // 
            // lb_Lat
            // 
            this.lb_Lat.Name = "lb_Lat";
            this.lb_Lat.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_DrawLine,
            this.btn_SaveImage,
            this.txt_City,
            this.txt_Address});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(573, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_DrawLine
            // 
            this.btn_DrawLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_DrawLine.Image = ((System.Drawing.Image)(resources.GetObject("btn_DrawLine.Image")));
            this.btn_DrawLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_DrawLine.Name = "btn_DrawLine";
            this.btn_DrawLine.Size = new System.Drawing.Size(23, 22);
            this.btn_DrawLine.Text = "画线";
            this.btn_DrawLine.Click += new System.EventHandler(this.btn_DrawLine_Click);
            // 
            // btn_SaveImage
            // 
            this.btn_SaveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_SaveImage.Image = ((System.Drawing.Image)(resources.GetObject("btn_SaveImage.Image")));
            this.btn_SaveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_SaveImage.Name = "btn_SaveImage";
            this.btn_SaveImage.Size = new System.Drawing.Size(23, 22);
            this.btn_SaveImage.Text = "截图";
            this.btn_SaveImage.Click += new System.EventHandler(this.btn_SaveImage_Click);
            // 
            // txt_City
            // 
            this.txt_City.Name = "txt_City";
            this.txt_City.Size = new System.Drawing.Size(100, 25);
            // 
            // txt_Address
            // 
            this.txt_Address.Name = "txt_Address";
            this.txt_Address.Size = new System.Drawing.Size(100, 25);
            this.txt_Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Address_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 367);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gMapControl1);
            this.Name = "Form1";
            this.Text = "GIS - 魏守峰 - 2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lb_Londis;
        private System.Windows.Forms.ToolStripStatusLabel lb_Lon;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lb_Lat;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_DrawLine;
        private System.Windows.Forms.ToolStripButton btn_SaveImage;
        private System.Windows.Forms.ToolStripTextBox txt_City;
        private System.Windows.Forms.ToolStripTextBox txt_Address;
    }
}

