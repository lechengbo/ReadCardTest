﻿namespace EmguTest
{
    partial class AnwserRegTest
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
            this.btn_validate = new System.Windows.Forms.Button();
            this.btn_recover = new System.Windows.Forms.Button();
            this.btn_validate2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picSrc);
            this.panel1.Location = new System.Drawing.Point(205, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 781);
            this.panel1.TabIndex = 0;
            // 
            // picSrc
            // 
            this.picSrc.CurrentSelectedRect = null;
            this.picSrc.Location = new System.Drawing.Point(3, 10);
            this.picSrc.MinWidth = 0;
            this.picSrc.Name = "picSrc";
            this.picSrc.RegionInfo = null;
            this.picSrc.Size = new System.Drawing.Size(584, 766);
            this.picSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc.TabIndex = 0;
            this.picSrc.TabStop = false;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(26, 22);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(86, 23);
            this.btn_load.TabIndex = 1;
            this.btn_load.Text = "加载扫描结果";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.Btn_load_Click);
            // 
            // btn_validate
            // 
            this.btn_validate.Location = new System.Drawing.Point(26, 82);
            this.btn_validate.Name = "btn_validate";
            this.btn_validate.Size = new System.Drawing.Size(75, 23);
            this.btn_validate.TabIndex = 2;
            this.btn_validate.Text = "校正识别框";
            this.btn_validate.UseVisualStyleBackColor = true;
            this.btn_validate.Click += new System.EventHandler(this.Btn_validate_Click);
            // 
            // btn_recover
            // 
            this.btn_recover.Location = new System.Drawing.Point(26, 130);
            this.btn_recover.Name = "btn_recover";
            this.btn_recover.Size = new System.Drawing.Size(75, 23);
            this.btn_recover.TabIndex = 3;
            this.btn_recover.Text = "恢复未效验";
            this.btn_recover.UseVisualStyleBackColor = true;
            this.btn_recover.Click += new System.EventHandler(this.Btn_recover_Click);
            // 
            // btn_validate2
            // 
            this.btn_validate2.Location = new System.Drawing.Point(26, 186);
            this.btn_validate2.Name = "btn_validate2";
            this.btn_validate2.Size = new System.Drawing.Size(86, 23);
            this.btn_validate2.TabIndex = 4;
            this.btn_validate2.Text = "校正识别框2";
            this.btn_validate2.UseVisualStyleBackColor = true;
            this.btn_validate2.Click += new System.EventHandler(this.Btn_validate2_Click);
            // 
            // AnwserRegTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 794);
            this.Controls.Add(this.btn_validate2);
            this.Controls.Add(this.btn_recover);
            this.Controls.Add(this.btn_validate);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.panel1);
            this.Name = "AnwserRegTest";
            this.Text = "选项答案识别测试";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_load;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc;
        private System.Windows.Forms.Button btn_validate;
        private System.Windows.Forms.Button btn_recover;
        private System.Windows.Forms.Button btn_validate2;
    }
}