using Emgu.CV;
using Emgu.CV.CvEnum;
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

        public void CalcOffset(Rectangle originalRect, Rectangle newRect, out int offsetX, out int offsetY)
        {
            offsetX = newRect.X - originalRect.X;
            offsetY = newRect.Y - originalRect.Y;
        }

        private void Btn_validate_Click(object sender, EventArgs e)
        {
            //思路：获取扫描结果的左上角定位点，-》计算偏移量-》整体偏移-》获取答案结果-》是否根据偏移块进行详细偏移

            //重新获取扫描结果的定位点
            var leftTopArea = this.OriginalPaper.FixedPoint.LeftTop.GetEnlargeOuter();
            var cutBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftTopArea.X, leftTopArea.Y, leftTopArea.Width, leftTopArea.Height);
            var leftTopInner = common.GetBigRectFromBitmap(cutBitmap, 2000, 6000, leftTopArea.X, leftTopArea.Y);
            CalcOffset(this.OriginalPaper.FixedPoint.LeftTop.Inner, leftTopInner, out int offsetX, out int offsetY);
            var scanPaper = this.OriginalPaper.NewPaperByOffset(offsetX, offsetY);

            //根据偏移块进行调整
            scanPaper = CalPaperByOffsetS(scanPaper,this.OriginalBitmap);

            var mat = new Image<Bgr, byte>(this.OriginalBitmap).Mat;
            PaperRegResultShowForm.DrawPaperRect(mat, scanPaper);


            this.picSrc.LoadImage(mat.Bitmap);

            //保存
            string calOffsetStr = this.IsCalOffset.Checked ? "并模块偏移" : "";
            common.SaveMat(mat, $"试卷结果校验后{ calOffsetStr }在扫描模板中显示");

        }

        /// <summary>
        /// 根据偏移块，构造新的
        /// </summary>
        /// <param name="paper"></param>
        /// <returns></returns>
        private Paper CalPaperByOffsetS(Paper paper,Bitmap bitmap)
        {
            

            if (this.IsCalOffset.Checked || this.IsCalOffsetRow.Checked)
            {
                var newOffsetAreaList = new List<OffsetArea>();
                paper.OffsetAreas.ForEach(offsetArea =>
                {

                    var offsetCutBitmap = PictureBoxReadCard.Cut(bitmap, offsetArea.Area);
                    var rectlist = common.GetRectListFromBitmap(offsetCutBitmap, 200, 2000, 0, 0, false, 0);
                    newOffsetAreaList.Add(new OffsetArea(offsetArea.Area, rectlist, offsetArea.OffsetType));


                });

                if (!this.IsCalOffsetRow.Checked)
                {
                    var result = newOffsetAreaList.RemoveAll(a => a.OffsetType == OffsetType.Rows);
                    newOffsetAreaList.AddRange(paper.OffsetAreas.Where(a => a.OffsetType == OffsetType.Rows));
                }
                if (!this.IsCalOffset.Checked)
                {
                    var result = newOffsetAreaList.RemoveAll(a => a.OffsetType == OffsetType.Column);
                    newOffsetAreaList.AddRange(paper.OffsetAreas.Where(a => a.OffsetType == OffsetType.Column));
                }
                //检查数量是否相同
                var newCount = 0;
                var originalCount = 0;
                newOffsetAreaList.ForEach(a =>
                {
                    newCount += a.OffsetList.Count;
                });
                paper.OffsetAreas.ForEach(a =>
                {
                    originalCount += a.OffsetList.Count;
                });

                if (newCount == originalCount)
                {
                    paper = paper.NewPaperByOffsetS(newOffsetAreaList);

                }
                else
                {
                    MessageBox.Show("检查到的偏移定位块数量不等，将不会根据定位块进行调整偏移量");
                }
            }
            

            return paper;
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
            List<PointF> srcPoints = this.OriginalPaper.FixedPoint.GetPointsByClockwise();
            List<PointF> desPoints = new List<PointF>();
            //重新获取扫描结果的全部定位点-》重新调整大小（根据四个定位点透视）
            //左上
            var leftTopArea = this.OriginalPaper.FixedPoint.LeftTop.GetEnlargeOuter();
            var cutBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftTopArea.X, leftTopArea.Y, leftTopArea.Width, leftTopArea.Height);
            var leftTopInner = common.GetBigRectFromBitmap(cutBitmap, 1000, 6000, leftTopArea.X, leftTopArea.Y);
            desPoints.Add(leftTopInner.Location);
            //右上
            var rightTopArea = this.OriginalPaper.FixedPoint.RightTop.GetEnlargeOuter();
            var rightTopBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, rightTopArea);
            var rightTopInner = common.GetBigRectFromBitmap(rightTopBitmap, 1000, 6000, rightTopArea.X, rightTopArea.Y);
            desPoints.Add(CommonUse.PointToPointF(rightTopInner.Location));
            //右下
            var rightBottomArea = this.OriginalPaper.FixedPoint.RightBottom.GetEnlargeOuter();
            var rightBottomBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, rightBottomArea);
            var rightBottomInner = common.GetBigRectFromBitmap(rightBottomBitmap, 1000, 6000, rightBottomArea.X, rightBottomArea.Y);
            desPoints.Add(CommonUse.PointToPointF(rightBottomInner.Location));
            //左下
            var leftBottomArea = this.OriginalPaper.FixedPoint.LeftBottom.GetEnlargeOuter();
            var leftBottomBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftBottomArea);
            var leftBottomInner = common.GetBigRectFromBitmap(leftBottomBitmap, 1000, 6000, leftBottomArea.X, leftBottomArea.Y);
            desPoints.Add(CommonUse.PointToPointF(leftBottomInner.Location));



            //计算透视矩阵
            Mat data = CvInvoke.GetPerspectiveTransform(desPoints.ToArray(), srcPoints.ToArray());
            //进行透视操作
            var mat = new Image<Bgr, byte>(this.OriginalBitmap).Mat;
            Mat mat_Perspective = new Mat();
            CvInvoke.WarpPerspective(mat, mat_Perspective, data, this.OriginalBitmap.Size,Inter.Nearest,Warp.Default,BorderType.Constant,new MCvScalar(255,255,255));
            
            //根据偏移块进行调整
            var paper = CalPaperByOffsetS(this.OriginalPaper, mat_Perspective.Bitmap);

            PaperRegResultShowForm.DrawPaperRect(mat_Perspective, paper);


            this.picSrc.LoadImage(mat_Perspective.Bitmap);


            //保存
            string calOffsetStr = this.IsCalOffset.Checked ? "并模块偏移" : "";
            common.SaveMat(mat_Perspective, $"试卷结果直接透视后{calOffsetStr}在扫描模板中显示");

        }

        private void Btn_regWrap_Click(object sender, EventArgs e)
        {
            //思路：重新调整大小（根据四个定位点），获取扫描结果的定位点，获取答案结果

            //重新获取扫描结果的定位点
            var leftTopArea = this.OriginalPaper.FixedPoint.LeftTop.GetEnlargeOuter();
            var cutBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftTopArea.X, leftTopArea.Y, leftTopArea.Width, leftTopArea.Height);
            var leftTopInner = common.GetBigRectFromBitmap(cutBitmap, 2000, 6000, leftTopArea.X, leftTopArea.Y);
            CalcOffset(this.OriginalPaper.FixedPoint.LeftTop.Inner, leftTopInner, out int offsetX, out int offsetY);
            var scanPaper = this.OriginalPaper.NewPaperByOffset(offsetX, offsetY);


            var mat = new Image<Bgr, byte>(this.OriginalBitmap).Mat;
            //通过透视调整大小
            List<PointF> srcPoints = scanPaper.FixedPoint.GetPointsByClockwise();
            List<PointF> desPoints = new List<PointF>() { CommonUse.PointToPointF(scanPaper.FixedPoint.LeftTop.Inner.Location) };
            //右上
            var rightTopArea = scanPaper.FixedPoint.RightTop.Outer;
            var rightTopBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, rightTopArea);
            var rightTopInner = common.GetBigRectFromBitmap(rightTopBitmap, 2000, 6000, rightTopArea.X, rightTopArea.Y);
            desPoints.Add(CommonUse.PointToPointF(rightTopInner.Location));
            //右下
            var rightBottomArea = scanPaper.FixedPoint.RightBottom.Outer;
            var rightBottomBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, rightBottomArea);
            var rightBottomInner = common.GetBigRectFromBitmap(rightBottomBitmap, 2000, 6000, rightBottomArea.X, rightBottomArea.Y);
            desPoints.Add(CommonUse.PointToPointF(rightBottomInner.Location));
            //左下
            var leftBottomArea = scanPaper.FixedPoint.LeftBottom.Outer;
            var leftBottomBitmap = PictureBoxReadCard.Cut(this.OriginalBitmap, leftBottomArea);
            var leftBottomInner = common.GetBigRectFromBitmap(leftBottomBitmap, 2000, 6000, leftBottomArea.X, leftBottomArea.Y);
            desPoints.Add(CommonUse.PointToPointF(leftBottomInner.Location));

            //计算透视矩阵
            Mat data = CvInvoke.GetPerspectiveTransform(desPoints.ToArray(), srcPoints.ToArray());
            //进行透视操作
            //Mat src_gray = new Mat();
            Mat mat_Perspective = new Mat();
            CvInvoke.WarpPerspective(mat, mat_Perspective, data, this.OriginalBitmap.Size);
            
            //根据偏移块进行调整
            scanPaper = CalPaperByOffsetS(scanPaper,mat_Perspective.Bitmap);

            PaperRegResultShowForm.DrawPaperRect(mat_Perspective, scanPaper);


            this.picSrc.LoadImage(mat_Perspective.Bitmap);

            //保存
            string calOffsetStr = this.IsCalOffset.Checked ? "并模块偏移" : "";
            common.SaveMat(mat_Perspective, $"试卷结果校正透视后{calOffsetStr}在扫描模板中显示");
        }
    }
}
