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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ib_result = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ib_original = new yue_juan_care.customerControl.PictureBoxReadCard();
            this.btn_reg = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.minNum = new System.Windows.Forms.NumericUpDown();
            this.maxNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_regResul = new System.Windows.Forms.Label();
            this.btn_regMult = new System.Windows.Forms.Button();
            this.lbl_picNumInfo = new System.Windows.Forms.Label();
            this.btn_loadMult = new System.Windows.Forms.Button();
            this.group_Config = new System.Windows.Forms.GroupBox();
            this.btn_fiexPointReg = new System.Windows.Forms.Button();
            this.btn_resultShow = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ib_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxNum)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.group_Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_resultShow);
            this.groupBox1.Controls.Add(this.btn_fiexPointReg);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btn_reg);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 811);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单张图片自定义选择测试";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ib_result);
            this.panel2.Location = new System.Drawing.Point(17, 424);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 381);
            this.panel2.TabIndex = 5;
            // 
            // ib_result
            // 
            this.ib_result.CurrentSelectedRect = null;
            this.ib_result.Location = new System.Drawing.Point(4, 14);
            this.ib_result.MinWidth = 0;
            this.ib_result.Name = "ib_result";
            this.ib_result.RegionInfo = null;
            this.ib_result.Size = new System.Drawing.Size(423, 343);
            this.ib_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_result.TabIndex = 0;
            this.ib_result.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ib_original);
            this.panel1.Location = new System.Drawing.Point(17, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 370);
            this.panel1.TabIndex = 4;
            // 
            // ib_original
            // 
            this.ib_original.CurrentSelectedRect = null;
            this.ib_original.Location = new System.Drawing.Point(3, 17);
            this.ib_original.MinWidth = 0;
            this.ib_original.Name = "ib_original";
            this.ib_original.RegionInfo = null;
            this.ib_original.Size = new System.Drawing.Size(422, 320);
            this.ib_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_original.TabIndex = 0;
            this.ib_original.TabStop = false;
            // 
            // btn_reg
            // 
            this.btn_reg.Location = new System.Drawing.Point(453, 97);
            this.btn_reg.Name = "btn_reg";
            this.btn_reg.Size = new System.Drawing.Size(75, 23);
            this.btn_reg.TabIndex = 3;
            this.btn_reg.Text = "识别";
            this.btn_reg.UseVisualStyleBackColor = true;
            this.btn_reg.Click += new System.EventHandler(this.Btn_reg_Click);
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
            // minNum
            // 
            this.minNum.Location = new System.Drawing.Point(163, 29);
            this.minNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minNum.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.minNum.Name = "minNum";
            this.minNum.Size = new System.Drawing.Size(87, 21);
            this.minNum.TabIndex = 6;
            this.minNum.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // maxNum
            // 
            this.maxNum.Location = new System.Drawing.Point(164, 72);
            this.maxNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.maxNum.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.maxNum.Name = "maxNum";
            this.maxNum.Size = new System.Drawing.Size(86, 21);
            this.maxNum.TabIndex = 7;
            this.maxNum.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "过滤面积最小值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "过滤面积最大值：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.lbl_regResul);
            this.groupBox2.Controls.Add(this.btn_regMult);
            this.groupBox2.Controls.Add(this.lbl_picNumInfo);
            this.groupBox2.Controls.Add(this.btn_loadMult);
            this.groupBox2.Location = new System.Drawing.Point(642, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(601, 231);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "批量处理";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "打开文件夹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // lbl_regResul
            // 
            this.lbl_regResul.AutoSize = true;
            this.lbl_regResul.Location = new System.Drawing.Point(162, 99);
            this.lbl_regResul.Name = "lbl_regResul";
            this.lbl_regResul.Size = new System.Drawing.Size(0, 12);
            this.lbl_regResul.TabIndex = 3;
            // 
            // btn_regMult
            // 
            this.btn_regMult.Location = new System.Drawing.Point(19, 88);
            this.btn_regMult.Name = "btn_regMult";
            this.btn_regMult.Size = new System.Drawing.Size(75, 23);
            this.btn_regMult.TabIndex = 2;
            this.btn_regMult.Text = "批量识别";
            this.btn_regMult.UseVisualStyleBackColor = true;
            this.btn_regMult.Click += new System.EventHandler(this.Btn_regMult_Click);
            // 
            // lbl_picNumInfo
            // 
            this.lbl_picNumInfo.AutoSize = true;
            this.lbl_picNumInfo.Location = new System.Drawing.Point(162, 43);
            this.lbl_picNumInfo.Name = "lbl_picNumInfo";
            this.lbl_picNumInfo.Size = new System.Drawing.Size(0, 12);
            this.lbl_picNumInfo.TabIndex = 1;
            // 
            // btn_loadMult
            // 
            this.btn_loadMult.Location = new System.Drawing.Point(19, 38);
            this.btn_loadMult.Name = "btn_loadMult";
            this.btn_loadMult.Size = new System.Drawing.Size(75, 23);
            this.btn_loadMult.TabIndex = 0;
            this.btn_loadMult.Text = "批量加载";
            this.btn_loadMult.UseVisualStyleBackColor = true;
            this.btn_loadMult.Click += new System.EventHandler(this.Btn_loadMult_Click);
            // 
            // group_Config
            // 
            this.group_Config.Controls.Add(this.label2);
            this.group_Config.Controls.Add(this.label1);
            this.group_Config.Controls.Add(this.minNum);
            this.group_Config.Controls.Add(this.maxNum);
            this.group_Config.Location = new System.Drawing.Point(642, 12);
            this.group_Config.Name = "group_Config";
            this.group_Config.Size = new System.Drawing.Size(601, 100);
            this.group_Config.TabIndex = 2;
            this.group_Config.TabStop = false;
            this.group_Config.Text = "公共参数设置";
            // 
            // btn_fiexPointReg
            // 
            this.btn_fiexPointReg.Location = new System.Drawing.Point(454, 136);
            this.btn_fiexPointReg.Name = "btn_fiexPointReg";
            this.btn_fiexPointReg.Size = new System.Drawing.Size(75, 23);
            this.btn_fiexPointReg.TabIndex = 6;
            this.btn_fiexPointReg.Text = "定位点识别";
            this.btn_fiexPointReg.UseVisualStyleBackColor = true;
            this.btn_fiexPointReg.Click += new System.EventHandler(this.Btn_fiexPointReg_Click);
            // 
            // btn_resultShow
            // 
            this.btn_resultShow.Location = new System.Drawing.Point(454, 176);
            this.btn_resultShow.Name = "btn_resultShow";
            this.btn_resultShow.Size = new System.Drawing.Size(85, 23);
            this.btn_resultShow.TabIndex = 7;
            this.btn_resultShow.Text = "总体结果显示";
            this.btn_resultShow.UseVisualStyleBackColor = true;
            this.btn_resultShow.Click += new System.EventHandler(this.Btn_resultShow_Click);
            // 
            // ChoseOptionTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 853);
            this.Controls.Add(this.group_Config);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChoseOptionTestForm";
            this.Text = "答题卡选项识别测试";
            this.Load += new System.EventHandler(this.ChoseOptionTestForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ib_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxNum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.group_Config.ResumeLayout(false);
            this.group_Config.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_load;
        private yue_juan_care.customerControl.PictureBoxReadCard ib_original;
        private System.Windows.Forms.Button btn_reg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private yue_juan_care.customerControl.PictureBoxReadCard ib_result;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown maxNum;
        private System.Windows.Forms.NumericUpDown minNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_regResul;
        private System.Windows.Forms.Button btn_regMult;
        private System.Windows.Forms.Label lbl_picNumInfo;
        private System.Windows.Forms.Button btn_loadMult;
        private System.Windows.Forms.GroupBox group_Config;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_fiexPointReg;
        private System.Windows.Forms.Button btn_resultShow;
    }
}