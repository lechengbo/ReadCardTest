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

namespace EmguTest
{
    public partial class PaperRegResultShowForm : Form
    {
        public Paper  Paper { get; set; }
        public Mat OriginalMat { get; set; }
        public PaperRegResultShowForm(Paper paper,Bitmap bitmap)
        {
            InitializeComponent();

            this.Paper = paper;
            this.OriginalMat = new Image<Bgr, byte>(bitmap).Mat;

            DrawPaperRect(this.OriginalMat,this.Paper);

            this.picSrc.SetContainer(this.panel1);
            this.picSrc.LoadImage(this.OriginalMat.Bitmap);

            //保存
            new CommonUse().SaveMat(this.OriginalMat, "试卷结果暂时");

        }

        public static void DrawPaperRect(Mat mat,Paper paper)
        {
            //添加试卷的矩形框
            //添加 定位点
            //CvInvoke.Rectangle(this.OriginalBitmap,)
            //this.picSrc.RegionInfo.RectList.Clear();
            CvInvoke.Rectangle(mat, paper.FixedPoint.LeftTop.Outer, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.LeftTop.Inner, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.RightTop.Outer, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.RightTop.Inner, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.LeftBottom.Outer, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.LeftBottom.Inner, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.RightBottom.Outer, new MCvScalar(0, 0, 255));
            CvInvoke.Rectangle(mat, paper.FixedPoint.RightBottom.Inner, new MCvScalar(0, 0, 255));
            //添加客观题区域
            paper.OptionAreaList.ForEach(a =>
            {
                CvInvoke.Rectangle(mat, a.Area, new MCvScalar(0, 0, 255));
                foreach (var item in a.Options.Values)
                {
                    item.ForEach(r =>
                    {
                        var tmpRect = new Rectangle(new Point(a.Area.X + r.X, a.Area.Y + r.Y), r.Size);
                        CvInvoke.Rectangle(mat, tmpRect, new MCvScalar(0, 0, 255));
                    });
                }

            });
        }

        private void PaperRegResultShowForm_Load(object sender, EventArgs e)
        {

        }

        private void Btn_showScan_Click(object sender, EventArgs e)
        {
            AnwserRegTest form = new AnwserRegTest(this.Paper);
            form.Show();
        }
    }
}
