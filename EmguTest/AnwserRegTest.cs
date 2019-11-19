using Emgu.CV;
using Emgu.CV.Structure;
using EmguTest.Aggregation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yue_juan_care.customerControl;

namespace EmguTest
{
    public partial class AnwserRegTest : Form
    {
        private CommonUse common = new CommonUse();

        public Bitmap OriginalBitmap { get; set; }
        public Paper ValidatedPaper { get; set; }
        public Paper OriginalPaper { get; set; }
        public Mat OriginalMat { get; set; }


        public AnwserRegTest(Paper paper)
        {
            InitializeComponent();
            this.OriginalPaper = paper;
        }

        private void Btn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "所有图片文件|*.png;*.jpg;*.jpeg|所有文件 (*.*)|*.*"; 
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);
                this.OriginalBitmap = (Bitmap)bitmap.Clone();

                this.picSrc.SetContainer(this.panel1);

                Btn_recover_Click(null, null);
            }
            
        }

        public void CalcOffset(Rectangle originalRect,Rectangle newRect,out int offsetX,out int offsetY)
        {
            offsetX = newRect.X - originalRect.X;
            offsetY = newRect.Y - originalRect.Y;
        }

        private void Btn_validate_Click(object sender, EventArgs e)
        {
            //思路：重新调整大小（根据四个定位点），获取扫描结果的定位点，获取答案结果

            //重新获取扫描结果的定位点
            var leftTopArea = this.OriginalPaper.FixedPoint.LeftTop.GetEnlargeOuter();
            var cutBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftTopArea.X, leftTopArea.Y, leftTopArea.Width, leftTopArea.Height);
            var leftTopInner = common.GetBigRectFromBitmap(cutBitmap, 2000, 6000, leftTopArea.X, leftTopArea.Y);
            CalcOffset(this.OriginalPaper.FixedPoint.LeftTop.Inner, leftTopInner, out int offsetX, out int offsetY);
            var scanPaper = this.OriginalPaper.NewPaperByOffset(offsetX, offsetY);


            var mat = new Image<Bgr, byte>(this.OriginalBitmap).Mat;
            PaperRegResultShowForm.DrawPaperRect(mat, scanPaper);

           
            this.picSrc.LoadImage(mat.Bitmap);

            //保存
            common.SaveMat(mat, "试卷结果校验后在扫描模板中显示");

        }

        private void Btn_recover_Click(object sender, EventArgs e)
        {
            var mat = new Image<Bgr, byte>(this.OriginalBitmap).Mat;



            PaperRegResultShowForm.DrawPaperRect(mat, this.OriginalPaper);

            
            this.picSrc.LoadImage(mat.Bitmap);

            //保存
            common.SaveMat(mat, "试卷结果在扫描模板中显示");
        }

        private void Btn_validate2_Click(object sender, EventArgs e)
        {
            //思路获取偏移量，
            //重新获取扫描结果的定位点
            var leftTopArea = this.OriginalPaper.FixedPoint.LeftTop.GetEnlargeOuter();
            var cutBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftTopArea.X, leftTopArea.Y, leftTopArea.Width, leftTopArea.Height);
            var leftTopInner = common.GetBigRectFromBitmap(cutBitmap, 2000, 6000, leftTopArea.X, leftTopArea.Y);
            //计算偏移量
            CalcOffset(this.OriginalPaper.FixedPoint.LeftTop.Inner, leftTopInner, out int offsetX, out int offsetY);
        }
    }
}
