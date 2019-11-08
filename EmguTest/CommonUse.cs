using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using EmguTest.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public class CommonUse
    {
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
        public void DrawRectAndSave(Mat mat, List<Rectangle> rectangles, string fileName)
        {
            foreach (var item in rectangles)
            {
                CvInvoke.Rectangle(mat, Rectangle.Round(item), new MCvScalar(0, 0, 255));

            }
            SaveMat(mat, fileName);
        }
        /// <summary>
        /// 获取最大的矩形，重叠和包括，只取最大,过滤长宽比例不协调的矩形，宽高比例不能>4或者0.24
        /// </summary>
        /// <param name="pointArray"></param>
        /// <returns></returns>
        public List<Rectangle> GetRectList(VectorOfVectorOfPoint pointArray)
        {


            List<Rectangle> list = new List<Rectangle>();
            var pointSize = pointArray.Size;
            for (int i = 0; i < pointSize; i++)
            {
                var pointList = pointArray[i];

                //var tempPoit = pointList[0];
                //Point leftTop = tempPoit;
                //Point rightBottom = tempPoit;

                //for (int j = 1; j < pointList.Size; j++)
                //{
                //    tempPoit = pointList[j];
                //    leftTop.X = Math.Min(leftTop.X, tempPoit.X);
                //    leftTop.Y = Math.Min(leftTop.Y, tempPoit.Y);

                //    rightBottom.X = Math.Max(rightBottom.X, tempPoit.X);
                //    rightBottom.Y = Math.Max(rightBottom.Y, tempPoit.Y);
                //}

                //var width = rightBottom.X - leftTop.X;// Math.Sqrt(Math.Pow(rightBottom.X - leftTop.X, 2) + Math.Pow(0, 2));
                //var height = rightBottom.Y - leftTop.Y; //Math.Sqrt(Math.Pow(rightBottom.Y - leftTop.Y, 2) + Math.Pow(0, 2));

                //var tmpRect = new Rectangle(leftTop, new Size((int)width, (int)height));
                var tmpRect = CvInvoke.BoundingRectangle(pointList);
                //宽高比例不能>4或者0.24
                if (tmpRect.Width * 1.0 / tmpRect.Height > 4 || tmpRect.Width * 1.0 / tmpRect.Height < 0.25)
                {
                    continue;
                }
                if (!IsOverlapAndReplace(list, tmpRect))
                {
                    list.Add(tmpRect);//new Rectangle(leftTop.X,leftTop.Y, (int)width,(int)height);
                }


            }
            list = RemoveInnerRect(list);
            return list;
        }

        private bool IsOverlapAndReplace(List<Rectangle> sourceList, Rectangle rect)
        {
            bool isOverlap = false;

            int startIndex = sourceList.Count - 1;
            if (startIndex < 0)
            {
                return false;
            }

            for (; startIndex > -1; startIndex--)
            {
                float tmpPercent = DecideOverlap(sourceList[startIndex], rect, out Rectangle maxRect);
                isOverlap = isOverlap || tmpPercent > 0.2f;
                if (isOverlap)
                {
                    sourceList.RemoveAt(startIndex);
                    //rect = maxRect;
                    sourceList.Add(maxRect);
                    return true;
                }
                else if (tmpPercent > 0)
                {
                    Console.WriteLine(tmpPercent);

                }
            }
            //if (isOverlap)
            //{
            //    sourceList.Add(rect);
            //}
            return isOverlap;
        }
        private List<Rectangle> RemoveInnerRect(List<Rectangle> list)
        {

            var needRemovedList = new List<Rectangle>();
            for (int i = 0; i < list.Count; i++)
            {
                Rectangle rect1 = list[i];
                for (int j = 0; j < list.Count; j++)
                {
                    Rectangle rect2 = list[j];
                    if (rect1 == rect2)
                    {
                        continue;
                    }

                    if (rect1.X >= rect2.X && rect1.X + rect1.Width <= rect2.X + rect2.Width
                        && rect1.Y >= rect2.Y && rect1.Y + rect1.Height <= rect2.Y + rect2.Height)
                    {
                        needRemovedList.Add(rect1);
                    }
                }

            }
            needRemovedList.ForEach(r =>
            {
                list.Remove(r);
            });

            return list;

        }

        public float DecideOverlap(Rectangle r1, Rectangle r2, out Rectangle maxRect)
        {
            int x1 = r1.X;
            int y1 = r1.Y;
            int width1 = r1.Width;
            int height1 = r1.Height;

            int x2 = r2.X;
            int y2 = r2.Y;
            int width2 = r2.Width;
            int height2 = r2.Height;

            int endx = Math.Max(x1 + width1, x2 + width2);
            int startx = Math.Min(x1, x2);
            int width = width1 + width2 - (endx - startx);

            int endy = Math.Max(y1 + height1, y2 + height2);
            int starty = Math.Min(y1, y2);
            int height = height1 + height2 - (endy - starty);

            maxRect = new Rectangle(startx, starty, endx - startx, endy - starty);

            float ratio = 0.0f;
            float Area, Area1, Area2;

            //if((x1<x2 && y1<y2 && width1>width2 && height1 > height2) ||
            //    (x1 > x2 && y1 > y2 && width1 < width2 && height1 < height2))
            //{
            //    return 1f;
            //}

            if (width <= 0 || height <= 0)
                return 0.0f;
            else
            {
                Area = width * height;
                Area1 = width1 * height1;
                Area2 = width2 * height2;
                ratio = Area / (Area1 + Area2 - Area);
            }

            return ratio;
        }


        /// <summary>
        /// 获取矩形框从图片中
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="minArea">符合矩形框的最小面积，否则抛弃</param>
        /// <param name="originalStartX">在原始图片中的X起始位置</param>
        /// <param name="originalStartY">在原始图片中的Y起始位置</param>
        /// <returns></returns>
        public List<Rectangle> GetRectListFromBitmap(Bitmap bitmap, double minArea = 100, double maxArea = 2000, int originalStartX = 0, int originalStartY = 0, bool isAutoFillFull = false, int optimizeTimes = 1)
        {
            List<Rectangle> list;

            //Mat matOrginal = new Image<Bgr, byte>(bitmap).Mat;
            Mat mat = new Image<Bgr, byte>(bitmap).Mat;// new Mat();
            //降噪处理
            //CvInvoke.PyrMeanShiftFiltering(matOrginal, mat, 25, 10, 1, new MCvTermCriteria(5, 1));

            //图片灰度处理
            Mat matCanny = new Mat();
            Mat matGray = new Mat();
            CvInvoke.CvtColor(mat, matGray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            SaveMat(matGray, "普通灰度图片");

            //二值化
            Mat mat_threshold = new Mat();
            int myThreshold = 230;
            CvInvoke.Threshold(matGray, mat_threshold, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            SaveMat(mat_threshold, "二值化");
            //形态学膨胀
            //Mat mat_dilate = MyDilate(mat_threshold);
            //SaveMat(mat_threshold, "形态学膨胀");
            //边缘检测
            CvInvoke.Canny(mat_threshold, matCanny, 120, 180, 5);
            SaveMat(matCanny, "边缘检测");
            //寻找答题卡矩形边界（所有的矩形）
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();//创建VectorOfVectorOfPoint数据类型用于存储轮廓

            VectorOfVectorOfPoint validContours = new VectorOfVectorOfPoint();//有效的，所有的选项的

            CvInvoke.FindContours(matCanny, contours, null, Emgu.CV.CvEnum.RetrType.Ccomp,
                Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);//提取轮廓

            //获取符合条件的矩形  闭合矩形的面积>200
            int size = contours.Size;
            for (int i = 0; i < size; i++)
            {
                var item = contours[i];
                var tempArea = CvInvoke.ContourArea(item);
                var tempArc = CvInvoke.ArcLength(item, true);
                Console.WriteLine($"面积：{tempArea}；周长：{tempArc}"); ;
                if (tempArea > minArea && tempArea < maxArea)
                {
                    validContours.Push(item);
                }
            }

            list = GetRectList(validContours);
            //在原始开始位置上获取矩形列表
            var originalRectList = new List<Rectangle>();
            foreach (var item in list)
            {
                var tmpRect = new Rectangle(new Point(originalStartX + item.X, originalStartY + item.Y), item.Size);

                originalRectList.Add(tmpRect);


            }

            //画出去掉重合的矩形框
            //SaveMat(mat, "原始");
            DrawRectAndSave(mat, originalRectList, "去掉重合的矩形框");
            for (int i = 0; i < list.Count; i++)
            {
                using (Mat tmpMat = new Mat(mat_threshold, list[i]))
                {


                    var fileName = OCRHelper.Ocr(tmpMat);
                    fileName = fileName.Replace("\n", "").Replace("\r", "").Replace("\\", "").Replace(" ","").Replace("|","");
                    SaveMat(tmpMat, "解析后-" + fileName);
                    Console.WriteLine(fileName);
                }
            }

            if (isAutoFillFull)
            {
                originalRectList = FillFull(originalRectList);
            }

            if (optimizeTimes > 0)
            {
                originalRectList = OptimizeRect(originalRectList, optimizeTimes);
            }

            return originalRectList;
        }

        /// <summary>
        /// 把矩形框按照 列或者宽排序
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="isrow">是否按照行分组</param>
        /// <param name="IsFillFull">是否补全</param>
        /// <returns></returns>
        public Dictionary<int, List<Rectangle>> OrderRectList(List<Rectangle> sourceList, bool isrow = true, bool IsFillFull = true)
        {
            if (isrow)
            {
                return OrderRectListByRow(sourceList);
            }
            else
            {
                return OrderRectListByColumn(sourceList);
            }


        }
        private Dictionary<int, List<Rectangle>> OrderRectListByRow(List<Rectangle> sourceList)
        {
            if (sourceList?.Count < 1)
            {
                return null;
            }

            Dictionary<int, List<Rectangle>> pairs = new Dictionary<int, List<Rectangle>>();
            sourceList = sourceList.OrderBy(r => r.Y).ThenBy(r => r.X).ToList();

            int rowNum = 1;
            var tmpRectList = new List<Rectangle>();
            tmpRectList.Add(sourceList[0]);
            pairs.Add(rowNum, tmpRectList);

            for (int i = 1; i < sourceList.Count; i++)
            {
                var item = sourceList[i];
                if (item.Y > (sourceList[i - 1].Y + sourceList[i - 1].Height))
                {
                    rowNum++;
                    tmpRectList = new List<Rectangle>();
                    tmpRectList.Add(item);
                    pairs.Add(rowNum, tmpRectList);
                }
                else
                {
                    tmpRectList.Add(item);
                }
            }

            return pairs;

        }
        private Dictionary<int, List<Rectangle>> OrderRectListByColumn(List<Rectangle> sourceList)
        {
            if (sourceList?.Count < 1)
            {
                return null;
            }

            Dictionary<int, List<Rectangle>> pairs = new Dictionary<int, List<Rectangle>>();
            sourceList = sourceList.OrderBy(r => r.X).ThenBy(r => r.Y).ToList();

            int colNum = 1;
            var tmpRectList = new List<Rectangle>();
            tmpRectList.Add(sourceList[0]);
            pairs.Add(colNum, tmpRectList);

            for (int i = 1; i < sourceList.Count; i++)
            {
                var item = sourceList[i];
                if (item.X > (sourceList[i - 1].X + sourceList[i - 1].Width))
                {
                    colNum++;
                    tmpRectList = new List<Rectangle>();
                    tmpRectList.Add(item);
                    pairs.Add(colNum, tmpRectList);
                }
                else
                {
                    tmpRectList.Add(item);
                }
            }

            return pairs;

        }


        /// <summary>
        /// 优化不合格的方框
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<Rectangle> OptimizeRect(List<Rectangle> sourceList, int times = 1)
        {
            sourceList = RemoveUnqualified(sourceList);

            //再填充
            sourceList = FillFull(sourceList);

            if (times-- > 1)
            {
                sourceList = OptimizeRect(sourceList, times);
            }

            return sourceList;
        }

        /// <summary>
        /// 去除不合格的矩形，
        /// </summary>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        private List<Rectangle> RemoveUnqualified(List<Rectangle> sourceList)
        {
            if (sourceList?.Count == 0)
            {
                return sourceList;
            }
            sourceList = sourceList.OrderBy(r => r.Y).ThenBy(r => r.X).ToList();
            double averageWidth = sourceList.Average(r => r.Width), averageHeight = sourceList.Average(r => r.Height);
            double qualifiedWidthPercent = 0.85, qualifiedHeightPercent = 0.85, qualifiedPercent = 0.9;


            //优化不合格的矩形，去掉太小的，去掉太大的
            var unqualifiedRectList = new List<Rectangle>();
            var dic = OrderRectListByRow(sourceList);
            foreach (var key in dic.Keys)
            {
                int qualifiedCount = 0;
                foreach (var rect in dic[key])
                {
                    if (rect.Width < averageWidth * qualifiedWidthPercent ||
                        rect.Height < averageHeight * qualifiedHeightPercent ||
                        (rect.Width < averageWidth * qualifiedPercent && rect.Height < averageHeight * qualifiedPercent) ||
                        rect.Width > averageWidth * 2 || rect.Height > averageHeight * 2)
                    {
                        unqualifiedRectList.Add(rect);
                        continue;
                    }
                    qualifiedCount++;
                }
                if (qualifiedCount == 0)
                {
                    unqualifiedRectList.RemoveAt(unqualifiedRectList.Count - 1);
                }
            }

            for (int i = 0; i < unqualifiedRectList.Count; i++)
            {
                sourceList.Remove(unqualifiedRectList[i]);
            }

            return sourceList;
        }


        /// <summary>
        /// 填充缺失的方框
        /// </summary>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public List<Rectangle> FillFull(List<Rectangle> sourceList)
        {

            if (sourceList.Count <= 2)
            {
                return sourceList;
            }

            //去掉不合格的矩形
            sourceList = RemoveUnqualified(sourceList);

            //按照行分组,先Y，后X排序
            sourceList = sourceList.OrderBy(r => r.Y).ThenBy(r => r.X).ToList();
            var firstRect = sourceList.FirstOrDefault();
            int rowNum = 1;//行数量
            int tempColNum = 1, colNum = 1;//列数量
            int startX = firstRect.X, startY = firstRect.Y, endX = firstRect.X + firstRect.Width, endY = firstRect.Y + firstRect.Height;
            //double averageWidht = sourceList.Average(r => r.Width), averageHeight = sourceList.Average(r => r.Height);
            //double intervalWidth = 0, intervalHeight = 0;//间隙

            for (int i = 1; i < sourceList.Count; i++)
            {
                var rect = sourceList[i];
                startX = Math.Min(startX, rect.X);
                startY = Math.Min(startY, rect.Y);
                endX = Math.Max(endX, rect.X + rect.Width);
                endY = Math.Max(endY, rect.Y + rect.Height);

                if (rect.Y > (sourceList[i - 1].Y + sourceList[i - 1].Height))
                {
                    rowNum++;
                    tempColNum = 1;
                }
                else
                {
                    tempColNum++;

                }

                colNum = Math.Max(colNum, tempColNum);

            }
            //intervalWidth = (endX - startX - averageWidht * colNum) * 1.0 / (colNum - 1);
            //intervalHeight = (endY - startY - averageHeight * rowNum) * 1.0 / (rowNum - 1);

            CaculateRectInfo(sourceList, out double averageWidth, out double averageHeight, out double intervalWidth, out double intervalHeight);

            //从第一个开始逐步检查
            for (int i = 0; i < rowNum; i++)
            {
                int tmpY = Convert.ToInt32((startY + (averageHeight + intervalHeight) * i));
                for (int j = 0; j < colNum; j++)
                {
                    Rectangle tmpRect;
                    //if()
                    tmpRect = new Rectangle(Convert.ToInt32((startX + (averageWidth + intervalWidth) * j)), tmpY,
                       Convert.ToInt32((averageWidth)), Convert.ToInt32((averageHeight)));
                    IsInRectListAndInsert(sourceList, tmpRect);
                }
            }


            return sourceList;
        }

        /// <summary>
        /// 计算矩形框队列的宽、高，行间隙、列间隙
        /// </summary>
        /// <param name="sourceList">数据源</param>
        /// <param name="averageWidth">矩形平均宽</param>
        /// <param name="averageHeight">矩形平均高</param>
        /// <param name="intervalWidth">列平均间隙</param>
        /// <param name="intervalHeight">行平均间隙</param>
        public void CaculateRectInfo(List<Rectangle> sourceList, out double averageWidth, out double averageHeight, out double intervalWidth, out double intervalHeight)
        {
            averageWidth = averageHeight = intervalWidth = intervalHeight = 0;
            if (sourceList?.Count == 1)
            {
                averageWidth = sourceList[0].Width;
                averageHeight = sourceList[0].Height;
                return;
            }
            else if (sourceList?.Count == 0)
            {
                return;
            }

            sourceList = RemoveUnqualified(sourceList);
            averageWidth = sourceList.Average(r => r.Width);
            averageHeight = sourceList.Average(r => r.Height);
            var firstRect = sourceList.FirstOrDefault();
            int startX = firstRect.X, endX = firstRect.X, startY = firstRect.Y, endY = firstRect.Y;

            //计算行之间的间距
            var dicRow = OrderRectListByRow(sourceList);
            var intervalWidthList = new List<double>();

            foreach (var item in dicRow.Values)
            {
                var tempRects = item.OrderBy(r => r.X).ToList();
                for (int i = 1; i < tempRects.Count; i++)
                {
                    startX = Math.Min(startX, tempRects[i - 1].X);
                    endX = Math.Max(endX, tempRects[i].X);

                    var tempIntervalWidth = tempRects[i].X - tempRects[i - 1].Width - tempRects[i - 1].X;
                    if (tempIntervalWidth < averageWidth)
                    {
                        intervalWidthList.Add(tempIntervalWidth);
                    }
                }
            }
            if (intervalWidthList.Count > 0)
            {
                intervalWidth = intervalWidthList.Average();
            }
            else
            {
                intervalWidth = CaculateInterval(startX, endX, averageWidth);
            }

            //计算列之间的的间距
            var dicCol = OrderRectListByColumn(sourceList);
            var intervalHeightList = new List<double>();
            foreach (var item in dicCol.Values)
            {
                var tempRects = item.OrderBy(r => r.Y).ToList();
                for (int i = 1; i < tempRects.Count; i++)
                {
                    startY = Math.Min(startY, tempRects[i - 1].Y);
                    endY = Math.Max(endY, tempRects[i].Y);

                    var tempIntervalHeight = tempRects[i].Y - tempRects[i - 1].Height - tempRects[i - 1].Y;
                    if (tempIntervalHeight < averageHeight)
                    {
                        intervalHeightList.Add(tempIntervalHeight);
                    }
                }

            }
            if (intervalHeightList.Count > 0)
            {
                intervalHeight = intervalHeightList.Average();
            }
            else
            {
                intervalHeight = CaculateInterval(startY, endY, averageHeight);
            }

        }

        /// <summary>
        /// 计算两点之间最可能拥有矩形的数量
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        private double CaculateInterval(int start, int end, double lenth)
        {
            int count = (int)Math.Floor((end - start + lenth / 2) / lenth);

            double interval = (end - start - lenth * count) * 1.0 / (count - 1);
            return interval;
        }

        /// <summary>
        /// 该rect是否在List中，如果没有则插入
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private bool IsInRectListAndInsert(List<Rectangle> sourceList, Rectangle rect, int startIndex = 0)
        {
            var isIn = false;
            for (int i = startIndex; i < sourceList.Count; i++)
            {
                var item = sourceList[i];
                float tmpPercent = DecideOverlap(sourceList[i], rect, out Rectangle maxRect);
                if (tmpPercent.Equals(0.0f))
                {
                    isIn = false || isIn;

                }
                else
                {
                    isIn = true;
                    break;
                }

            }

            if (!isIn)
            {
                sourceList.Add(rect);
            }

            return isIn;
        }

        /// <summary>
        /// 获取给定图像的最大矩形边界
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public VectorOfVectorOfPoint GetBoundaryOfPic(Mat src)
        {
            Mat dst = new Mat();
            Mat src_gray = new Mat();
            CvInvoke.CvtColor(src, src_gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            //边缘检测
            CvInvoke.Canny(src_gray, dst, 120, 180);

            //寻找答题卡矩形边界（最大的矩形）
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();//创建VectorOfVectorOfPoint数据类型用于存储轮廓

            CvInvoke.FindContours(dst, contours, null, Emgu.CV.CvEnum.RetrType.External,
                Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);//提取轮廓

            VectorOfVectorOfPoint max_contour = new VectorOfVectorOfPoint();//用于存储筛选过后的轮廓

            int ksize = contours.Size; //获取连通区域个数
            if (ksize == 1)
            {
                max_contour = contours;
            }
            else
            {
                //double maxLength = -1;//用于保存轮廓周长的最大值
                double maxArea = -1;//面积
                int index = -1;//轮廓周长的最大值的序号
                for (int i = 0; i < ksize; i++)
                {
                    VectorOfPoint contour = contours[i];//获取独立的连通轮廓
                                                        //double length = CvInvoke.ArcLength(contour, true);//计算连通轮廓的周长

                    //if (length > maxLength)
                    //{
                    //    maxLength = length;
                    //    index = i;
                    //}

                    double area = CvInvoke.ContourArea(contour, false);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        index = i;
                    }
                }
                max_contour.Push(contours[index]);//筛选后的连通轮廓
            }
            return max_contour;
        }
        /// <summary>
        /// 获取定位点
        /// </summary>
        public void FindAnchorPoint(Mat src, Mat matchMask, ref VectorOfPoint anchorPoint)
        {

        }
        /// <summary>
        /// 进行透视操作，获取矫正后图像
        /// </summary>
        /// <param name="src"></param>
        /// <param name="result_contour"></param>
        public Mat MyWarpPerspective(Mat src, VectorOfVectorOfPoint max_contour)
        {
            //拟合答题卡的几何轮廓,保存点集pts并顺时针排序
            VectorOfPoint pts = new VectorOfPoint();//用于存放逼近的结果
            VectorOfPoint tempContour = max_contour[0];//临时用
            double result_length = CvInvoke.ArcLength(tempContour, true);
            CvInvoke.ApproxPolyDP(tempContour, pts, result_length * 0.02, true); //几何逼近，获取矩形4个顶点坐标

            if (pts.Size != 4)
            {
                //最大轮廓不是矩形时，将原图转灰度图后返回
                Mat src_gray = new Mat();
                CvInvoke.CvtColor(src, src_gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);//转灰度图
                return src_gray;
            }
            else
            {
                //Point[]转换为PointF[]类型
                PointF[] pts_src = Array.ConvertAll(pts.ToArray(), new Converter<Point, PointF>(PointToPointF));

                //点集顺时针排序
                pts_src = SortPointsByClockwise(pts_src);

                //确定透视变换的宽度、高度
                Size sizeOfRect = CalSizeOfRect(pts_src);
                int width = sizeOfRect.Width;
                int height = sizeOfRect.Height;

                //计算透视变换矩阵
                PointF[] pts_target = new PointF[] { new PointF(0, 0), new PointF(width - 1, 0) ,
                        new PointF(width - 1, height - 1) ,new PointF(0, height - 1)};

                //计算透视矩阵
                Mat data = CvInvoke.GetPerspectiveTransform(pts_src, pts_target);
                //进行透视操作
                Mat src_gray = new Mat();
                Mat mat_Perspective = new Mat();
                CvInvoke.CvtColor(src, src_gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                CvInvoke.WarpPerspective(src_gray, mat_Perspective, data, new Size(width, height));

                return mat_Perspective;
            }
        }

        /// <summary>
        /// 将给定点集顺时针排序
        /// </summary>
        /// <param name="pts_src">四边形四个顶点组成的点集</param>
        /// <returns></returns>
        public PointF[] SortPointsByClockwise(PointF[] pts_src)
        {
            if (pts_src.Length != 4) return null;//确保为四边形

            //求四边形中心点？坐标
            float x_average = 0;
            float y_average = 0;
            float x_sum = 0;
            float y_sum = 0;
            for (int i = 0; i < 4; i++)
            {
                x_sum += pts_src[i].X;
                y_sum += pts_src[i].Y;
            }
            x_average = x_sum / 4;
            y_average = y_sum / 4;
            PointF center = new PointF(x_average, y_average);

            PointF[] result = new PointF[4];
            for (int i = 0; i < 4; i++)
            {
                if (pts_src[i].X < center.X && pts_src[i].Y < center.Y)
                {
                    result[0] = pts_src[i];//左上角点
                    continue;
                }
                if (pts_src[i].X > center.X && pts_src[i].Y < center.Y)
                {
                    result[1] = pts_src[i];//右上角点
                    continue;
                }
                if (pts_src[i].X > center.X && pts_src[i].Y > center.Y)
                {
                    result[2] = pts_src[i];//右下角点
                    continue;
                }
                if (pts_src[i].X < center.X && pts_src[i].Y > center.Y)
                {
                    result[3] = pts_src[i];//左下角点
                    continue;
                }
            }

            return result;
        }

        /// <summary>
        /// 计算给定四个坐标点四边形的宽、高
        /// </summary>
        /// <param name="pts_src"></param>
        /// <returns></returns>
        public Size CalSizeOfRect(PointF[] pts_src)
        {
            if (pts_src.Length != 4) return new Size(0, 0);//确保为四边形

            //点集顺时针排序
            pts_src = SortPointsByClockwise(pts_src);

            //确定透视变换的宽度、高度
            int width;
            int height;

            double width1 = Math.Pow(pts_src[0].X - pts_src[1].X, 2) + Math.Pow(pts_src[0].Y - pts_src[1].Y, 2);
            double width2 = Math.Pow(pts_src[2].X - pts_src[3].X, 2) + Math.Pow(pts_src[2].Y - pts_src[3].Y, 2);

            width = width1 > width2 ? (int)Math.Sqrt(width1) : (int)Math.Sqrt(width2);//根号下a方+b方，且取宽度最大的

            double height1 = Math.Pow(pts_src[0].X - pts_src[3].X, 2) + Math.Pow(pts_src[0].Y - pts_src[3].Y, 2);
            double height2 = Math.Pow(pts_src[1].X - pts_src[2].X, 2) + Math.Pow(pts_src[1].Y - pts_src[2].Y, 2);

            height = height1 > height2 ? (int)Math.Sqrt(height1) : (int)Math.Sqrt(height2);

            return new Size(width, height);
        }

        /// <summary>
        /// Point转换为PointF类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PointF PointToPointF(Point p)
        {
            return new PointF(p.X, p.Y);
        }

        /// <summary>
        /// 形态学膨胀
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public Mat MyDilate(Mat mat)
        {
            //1.膨胀，改善轮廓
            Mat struct_element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross,
                new Size(3, 3), new Point(-1, -1));//结构元素
            Mat mat_dilate = new Mat();
            CvInvoke.MorphologyEx(mat, mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate, struct_element, new Point(-1, -1), 1,
                Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));//形态学膨胀

            return mat_dilate;
        }

        /// <summary>
        /// 筛选图中符合给定条件的轮廓
        /// </summary>
        /// <param name="mat_threshold">要提取轮廓的图片</param>
        /// <param name="width1">轮廓外接矩形大于该宽度值</param>
        /// <param name="width2">轮廓外接矩形小于该宽度值</param>
        /// <param name="height1">轮廓外接矩形大于该高度值</param>
        /// <param name="height2">轮廓外接矩形小于该高度值</param>
        public VectorOfVectorOfPoint GetUsefulContours(Mat mat, double ratio)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();//所有的轮廓
            VectorOfVectorOfPoint selected_contours = new VectorOfVectorOfPoint();//用于存储筛选过后的轮廓
            CvInvoke.FindContours(mat, contours, null, Emgu.CV.CvEnum.RetrType.List,
                Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);//提取所有轮廓，操作过程中会对输入图像进行修改

            //筛选轮廓。筛选条件：长宽比大于给定值
            for (int i = 0; i < contours.Size; i++)
            {
                Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);//外接矩形
                Mat temp = new Mat(mat, rect);//提取ROI矩形区域
                int pxNums = CvInvoke.CountNonZero(temp);//计算图像内非零像素个数

                double area = CvInvoke.ContourArea(contours[i]);//计算连通轮廓的面积
                double length = CvInvoke.ArcLength(contours[i], false); //计算连通轮廓的周长

                VectorOfPoint approx_curve = new VectorOfPoint();//用于存放逼近的结果
                CvInvoke.ApproxPolyDP(contours[i], approx_curve, length * 0.02, true);

                //外接矩形宽、高需在给定范围内
                bool bo = (rect.Width / rect.Height >= ratio && (rect.Width > 10 && rect.Height > 5) && area < 300);
                //bool bo = pxNums > 130 && pxNums < 300;
                if (bo)
                {
                    selected_contours.Push(contours[i]);
                }
            }

            return selected_contours;
        }

        /// <summary>
        ///按列根据给定的轮廓及范围信息计算涂卡的结果，返回int数组
        /// </summary>
        /// <param name="contours"></param>
        /// <param name="x"></param>
        /// <param name="x_interval"></param>
        /// <param name="x_num"></param>
        /// <param name="y"></param>
        /// <param name="y_interval"></param>
        /// <param name="y_num"></param>
        /// <returns></returns>
        public int[] GetColTargetValues(VectorOfVectorOfPoint contours, int x_begin, int x_interval, int x_num,
            int y_begin, int y_interval, int y_num)
        {
            int[] result = new int[x_num];//结果数组
            //数组初值默认为-1
            for (int i = 0; i < x_num; i++)
            {
                result[i] = -1;
            }
            int x_max = x_begin + x_interval * x_num;
            int y_max = y_begin + y_interval * y_num;
            VectorOfVectorOfPoint targetContours = new VectorOfVectorOfPoint();
            Point[] gravity = GetGravityOfContours(contours);//轮廓中心点坐标

            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                if (gravity[i].X < x_begin || gravity[i].X > x_max || gravity[i].Y < y_begin || gravity[i].Y > y_max)
                {
                    continue;//判断中心点是否超出范围
                }
                int x_id = (int)Math.Floor((double)(gravity[i].X - x_begin) / x_interval);//向下取整
                int value = (int)Math.Floor((double)(gravity[i].Y - y_begin) / y_interval);

                if (result[x_id] != -1)
                {
                    string str = string.Format("第{0}列存在多个答案！请擦拭干净后再扫描", x_id);
                    MessageBox.Show(str);
                }
                else
                    result[x_id] = value;
            }
            return result;
        }

        /// <summary>
        /// 按行根据给定的轮廓及范围信息计算涂卡的结果，返回int数组
        /// </summary>
        /// <param name="contours"></param>
        /// <param name="x_begin"></param>
        /// <param name="x_interval"></param>
        /// <param name="x_num"></param>
        /// <param name="y_begin"></param>
        /// <param name="y_interval"></param>
        /// <param name="y_num"></param>
        /// <returns></returns>
        public int[] GetRowTargetValues(VectorOfVectorOfPoint contours, int x_begin, int x_interval, int x_num,
           int y_begin, int y_interval, int y_num)
        {
            int[] result = new int[y_num];//结果数组
            //数组初值默认为-1
            for (int i = 0; i < y_num; i++)
            {
                result[i] = -1;
            }
            int x_max = x_begin + x_interval * x_num;
            int y_max = y_begin + y_interval * y_num;
            VectorOfVectorOfPoint targetContours = new VectorOfVectorOfPoint();
            Point[] gravity = GetGravityOfContours(contours);//轮廓中心点坐标

            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                if (gravity[i].X < x_begin || gravity[i].X > x_max || gravity[i].Y < y_begin || gravity[i].Y > y_max)
                {
                    continue;//判断中心点是否超出范围
                }
                int value = (int)Math.Floor((double)(gravity[i].X - x_begin) / x_interval);//向下取整
                int y_id = (int)Math.Floor((double)(gravity[i].Y - y_begin) / y_interval);

                if (result[y_id] != -1)
                {
                    string str = string.Format("第{0}行存在多个答案！请擦拭干净后再扫描", y_id);
                    MessageBox.Show(str);
                }
                else
                    result[y_id] = value;
            }
            return result;
        }

        /// <summary>
        /// 画出网格并返回填图结果
        /// </summary>
        /// <param name="img"></param>
        /// <param name="contours"></param>
        /// <param name="x_begin"></param>
        /// <param name="x_interval"></param>
        /// <param name="x_num"></param>
        /// <param name="y_begin"></param>
        /// <param name="y_interval"></param>
        /// <param name="y_num"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        public string GetValueAndDrawGrid(ImageBox img, VectorOfVectorOfPoint contours,
            int x_begin, int x_interval, int x_num, int y_begin, int y_interval, int y_num, string strText, bool iscol = true)
        {
            //画网格
            Mat src = new Image<Bgr, byte>(img.Image.Bitmap).Mat;
            Mat mat_grid = DrawGridByXY(img, x_begin, x_interval, x_num, y_begin, y_interval, y_num);

            int[] intArray = iscol ? GetColTargetValues(contours, x_begin, x_interval, x_num, y_begin, y_interval, y_num) : GetRowTargetValues(contours, x_begin, x_interval, x_num, y_begin, y_interval, y_num);
            int maxValue = GetMaxValueOfArray(intArray);//数组最大值

            string str = "";
            str += Environment.NewLine;//回车
            str += strText;
            if (maxValue >= 4)
            {
                str += GetStringOfIntArray(intArray);
            }
            else
            {
                str += GetStringOfIntArray(intArray, "ABCD");
            }

            return str;
        }

        /// <summary>
        /// 计算轮廓中心点坐标
        /// </summary>
        /// <param name="selected_contours">要计算中心点的轮廓</param>
        /// <returns></returns>
        public Point[] GetGravityOfContours(VectorOfVectorOfPoint selected_contours)
        {
            int ksize = selected_contours.Size;

            double[] m00 = new double[ksize];
            double[] m01 = new double[ksize];
            double[] m10 = new double[ksize];
            Point[] gravity = new Point[ksize];//用于存储轮廓中心点坐标
            MCvMoments[] moments = new MCvMoments[ksize];

            for (int i = 0; i < ksize; i++)
            {
                VectorOfPoint contour = selected_contours[i];
                //计算当前轮廓的矩
                moments[i] = CvInvoke.Moments(contour, false);

                m00[i] = moments[i].M00;
                m01[i] = moments[i].M01;
                m10[i] = moments[i].M10;
                int x = Convert.ToInt32(m10[i] / m00[i]);//计算当前轮廓中心点坐标
                int y = Convert.ToInt32(m01[i] / m00[i]);
                gravity[i] = new Point(x, y);
            }
            return gravity;
        }
        /// <summary>
        /// 根据给定XY初始坐标、间距、数量画网格
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="x"></param>
        /// <param name="x_interval"></param>
        /// <param name="x_num"></param>
        /// <param name="y"></param>
        /// <param name="y_interval"></param>
        /// <param name="y_num"></param>
        /// <returns></returns>
        public Mat DrawGridByXY(ImageBox img, int x_begin, int x_interval, int x_num, int y_begin, int y_interval, int y_num)
        {
            Mat src = new Image<Bgr, byte>(img.Image.Bitmap).Mat;

            //转换颜色空间
            Mat mat_color = new Mat();
            if (src.NumberOfChannels == 1)
                CvInvoke.CvtColor(src, mat_color, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);
            else
                mat_color = src;

            for (int i = 0; i <= x_num; i++)
            {
                //先画竖线
                Point p1 = new Point(x_begin + x_interval * i, y_begin);
                Point p2 = new Point(x_begin + x_interval * i, y_begin + y_interval * y_num);
                CvInvoke.Line(mat_color, p1, p2, new MCvScalar(0, 0, 255), 1);
            }

            for (int i = 0; i <= y_num; i++)
            {
                //再画横线
                Point p1 = new Point(x_begin, y_begin + y_interval * i);
                Point p2 = new Point(x_begin + x_interval * x_num, y_begin + y_interval * i);
                CvInvoke.Line(mat_color, p1, p2, new MCvScalar(0, 0, 255), 1);
            }

            img.Image = mat_color;
            return mat_color;
        }

        /// <summary>
        /// 画出给定轮廓
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="contours"></param>
        /// <returns></returns>
        public Mat DrawContours(Mat mat, VectorOfVectorOfPoint contours)
        {
            //转换颜色空间
            Mat mat_color = new Mat();
            CvInvoke.CvtColor(mat, mat_color, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            CvInvoke.DrawContours(mat_color, contours, -1, new MCvScalar(255, 0, 0), 2);
            return mat_color;
        }

        /// <summary>
        /// 拼接给定int数组内容，并返回拼接后字符串
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public string GetStringOfIntArray(int[] arr)
        {
            string str = "";
            foreach (int a in arr)
            {
                str += a.ToString() + " ";
            }
            return str;
        }

        /// <summary>
        /// 拼接给定int数组内容，并返回拼接后字符串
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public string GetStringOfIntArray(int[] arr, string ss = "ABCD")
        {
            string str = "";
            //char[] ch = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            char[] ch = ss.ToCharArray();
            foreach (int a in arr)
            {
                if (a == -1)
                {
                    str += "null ";//未识别时，标示为空
                }
                else
                {
                    str += ch[a] + " ";
                }

            }
            return str;
        }

        /// <summary>
        /// 获取一维int数组中最大值
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public int GetMaxValueOfArray(int[] arr)
        {
            int[] dst = new int[arr.Length];
            Array.Copy(arr, dst, arr.Length);//深度复制数组，防止排序对原数组产生影响
            Array.Sort(dst);//数组排序
            int maxValue = dst[arr.Length - 1];//数组最大值
            return maxValue;
        }
    }
}
