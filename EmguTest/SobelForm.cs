using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
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
    public partial class SobelForm : Form
    {
        public SobelForm()
        {
            InitializeComponent();
        }

        private void Bt_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);

                this.pic_src.LoadImage(bitmap);

            }
        }

        private void SobelForm_Load(object sender, EventArgs e)
        {
            this.pic_src.SetContainer(this.panel1);
        }

        private void Btn_soble_Click(object sender, EventArgs e)
        {
            var bitmap = this.pic_src.GetFirstRegionRect();
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);

            Mat matX = new Mat(image.Size, DepthType.Cv64F, 1);
            Mat matY = new Mat(image.Size, DepthType.Cv64F, 1);

            CvInvoke.Sobel(image, matX, DepthType.Cv64F, 1, 0);
            CvInvoke.ConvertScaleAbs(matX, matX, 1, 0);
            CvInvoke.Sobel(image, matY, DepthType.Cv64F, 0, 1);
            CvInvoke.ConvertScaleAbs(matY, matY, 1, 0);
            
            this.ibX.Image = matX;
            this.ibY.Image = matY;
            //CvInvoke.Scharr
        }

        private void Btn_Scharr_Click(object sender, EventArgs e)
        {
            var bitmap = this.pic_src.GetFirstRegionRect();
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);

            Mat matX = new Mat(image.Size, DepthType.Cv64F, 1);
            Mat matY = new Mat(image.Size, DepthType.Cv64F, 1);

            CvInvoke.Scharr(image, matX, DepthType.Cv64F, 1, 0);
            CvInvoke.ConvertScaleAbs(matX, matX, 1, 0);
            CvInvoke.Scharr(image, matY, DepthType.Cv64F, 0, 1);
            CvInvoke.ConvertScaleAbs(matY, matY, 1, 0);
            this.ibX.Image = matX;
            this.ibY.Image = matY;
        }

        private void Bt_filter_Click(object sender, EventArgs e)
        {
            var bitmap = this.pic_src.GetFirstRegionRect();
            float[,] arr = new float[3, 3] { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
            //
            //    |-1 0 1| |-2 0 2| |-1 0 1|
            float[,] arr1 = new float[3, 3] { { -1 ,0 ,1 }, { -2, 0, 2 }, { -1, 0, 1 } };

            Matrix<float> kernel = new Matrix<float>(arr1);

            var image = new Image<Gray, byte>(bitmap);
            Mat matX = new Mat(image.Size, DepthType.Cv64F, 1);
            CvInvoke.Filter2D(image, matX, kernel, new Point(-1, -1));
            CvInvoke.ConvertScaleAbs(matX, matX, 1, 0);

            this.ibX.Image = matX;
        }
    }
}
