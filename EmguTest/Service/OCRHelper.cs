using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest.Service
{
    public class OCRHelper
    {
        private static Tesseract tesseract = new Tesseract(Path.Combine(System.Windows.Forms.Application.StartupPath, "tessdata"), "chi_sim", OcrEngineMode.TesseractLstmCombined);
        private static Tesseract jtyTesseract = new Tesseract(Path.Combine(System.Windows.Forms.Application.StartupPath, "tessdata"), "jtycard", OcrEngineMode.TesseractLstmCombined);
        public static string Ocr(Bitmap bitmap)
        {
            using (Image<Bgr, byte> bgr = new Image<Bgr, byte>((Bitmap)bitmap.Clone()))
            using (Mat image = bgr.Mat)
            {
                var dest = new Mat();
                if (image.NumberOfChannels == 1)
                    CvInvoke.CvtColor(image, dest, ColorConversion.Gray2Bgr);
                else
                    image.CopyTo(dest);
                tesseract.SetImage(dest);
                if (tesseract.Recognize() != 0) return null;
                return tesseract.GetUTF8Text();

            }
        }
        public static string OcrJty(Bitmap bitmap)
        {
            using (Image<Bgr, byte> bgr = new Image<Bgr, byte>((Bitmap)bitmap.Clone()))
            using (Mat image = bgr.Mat)
            {
                var dest = new Mat();
                if (image.NumberOfChannels == 1)
                    CvInvoke.CvtColor(image, dest, ColorConversion.Gray2Bgr);
                else
                    image.CopyTo(dest);
                jtyTesseract.SetImage(dest);
                if (jtyTesseract.Recognize() != 0) return null;
                return jtyTesseract.GetUTF8Text();

            }
        }
        public static string Ocr(Mat mat)
        {
            tesseract.SetImage(mat);
            if (tesseract.Recognize() != 0) return null;
            return tesseract.GetUTF8Text();
        }
    }
}
