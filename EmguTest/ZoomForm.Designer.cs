using Emgu.CV.UI;
using System.Windows.Forms;

namespace EmguTest
{
    partial class ZoomForm
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
            this.panelImage = new System.Windows.Forms.Panel();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btn_loadPic = new System.Windows.Forms.Button();
            this.btn_showPicBoxLocation = new System.Windows.Forms.Button();
            this.btn_getPanelAutoInfo = new System.Windows.Forms.Button();
            this.btn_panelDragToggle = new System.Windows.Forms.Button();
            this.btn_rotate = new System.Windows.Forms.Button();
            this.num_angle = new System.Windows.Forms.NumericUpDown();
            this.btn_savePictureBoxImg = new System.Windows.Forms.Button();
            this.btn_orc = new System.Windows.Forms.Button();
            this.btn_imShow = new System.Windows.Forms.Button();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_angle)).BeginInit();
            this.SuspendLayout();
            // 
            // panelImage
            // 
            this.panelImage.AutoScroll = true;
            this.panelImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImage.Controls.Add(this.picBox);
            this.panelImage.Location = new System.Drawing.Point(300, 39);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(807, 670);
            this.panelImage.TabIndex = 0;
            this.panelImage.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelImage_Paint);
            this.panelImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelImage_MouseDown);
            this.panelImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelImage_MouseMove);
            // 
            // picBox
            // 
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(134, 68);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(505, 487);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PicBox_Paint);
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseMove);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseUp);
            // 
            // btn_loadPic
            // 
            this.btn_loadPic.Location = new System.Drawing.Point(47, 39);
            this.btn_loadPic.Name = "btn_loadPic";
            this.btn_loadPic.Size = new System.Drawing.Size(75, 23);
            this.btn_loadPic.TabIndex = 1;
            this.btn_loadPic.Text = "载入图片";
            this.btn_loadPic.UseVisualStyleBackColor = true;
            this.btn_loadPic.Click += new System.EventHandler(this.Btn_loadPic_Click);
            // 
            // btn_showPicBoxLocation
            // 
            this.btn_showPicBoxLocation.Location = new System.Drawing.Point(47, 81);
            this.btn_showPicBoxLocation.Name = "btn_showPicBoxLocation";
            this.btn_showPicBoxLocation.Size = new System.Drawing.Size(75, 23);
            this.btn_showPicBoxLocation.TabIndex = 2;
            this.btn_showPicBoxLocation.Text = "显示picBox位置信息";
            this.btn_showPicBoxLocation.UseVisualStyleBackColor = true;
            this.btn_showPicBoxLocation.Click += new System.EventHandler(this.Btn_showPicBoxLocation_Click);
            // 
            // btn_getPanelAutoInfo
            // 
            this.btn_getPanelAutoInfo.Location = new System.Drawing.Point(47, 139);
            this.btn_getPanelAutoInfo.Name = "btn_getPanelAutoInfo";
            this.btn_getPanelAutoInfo.Size = new System.Drawing.Size(75, 54);
            this.btn_getPanelAutoInfo.TabIndex = 3;
            this.btn_getPanelAutoInfo.Text = "获取panel的scroll信息";
            this.btn_getPanelAutoInfo.UseVisualStyleBackColor = true;
            this.btn_getPanelAutoInfo.Click += new System.EventHandler(this.Btn_getPanelAutoInfo_Click);
            // 
            // btn_panelDragToggle
            // 
            this.btn_panelDragToggle.Location = new System.Drawing.Point(47, 241);
            this.btn_panelDragToggle.Name = "btn_panelDragToggle";
            this.btn_panelDragToggle.Size = new System.Drawing.Size(75, 23);
            this.btn_panelDragToggle.TabIndex = 4;
            this.btn_panelDragToggle.Text = "开启拖动";
            this.btn_panelDragToggle.UseVisualStyleBackColor = true;
            this.btn_panelDragToggle.Click += new System.EventHandler(this.Btn_panelDragToggle_Click);
            // 
            // btn_rotate
            // 
            this.btn_rotate.Location = new System.Drawing.Point(47, 290);
            this.btn_rotate.Name = "btn_rotate";
            this.btn_rotate.Size = new System.Drawing.Size(75, 23);
            this.btn_rotate.TabIndex = 5;
            this.btn_rotate.Text = "旋转";
            this.btn_rotate.UseVisualStyleBackColor = true;
            this.btn_rotate.Click += new System.EventHandler(this.Btn_rotate_Click);
            // 
            // num_angle
            // 
            this.num_angle.Location = new System.Drawing.Point(140, 290);
            this.num_angle.Name = "num_angle";
            this.num_angle.Size = new System.Drawing.Size(88, 21);
            this.num_angle.TabIndex = 6;
            this.num_angle.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // btn_savePictureBoxImg
            // 
            this.btn_savePictureBoxImg.Location = new System.Drawing.Point(47, 337);
            this.btn_savePictureBoxImg.Name = "btn_savePictureBoxImg";
            this.btn_savePictureBoxImg.Size = new System.Drawing.Size(75, 23);
            this.btn_savePictureBoxImg.TabIndex = 7;
            this.btn_savePictureBoxImg.Text = "保存";
            this.btn_savePictureBoxImg.UseVisualStyleBackColor = true;
            this.btn_savePictureBoxImg.Click += new System.EventHandler(this.Btn_savePictureBoxImg_Click);
            // 
            // btn_orc
            // 
            this.btn_orc.Location = new System.Drawing.Point(47, 391);
            this.btn_orc.Name = "btn_orc";
            this.btn_orc.Size = new System.Drawing.Size(75, 23);
            this.btn_orc.TabIndex = 8;
            this.btn_orc.Text = "文字识别";
            this.btn_orc.UseVisualStyleBackColor = true;
            this.btn_orc.Click += new System.EventHandler(this.Btn_orc_Click);
            // 
            // btn_imShow
            // 
            this.btn_imShow.Location = new System.Drawing.Point(47, 439);
            this.btn_imShow.Name = "btn_imShow";
            this.btn_imShow.Size = new System.Drawing.Size(75, 23);
            this.btn_imShow.TabIndex = 9;
            this.btn_imShow.Text = "ImShow";
            this.btn_imShow.UseVisualStyleBackColor = true;
            this.btn_imShow.Click += new System.EventHandler(this.Btn_imShow_Click);
            // 
            // ZoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 779);
            this.Controls.Add(this.btn_imShow);
            this.Controls.Add(this.btn_orc);
            this.Controls.Add(this.btn_savePictureBoxImg);
            this.Controls.Add(this.num_angle);
            this.Controls.Add(this.btn_rotate);
            this.Controls.Add(this.btn_panelDragToggle);
            this.Controls.Add(this.btn_getPanelAutoInfo);
            this.Controls.Add(this.btn_showPicBoxLocation);
            this.Controls.Add(this.btn_loadPic);
            this.Controls.Add(this.panelImage);
            this.KeyPreview = true;
            this.Name = "ZoomForm";
            this.Text = "ZoomForm";
            this.Load += new System.EventHandler(this.ZoomForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZoomForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ZoomForm_KeyUp);
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_angle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelImage;
        private PictureBox picBox;
        private System.Windows.Forms.Button btn_loadPic;
        private System.Windows.Forms.Button btn_showPicBoxLocation;
        private System.Windows.Forms.Button btn_getPanelAutoInfo;
        private System.Windows.Forms.Button btn_panelDragToggle;
        private System.Windows.Forms.Button btn_rotate;
        private System.Windows.Forms.NumericUpDown num_angle;
        private System.Windows.Forms.Button btn_savePictureBoxImg;
        private Button btn_orc;
        private Button btn_imShow;
    }
}