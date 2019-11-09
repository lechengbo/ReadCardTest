namespace EmguTest
{
    partial class CompareForm
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
            this.picSrc1 = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picSrc2 = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.picCompare1 = new Emgu.CV.UI.ImageBox();
            this.picCompare2 = new Emgu.CV.UI.ImageBox();
            this.btn_load1 = new System.Windows.Forms.Button();
            this.btn_load2 = new System.Windows.Forms.Button();
            this.btn_cut = new System.Windows.Forms.Button();
            this.btn_compare = new System.Windows.Forms.Button();
            this.lbl_result = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSrc2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompare1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompare2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picSrc1);
            this.panel1.Location = new System.Drawing.Point(23, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 251);
            this.panel1.TabIndex = 0;
            // 
            // picSrc1
            // 
            this.picSrc1.CurrentSelectedRect = null;
            this.picSrc1.Location = new System.Drawing.Point(3, 3);
            this.picSrc1.MinWidth = 0;
            this.picSrc1.Name = "picSrc1";
            this.picSrc1.RegionInfo = null;
            this.picSrc1.Size = new System.Drawing.Size(340, 245);
            this.picSrc1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc1.TabIndex = 0;
            this.picSrc1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.picSrc2);
            this.panel2.Location = new System.Drawing.Point(23, 336);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 272);
            this.panel2.TabIndex = 1;
            // 
            // picSrc2
            // 
            this.picSrc2.CurrentSelectedRect = null;
            this.picSrc2.Location = new System.Drawing.Point(0, 3);
            this.picSrc2.MinWidth = 0;
            this.picSrc2.Name = "picSrc2";
            this.picSrc2.RegionInfo = null;
            this.picSrc2.Size = new System.Drawing.Size(343, 266);
            this.picSrc2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSrc2.TabIndex = 0;
            this.picSrc2.TabStop = false;
            // 
            // picCompare1
            // 
            this.picCompare1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCompare1.Location = new System.Drawing.Point(535, 49);
            this.picCompare1.Name = "picCompare1";
            this.picCompare1.Size = new System.Drawing.Size(169, 185);
            this.picCompare1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCompare1.TabIndex = 2;
            this.picCompare1.TabStop = false;
            // 
            // picCompare2
            // 
            this.picCompare2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCompare2.Location = new System.Drawing.Point(749, 49);
            this.picCompare2.Name = "picCompare2";
            this.picCompare2.Size = new System.Drawing.Size(169, 185);
            this.picCompare2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCompare2.TabIndex = 2;
            this.picCompare2.TabStop = false;
            // 
            // btn_load1
            // 
            this.btn_load1.Location = new System.Drawing.Point(386, 49);
            this.btn_load1.Name = "btn_load1";
            this.btn_load1.Size = new System.Drawing.Size(75, 23);
            this.btn_load1.TabIndex = 3;
            this.btn_load1.Text = "加载";
            this.btn_load1.UseVisualStyleBackColor = true;
            this.btn_load1.Click += new System.EventHandler(this.Btn_load1_Click);
            // 
            // btn_load2
            // 
            this.btn_load2.Location = new System.Drawing.Point(395, 340);
            this.btn_load2.Name = "btn_load2";
            this.btn_load2.Size = new System.Drawing.Size(75, 23);
            this.btn_load2.TabIndex = 4;
            this.btn_load2.Text = "加载";
            this.btn_load2.UseVisualStyleBackColor = true;
            this.btn_load2.Click += new System.EventHandler(this.Btn_load2_Click);
            // 
            // btn_cut
            // 
            this.btn_cut.Location = new System.Drawing.Point(535, 275);
            this.btn_cut.Name = "btn_cut";
            this.btn_cut.Size = new System.Drawing.Size(75, 23);
            this.btn_cut.TabIndex = 5;
            this.btn_cut.Text = "裁剪";
            this.btn_cut.UseVisualStyleBackColor = true;
            this.btn_cut.Click += new System.EventHandler(this.Btn_cut_Click);
            // 
            // btn_compare
            // 
            this.btn_compare.Location = new System.Drawing.Point(651, 275);
            this.btn_compare.Name = "btn_compare";
            this.btn_compare.Size = new System.Drawing.Size(75, 23);
            this.btn_compare.TabIndex = 6;
            this.btn_compare.Text = "对比";
            this.btn_compare.UseVisualStyleBackColor = true;
            this.btn_compare.Click += new System.EventHandler(this.Btn_compare_Click);
            // 
            // lbl_result
            // 
            this.lbl_result.AutoSize = true;
            this.lbl_result.Location = new System.Drawing.Point(778, 285);
            this.lbl_result.Name = "lbl_result";
            this.lbl_result.Size = new System.Drawing.Size(0, 12);
            this.lbl_result.TabIndex = 7;
            // 
            // CompareForm
            // 
            this.ClientSize = new System.Drawing.Size(974, 680);
            this.Controls.Add(this.lbl_result);
            this.Controls.Add(this.btn_compare);
            this.Controls.Add(this.btn_cut);
            this.Controls.Add(this.btn_load2);
            this.Controls.Add(this.btn_load1);
            this.Controls.Add(this.picCompare2);
            this.Controls.Add(this.picCompare1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CompareForm";
            this.Load += new System.EventHandler(this.CompareForm_Load_1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSrc2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompare1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompare2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private yue_juan_care.customerControl.PictureBoxReadCard picSrc1;
        //private yue_juan_care.customerControl.PictureBoxReadCard picSrc2;
        //private System.Windows.Forms.Button btn_load1;
        //private System.Windows.Forms.Button btn_load2;
        //private System.Windows.Forms.Button btn_cut;
        //private System.Windows.Forms.Button btn_compare;
        //private System.Windows.Forms.Label lbl_result;
        //private System.Windows.Forms.Panel panel1;
        //private System.Windows.Forms.Panel panel2;
        //private Emgu.CV.UI.ImageBox picCompare1;
        //private Emgu.CV.UI.ImageBox picCompare2;

        private System.Windows.Forms.Button btn_histShow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc1;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc2;
        private Emgu.CV.UI.ImageBox picCompare1;
        private Emgu.CV.UI.ImageBox picCompare2;
        private System.Windows.Forms.Button btn_load1;
        private System.Windows.Forms.Button btn_load2;
        private System.Windows.Forms.Button btn_cut;
        private System.Windows.Forms.Button btn_compare;
        private System.Windows.Forms.Label lbl_result;
    }
}