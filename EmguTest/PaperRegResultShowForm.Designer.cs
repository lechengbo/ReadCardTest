namespace EmguTest
{
    partial class PaperRegResultShowForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picSrc = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.btn_showScan = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picSrc);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 800);
            this.panel1.TabIndex = 0;
            // 
            // picSrc
            // 
            this.picSrc.CurrentSelectedRect = null;
            this.picSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSrc.Location = new System.Drawing.Point(0, 0);
            this.picSrc.MinWidth = 0;
            this.picSrc.Name = "picSrc";
            this.picSrc.RegionInfo = null;
            this.picSrc.Size = new System.Drawing.Size(666, 798);
            this.picSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc.TabIndex = 0;
            this.picSrc.TabStop = false;
            // 
            // btn_showScan
            // 
            this.btn_showScan.Location = new System.Drawing.Point(748, 14);
            this.btn_showScan.Name = "btn_showScan";
            this.btn_showScan.Size = new System.Drawing.Size(111, 31);
            this.btn_showScan.TabIndex = 1;
            this.btn_showScan.Text = "打开扫描结果匹配";
            this.btn_showScan.UseVisualStyleBackColor = true;
            this.btn_showScan.Click += new System.EventHandler(this.Btn_showScan_Click);
            // 
            // PaperRegResultShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 825);
            this.Controls.Add(this.btn_showScan);
            this.Controls.Add(this.panel1);
            this.Name = "PaperRegResultShowForm";
            this.Text = "答题卡识别结果展示";
            this.Load += new System.EventHandler(this.PaperRegResultShowForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc;
        private System.Windows.Forms.Button btn_showScan;
    }
}