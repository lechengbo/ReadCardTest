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
            this.SuspendLayout();
            // 
            // CompareForm
            // 
            this.ClientSize = new System.Drawing.Size(974, 680);
            this.Name = "CompareForm";
            this.ResumeLayout(false);

        }

        #endregion

        private yue_juan_care.customerControl.PictureBoxReadCard picSrc1;
        private yue_juan_care.customerControl.PictureBoxReadCard picSrc2;
        private System.Windows.Forms.Button btn_load1;
        private System.Windows.Forms.Button btn_load2;
        private System.Windows.Forms.Button btn_cut;
        private System.Windows.Forms.Button btn_compare;
        private System.Windows.Forms.Label lbl_result;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Emgu.CV.UI.ImageBox picCompare1;
        private Emgu.CV.UI.ImageBox picCompare2;
        private Emgu.CV.UI.HistogramBox histogramBox1;
        private Emgu.CV.UI.HistogramBox histogramBox2;
        private System.Windows.Forms.Button btn_histShow;
    }
}