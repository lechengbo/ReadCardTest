using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
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
    public partial class Form1 : Form
    {
        Point start; //画框的起始点
        Point end;//画框的结束点<br>
        bool blnDraw;//判断是否绘制<br>
        Rectangle rect;//pictureBox控件的矩形位置
        Rectangle destRect;//pictureBox中图片的矩形

        public static List<Rectangle> OrginalRectList { get; set; }
        public static List<Rectangle> CutedRectList { get; set; }

        private string fileName = "";
        public Form1()
        {
            InitializeComponent();
            this.ib_original.MouseWheel += (object sender, MouseEventArgs e) =>
            {
                this.ResizePicBox((PictureBox)sender, e.Delta);

            };
        }

        public void Start()
        {
            CommonUse commonUse = new CommonUse();
            if (ib_original.Image != null)
            {
                //Mat src = new Image<Bgr, byte>(ib_original.Image.Bitmap).Mat;
                Mat src = ib_original.Image.GetInputArray().GetMat();
                //1.获取当前图像的最大矩形边界
                VectorOfVectorOfPoint max_contour = commonUse.GetBoundaryOfPic(src);

                //2.对图像进行矫正
                Mat mat_Perspective = commonUse.MyWarpPerspective(src, max_contour);
                //规范图像大小
                CvInvoke.Resize(mat_Perspective, mat_Perspective, new Size(590, 384), 0, 0, Emgu.CV.CvEnum.Inter.Cubic);
                //3.二值化处理（大于阈值取0，小于阈值取255。其中白色为0，黑色为255)
                Mat mat_threshold = new Mat();
                int myThreshold = Convert.ToInt32(num_threshold.Value);
                CvInvoke.Threshold(mat_Perspective, mat_threshold, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
                //ib_middle.Image = mat_threshold;

                //形态学膨胀
                Mat mat_dilate = commonUse.MyDilate(mat_threshold);
                //ib_middle.Image = mat_dilate;

                //筛选长宽比大于2的轮廓
                VectorOfVectorOfPoint selected_contours = commonUse.GetUsefulContours(mat_dilate, 1);
                //画出轮廓
                Mat color_mat = commonUse.DrawContours(mat_Perspective, selected_contours);
                ib_middle.Image = color_mat;


                ib_result.Image = mat_Perspective;
                //准考证号，x=230+26*5,y=40+17*10
                //tb_log.Text = commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 230, 26, 5, 40, 17, 10, "准考证号：");

                ////答题区1-5题，x=8+25*5,y=230+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 8, 25, 5, 230, 16, 4, "1-5：");

                ////答题区6-10题,x=159+25*5,y=230+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 159, 25, 5, 230, 16, 4, "6-10：");

                ////答题区11-15题,x=310+25*5,y=230+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 310, 25, 5, 230, 16, 4, "11-15：");

                ////答题区16-20题,x=461+25*5,y=230+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 461, 25, 5, 230, 16, 4, "16-20：");

                ////答题区21-25题,x=8+25*5,y=312+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 8, 25, 5, 312, 16, 4, "21-25：");

                ////答题区26-30题,x=159+25*5,y=312+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 159, 25, 5, 312, 16, 4, "26-30：");

                ////答题区31-35题,x=310+25*5,y=312+16*4
                //tb_log.Text += commonUse.GetValueAndDrawGrid(ib_result, selected_contours, 310, 25, 5, 312, 16, 4, "31-35：");
            }
            else
            {
                MessageBox.Show("请先加载图片");
            }
        }

        private void BtLoad_Click(object sender, EventArgs e)
        {
            //this.ib_original.Image=new InputArray()
            //this.ib_original.Load(@"./source/card.png");

            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                fileName = op.FileName;
                Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                ResizePictureBoxSizeByBitmap(this.ib_original, img.Bitmap.Width, img.Bitmap.Height);
                ib_original.Image = img;
            }
        }
        private void ResizePictureBoxSizeByBitmap(PictureBox picBox, int bitmapWidth, int bitmapHeight) {
            //计算出比例picbox 和里面图片的长宽比例
            double widthPercent = bitmapWidth * 1.0 / picBox.Width;
            double heightPercent = bitmapHeight * 1.0 / picBox.Height;
            double resultPercent = Math.Max(widthPercent, heightPercent);
            picBox.Width =Convert.ToInt32( Math.Round( bitmapWidth / resultPercent, MidpointRounding.AwayFromZero));
            picBox.Height = Convert.ToInt32(Math.Round(bitmapHeight / resultPercent, MidpointRounding.AwayFromZero));
        }

        private void Btn_answerReg3_Click(object sender, EventArgs e)
        {
            CommonUse commonUse = new CommonUse();
            var src = new Image<Gray, byte>(ib_middleCut.Image.Bitmap);

            var thresholdImage = src.CopyBlank();
            int myThreshold = 210;
            CvInvoke.Threshold(src, thresholdImage, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
            commonUse.SaveMat(thresholdImage.Mat, "二值化后");
            //思路 close -腐蚀-腐蚀-膨胀
            //形态学膨胀
            Mat mat_dilate = commonUse.MyDilate(thresholdImage.Mat, Emgu.CV.CvEnum.MorphOp.Close);
            commonUse.SaveMat(mat_dilate, "形态学膨胀");
            //mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Close);
            //commonUse.SaveMat(mat_dilate, "形态学膨胀1");
            mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Erode);
            commonUse.SaveMat(mat_dilate, "形态学膨胀腐蚀1");
            mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Erode);
            commonUse.SaveMat(mat_dilate, "形态学膨胀腐蚀2");

            mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);
            commonUse.SaveMat(mat_dilate, "形态学膨胀2");

            var image_dilate = mat_dilate.ToImage<Gray,byte>();

            List<Rectangle> validRectList = new List<Rectangle>();
            CutedRectList.ForEach(rect =>
            {
                var newRect = new Rectangle(Math.Max(0, rect.X - 8), Math.Max(0,rect.Y - 8), rect.Width,rect.Height);
                var tmpImage = image_dilate.Copy(newRect);
                
                var result = GetWhiteColorPercenter(tmpImage);
                if ( result > 0.2)
                {
                    validRectList.Add(rect);
                }
                commonUse.SaveMat(tmpImage.Mat, "形态学后"+result);
            });

            validRectList.ForEach(r =>
            {
                CvInvoke.Rectangle(src, r, new MCvScalar(0, 0, 255));
            });

            new CommonUse().SaveMat(src.Mat, "通过比例计算获取的答案");
            this.ib_result.Image = src;


        }
        private double GetWhiteColorPercenter(Image<Gray,byte> image)
        {
            Gray c = image[1, 1];
            int rank = image.Data.GetLength(0);
            int cols = image.Data.GetLength(1);
            int whiteColorCount = 0;
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (image.Data[i, j, 0] == 255)
                    {
                        whiteColorCount++;
                    }
                }
            }

            var result= whiteColorCount * 1.0 / image.Data.Length;
            return result;

        }
        private void Btn_anwserReg_Click(object sender, EventArgs e)
        {
            if (this.ib_middleCut.Image == null)
            {
                MessageBox.Show("裁剪图片不能为空");
                return;
            }

            CommonUse commonUse = new CommonUse();

            Mat src = new Image<Bgr, byte>(ib_middleCut.Image.Bitmap).Mat;// new Mat();

            //CvInvoke.PyrMeanShiftFiltering(src1, src, 25, 10, 1, new MCvTermCriteria(5, 1));
            //commonUse.SaveMat(src, "降噪后");
            //commonUse.SaveMat(src1, "降噪后原始");

            Mat dst = new Mat();
            Mat src_gray = new Mat();
            CvInvoke.CvtColor(src, src_gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            //存储灰度图片
            commonUse.SaveMat(src_gray, "灰度");

            #region 二值化
            //二值化
            Mat mat_threshold = new Mat();
            int myThreshold = Convert.ToInt32(num_threshold.Value);
            CvInvoke.Threshold(src_gray, mat_threshold, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
            commonUse.SaveMat(mat_threshold, "二值化");
            //思路 close -腐蚀-腐蚀-膨胀
            //形态学膨胀
            Mat mat_dilate = commonUse.MyDilate(mat_threshold, Emgu.CV.CvEnum.MorphOp.Close);
            commonUse.SaveMat(mat_dilate, "形态学膨胀");
            //mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Close);
            //commonUse.SaveMat(mat_dilate, "形态学膨胀1");
            mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Erode);
            commonUse.SaveMat(mat_dilate, "形态学膨胀腐蚀1");
            mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Erode);
            commonUse.SaveMat(mat_dilate, "形态学膨胀腐蚀2");

            mat_dilate = commonUse.MyDilate(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);
            commonUse.SaveMat(mat_dilate, "形态学膨胀2");
            #endregion

            //边缘检测
            CvInvoke.Canny(mat_dilate, dst, Convert.ToInt32(this.num_Min.Value), Convert.ToInt32(this.num_Max.Value), Convert.ToInt32(this.num_apertureSize.Value));
            commonUse.SaveMat(dst, "边缘检测");

            //寻找答题卡矩形边界（所有的矩形）
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();//创建VectorOfVectorOfPoint数据类型用于存储轮廓

            VectorOfVectorOfPoint validContours = new VectorOfVectorOfPoint();//有效的，所有的选项的

            CvInvoke.FindContours(dst, contours, null, Emgu.CV.CvEnum.RetrType.Ccomp,
                Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple,new Point(8,8));//提取轮廓

            //打印所以后矩形面积和周长
            int size = contours.Size;
            for (int i = 0; i < size; i++)
            {
                var item = contours[i];
                var tempArea = CvInvoke.ContourArea(item);
                var tempArc = CvInvoke.ArcLength(item, true);
                Console.WriteLine($"面积：{tempArea}；周长：{tempArc}"); ;
                if (tempArea > 200 && tempArea < 2000)
                {
                    validContours.Push(item);
                }
            }

            //CvInvoke.ApproxPolyDP

            //画出所有轮廓
            Mat middleMat = new Image<Bgr, byte>(this.ib_middleCut.Image.Bitmap).Mat;
            CvInvoke.DrawContours(middleMat, validContours, -1, new MCvScalar(0, 0, 255), 1);
            this.ib_result.Image = middleMat;
            commonUse.SaveMat(middleMat, "画出所有轮廓的");

            //画出所有矩形
            Mat tmpMat = new Image<Bgr, byte>(this.ib_middleCut.Image.Bitmap).Mat;
            List<Rectangle> rectangles = commonUse.GetRectList(validContours,false);

            rectangles.ForEach(rect =>
            {
                CvInvoke.Rectangle(tmpMat, rect, new MCvScalar(0, 0, 255));
            });

            commonUse.SaveMat(tmpMat, "画出所有矩形轮廓的");

            this.ib_result.Image = tmpMat;
            

        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            //this.Start();

            CommonUse commonUse = new CommonUse();
            if (this.ib_middleCut.Image != null)
            {

                //Mat src1 = new Image<Bgr, byte>(ib_middleCut.Image.Bitmap).Mat;
                Mat src = new Image<Bgr, byte>(ib_middleCut.Image.Bitmap).Mat;// new Mat();
                
                //CvInvoke.PyrMeanShiftFiltering(src1, src, 25, 10, 1, new MCvTermCriteria(5, 1));
                //commonUse.SaveMat(src, "降噪后");
                //commonUse.SaveMat(src1, "降噪后原始");

                Mat dst = new Mat();
                Mat src_gray = new Mat();
                CvInvoke.CvtColor(src, src_gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                //存储灰度图片
                commonUse.SaveMat(src_gray, "灰度");

                #region 二值化
                //二值化
                Mat mat_threshold = new Mat();
                int myThreshold = Convert.ToInt32(num_threshold.Value);
                CvInvoke.Threshold(src_gray, mat_threshold, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
                commonUse.SaveMat(mat_threshold, "二值化");
                //形态学膨胀
                Mat mat_dilate = commonUse.MyDilate(mat_threshold, Emgu.CV.CvEnum.MorphOp.Close);
                commonUse.SaveMat(mat_dilate, "形态学膨胀");
                mat_dilate = commonUse.MyDilate(mat_threshold, Emgu.CV.CvEnum.MorphOp.Open);
                commonUse.SaveMat(mat_dilate, "形态学膨胀1");
                mat_dilate = commonUse.MyDilate(mat_threshold, Emgu.CV.CvEnum.MorphOp.Erode);
                commonUse.SaveMat(mat_dilate, "形态学膨胀");
                #endregion

                //边缘检测
                CvInvoke.Canny(mat_dilate, dst, Convert.ToInt32(this.num_Min.Value), Convert.ToInt32(this.num_Max.Value), Convert.ToInt32(this.num_apertureSize.Value));
                commonUse.SaveMat(dst, "边缘检测");

                //寻找答题卡矩形边界（所有的矩形）
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();//创建VectorOfVectorOfPoint数据类型用于存储轮廓

                VectorOfVectorOfPoint validContours = new VectorOfVectorOfPoint();//有效的，所有的选项的

                CvInvoke.FindContours(dst, contours, null, Emgu.CV.CvEnum.RetrType.Ccomp,
                    Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);//提取轮廓

                //CvInvoke.ApproxPolyDP

                //画出所有矩形
                Mat middleMat = new Image<Bgr, byte>(this.ib_middleCut.Image.Bitmap).Mat;
                CvInvoke.DrawContours(middleMat, contours, -1, new MCvScalar(0, 0, 255), 1);
                this.ib_result.Image = middleMat;
                commonUse.SaveMat(middleMat, "画出所有轮廓的");


                //获取矩形边界另一种方法
                //var tempRect=CvInvoke.BoundingRectangle(contours);
                //CvInvoke.Rectangle(src, tempRect, new MCvScalar(0, 0, 255));
                //this.ib_middle.Image = src;
                //return;


                //打印所以后矩形面积和周长
                int size = contours.Size;
                //for (int i = 0; i < size; i++)
                //{
                //    var item = contours[i];
                //    var tempArea = CvInvoke.ContourArea(item);
                //    var tempArc = CvInvoke.ArcLength(item, true);
                //    Console.WriteLine($"面积：{tempArea}；周长：{tempArc}"); ;
                //    if (tempArea > 200 && tempArea<1000)
                //    {
                //        validContours.Push(item);
                //    }
                //}

                //画出符合要求的所有矩形
                //CvInvoke.DrawContours(src, validContours, -1, new MCvScalar(0, 0, 255), 1);

                //var rectList = commonUse.GetRectList(validContours);
                //foreach (var item in rectList)
                //{
                //    //宽高比例不能>4或者0.24
                //    if (item.Width * 1.0 / item.Height > 4 || item.Width * 1.0 / item.Height < 0.25)
                //    {
                //        continue;
                //    }
                //    CvInvoke.Rectangle(src, item, new MCvScalar(0, 0, 255));
                //}

                //commonUse.SaveMat(src, "带有所有轮廓边界的");

                //this.ib_middle.Image = src;

            }
            else
            {
                MessageBox.Show("请先加载图片");
            }

        }

        private void Btn_checkLine_Click(object sender, EventArgs e)
        {

        }

        private void Btn_rectReg_Click(object sender, EventArgs e)
        {
            if (this.ib_middleCut.Image == null) {
                MessageBox.Show("裁剪图片不能为空");
                return;
            }

            CommonUse commonUse = new CommonUse();

            var rectList = commonUse.GetRectListFromBitmap(this.ib_middleCut.Image.Bitmap,100,2000,0,0,true,1);

            //排序
            var rectListDic = commonUse.OrderRectList(rectList);

            Mat src = new Image<Bgr, byte>(ib_middleCut.Image.Bitmap).Mat;

            foreach (var item in rectList)
            {
                CvInvoke.Rectangle(src, item, new MCvScalar(0, 0, 255));
            }

            commonUse.SaveMat(src, "获取裁剪图中的所有轮廓边界提取");

            this.ib_middle.Image = src;

            //在原始图片中画出矩形框
            var orginalRectList = new List<Rectangle>();
            foreach (var item in rectList)
            {
                var tmpRect = new Rectangle(new Point(this.destRect.X + item.X, this.destRect.Y + item.Y), item.Size);
                orginalRectList.Add(tmpRect);
                

            }
            Mat matOrginal = new Image<Bgr, byte>(this.ib_original.Image.Bitmap).Mat;
            foreach (var item in orginalRectList)
            {
                CvInvoke.Rectangle(matOrginal, Rectangle.Round(item), new MCvScalar(0, 0, 255));
                
            }
            commonUse.SaveMat(matOrginal, "获取裁剪图中的所有轮廓边在原始图中");
            this.ib_result.Image = matOrginal;

            //在原始的PictureBox中画出框

            this.DrawRectInPictureBox(this.ib_original, orginalRectList);

            //
            OrginalRectList = orginalRectList;
            CutedRectList = rectList;
        }

        private void Ib_original_Paint(object sender, PaintEventArgs e)
        {
            
            if (blnDraw)
            {
                if (this.ib_original.Image != null)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色
                    }
                }
            }
        }

        private void Ib_original_MouseUp(object sender, MouseEventArgs e)
        {
            blnDraw = false; //结束绘制
        }

        private void Ib_original_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            Invalidate();
            blnDraw = true;
        }

        private void Ib_original_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDraw)
            {
                if (e.Button != MouseButtons.Left)//判断是否按下左键
                    return;
                Point tempEndPoint = e.Location; //记录框的位置和大小
                rect.Location = new Point(
                Math.Min(start.X, tempEndPoint.X),
                Math.Min(start.Y, tempEndPoint.Y));
                rect.Size = new Size(
                Math.Abs(start.X - tempEndPoint.X),
                Math.Abs(start.Y - tempEndPoint.Y));
                this.ib_original.Invalidate();
            }
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            this.ib_original.Invalidate();
        }

        public static Bitmap Cut(Bitmap b, Rectangle rect) {
            return Cut(b, rect.X, rect.Y, rect.Width, rect.Height);
        }
        /// <summary>
            /// 剪裁 -- 用GDI+
            /// </summary>
            /// <param name="b">原始Bitmap</param>
            /// <param name="StartX">开始坐标X</param>
            /// <param name="StartY">开始坐标Y</param>
            /// <param name="iWidth">宽度</param>
            /// <param name="iHeight">高度</param>
            /// <returns>剪裁后的Bitmap</returns>
        public static Bitmap Cut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth,iHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        private void Btn_cut_Click(object sender, EventArgs e)
        {
            if (ib_original.Image == null) {
                MessageBox.Show("请先加载图片");
                return;
            }
            //计算出比例picbox 和里面图片的长宽比例
            var currentBitMap = (Bitmap)((PictureBox)this.ib_original).Image;
            double widthPercent = currentBitMap.Width * 1.0 / this.ib_original.Width;
            double heightPercent = currentBitMap.Height * 1.0 / this.ib_original.Height;
            //double resultPercent = Math.Max(widthPercent, heightPercent);

            destRect = new Rectangle(Convert.ToInt32( rect.X * widthPercent), Convert.ToInt32(rect.Y * heightPercent),
                Convert.ToInt32(rect.Width * widthPercent), Convert.ToInt32(rect.Height * heightPercent));
            var cutedBitmap = Cut((Bitmap)((PictureBox)this.ib_original).Image, destRect); //Cut(this.ib_original.Image.Bitmap, destRect)
            if (cutedBitmap == null) {
                MessageBox.Show("未选中裁剪区域");
                return;
            }
            this.ib_middleCut.Image = new Image<Bgr, byte>(cutedBitmap).Mat;

        }

        private void DrawRectInPictureBox(PictureBox picBox,List<Rectangle> bitMapRectangleList){
            if (bitMapRectangleList?.Count <= 0) {
                return;
            }
            //计算出比例picbox 和里面图片的长宽比例
            float widthPercent = this.ib_original.Image.Bitmap.Width * 1.0f / this.ib_original.Width;
            float heightPercent = this.ib_original.Image.Bitmap.Height * 1.0f / this.ib_original.Height;

            var destRectList = new List<RectangleF>();
            foreach (var item in bitMapRectangleList)
            {
                RectangleF tmpRect = new RectangleF(item.X / widthPercent, item.Y / heightPercent, item.Width / widthPercent, item.Height / heightPercent);
                destRectList.Add(tmpRect);
            }

            var g = picBox.CreateGraphics();
            g.DrawRectangles(new Pen(Color.Red, 1), destRectList.ToArray());
        }

        private void Btn_Plus_Click(object sender, EventArgs e)
        {
            ResizePicBox(this.ib_original, 30);
        }

        private void ResizePicBox(PictureBox picBox, int step) {
            var lastWidth = picBox.Width + step;
            var lastHeight = Convert.ToInt32( Math.Round( picBox.Height * (lastWidth * 1.0 / picBox.Width), MidpointRounding.AwayFromZero));
            picBox.Size = new Size(lastWidth, lastHeight);


        }

        private void Btn_reduce_Click(object sender, EventArgs e)
        {
            
            ResizePicBox(this.ib_original, -30);
        }

        private void Btn_openZoomForm_Click(object sender, EventArgs e)
        {
            ZoomForm form = new ZoomForm();
            form.Show();
        }

        private void Btn_rotate_Click(object sender, EventArgs e)
        {
            PictureHelper.RotatePictureFormCenter(this.ib_original, 90);
        }

        private void Btn_openZoomForm2_Click(object sender, EventArgs e)
        {
            ZoomForm2 form2 = new ZoomForm2();
            form2.Show();
        }

        private void Btn_joinForm_Click(object sender, EventArgs e)
        {
            ZoomForm2 zoom = new ZoomForm2();
            zoom.TopLevel = false;
            this.panel1.Controls.Add(zoom);
            zoom.FormBorderStyle = FormBorderStyle.None;
            zoom.Dock = DockStyle.Fill;
            zoom.Show();
        }

        private void Btn_openCompareForm_Click(object sender, EventArgs e)
        {
            CompareForm form = new CompareForm();
            form.Show();
        }

        private void Btn_loadChosetestForm_Click(object sender, EventArgs e)
        {
            ChoseOptionTestForm form = new ChoseOptionTestForm();
            form.Show();
        }

        private void Btn_answerReg_Click(object sender, EventArgs e)
        {
            if (this.ib_middleCut.Image == null)
            {
                MessageBox.Show("裁剪图片不能为空");
                return;
            }

            CommonUse commonUse = new CommonUse();

            
            var centerList = commonUse.GetCenterPointListFromBitmap(this.ib_middleCut.Image.Bitmap,(int)this.num_threshold.Value);//300分辨率用200,150分辨率用45
            Mat tmpMat = new Image<Bgr, byte>(this.ib_middleCut.Image.Bitmap).Mat;
            centerList.ForEach(p =>
            {
                CvInvoke.Circle(tmpMat, p, 6, new MCvScalar(0,0,255),2);
            });

            this.ib_result.Image = tmpMat;
        }

        private void Btn_answerReg4_Click(object sender, EventArgs e)
        {
            if (this.ib_middleCut.Image == null)
            {
                MessageBox.Show("裁剪图片不能为空");
                return;
            }
           
            CommonUse commonUse = new CommonUse();


            var centerList = commonUse.GetCenterPointListFromBitmapByWhiteArea(this.ib_middleCut.Image.Bitmap,CutedRectList);
            Mat tmpMat = new Image<Bgr, byte>(this.ib_middleCut.Image.Bitmap).Mat;
            centerList.ForEach(p =>
            {
                CvInvoke.Circle(tmpMat, p, 6, new MCvScalar(0, 0, 255),2);
            });

            this.ib_result.Image = tmpMat;
        }
        private double Similar2(Bitmap bitmap1, Bitmap bitmap2)
        {
            //FileStorage
            //var fileName = @"C:\Users\Administrator\Pictures\A.png";
            //bitmap1 = new Bitmap(fileName);
            //bitmap2 = new Bitmap(fileName);

            var mat1 = new Image<Gray, byte>(bitmap1);
            var mat2 = new Image<Gray, byte>(bitmap2);

            var hist1 = mat1.CopyBlank();var hist2 = mat2.CopyBlank();
            float[] range = { 0, 256 };
            int[] channels = new int[] { 0 };
            int[] histSize = new int[] { 256 };
            bool uniform = true;
            bool accumulate = false;
            CvInvoke.CalcHist(mat1, channels, null, hist1, histSize, range, accumulate);
            CvInvoke.CalcHist(mat1, channels, null, hist2, histSize, range, accumulate);

            return 1;
        }

        private void Btn_openTemplate_Click(object sender, EventArgs e)
        {
            TemplateValidate templateValidate = new TemplateValidate();
            templateValidate.Show();
        }

        private void Btn_openCompressForm_Click(object sender, EventArgs e)
        {
            PicCompressForm form = new PicCompressForm();
            form.Show();
        }

        private void Btn_regBrokenRect_Click(object sender, EventArgs e)
        {
            //FastDetector
            if (this.ib_middleCut.Image == null)
            {
                MessageBox.Show("裁剪图片不能为空");
                return;
            }

            CommonUse commonUse = new CommonUse();

            var rectList = commonUse.GetRectListFromBitmap(this.ib_middleCut.Image.Bitmap,40,600, isAutoFillFull: true,optimizeTimes:2,isBrokenOption:true);

            //排序
            var rectListDic = commonUse.OrderRectList(rectList);

            Mat src = new Image<Bgr, byte>(ib_middleCut.Image.Bitmap).Mat;

            foreach (var item in rectList)
            {
                CvInvoke.Rectangle(src, item, new MCvScalar(0, 0, 255));
            }

            commonUse.SaveMat(src, "获取裁剪图中的所有轮廓边界提取");

            this.ib_middle.Image = src;

            //在原始图片中画出矩形框
            var orginalRectList = new List<Rectangle>();
            foreach (var item in rectList)
            {
                var tmpRect = new Rectangle(new Point(this.destRect.X + item.X, this.destRect.Y + item.Y), item.Size);
                orginalRectList.Add(tmpRect);


            }
            Mat matOrginal = new Image<Bgr, byte>(this.ib_original.Image.Bitmap).Mat;
            foreach (var item in orginalRectList)
            {
                CvInvoke.Rectangle(matOrginal, Rectangle.Round(item), new MCvScalar(0, 0, 255));

            }
            commonUse.SaveMat(matOrginal, "获取裁剪图中的所有轮廓边在原始图中");
            this.ib_result.Image = matOrginal;

            //在原始的PictureBox中画出框

            this.DrawRectInPictureBox(this.ib_original, orginalRectList);

            //
            OrginalRectList = orginalRectList;
            CutedRectList = rectList;
        }

        private void Btn_cornerForm_Click(object sender, EventArgs e)
        {
            CornerFinderForm form = new CornerFinderForm();
            form.Show();
        }

        private void Btn_reg2All_Click(object sender, EventArgs e)
        {
            if (this.ib_original.Image == null)
            {
                MessageBox.Show("原始图片不能为空");
                return;
            }

            CommonUse commonUse = new CommonUse();


            var centerList = commonUse.GetCenterPointListFromBitmap(this.ib_original.Image.Bitmap, (int)this.num_threshold.Value);//300分辨率用200,150分辨率用45
            Mat tmpMat = new Image<Bgr, byte>(this.ib_original.Image.Bitmap).Mat;
            centerList.ForEach(p =>
            {
                CvInvoke.Circle(tmpMat, p, 6, new MCvScalar(0, 0, 255), 2);
            });

            this.ib_result.Image = tmpMat;
        }

        private void Bt_openOcrForm_Click(object sender, EventArgs e)
        {
            OCRForm form = new OCRForm();
            form.Show();
        }
    }
}
