namespace EmguTest
{
    partial class ZoomForm2
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
            this.panel_PicBox = new System.Windows.Forms.Panel();
            this.picBox = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.btn_loadImage = new System.Windows.Forms.Button();
            this.btn_moveRect = new System.Windows.Forms.Button();
            this.btn_paintFormRect = new System.Windows.Forms.Button();
            this.btn_percentTest = new System.Windows.Forms.Button();
            this.panel_PicBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_PicBox
            // 
            this.panel_PicBox.AutoScroll = true;
            this.panel_PicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_PicBox.Controls.Add(this.picBox);
            this.panel_PicBox.Location = new System.Drawing.Point(369, 108);
            this.panel_PicBox.Name = "panel_PicBox";
            this.panel_PicBox.Size = new System.Drawing.Size(605, 499);
            this.panel_PicBox.TabIndex = 1;
            // 
            // picBox
            // 
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.CurrentSelectedRect = null;
            this.picBox.Location = new System.Drawing.Point(40, 60);
            this.picBox.MinWidth = 0;
            this.picBox.Name = "picBox";
            this.picBox.RegionInfo = null;
            this.picBox.Size = new System.Drawing.Size(462, 323);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // btn_loadImage
            // 
            this.btn_loadImage.Location = new System.Drawing.Point(68, 108);
            this.btn_loadImage.Name = "btn_loadImage";
            this.btn_loadImage.Size = new System.Drawing.Size(75, 23);
            this.btn_loadImage.TabIndex = 2;
            this.btn_loadImage.Text = "加载图片";
            this.btn_loadImage.UseVisualStyleBackColor = true;
            this.btn_loadImage.Click += new System.EventHandler(this.Btn_loadImage_Click);
            // 
            // btn_moveRect
            // 
            this.btn_moveRect.Location = new System.Drawing.Point(68, 169);
            this.btn_moveRect.Name = "btn_moveRect";
            this.btn_moveRect.Size = new System.Drawing.Size(75, 44);
            this.btn_moveRect.TabIndex = 3;
            this.btn_moveRect.Text = "选中需要调整的区域";
            this.btn_moveRect.UseVisualStyleBackColor = true;
            this.btn_moveRect.Click += new System.EventHandler(this.Btn_moveRect_Click);
            // 
            // btn_paintFormRect
            // 
            this.btn_paintFormRect.Location = new System.Drawing.Point(68, 263);
            this.btn_paintFormRect.Name = "btn_paintFormRect";
            this.btn_paintFormRect.Size = new System.Drawing.Size(75, 44);
            this.btn_paintFormRect.TabIndex = 4;
            this.btn_paintFormRect.Text = "画出Form中的矩形框";
            this.btn_paintFormRect.UseVisualStyleBackColor = true;
            this.btn_paintFormRect.Click += new System.EventHandler(this.Btn_paintFormRect_Click);
            // 
            // btn_percentTest
            // 
            this.btn_percentTest.Location = new System.Drawing.Point(68, 372);
            this.btn_percentTest.Name = "btn_percentTest";
            this.btn_percentTest.Size = new System.Drawing.Size(75, 50);
            this.btn_percentTest.TabIndex = 5;
            this.btn_percentTest.Text = "矩形框重复率测试";
            this.btn_percentTest.UseVisualStyleBackColor = true;
            this.btn_percentTest.Click += new System.EventHandler(this.Btn_percentTest_Click);
            // 
            // ZoomForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 735);
            this.Controls.Add(this.btn_percentTest);
            this.Controls.Add(this.btn_paintFormRect);
            this.Controls.Add(this.btn_moveRect);
            this.Controls.Add(this.btn_loadImage);
            this.Controls.Add(this.panel_PicBox);
            this.KeyPreview = true;
            this.Name = "ZoomForm2";
            this.Text = "ZoomForm2";
            this.Load += new System.EventHandler(this.ZoomForm2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZoomForm2_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ZoomForm2_KeyUp);
            this.panel_PicBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private yue_juan_care.customerControl.PictureBoxReadCard picBox;
        private System.Windows.Forms.Panel panel_PicBox;
        private System.Windows.Forms.Button btn_loadImage;
        private System.Windows.Forms.Button btn_moveRect;
        private System.Windows.Forms.Button btn_paintFormRect;
        private System.Windows.Forms.Button btn_percentTest;
    }
}