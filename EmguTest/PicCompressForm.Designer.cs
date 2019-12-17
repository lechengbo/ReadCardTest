namespace EmguTest
{
    partial class PicCompressForm
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
            this.picSrc = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_compress = new System.Windows.Forms.Button();
            this.btn_compress1 = new System.Windows.Forms.Button();
            this.btn_compress2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picSrc
            // 
            this.picSrc.CurrentSelectedRect = null;
            this.picSrc.Location = new System.Drawing.Point(3, 3);
            this.picSrc.MinWidth = 0;
            this.picSrc.Name = "picSrc";
            this.picSrc.RegionInfo = null;
            this.picSrc.Size = new System.Drawing.Size(486, 702);
            this.picSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc.TabIndex = 0;
            this.picSrc.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picSrc);
            this.panel1.Location = new System.Drawing.Point(38, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 708);
            this.panel1.TabIndex = 1;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(570, 48);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 2;
            this.btn_load.Text = "加载";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.Btn_load_Click);
            // 
            // btn_compress
            // 
            this.btn_compress.Location = new System.Drawing.Point(570, 106);
            this.btn_compress.Name = "btn_compress";
            this.btn_compress.Size = new System.Drawing.Size(75, 23);
            this.btn_compress.TabIndex = 3;
            this.btn_compress.Text = "压缩";
            this.btn_compress.UseVisualStyleBackColor = true;
            this.btn_compress.Click += new System.EventHandler(this.Btn_compress_Click);
            // 
            // btn_compress1
            // 
            this.btn_compress1.Location = new System.Drawing.Point(570, 157);
            this.btn_compress1.Name = "btn_compress1";
            this.btn_compress1.Size = new System.Drawing.Size(75, 23);
            this.btn_compress1.TabIndex = 4;
            this.btn_compress1.Text = "压缩1";
            this.btn_compress1.UseVisualStyleBackColor = true;
            this.btn_compress1.Click += new System.EventHandler(this.Compress1);
            // 
            // btn_compress2
            // 
            this.btn_compress2.Location = new System.Drawing.Point(570, 210);
            this.btn_compress2.Name = "btn_compress2";
            this.btn_compress2.Size = new System.Drawing.Size(75, 23);
            this.btn_compress2.TabIndex = 5;
            this.btn_compress2.Text = "压缩2";
            this.btn_compress2.UseVisualStyleBackColor = true;
            this.btn_compress2.Click += new System.EventHandler(this.Compress2);
            // 
            // PicCompressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 764);
            this.Controls.Add(this.btn_compress2);
            this.Controls.Add(this.btn_compress1);
            this.Controls.Add(this.btn_compress);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.panel1);
            this.Name = "PicCompressForm";
            this.Text = "PicCompressForm";
            this.Load += new System.EventHandler(this.PicCompressForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private yue_juan_care.customerControl.PictureBoxReadCard picSrc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_compress;
        private System.Windows.Forms.Button btn_compress1;
        private System.Windows.Forms.Button btn_compress2;
    }
}