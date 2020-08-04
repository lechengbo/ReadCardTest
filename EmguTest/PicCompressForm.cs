using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public partial class PicCompressForm : Form
    {
        public PicCompressForm()
        {
            InitializeComponent();
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

        private void PicCompressForm_Load(object sender, EventArgs e)
        {
            this.picSrc.SetContainer(this.panel1);
        }

        private void Btn_compress_Click(object sender, EventArgs e)
        {
            var bitmap = (Bitmap)this.picSrc.orignalBitmap.Clone();
            bitmap.SetResolution(96f, 96f);
            SaveBitmap(bitmap, "直接修改dpi");
        }

        private void Compress1(object sender, EventArgs e)
        {
            var bitmap = (Bitmap)this.picSrc.orignalBitmap.Clone();
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            newBitmap.SetResolution(96f, 96f);
            Graphics g = Graphics.FromImage(newBitmap);
            g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            SaveBitmap(bitmap, "Graphics方式");

            Matrix<Byte> matrix = new Matrix<Byte>(3, 3, 3);
            
        }
        private void Compress2(object sender, EventArgs e)
        {
            var bitmap = (Bitmap)this.picSrc.orignalBitmap.Clone();
            var image = new Image<Bgr, byte>(bitmap);
            
            var newMat = new Mat();
            //CvInvoke.Resize(image, newMat, new Size(bitmap.Width, bitmap.Height));
            CvInvoke.CvtColor(image, newMat, ColorConversion.Bgr2Gray,1);
            new CVHelper().SaveMat(image.Mat, "通过resize");
            new CVHelper().SaveMat(newMat, "通过resize并灰度");
        }

        public void SaveBitmap(Bitmap bitmap,string fileName)
        {
            string directoryPath = Application.StartupPath + "\\compress\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            fileName = $"{directoryPath}{DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo)}{fileName}.jpg";

            bitmap.Save(fileName);

        }
    }
}
