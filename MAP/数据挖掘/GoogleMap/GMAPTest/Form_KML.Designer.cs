namespace GMAPTest
{
    partial class Form_KML
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
            this.button1 = new System.Windows.Forms.Button();
            this.btn_WriteKML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "读KML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_WriteKML
            // 
            this.btn_WriteKML.Location = new System.Drawing.Point(126, 93);
            this.btn_WriteKML.Name = "btn_WriteKML";
            this.btn_WriteKML.Size = new System.Drawing.Size(75, 23);
            this.btn_WriteKML.TabIndex = 1;
            this.btn_WriteKML.Text = "写KML";
            this.btn_WriteKML.UseVisualStyleBackColor = true;
            this.btn_WriteKML.Click += new System.EventHandler(this.btn_WriteKML_Click);
            // 
            // Form_KML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 343);
            this.Controls.Add(this.btn_WriteKML);
            this.Controls.Add(this.button1);
            this.Name = "Form_KML";
            this.Text = "Form_KML";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_WriteKML;
    }
}