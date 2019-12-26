using Emgu.CV.ML;
using Emgu.CV.OCR;
using EmguTest.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public partial class OCRForm : Form
    {
        public OCRForm()
        {
            InitializeComponent();
        }

        private void OCRForm_Load(object sender, EventArgs e)
        {
            this.picSrc.SetContainer(this.panel1);
        }

        private void Btn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "所有图片文件|*.png;*.jpg;*.jpeg|所有文件 (*.*)|*.*"; ;
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);

                this.picSrc.LoadImage(bitmap);

            }
        }

        private void Btn_ocr_Click(object sender, EventArgs e)
        {
            var text = OCRHelper.Ocr(this.picSrc.GetFirstRegionRect());
            this.tb_ocrResult.Text = text;
        }

        private void Btn_jtyReg_Click(object sender, EventArgs e)
        {
            var text = OCRHelper.OcrJty(this.picSrc.GetFirstRegionRect());
            this.tb_ocrResult.Text = text;
            //OcrInvoke
            SVM sVM = new SVM();
            
           
        }
    }
}
