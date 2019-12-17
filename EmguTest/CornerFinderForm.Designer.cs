namespace EmguTest
{
    partial class CornerFinderForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.picTarget = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_findCorner = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btn_findLine = new System.Windows.Forms.Button();
            this.btn_shapFind = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picCircle = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.panel4 = new System.Windows.Forms.Panel();
            this.picRect = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.btn_hog = new System.Windows.Forms.Button();
            this.btn_svm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCircle)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRect)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picSrc);
            this.panel1.Location = new System.Drawing.Point(43, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 353);
            this.panel1.TabIndex = 0;
            // 
            // picSrc
            // 
            this.picSrc.CurrentSelectedRect = null;
            this.picSrc.Location = new System.Drawing.Point(3, 3);
            this.picSrc.MinWidth = 0;
            this.picSrc.Name = "picSrc";
            this.picSrc.RegionInfo = null;
            this.picSrc.Size = new System.Drawing.Size(450, 343);
            this.picSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc.TabIndex = 0;
            this.picSrc.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.picTarget);
            this.panel2.Location = new System.Drawing.Point(43, 412);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(458, 399);
            this.panel2.TabIndex = 1;
            // 
            // picTarget
            // 
            this.picTarget.CurrentSelectedRect = null;
            this.picTarget.Location = new System.Drawing.Point(4, 4);
            this.picTarget.MinWidth = 0;
            this.picTarget.Name = "picTarget";
            this.picTarget.RegionInfo = null;
            this.picTarget.Size = new System.Drawing.Size(449, 390);
            this.picTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTarget.TabIndex = 0;
            this.picTarget.TabStop = false;
            // 
            // btn_load
            // 
            this.btn_load.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.btn_load.Location = new System.Drawing.Point(554, 41);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 2;
            this.btn_load.Text = "加载图片";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.Btn_load_Click);
            // 
            // btn_findCorner
            // 
            this.btn_findCorner.Location = new System.Drawing.Point(554, 101);
            this.btn_findCorner.Name = "btn_findCorner";
            this.btn_findCorner.Size = new System.Drawing.Size(75, 23);
            this.btn_findCorner.TabIndex = 3;
            this.btn_findCorner.Text = "角检查";
            this.btn_findCorner.UseVisualStyleBackColor = true;
            this.btn_findCorner.Click += new System.EventHandler(this.Btn_findCorner_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(554, 151);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 21);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // btn_findLine
            // 
            this.btn_findLine.Location = new System.Drawing.Point(554, 215);
            this.btn_findLine.Name = "btn_findLine";
            this.btn_findLine.Size = new System.Drawing.Size(75, 23);
            this.btn_findLine.TabIndex = 5;
            this.btn_findLine.Text = "直线检测";
            this.btn_findLine.UseVisualStyleBackColor = true;
            this.btn_findLine.Click += new System.EventHandler(this.Btn_findLine_Click);
            // 
            // btn_shapFind
            // 
            this.btn_shapFind.Location = new System.Drawing.Point(554, 291);
            this.btn_shapFind.Name = "btn_shapFind";
            this.btn_shapFind.Size = new System.Drawing.Size(93, 23);
            this.btn_shapFind.TabIndex = 6;
            this.btn_shapFind.Text = "各种形状检查";
            this.btn_shapFind.UseVisualStyleBackColor = true;
            this.btn_shapFind.Click += new System.EventHandler(this.Btn_shapFind_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.picCircle);
            this.panel3.Location = new System.Drawing.Point(554, 417);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(403, 394);
            this.panel3.TabIndex = 7;
            // 
            // picCircle
            // 
            this.picCircle.CurrentSelectedRect = null;
            this.picCircle.Location = new System.Drawing.Point(4, 4);
            this.picCircle.MinWidth = 0;
            this.picCircle.Name = "picCircle";
            this.picCircle.RegionInfo = null;
            this.picCircle.Size = new System.Drawing.Size(394, 385);
            this.picCircle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCircle.TabIndex = 0;
            this.picCircle.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.picRect);
            this.panel4.Location = new System.Drawing.Point(1022, 417);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(413, 394);
            this.panel4.TabIndex = 8;
            // 
            // picRect
            // 
            this.picRect.CurrentSelectedRect = null;
            this.picRect.Location = new System.Drawing.Point(3, 4);
            this.picRect.MinWidth = 0;
            this.picRect.Name = "picRect";
            this.picRect.RegionInfo = null;
            this.picRect.Size = new System.Drawing.Size(405, 385);
            this.picRect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRect.TabIndex = 0;
            this.picRect.TabStop = false;
            // 
            // btn_hog
            // 
            this.btn_hog.Location = new System.Drawing.Point(554, 336);
            this.btn_hog.Name = "btn_hog";
            this.btn_hog.Size = new System.Drawing.Size(75, 23);
            this.btn_hog.TabIndex = 9;
            this.btn_hog.Text = "HOG检测";
            this.btn_hog.UseVisualStyleBackColor = true;
            this.btn_hog.Click += new System.EventHandler(this.Btn_hog_Click);
            // 
            // btn_svm
            // 
            this.btn_svm.Location = new System.Drawing.Point(684, 335);
            this.btn_svm.Name = "btn_svm";
            this.btn_svm.Size = new System.Drawing.Size(75, 23);
            this.btn_svm.TabIndex = 10;
            this.btn_svm.Text = "svm训练";
            this.btn_svm.UseVisualStyleBackColor = true;
            this.btn_svm.Click += new System.EventHandler(this.Btn_svm_Click);
            // 
            // CornerFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 823);
            this.Controls.Add(this.btn_svm);
            this.Controls.Add(this.btn_hog);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btn_shapFind);
            this.Controls.Add(this.btn_findLine);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btn_findCorner);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CornerFinderForm";
            this.Text = "角发现测试";
            this.Load += new System.EventHandler(this.CornerFinderForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCircle)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc;
        private System.Windows.Forms.Panel panel2;
        private yue_juan_care.customerControl.PictureBoxReadCard picTarget;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_findCorner;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btn_findLine;
        private System.Windows.Forms.Button btn_shapFind;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private yue_juan_care.customerControl.PictureBoxReadCard picCircle;
        private yue_juan_care.customerControl.PictureBoxReadCard picRect;
        private System.Windows.Forms.Button btn_hog;
        private System.Windows.Forms.Button btn_svm;
    }
}