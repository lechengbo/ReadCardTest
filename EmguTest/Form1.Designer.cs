using System.Windows.Forms;

namespace EmguTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ib_original = new Emgu.CV.UI.ImageBox();
            this.ib_middle = new Emgu.CV.UI.ImageBox();
            this.ib_result = new Emgu.CV.UI.ImageBox();
            this.num_threshold = new System.Windows.Forms.NumericUpDown();
            this.btLoad = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.num_Min = new System.Windows.Forms.NumericUpDown();
            this.num_Max = new System.Windows.Forms.NumericUpDown();
            this.btn_checkLine = new System.Windows.Forms.Button();
            this.grp_rectGet = new System.Windows.Forms.GroupBox();
            this.num_apertureSize = new System.Windows.Forms.NumericUpDown();
            this.btn_rectReg = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_cut = new System.Windows.Forms.Button();
            this.btn_Plus = new System.Windows.Forms.Button();
            this.btn_reduce = new System.Windows.Forms.Button();
            this.ib_middleCut = new Emgu.CV.UI.ImageBox();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.btn_openZoomForm = new System.Windows.Forms.Button();
            this.btn_rotate = new System.Windows.Forms.Button();
            this.btn_openZoomForm2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_joinForm = new System.Windows.Forms.Button();
            this.btn_openCompareForm = new System.Windows.Forms.Button();
            this.btn_loadChosetestForm = new System.Windows.Forms.Button();
            this.btn_anwserReg = new System.Windows.Forms.Button();
            this.btn_answerReg = new System.Windows.Forms.Button();
            this.btn_answerReg3 = new System.Windows.Forms.Button();
            this.btn_answerReg4 = new System.Windows.Forms.Button();
            this.btn_openTemplate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ib_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_middle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Max)).BeginInit();
            this.grp_rectGet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_apertureSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_middleCut)).BeginInit();
            this.SuspendLayout();
            // 
            // ib_original
            // 
            this.ib_original.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ib_original.Location = new System.Drawing.Point(33, 28);
            this.ib_original.Name = "ib_original";
            this.ib_original.Size = new System.Drawing.Size(357, 419);
            this.ib_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_original.TabIndex = 2;
            this.ib_original.TabStop = false;
            this.ib_original.Paint += new System.Windows.Forms.PaintEventHandler(this.Ib_original_Paint);
            this.ib_original.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ib_original_MouseDown);
            this.ib_original.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ib_original_MouseMove);
            this.ib_original.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ib_original_MouseUp);
            // 
            // ib_middle
            // 
            this.ib_middle.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ib_middle.Location = new System.Drawing.Point(451, 28);
            this.ib_middle.Name = "ib_middle";
            this.ib_middle.Size = new System.Drawing.Size(368, 427);
            this.ib_middle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_middle.TabIndex = 2;
            this.ib_middle.TabStop = false;
            // 
            // ib_result
            // 
            this.ib_result.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ib_result.Location = new System.Drawing.Point(863, 28);
            this.ib_result.Name = "ib_result";
            this.ib_result.Size = new System.Drawing.Size(353, 427);
            this.ib_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_result.TabIndex = 2;
            this.ib_result.TabStop = false;
            // 
            // num_threshold
            // 
            this.num_threshold.Location = new System.Drawing.Point(33, 493);
            this.num_threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_threshold.Name = "num_threshold";
            this.num_threshold.Size = new System.Drawing.Size(120, 21);
            this.num_threshold.TabIndex = 4;
            this.num_threshold.Value = new decimal(new int[] {
            190,
            0,
            0,
            0});
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(203, 486);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(75, 23);
            this.btLoad.TabIndex = 5;
            this.btLoad.Text = "载入图片";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.BtLoad_Click);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(6, 33);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 6;
            this.btStart.Text = "识别矩形框";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // num_Min
            // 
            this.num_Min.Location = new System.Drawing.Point(6, 94);
            this.num_Min.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_Min.Name = "num_Min";
            this.num_Min.Size = new System.Drawing.Size(120, 21);
            this.num_Min.TabIndex = 7;
            this.num_Min.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // num_Max
            // 
            this.num_Max.Location = new System.Drawing.Point(6, 128);
            this.num_Max.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_Max.Name = "num_Max";
            this.num_Max.Size = new System.Drawing.Size(120, 21);
            this.num_Max.TabIndex = 8;
            this.num_Max.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // btn_checkLine
            // 
            this.btn_checkLine.Location = new System.Drawing.Point(315, 493);
            this.btn_checkLine.Name = "btn_checkLine";
            this.btn_checkLine.Size = new System.Drawing.Size(75, 23);
            this.btn_checkLine.TabIndex = 9;
            this.btn_checkLine.Text = "直线检测";
            this.btn_checkLine.UseVisualStyleBackColor = true;
            this.btn_checkLine.Click += new System.EventHandler(this.Btn_checkLine_Click);
            // 
            // grp_rectGet
            // 
            this.grp_rectGet.Controls.Add(this.num_apertureSize);
            this.grp_rectGet.Controls.Add(this.num_Max);
            this.grp_rectGet.Controls.Add(this.btStart);
            this.grp_rectGet.Controls.Add(this.num_Min);
            this.grp_rectGet.Location = new System.Drawing.Point(33, 541);
            this.grp_rectGet.Name = "grp_rectGet";
            this.grp_rectGet.Size = new System.Drawing.Size(148, 183);
            this.grp_rectGet.TabIndex = 11;
            this.grp_rectGet.TabStop = false;
            this.grp_rectGet.Text = "方框识别";
            // 
            // num_apertureSize
            // 
            this.num_apertureSize.Location = new System.Drawing.Point(6, 162);
            this.num_apertureSize.Name = "num_apertureSize";
            this.num_apertureSize.Size = new System.Drawing.Size(120, 21);
            this.num_apertureSize.TabIndex = 9;
            this.num_apertureSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btn_rectReg
            // 
            this.btn_rectReg.Location = new System.Drawing.Point(203, 602);
            this.btn_rectReg.Name = "btn_rectReg";
            this.btn_rectReg.Size = new System.Drawing.Size(88, 23);
            this.btn_rectReg.TabIndex = 12;
            this.btn_rectReg.Text = "裁剪中方形识别";
            this.btn_rectReg.UseVisualStyleBackColor = true;
            this.btn_rectReg.Click += new System.EventHandler(this.Btn_rectReg_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(203, 573);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 13;
            this.btn_clear.Text = "清空痕迹";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.Btn_clear_Click);
            // 
            // btn_cut
            // 
            this.btn_cut.Location = new System.Drawing.Point(203, 544);
            this.btn_cut.Name = "btn_cut";
            this.btn_cut.Size = new System.Drawing.Size(75, 23);
            this.btn_cut.TabIndex = 14;
            this.btn_cut.Text = "开始裁剪";
            this.btn_cut.UseVisualStyleBackColor = true;
            this.btn_cut.Click += new System.EventHandler(this.Btn_cut_Click);
            // 
            // btn_Plus
            // 
            this.btn_Plus.Location = new System.Drawing.Point(203, 632);
            this.btn_Plus.Name = "btn_Plus";
            this.btn_Plus.Size = new System.Drawing.Size(34, 23);
            this.btn_Plus.TabIndex = 15;
            this.btn_Plus.Text = "+";
            this.btn_Plus.UseVisualStyleBackColor = true;
            this.btn_Plus.Click += new System.EventHandler(this.Btn_Plus_Click);
            // 
            // btn_reduce
            // 
            this.btn_reduce.Location = new System.Drawing.Point(243, 632);
            this.btn_reduce.Name = "btn_reduce";
            this.btn_reduce.Size = new System.Drawing.Size(35, 23);
            this.btn_reduce.TabIndex = 16;
            this.btn_reduce.Text = "-";
            this.btn_reduce.UseVisualStyleBackColor = true;
            this.btn_reduce.Click += new System.EventHandler(this.Btn_reduce_Click);
            // 
            // ib_middleCut
            // 
            this.ib_middleCut.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.ib_middleCut.Location = new System.Drawing.Point(1297, 28);
            this.ib_middleCut.Name = "ib_middleCut";
            this.ib_middleCut.Size = new System.Drawing.Size(381, 427);
            this.ib_middleCut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ib_middleCut.TabIndex = 17;
            this.ib_middleCut.TabStop = false;
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(763, 486);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.Size = new System.Drawing.Size(146, 41);
            this.tb_log.TabIndex = 18;
            // 
            // btn_openZoomForm
            // 
            this.btn_openZoomForm.Location = new System.Drawing.Point(417, 493);
            this.btn_openZoomForm.Name = "btn_openZoomForm";
            this.btn_openZoomForm.Size = new System.Drawing.Size(75, 23);
            this.btn_openZoomForm.TabIndex = 19;
            this.btn_openZoomForm.Text = "打开缩放窗口";
            this.btn_openZoomForm.UseVisualStyleBackColor = true;
            this.btn_openZoomForm.Click += new System.EventHandler(this.Btn_openZoomForm_Click);
            // 
            // btn_rotate
            // 
            this.btn_rotate.Location = new System.Drawing.Point(203, 515);
            this.btn_rotate.Name = "btn_rotate";
            this.btn_rotate.Size = new System.Drawing.Size(75, 23);
            this.btn_rotate.TabIndex = 20;
            this.btn_rotate.Text = "旋转";
            this.btn_rotate.UseVisualStyleBackColor = true;
            this.btn_rotate.Click += new System.EventHandler(this.Btn_rotate_Click);
            // 
            // btn_openZoomForm2
            // 
            this.btn_openZoomForm2.Location = new System.Drawing.Point(524, 493);
            this.btn_openZoomForm2.Name = "btn_openZoomForm2";
            this.btn_openZoomForm2.Size = new System.Drawing.Size(99, 23);
            this.btn_openZoomForm2.TabIndex = 21;
            this.btn_openZoomForm2.Text = "打开缩放窗口2";
            this.btn_openZoomForm2.UseVisualStyleBackColor = true;
            this.btn_openZoomForm2.Click += new System.EventHandler(this.Btn_openZoomForm2_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(977, 486);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 327);
            this.panel1.TabIndex = 22;
            // 
            // btn_joinForm
            // 
            this.btn_joinForm.Location = new System.Drawing.Point(417, 544);
            this.btn_joinForm.Name = "btn_joinForm";
            this.btn_joinForm.Size = new System.Drawing.Size(75, 23);
            this.btn_joinForm.TabIndex = 23;
            this.btn_joinForm.Text = "加入ZoomForm2";
            this.btn_joinForm.UseVisualStyleBackColor = true;
            this.btn_joinForm.Click += new System.EventHandler(this.Btn_joinForm_Click);
            // 
            // btn_openCompareForm
            // 
            this.btn_openCompareForm.Location = new System.Drawing.Point(524, 543);
            this.btn_openCompareForm.Name = "btn_openCompareForm";
            this.btn_openCompareForm.Size = new System.Drawing.Size(99, 23);
            this.btn_openCompareForm.TabIndex = 24;
            this.btn_openCompareForm.Text = "打开对比窗口";
            this.btn_openCompareForm.UseVisualStyleBackColor = true;
            this.btn_openCompareForm.Click += new System.EventHandler(this.Btn_openCompareForm_Click);
            // 
            // btn_loadChosetestForm
            // 
            this.btn_loadChosetestForm.Location = new System.Drawing.Point(524, 602);
            this.btn_loadChosetestForm.Name = "btn_loadChosetestForm";
            this.btn_loadChosetestForm.Size = new System.Drawing.Size(99, 23);
            this.btn_loadChosetestForm.TabIndex = 25;
            this.btn_loadChosetestForm.Text = "打开测试窗口";
            this.btn_loadChosetestForm.UseVisualStyleBackColor = true;
            this.btn_loadChosetestForm.Click += new System.EventHandler(this.Btn_loadChosetestForm_Click);
            // 
            // btn_anwserReg
            // 
            this.btn_anwserReg.Location = new System.Drawing.Point(203, 669);
            this.btn_anwserReg.Name = "btn_anwserReg";
            this.btn_anwserReg.Size = new System.Drawing.Size(75, 23);
            this.btn_anwserReg.TabIndex = 26;
            this.btn_anwserReg.Text = "答案卡识别";
            this.btn_anwserReg.UseVisualStyleBackColor = true;
            this.btn_anwserReg.Click += new System.EventHandler(this.Btn_anwserReg_Click);
            // 
            // btn_answerReg
            // 
            this.btn_answerReg.Location = new System.Drawing.Point(203, 700);
            this.btn_answerReg.Name = "btn_answerReg";
            this.btn_answerReg.Size = new System.Drawing.Size(88, 23);
            this.btn_answerReg.TabIndex = 27;
            this.btn_answerReg.Text = "答案卡识别2";
            this.btn_answerReg.UseVisualStyleBackColor = true;
            this.btn_answerReg.Click += new System.EventHandler(this.Btn_answerReg_Click);
            // 
            // btn_answerReg3
            // 
            this.btn_answerReg3.Location = new System.Drawing.Point(203, 745);
            this.btn_answerReg3.Name = "btn_answerReg3";
            this.btn_answerReg3.Size = new System.Drawing.Size(88, 23);
            this.btn_answerReg3.TabIndex = 28;
            this.btn_answerReg3.Text = "答案卡识别3";
            this.btn_answerReg3.UseVisualStyleBackColor = true;
            this.btn_answerReg3.Click += new System.EventHandler(this.Btn_answerReg3_Click);
            // 
            // btn_answerReg4
            // 
            this.btn_answerReg4.Location = new System.Drawing.Point(203, 775);
            this.btn_answerReg4.Name = "btn_answerReg4";
            this.btn_answerReg4.Size = new System.Drawing.Size(88, 23);
            this.btn_answerReg4.TabIndex = 29;
            this.btn_answerReg4.Text = "答案卡识别4";
            this.btn_answerReg4.UseVisualStyleBackColor = true;
            this.btn_answerReg4.Click += new System.EventHandler(this.Btn_answerReg4_Click);
            // 
            // btn_openTemplate
            // 
            this.btn_openTemplate.Location = new System.Drawing.Point(524, 658);
            this.btn_openTemplate.Name = "btn_openTemplate";
            this.btn_openTemplate.Size = new System.Drawing.Size(99, 23);
            this.btn_openTemplate.TabIndex = 30;
            this.btn_openTemplate.Text = "打开模板定位";
            this.btn_openTemplate.UseVisualStyleBackColor = true;
            this.btn_openTemplate.Click += new System.EventHandler(this.Btn_openTemplate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1727, 835);
            this.Controls.Add(this.btn_openTemplate);
            this.Controls.Add(this.btn_answerReg4);
            this.Controls.Add(this.btn_answerReg3);
            this.Controls.Add(this.btn_answerReg);
            this.Controls.Add(this.btn_anwserReg);
            this.Controls.Add(this.btn_loadChosetestForm);
            this.Controls.Add(this.btn_openCompareForm);
            this.Controls.Add(this.btn_joinForm);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_openZoomForm2);
            this.Controls.Add(this.btn_rotate);
            this.Controls.Add(this.btn_openZoomForm);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.ib_middleCut);
            this.Controls.Add(this.btn_reduce);
            this.Controls.Add(this.btn_Plus);
            this.Controls.Add(this.btn_cut);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_rectReg);
            this.Controls.Add(this.grp_rectGet);
            this.Controls.Add(this.btn_checkLine);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.num_threshold);
            this.Controls.Add(this.ib_result);
            this.Controls.Add(this.ib_middle);
            this.Controls.Add(this.ib_original);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ib_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_middle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Max)).EndInit();
            this.grp_rectGet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_apertureSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib_middleCut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ib_original;
        private Emgu.CV.UI.ImageBox ib_middle;
        private Emgu.CV.UI.ImageBox ib_result;
        private NumericUpDown num_threshold;
        private Button btLoad;
        private Button btStart;
        private NumericUpDown num_Min;
        private NumericUpDown num_Max;
        private Button btn_checkLine;
        private GroupBox grp_rectGet;
        private NumericUpDown num_apertureSize;
        private Button btn_rectReg;
        private Button btn_clear;
        private Button btn_cut;
        private Button btn_Plus;
        private Button btn_reduce;
        private Emgu.CV.UI.ImageBox ib_middleCut;
        private TextBox tb_log;
        private Button btn_openZoomForm;
        private Button btn_rotate;
        private Button btn_openZoomForm2;
        private Panel panel1;
        private Button btn_joinForm;
        private Button btn_openCompareForm;
        private Button btn_loadChosetestForm;
        private Button btn_anwserReg;
        private Button btn_answerReg;
        private Button btn_answerReg3;
        private Button btn_answerReg4;
        private Button btn_openTemplate;
    }


}

