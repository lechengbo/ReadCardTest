namespace EmguTest
{
    partial class OCRForm
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
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_ocr = new System.Windows.Forms.Button();
            this.tb_ocrResult = new System.Windows.Forms.TextBox();
            this.btn_jtyReg = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picSrc);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 743);
            this.panel1.TabIndex = 0;
            // 
            // picSrc
            // 
            this.picSrc.CurrentSelectedRect = null;
            this.picSrc.Location = new System.Drawing.Point(4, 4);
            this.picSrc.MinWidth = 0;
            this.picSrc.Name = "picSrc";
            this.picSrc.RegionInfo = null;
            this.picSrc.Size = new System.Drawing.Size(548, 734);
            this.picSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc.TabIndex = 0;
            this.picSrc.TabStop = false;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(617, 32);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 1;
            this.btn_load.Text = "加载";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.Btn_load_Click);
            // 
            // btn_ocr
            // 
            this.btn_ocr.Location = new System.Drawing.Point(617, 86);
            this.btn_ocr.Name = "btn_ocr";
            this.btn_ocr.Size = new System.Drawing.Size(75, 23);
            this.btn_ocr.TabIndex = 2;
            this.btn_ocr.Text = "识别";
            this.btn_ocr.UseVisualStyleBackColor = true;
            this.btn_ocr.Click += new System.EventHandler(this.Btn_ocr_Click);
            // 
            // tb_ocrResult
            // 
            this.tb_ocrResult.Location = new System.Drawing.Point(787, 12);
            this.tb_ocrResult.Multiline = true;
            this.tb_ocrResult.Name = "tb_ocrResult";
            this.tb_ocrResult.Size = new System.Drawing.Size(443, 134);
            this.tb_ocrResult.TabIndex = 3;
            // 
            // btn_jtyReg
            // 
            this.btn_jtyReg.Location = new System.Drawing.Point(617, 122);
            this.btn_jtyReg.Name = "btn_jtyReg";
            this.btn_jtyReg.Size = new System.Drawing.Size(75, 23);
            this.btn_jtyReg.TabIndex = 4;
            this.btn_jtyReg.Text = "金太阳识别";
            this.btn_jtyReg.UseVisualStyleBackColor = true;
            this.btn_jtyReg.Click += new System.EventHandler(this.Btn_jtyReg_Click);
            // 
            // OCRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 811);
            this.Controls.Add(this.btn_jtyReg);
            this.Controls.Add(this.tb_ocrResult);
            this.Controls.Add(this.btn_ocr);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.panel1);
            this.Name = "OCRForm";
            this.Text = "OCR测试";
            this.Load += new System.EventHandler(this.OCRForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_ocr;
        private System.Windows.Forms.TextBox tb_ocrResult;
        private System.Windows.Forms.Button btn_jtyReg;
    }
}