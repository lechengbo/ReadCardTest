namespace EmguTest
{
    partial class SobelForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_src = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.bt_load = new System.Windows.Forms.Button();
            this.btn_soble = new System.Windows.Forms.Button();
            this.ibX = new Emgu.CV.UI.ImageBox();
            this.ibY = new Emgu.CV.UI.ImageBox();
            this.btn_Scharr = new System.Windows.Forms.Button();
            this.bt_filter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_src)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibY)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pic_src);
            this.panel1.Location = new System.Drawing.Point(26, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 525);
            this.panel1.TabIndex = 0;
            // 
            // pic_src
            // 
            this.pic_src.CurrentSelectedRect = null;
            this.pic_src.Location = new System.Drawing.Point(3, 3);
            this.pic_src.MinWidth = 0;
            this.pic_src.Name = "pic_src";
            this.pic_src.RegionInfo = null;
            this.pic_src.Size = new System.Drawing.Size(524, 517);
            this.pic_src.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_src.TabIndex = 0;
            this.pic_src.TabStop = false;
            // 
            // bt_load
            // 
            this.bt_load.Location = new System.Drawing.Point(593, 17);
            this.bt_load.Name = "bt_load";
            this.bt_load.Size = new System.Drawing.Size(75, 23);
            this.bt_load.TabIndex = 1;
            this.bt_load.Text = "加载";
            this.bt_load.UseVisualStyleBackColor = true;
            this.bt_load.Click += new System.EventHandler(this.Bt_load_Click);
            // 
            // btn_soble
            // 
            this.btn_soble.Location = new System.Drawing.Point(593, 84);
            this.btn_soble.Name = "btn_soble";
            this.btn_soble.Size = new System.Drawing.Size(75, 23);
            this.btn_soble.TabIndex = 2;
            this.btn_soble.Text = "Sobel";
            this.btn_soble.UseVisualStyleBackColor = true;
            this.btn_soble.Click += new System.EventHandler(this.Btn_soble_Click);
            // 
            // ibX
            // 
            this.ibX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibX.Location = new System.Drawing.Point(881, 16);
            this.ibX.Name = "ibX";
            this.ibX.Size = new System.Drawing.Size(151, 123);
            this.ibX.TabIndex = 2;
            this.ibX.TabStop = false;
            // 
            // ibY
            // 
            this.ibY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibY.Location = new System.Drawing.Point(881, 180);
            this.ibY.Name = "ibY";
            this.ibY.Size = new System.Drawing.Size(151, 128);
            this.ibY.TabIndex = 2;
            this.ibY.TabStop = false;
            // 
            // btn_Scharr
            // 
            this.btn_Scharr.Location = new System.Drawing.Point(593, 139);
            this.btn_Scharr.Name = "btn_Scharr";
            this.btn_Scharr.Size = new System.Drawing.Size(75, 23);
            this.btn_Scharr.TabIndex = 3;
            this.btn_Scharr.Text = "Scharr";
            this.btn_Scharr.UseVisualStyleBackColor = true;
            this.btn_Scharr.Click += new System.EventHandler(this.Btn_Scharr_Click);
            // 
            // bt_filter
            // 
            this.bt_filter.Location = new System.Drawing.Point(593, 193);
            this.bt_filter.Name = "bt_filter";
            this.bt_filter.Size = new System.Drawing.Size(75, 23);
            this.bt_filter.TabIndex = 4;
            this.bt_filter.Text = "Filter";
            this.bt_filter.UseVisualStyleBackColor = true;
            this.bt_filter.Click += new System.EventHandler(this.Bt_filter_Click);
            // 
            // SobelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 718);
            this.Controls.Add(this.bt_filter);
            this.Controls.Add(this.btn_Scharr);
            this.Controls.Add(this.ibY);
            this.Controls.Add(this.ibX);
            this.Controls.Add(this.btn_soble);
            this.Controls.Add(this.bt_load);
            this.Controls.Add(this.panel1);
            this.Name = "SobelForm";
            this.Text = "边缘检测测试";
            this.Load += new System.EventHandler(this.SobelForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_src)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private yue_juan_care.customerControl.PictureBoxReadCard pic_src;
        private System.Windows.Forms.Button bt_load;
        private System.Windows.Forms.Button btn_soble;
        private Emgu.CV.UI.ImageBox ibX;
        private Emgu.CV.UI.ImageBox ibY;
        private System.Windows.Forms.Button btn_Scharr;
        private System.Windows.Forms.Button bt_filter;
    }
}