namespace EmguTest
{
    partial class FormPicOperate
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
            this.btnmulop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnmulop
            // 
            this.btnmulop.Location = new System.Drawing.Point(71, 104);
            this.btnmulop.Name = "btnmulop";
            this.btnmulop.Size = new System.Drawing.Size(75, 23);
            this.btnmulop.TabIndex = 0;
            this.btnmulop.Text = "批量处理";
            this.btnmulop.UseVisualStyleBackColor = true;
            this.btnmulop.Click += new System.EventHandler(this.btnmulop_Click);
            // 
            // FormPicOperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 620);
            this.Controls.Add(this.btnmulop);
            this.Name = "FormPicOperate";
            this.Text = "FormPicOperate";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnmulop;
    }
}