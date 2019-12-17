namespace EmguTest
{
    partial class MachineTrainingFrom
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
            this.lb_tessPath = new System.Windows.Forms.Label();
            this.lb_samplePath = new System.Windows.Forms.Label();
            this.tb_tessPath = new System.Windows.Forms.TextBox();
            this.tb_samplePath = new System.Windows.Forms.TextBox();
            this.bt_tessPathChose = new System.Windows.Forms.Button();
            this.bt_samplePathChose = new System.Windows.Forms.Button();
            this.bt_refreshSamples = new System.Windows.Forms.Button();
            this.btn_productBox = new System.Windows.Forms.Button();
            this.lv_sapmoles = new System.Windows.Forms.ListView();
            this.cb_langs = new System.Windows.Forms.ComboBox();
            this.lb_lang = new System.Windows.Forms.Label();
            this.lv_tr = new System.Windows.Forms.ListView();
            this.bt_refreshTr = new System.Windows.Forms.Button();
            this.lv_box = new System.Windows.Forms.ListView();
            this.bt_refreshBox = new System.Windows.Forms.Button();
            this.bt_proTr = new System.Windows.Forms.Button();
            this.lv_trainResult = new System.Windows.Forms.ListView();
            this.bt_getChar = new System.Windows.Forms.Button();
            this.lb_trainLibName = new System.Windows.Forms.Label();
            this.tb_trainLibName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lb_tessPath
            // 
            this.lb_tessPath.AutoSize = true;
            this.lb_tessPath.Location = new System.Drawing.Point(13, 22);
            this.lb_tessPath.Name = "lb_tessPath";
            this.lb_tessPath.Size = new System.Drawing.Size(119, 12);
            this.lb_tessPath.TabIndex = 0;
            this.lb_tessPath.Text = "tesseract安装目录：";
            // 
            // lb_samplePath
            // 
            this.lb_samplePath.AutoSize = true;
            this.lb_samplePath.Location = new System.Drawing.Point(67, 67);
            this.lb_samplePath.Name = "lb_samplePath";
            this.lb_samplePath.Size = new System.Drawing.Size(65, 12);
            this.lb_samplePath.TabIndex = 1;
            this.lb_samplePath.Text = "样例目录：";
            // 
            // tb_tessPath
            // 
            this.tb_tessPath.Location = new System.Drawing.Point(164, 19);
            this.tb_tessPath.Name = "tb_tessPath";
            this.tb_tessPath.Size = new System.Drawing.Size(661, 21);
            this.tb_tessPath.TabIndex = 2;
            // 
            // tb_samplePath
            // 
            this.tb_samplePath.Location = new System.Drawing.Point(164, 57);
            this.tb_samplePath.Name = "tb_samplePath";
            this.tb_samplePath.Size = new System.Drawing.Size(661, 21);
            this.tb_samplePath.TabIndex = 3;
            // 
            // bt_tessPathChose
            // 
            this.bt_tessPathChose.Location = new System.Drawing.Point(864, 17);
            this.bt_tessPathChose.Name = "bt_tessPathChose";
            this.bt_tessPathChose.Size = new System.Drawing.Size(75, 23);
            this.bt_tessPathChose.TabIndex = 4;
            this.bt_tessPathChose.Text = "选择";
            this.bt_tessPathChose.UseVisualStyleBackColor = true;
            // 
            // bt_samplePathChose
            // 
            this.bt_samplePathChose.Location = new System.Drawing.Point(864, 59);
            this.bt_samplePathChose.Name = "bt_samplePathChose";
            this.bt_samplePathChose.Size = new System.Drawing.Size(75, 23);
            this.bt_samplePathChose.TabIndex = 5;
            this.bt_samplePathChose.Text = "选择";
            this.bt_samplePathChose.UseVisualStyleBackColor = true;
            this.bt_samplePathChose.Click += new System.EventHandler(this.Bt_samplePathChose_Click);
            // 
            // bt_refreshSamples
            // 
            this.bt_refreshSamples.Location = new System.Drawing.Point(38, 169);
            this.bt_refreshSamples.Name = "bt_refreshSamples";
            this.bt_refreshSamples.Size = new System.Drawing.Size(75, 23);
            this.bt_refreshSamples.TabIndex = 7;
            this.bt_refreshSamples.Text = "刷新样例";
            this.bt_refreshSamples.UseVisualStyleBackColor = true;
            this.bt_refreshSamples.Click += new System.EventHandler(this.Bt_refreshSamples_Click);
            // 
            // btn_productBox
            // 
            this.btn_productBox.Location = new System.Drawing.Point(153, 168);
            this.btn_productBox.Name = "btn_productBox";
            this.btn_productBox.Size = new System.Drawing.Size(75, 23);
            this.btn_productBox.TabIndex = 8;
            this.btn_productBox.Text = "生成BOX";
            this.btn_productBox.UseVisualStyleBackColor = true;
            this.btn_productBox.Click += new System.EventHandler(this.Btn_productBox_Click);
            // 
            // lv_sapmoles
            // 
            this.lv_sapmoles.GridLines = true;
            this.lv_sapmoles.HideSelection = false;
            this.lv_sapmoles.Location = new System.Drawing.Point(38, 198);
            this.lv_sapmoles.Name = "lv_sapmoles";
            this.lv_sapmoles.Size = new System.Drawing.Size(207, 594);
            this.lv_sapmoles.TabIndex = 9;
            this.lv_sapmoles.UseCompatibleStateImageBehavior = false;
            // 
            // cb_langs
            // 
            this.cb_langs.FormattingEnabled = true;
            this.cb_langs.Location = new System.Drawing.Point(97, 117);
            this.cb_langs.Name = "cb_langs";
            this.cb_langs.Size = new System.Drawing.Size(121, 20);
            this.cb_langs.TabIndex = 10;
            // 
            // lb_lang
            // 
            this.lb_lang.AutoSize = true;
            this.lb_lang.Location = new System.Drawing.Point(38, 120);
            this.lb_lang.Name = "lb_lang";
            this.lb_lang.Size = new System.Drawing.Size(53, 12);
            this.lb_lang.TabIndex = 11;
            this.lb_lang.Text = "训练库：";
            // 
            // lv_tr
            // 
            this.lv_tr.HideSelection = false;
            this.lv_tr.Location = new System.Drawing.Point(592, 198);
            this.lv_tr.Name = "lv_tr";
            this.lv_tr.Size = new System.Drawing.Size(231, 594);
            this.lv_tr.TabIndex = 12;
            this.lv_tr.UseCompatibleStateImageBehavior = false;
            // 
            // bt_refreshTr
            // 
            this.bt_refreshTr.Location = new System.Drawing.Point(592, 169);
            this.bt_refreshTr.Name = "bt_refreshTr";
            this.bt_refreshTr.Size = new System.Drawing.Size(75, 23);
            this.bt_refreshTr.TabIndex = 13;
            this.bt_refreshTr.Text = "刷新TR文件";
            this.bt_refreshTr.UseVisualStyleBackColor = true;
            this.bt_refreshTr.Click += new System.EventHandler(this.Bt_refreshTr_Click);
            // 
            // lv_box
            // 
            this.lv_box.HideSelection = false;
            this.lv_box.Location = new System.Drawing.Point(310, 198);
            this.lv_box.Name = "lv_box";
            this.lv_box.Size = new System.Drawing.Size(241, 594);
            this.lv_box.TabIndex = 14;
            this.lv_box.UseCompatibleStateImageBehavior = false;
            // 
            // bt_refreshBox
            // 
            this.bt_refreshBox.Location = new System.Drawing.Point(310, 168);
            this.bt_refreshBox.Name = "bt_refreshBox";
            this.bt_refreshBox.Size = new System.Drawing.Size(75, 23);
            this.bt_refreshBox.TabIndex = 15;
            this.bt_refreshBox.Text = "刷新Box文件";
            this.bt_refreshBox.UseVisualStyleBackColor = true;
            this.bt_refreshBox.Click += new System.EventHandler(this.Bt_refreshBox_Click);
            // 
            // bt_proTr
            // 
            this.bt_proTr.Location = new System.Drawing.Point(459, 169);
            this.bt_proTr.Name = "bt_proTr";
            this.bt_proTr.Size = new System.Drawing.Size(75, 23);
            this.bt_proTr.TabIndex = 16;
            this.bt_proTr.Text = "生成TR文件";
            this.bt_proTr.UseVisualStyleBackColor = true;
            this.bt_proTr.Click += new System.EventHandler(this.Bt_proTr_Click);
            // 
            // lv_trainResult
            // 
            this.lv_trainResult.HideSelection = false;
            this.lv_trainResult.Location = new System.Drawing.Point(864, 198);
            this.lv_trainResult.Name = "lv_trainResult";
            this.lv_trainResult.Size = new System.Drawing.Size(267, 405);
            this.lv_trainResult.TabIndex = 17;
            this.lv_trainResult.UseCompatibleStateImageBehavior = false;
            // 
            // bt_getChar
            // 
            this.bt_getChar.Location = new System.Drawing.Point(729, 131);
            this.bt_getChar.Name = "bt_getChar";
            this.bt_getChar.Size = new System.Drawing.Size(355, 61);
            this.bt_getChar.TabIndex = 18;
            this.bt_getChar.Text = "生成TR-提取字符-生成shap-生成聚集字符特征文件-合并所有tr文件";
            this.bt_getChar.UseVisualStyleBackColor = true;
            this.bt_getChar.Click += new System.EventHandler(this.Bt_getChar_Click);
            // 
            // lb_trainLibName
            // 
            this.lb_trainLibName.AutoSize = true;
            this.lb_trainLibName.Location = new System.Drawing.Point(258, 124);
            this.lb_trainLibName.Name = "lb_trainLibName";
            this.lb_trainLibName.Size = new System.Drawing.Size(77, 12);
            this.lb_trainLibName.TabIndex = 19;
            this.lb_trainLibName.Text = "训练库名称：";
            // 
            // tb_trainLibName
            // 
            this.tb_trainLibName.Location = new System.Drawing.Point(341, 120);
            this.tb_trainLibName.Name = "tb_trainLibName";
            this.tb_trainLibName.Size = new System.Drawing.Size(193, 21);
            this.tb_trainLibName.TabIndex = 20;
            // 
            // MachineTrainingFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 822);
            this.Controls.Add(this.tb_trainLibName);
            this.Controls.Add(this.lb_trainLibName);
            this.Controls.Add(this.bt_getChar);
            this.Controls.Add(this.lv_trainResult);
            this.Controls.Add(this.bt_proTr);
            this.Controls.Add(this.bt_refreshBox);
            this.Controls.Add(this.lv_box);
            this.Controls.Add(this.bt_refreshTr);
            this.Controls.Add(this.lv_tr);
            this.Controls.Add(this.lb_lang);
            this.Controls.Add(this.cb_langs);
            this.Controls.Add(this.lv_sapmoles);
            this.Controls.Add(this.btn_productBox);
            this.Controls.Add(this.bt_refreshSamples);
            this.Controls.Add(this.bt_samplePathChose);
            this.Controls.Add(this.bt_tessPathChose);
            this.Controls.Add(this.tb_samplePath);
            this.Controls.Add(this.tb_tessPath);
            this.Controls.Add(this.lb_samplePath);
            this.Controls.Add(this.lb_tessPath);
            this.Name = "MachineTrainingFrom";
            this.Text = "机器训练";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MachineTrainingFrom_FormClosing);
            this.Load += new System.EventHandler(this.MachineTrainingFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_tessPath;
        private System.Windows.Forms.Label lb_samplePath;
        private System.Windows.Forms.TextBox tb_tessPath;
        private System.Windows.Forms.TextBox tb_samplePath;
        private System.Windows.Forms.Button bt_tessPathChose;
        private System.Windows.Forms.Button bt_samplePathChose;
        private System.Windows.Forms.Button bt_refreshSamples;
        private System.Windows.Forms.Button btn_productBox;
        private System.Windows.Forms.ListView lv_sapmoles;
        private System.Windows.Forms.ComboBox cb_langs;
        private System.Windows.Forms.Label lb_lang;
        private System.Windows.Forms.ListView lv_tr;
        private System.Windows.Forms.Button bt_refreshTr;
        private System.Windows.Forms.ListView lv_box;
        private System.Windows.Forms.Button bt_refreshBox;
        private System.Windows.Forms.Button bt_proTr;
        private System.Windows.Forms.ListView lv_trainResult;
        private System.Windows.Forms.Button bt_getChar;
        private System.Windows.Forms.Label lb_trainLibName;
        private System.Windows.Forms.TextBox tb_trainLibName;
    }
}