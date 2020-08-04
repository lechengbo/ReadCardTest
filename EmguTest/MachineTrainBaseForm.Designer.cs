namespace EmguTest
{
    partial class MachineTrainBaseForm
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
            this.bt_EM = new System.Windows.Forms.Button();
            this.bt_ANN = new System.Windows.Forms.Button();
            this.ib_result = new Emgu.CV.UI.ImageBox();
            this.btn_CNN2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txb_avg = new System.Windows.Forms.TextBox();
            this.txb_percent = new System.Windows.Forms.TextBox();
            this.btn_reg = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btn_ann2 = new System.Windows.Forms.Button();
            this.btn_ANNReg = new System.Windows.Forms.Button();
            this.grpANN = new System.Windows.Forms.GroupBox();
            this.txbregPath = new System.Windows.Forms.TextBox();
            this.ckbIsAct = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.grpANN.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_EM
            // 
            this.bt_EM.Location = new System.Drawing.Point(44, 30);
            this.bt_EM.Name = "bt_EM";
            this.bt_EM.Size = new System.Drawing.Size(75, 23);
            this.bt_EM.TabIndex = 0;
            this.bt_EM.Text = "最大期望";
            this.bt_EM.UseVisualStyleBackColor = true;
            this.bt_EM.Click += new System.EventHandler(this.Bt_EM_Click);
            // 
            // bt_ANN
            // 
            this.bt_ANN.Location = new System.Drawing.Point(44, 101);
            this.bt_ANN.Name = "bt_ANN";
            this.bt_ANN.Size = new System.Drawing.Size(75, 23);
            this.bt_ANN.TabIndex = 1;
            this.bt_ANN.Text = "神经网络";
            this.bt_ANN.UseVisualStyleBackColor = true;
            this.bt_ANN.Click += new System.EventHandler(this.Bt_ANN_Click);
            // 
            // ib_result
            // 
            this.ib_result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ib_result.Location = new System.Drawing.Point(420, 12);
            this.ib_result.Name = "ib_result";
            this.ib_result.Size = new System.Drawing.Size(446, 402);
            this.ib_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ib_result.TabIndex = 2;
            this.ib_result.TabStop = false;
            // 
            // btn_CNN2
            // 
            this.btn_CNN2.Location = new System.Drawing.Point(44, 168);
            this.btn_CNN2.Name = "btn_CNN2";
            this.btn_CNN2.Size = new System.Drawing.Size(75, 23);
            this.btn_CNN2.TabIndex = 3;
            this.btn_CNN2.Text = "神经训练";
            this.btn_CNN2.UseVisualStyleBackColor = true;
            this.btn_CNN2.Click += new System.EventHandler(this.Btn_CNN2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txb_avg);
            this.groupBox1.Controls.Add(this.txb_percent);
            this.groupBox1.Controls.Add(this.btn_reg);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(159, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 142);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "识别";
            // 
            // txb_avg
            // 
            this.txb_avg.Location = new System.Drawing.Point(91, 58);
            this.txb_avg.Name = "txb_avg";
            this.txb_avg.Size = new System.Drawing.Size(100, 21);
            this.txb_avg.TabIndex = 6;
            this.txb_avg.Text = "187.5";
            // 
            // txb_percent
            // 
            this.txb_percent.Location = new System.Drawing.Point(91, 21);
            this.txb_percent.Name = "txb_percent";
            this.txb_percent.Size = new System.Drawing.Size(100, 21);
            this.txb_percent.TabIndex = 5;
            this.txb_percent.Text = "0.37";
            // 
            // btn_reg
            // 
            this.btn_reg.Location = new System.Drawing.Point(7, 101);
            this.btn_reg.Name = "btn_reg";
            this.btn_reg.Size = new System.Drawing.Size(75, 23);
            this.btn_reg.TabIndex = 4;
            this.btn_reg.Text = "识别";
            this.btn_reg.UseVisualStyleBackColor = true;
            this.btn_reg.Click += new System.EventHandler(this.Btn_reg_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "平均灰度：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "面积占比：";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(553, 477);
            this.trackBar1.Maximum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(188, 45);
            this.trackBar1.TabIndex = 5;
            // 
            // btn_ann2
            // 
            this.btn_ann2.Location = new System.Drawing.Point(6, 57);
            this.btn_ann2.Name = "btn_ann2";
            this.btn_ann2.Size = new System.Drawing.Size(75, 23);
            this.btn_ann2.TabIndex = 6;
            this.btn_ann2.Text = "神经训练2";
            this.btn_ann2.UseVisualStyleBackColor = true;
            this.btn_ann2.Click += new System.EventHandler(this.Btn_ann2_Click);
            // 
            // btn_ANNReg
            // 
            this.btn_ANNReg.Location = new System.Drawing.Point(112, 57);
            this.btn_ANNReg.Name = "btn_ANNReg";
            this.btn_ANNReg.Size = new System.Drawing.Size(75, 23);
            this.btn_ANNReg.TabIndex = 7;
            this.btn_ANNReg.Text = "识别2";
            this.btn_ANNReg.UseVisualStyleBackColor = true;
            this.btn_ANNReg.Click += new System.EventHandler(this.btn_ANNReg_Click);
            // 
            // grpANN
            // 
            this.grpANN.Controls.Add(this.ckbIsAct);
            this.grpANN.Controls.Add(this.txbregPath);
            this.grpANN.Controls.Add(this.btn_ann2);
            this.grpANN.Controls.Add(this.btn_ANNReg);
            this.grpANN.Location = new System.Drawing.Point(44, 425);
            this.grpANN.Name = "grpANN";
            this.grpANN.Size = new System.Drawing.Size(503, 179);
            this.grpANN.TabIndex = 8;
            this.grpANN.TabStop = false;
            this.grpANN.Text = "ANN识别";
            // 
            // txbregPath
            // 
            this.txbregPath.Location = new System.Drawing.Point(7, 152);
            this.txbregPath.Name = "txbregPath";
            this.txbregPath.Size = new System.Drawing.Size(490, 21);
            this.txbregPath.TabIndex = 8;
            // 
            // ckbIsAct
            // 
            this.ckbIsAct.AutoSize = true;
            this.ckbIsAct.Checked = true;
            this.ckbIsAct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbIsAct.Location = new System.Drawing.Point(7, 120);
            this.ckbIsAct.Name = "ckbIsAct";
            this.ckbIsAct.Size = new System.Drawing.Size(108, 16);
            this.ckbIsAct.TabIndex = 9;
            this.ckbIsAct.Text = "是否涂答文件夹";
            this.ckbIsAct.UseVisualStyleBackColor = true;
            // 
            // MachineTrainBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 694);
            this.Controls.Add(this.grpANN);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_CNN2);
            this.Controls.Add(this.ib_result);
            this.Controls.Add(this.bt_ANN);
            this.Controls.Add(this.bt_EM);
            this.Name = "MachineTrainBaseForm";
            this.Text = "机器训练基本算法";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MachineTrainBaseForm_FormClosing);
            this.Load += new System.EventHandler(this.MachineTrainBaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ib_result)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.grpANN.ResumeLayout(false);
            this.grpANN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_EM;
        private System.Windows.Forms.Button bt_ANN;
        private Emgu.CV.UI.ImageBox ib_result;
        private System.Windows.Forms.Button btn_CNN2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_reg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox txb_avg;
        private System.Windows.Forms.TextBox txb_percent;
        private System.Windows.Forms.Button btn_ann2;
        private System.Windows.Forms.Button btn_ANNReg;
        private System.Windows.Forms.GroupBox grpANN;
        private System.Windows.Forms.CheckBox ckbIsAct;
        private System.Windows.Forms.TextBox txbregPath;
    }
}