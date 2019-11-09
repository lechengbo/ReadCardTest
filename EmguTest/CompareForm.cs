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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public partial class CompareForm : Form
    {
        public CompareForm()
        {
            InitializeComponent();
        }

        private void Btn_load1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);

                this.picSrc1.LoadImage(bitmap);

            }
        }

        private void Btn_load2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);

                this.picSrc2.LoadImage(bitmap);

                var source1 = new Image<Bgr, byte>(bitmap).Mat;
                //Image<Bgr, byte> dest = new Image<Bgr, byte>(fileName);
                //CvInvoke.cvCopy(image, dest, IntPtr.Zero);
                //this.picSrc2.LoadImage(dest.ToBitmap());

            }

        }

        private void Btn_cut_Click(object sender, EventArgs e)
        {
            this.picCompare1.Image = new Image<Bgr, byte>( this.picSrc1.GetFirstRegionRect());
            this.picCompare2.Image = new Image<Bgr, byte>( this.picSrc2.GetFirstRegionRect());
        }

        private void Btn_compare_Click(object sender, EventArgs e)
        {
            //CvInvoke.im
            //var orignalBitmap = new Bitmap(this.picSrc1.Image);
            //bitmap = bitmap.Clone(new Rectangle(0,0,bitmap.Width,bitmap.Height), bitmap.PixelFormat);
            //Bitmap bitmap=new Bitmap(orignalBitmap.Width,orignalBitmap.Height);
            //OptimizePixelFormat(orignalBitmap, ref bitmap);

            var source1 = new Image<Bgr, byte>(this.picCompare1.Image.Bitmap).Mat;
            var source2 = new Image<Bgr, byte>(this.picCompare2.Image.Bitmap).Mat;

            //Mat gray1 = new Mat();
            //CvInvoke.CvtColor(source1, gray1, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            //Mat gray2 = new Mat();
            //CvInvoke.CvtColor(source2, gray2, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            //var result = CvInvoke.CompareHist(gray1, gray2, Emgu.CV.CvEnum.HistogramCompMethod.Correl);
            //Compare(source1, source2);
            this.lbl_result.Text = $"相似度：{Similar(ref source1,ref source2)}";
        }

        double Similar(ref Mat src, ref Mat src2)
        {
            Matrix<Byte> matrix = new Matrix<Byte>(src.Rows, src.Cols, src.NumberOfChannels);
            src.CopyTo(matrix);

            Mat gray1 = new Mat(),
                gray2 = new Mat();
            CvInvoke.CvtColor(src, gray1, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            CvInvoke.CvtColor(src2, gray2, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            var size = src.Size;
            CvInvoke.Resize(gray2, gray2, size);
            using (var scaledImg1 = gray1)
            using (var scaledImg2 = gray2)
            {
                int threshold = 230;
                CvInvoke.Threshold(scaledImg1, scaledImg1, threshold, 255, ThresholdType.Binary);
                CvInvoke.Threshold(scaledImg2, scaledImg2, threshold, 255, ThresholdType.Binary);

                //CvInvoke.CalcHist()
                Mat res = new Mat();
                CvInvoke.AbsDiff(scaledImg1, scaledImg2, res);

                CvInvoke.Imshow("第一张", scaledImg1);
                CvInvoke.Imshow("第二张", scaledImg2);
                //Mat mat1 = scaledImg1.Row(0);
                //Mat mat2 = scaledImg2.Col(0);
                //var r1 = CvInvoke.CompareHist(scaledImg1, scaledImg2, HistogramCompMethod.Correl);

                CvInvoke.Imshow("res", res);
                
               
                //var all = 1;// Convert.ToDouble( CvInvoke.Sum(scaledImg1));
                var sum1 = CvInvoke.Sum(scaledImg1);
                var sum = CvInvoke.Sum(res);
                var result = (1 - sum.V0 / sum1.V0);
                Console.WriteLine("result:" + result);
                return result;
            }

        }

        public void Compare(Mat mat1, Mat mat2) {
            CvInvoke.Resize(mat2, mat2, mat1.Size);
            CvInvoke.CvtColor(mat1, mat1, ColorConversion.Bgr2Gray);
            CvInvoke.CvtColor(mat2, mat2, ColorConversion.Bgr2Gray);

            //直方图尺寸设置
            //一个灰度值可以设定一个bins，256个灰度值就可以设定256个bins
            //对应HSV格式,构建二维直方图
            //每个维度的直方图灰度值划分为256块进行统计，也可以使用其他值
            int hBins = 256, sBins = 256;
            int[] histSize = { hBins };
            //H:0~180, S:0~255,V:0~255
            //H色调取值范围
            float[] hRanges = { 0, 180 };
            //S饱和度取值范围
            float[]  sRanges = { 180 };
            float[][] ranges = { new float[]{0,40 }, new float[] { 40, 80 }, new float[] { 40, 255 } };
            int[] channels = { 0 };//二维直方图
            Mat hist1=new Mat(), hist2=new Mat();
            CvInvoke.CalcHist( mat1 , channels, new Mat(), hist1, histSize, sRanges, false);
            CvInvoke.CalcHist(mat2, channels, new Mat(), hist2, histSize, sRanges, false);
            var result=CvInvoke.CompareHist(hist1, hist2, HistogramCompMethod.Correl);
            Console.WriteLine($"result:{result}");

        }

        private void CompareForm_Load(object sender, EventArgs e)
        {
            this.picSrc1.SetContainer(this.panel1);
            this.picSrc2.SetContainer(this.panel2);
        }


        public void SaveMat(Mat mat, string fileName)
        {
            string directoryPath = Application.StartupPath + "\\upload\\image\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            fileName = $"{directoryPath}{DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo)}{fileName}.jpg";

            mat.Save(fileName);

        }

        private void Btn_histShow_Click(object sender, EventArgs e)
        {
            //this.histogramBox1
        }

        private void CompareForm_Load_1(object sender, EventArgs e)
        {
            this.picSrc1.SetContainer(this.panel1);
            this.picSrc2.SetContainer(this.panel2);

        }
    }

    
    
}
