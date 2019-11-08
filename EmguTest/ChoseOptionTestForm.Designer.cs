namespace EmguTest
{
    partial class ChoseOptionTestForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ib_original = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.btn_load = new System.Windows.Forms.Button();
            this.ib_result = new System.Windows.Forms.PictureBox();
            this.btn_reg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ib_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_reg);
            this.groupBox1.Controls.Add(this.ib_result);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Controls.Add(this.ib_original);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 811);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单张图片自定义选择测试";
            // 
            // ib_original
            // 
            this.ib_original.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ib_original.CurrentSelectedRect = null;
            this.ib_original.Location = new System.Drawing.Point(6, 38);
            this.ib_original.MinWidth = 0;
            this.ib_original.Name = "ib_original";
            this.ib_original.RegionInfo = null;
            this.ib_original.Size = new System.Drawing.Size(428, 354);
            this.ib_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_original.TabIndex = 0;
            this.ib_original.TabStop = false;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(453, 38);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 1;
            this.btn_load.Text = "加载图片";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.Btn_load_Click);
            // 
            // ib_result
            // 
            this.ib_result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ib_result.Location = new System.Drawing.Point(7, 430);
            this.ib_result.Name = "ib_result";
            this.ib_result.Size = new System.Drawing.Size(427, 375);
            this.ib_result.TabIndex = 2;
            this.ib_result.TabStop = false;
            // 
            // btn_reg
            // 
            this.btn_reg.Location = new System.Drawing.Point(453, 97);
            this.btn_reg.Name = "btn_reg";
            this.btn_reg.Size = new System.Drawing.Size(75, 23);
            this.btn_reg.TabIndex = 3;
            this.btn_reg.Text = "识别";
            this.btn_reg.UseVisualStyleBackColor = true;
            // 
            // ChoseOptionTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 853);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChoseOptionTestForm";
            this.Text = "答题卡选项识别测试";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ib_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_load;
        private yue_juan_care.customerControl.PictureBoxReadCard ib_original;
        private System.Windows.Forms.Button btn_reg;
        private System.Windows.Forms.PictureBox ib_result;
    }
}