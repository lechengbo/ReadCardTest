using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTest.Service
{
    public class CVArea
    {
        public static int defatulThreshold = 180;
        public static int stepThreshold = 20;

        private Dictionary<int, List<Rectangle>> dicRectList;
        public CVArea(Rectangle area, Dictionary<int, List<Rectangle>> dicRectList, OptionType optionType = OptionType.Single,string name="")
        {
            this.Area = area;
            this.OptionType = OptionType;
            this.Name = name;
            this.SetQuestionList(dicRectList);
            this.dicRectList = dicRectList;
        }
        public Rectangle Area { get; private set; }
        public string Name { get; private set; }
        public OptionType OptionType { get; private set; }
        public List<CVQuestion> CVQuestionList { get; private set; }
        public void SetQuestionList(Dictionary<int, List<Rectangle>> dicRectList)
        {
            this.CVQuestionList = new List<CVQuestion>();
            foreach (var key in dicRectList.Keys)
            {
                var question = new CVQuestion(key, CVRectangle.NewList(dicRectList[key], this.Area.Location));
                this.CVQuestionList.Add(question);
            }
        }

        public void Recognition(Image<Gray, byte> src, int myThreshold = 180)
        {

            ////思路：第一次正常识别--->初步智能筛选---->再次智能筛选
            ////var src = new Image<Gray, byte>(bitmap);

            //var thresholdMat = src.CopyBlank().Mat;

            //CvInvoke.Threshold(src, thresholdMat, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

            ////new CommonUse().SaveMat(thresholdImage.Mat, "二值化hou");
            ////思路 open -膨胀
            ////形态学膨胀
            //Mat mat_dilate = CommonUse.MyDilateS(thresholdMat, Emgu.CV.CvEnum.MorphOp.Open);
            ////new CommonUse().SaveMat(mat_dilate, "Open行学");
            //mat_dilate = CommonUse.MyDilateS(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);
            //new CommonUse().SaveMat(mat_dilate, "Open行学");

            //补充偏移置换
            src = Max(src);

            foreach (var question in this.CVQuestionList)
            {
                for (int i = 0; i < question.OptionRectList.Count; i++)
                {
                    var cvRect = question.OptionRectList[i];
                    //计算平均灰度值
                    var fileName = $"{this.Name}第";
                    cvRect.CalVagGrayValueAndHist(src);

                    //var tmpRect = cvRect.Rectangle;
                    //var newRect = new Rectangle(Math.Max(0, tmpRect.X), Math.Max(0, tmpRect.Y), tmpRect.Width, tmpRect.Height);
                    //using (var tmpImage = new Mat(mat_dilate, newRect).ToImage<Gray, byte>())
                    //{
                    //    var result = CommonUse.GetWhiteColorPercenterS(tmpImage);
                    //    cvRect.AreaPercent = result;
                    //    //cvRect.IsAnwser = result > 0.3;
                    //    if (result > 0.3)
                    //    {
                    //        question.Results.Add(i);
                    //    }
                    //    //Console.WriteLine($"白色所占比：{result}");
                    //}

                    //计算面积比
                    cvRect.CalAreaPercent(src);
                    if (cvRect.AreaPercent > 0.3)
                    {
                        question.Results.Add(i);
                    }
                   
                }

            }
            //打印初步识别的
            //new CommonUse().DrawRectCircleAndSave(src.Mat.Clone(), this.GetRectList(), $"{this.Name}-初步识别的", -this.Area.X, -this.Area.Y, points: this.GetResultPointList(false));
            this.DrawDetail(src.Clone(),this.Name);
            //初步筛选
            this.Check();
            new CommonUse().DrawRectCircleAndSave(src.Mat.Clone(), this.GetRectList(false), $"{this.Name}-初步筛选结果", points: this.GetResultPointList(false));

            //再次智能处理异常的
            this.CVQuestionList.ForEach(q =>
            {
                this.IntelligentChose(src, q);
            });


        }

        /// <summary>
        /// 上下左右4分之一取最大
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image<Gray,byte> Max(Image<Gray,byte> image)
        {
            int maxX = image.Width, maxY = image.Height;
            this.CVQuestionList.ForEach(q =>
            {
                q.OptionRectList.ForEach(o =>
                {
                    int width = o.Rectangle.Width, height = o.Rectangle.Height;
                    int startX = o.Rectangle.X, startY = o.Rectangle.Y,
                    endX = startX +width, endY = startY + height,
                    maxExtensionDisX = width / 4, maxExtensionDisY = height / 4;
                    
                    //上下端
                    for (int x = startX; x < endX; x++)
                    {
                        //上端
                        for (int y = startY,oppositeY = y + height; y < startY+maxExtensionDisY && oppositeY<maxY; y++)
                        {
                            SetMaxColor(image, x, y, x,oppositeY);
                        }
                        //下端
                        for (int y = endY - maxExtensionDisY, oppositeY = y - height; y < endY; y++)
                        {
                            if (oppositeY < 0)
                            {
                                continue;
                            }
                            SetMaxColor(image, x, y, x, oppositeY);
                            //var val1 = image.Data[x, y, 0];
                            //var val2 = image.Data[x, oppositeY, 0];
                            //image.Data[x, y, 0] = Math.Min(val1, val2);
                        }

                    }
                    //左右端
                    for (int y = startY; y < endY; y++)
                    {
                        //左端
                        for (int x = startX,oppositeX=x+width; x < startX+maxExtensionDisX && oppositeX<maxX; x++)
                        {
                            SetMaxColor(image, x, y, oppositeX, y);
                        }
                        //右端
                        for (int x = endX-maxExtensionDisX,oppositeX=x-width; x < endX; x++)
                        {
                            if (oppositeX < 0)
                            {
                                continue;
                            }
                            SetMaxColor(image, x, y, oppositeX, y);
                        }
                    }
                   
                    
                });
            });
            return image;
        }

        private static void SetMaxColor(Image<Gray, byte> image, int x, int y, int oppositeX, int oppositeY)
        {
            var val1 = image.Data[y, x, 0];
            var val2 = image.Data[oppositeY, oppositeX, 0];
            image.Data[y, x, 0] = Math.Min(val1, val2);
        }

        /// <summary>
        /// 检查当前区域是否正常涂答,先初步排除一些明显的异常
        /// </summary>
        /// <returns></returns>
        public void Check()
        {
            foreach (var item in this.CVQuestionList)
            {
                item.Check(this.OptionType);
            }
            //var status = QuestionResultStatus.Right;
            //var errorStatusList = new List<QuestionResultStatus>();
            //foreach (var item in this.CVQuestionList)
            //{
            //    var resultStatus = item.Check(this.OptionType);
            //    if (resultStatus != QuestionResultStatus.Right)
            //    {
            //        errorStatusList.Add(resultStatus);
            //    }
            //}
            //if (errorStatusList.Count > 0)
            //{
            //    status = (errorStatusList.Contains(QuestionResultStatus.Absence) && errorStatusList.Contains(QuestionResultStatus.MultiAnwser)) ? QuestionResultStatus.Double : errorStatusList.FirstOrDefault();
            //}

            //return status;
        }

        public List<Point> GetResultPointList(bool needOffset = true)
        {
            List<Point> centerPoints = new List<Point>();
            this.CVQuestionList.ForEach(q =>
            {
                q.GetResultPoints().ForEach(p =>
                {
                    if (needOffset) p.Offset(this.Area.Location);
                    centerPoints.Add(p);
                });
                
            });
            
            return centerPoints;
        }
        public List<Rectangle> GetRectList(bool needOffset = true)
        {
            List<Rectangle> list = new List<Rectangle>();
            this.CVQuestionList.ForEach(q =>
            {
                q.OptionRectList.ForEach(cvr =>
                {
                    var r = cvr.Rectangle;
                    if (needOffset) r.Offset(this.Area.Location);
                    list.Add(r);
                });

            });

            return list;
            
        }
        /// <summary>
        /// 针对异常（通过初步智能处理后）的题目，在此处理
        /// 1、没有识别到的 增加阀门值，取最大面积的 +20
        /// 2、多识别的 减低阀门值，再比较取面积最大的和80%以内的 -20
        /// 3、取正常的所有面积平均，如果面积特别小的（小于平均的50%）算异常处理
        /// </summary>
        /// <param name="src">当前块的区域</param>
        /// <param name="question"></param>
        public void IntelligentChose(Image<Gray, byte> src, CVQuestion question)
        {
            if (question.ResultStatus == QuestionResultStatus.Right)
            {
                return;
            }

            int disThreshold = question.ResultStatus == QuestionResultStatus.Absence ? stepThreshold  : -stepThreshold;
            int currentThreshold = defatulThreshold + disThreshold;

            var optionList = question.NewOptionList(currentThreshold);
            //优化求结果和面积比
            for (int i = 0; i < optionList.Count; i++)
            {
                var currentCvRect = optionList[i];
                currentCvRect.CalAreaPercent(src);

                //using (var optionRectImage = src.Copy(currentCvRect.Rectangle))
                //{
                //    new CommonUse().SaveMat(optionRectImage.Mat, "异常选项原图");
                //    var thresholdMat = optionRectImage.CopyBlank().Mat;

                //    CvInvoke.Threshold(optionRectImage, thresholdMat, currentThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

                //    Mat mat_dilate = CommonUse.MyDilateS(thresholdMat, Emgu.CV.CvEnum.MorphOp.Open);
                //    mat_dilate = CommonUse.MyDilateS(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);
                //    new CommonUse().SaveMat(mat_dilate, "异常选项原图行学后");

                //    currentCvRect.AreaPercent = CommonUse.GetWhiteColorPercenterS(mat_dilate.ToImage<Gray, byte>());
                //    //if (currentCvRect.AreaPercent > 0.3)
                //    //{
                //    //    question.Results.Add(i);
                //    //}

                //}
            }

            question.IntelligentChose(optionList);


        }

        public void DrawDetail(Image<Gray, byte> image, string fileName="")
        {
            var colorMat = image.Convert<Bgr, byte>().Mat;
            new CommonUse().DrawRectCircleAndSave(colorMat, this.GetRectList(false), $"{this.Name}-标准识别结果",  points: this.GetResultPointList(false));

            var fontMat=Mat.Ones(colorMat.Rows*2+30 , colorMat.Cols*2, colorMat.Depth, colorMat.NumberOfChannels);
            var percentMat = Mat.Ones(colorMat.Rows * 2 + 30, colorMat.Cols * 2, colorMat.Depth, colorMat.NumberOfChannels);
            this.CVQuestionList.ForEach(q =>
            {
                q.OptionRectList.ForEach(o =>
                {
                    CvInvoke.PutText(fontMat, o.AvgGrayValue.ToString("f2"),new Point(o.Rectangle.Location.X*2, o.Rectangle.Location.Y*2+o.Rectangle.Height), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                    CvInvoke.PutText(fontMat, "Average Gray Value ", new Point(6, fontMat.Height - 30), FontFace.HersheySimplex, 0.6, new MCvScalar(255, 255, 255));
                    //面积咱比显示
                    CvInvoke.PutText(percentMat, o.AreaPercent.ToString("f2"), new Point(o.Rectangle.Location.X * 2, o.Rectangle.Location.Y * 2+o.Rectangle.Height), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                    CvInvoke.PutText(percentMat, "After  MorphOperated,the percent of area ", new Point(6, fontMat.Height - 30), FontFace.HersheySimplex, 0.6, new MCvScalar(255, 255, 255));
                });
            });
            //VectorOfMat matList = new VectorOfMat(colorMat, fontMat);
            //var mergeMat = new Mat();// Mat.Ones(colorMat.Rows*2, colorMat.Cols*2, colorMat.Depth, colorMat.NumberOfChannels); ;
            //CvInvoke.Add( fontMat, fontMat,mergeMat);
            
            new CommonUse().SaveMat(fontMat, $"{this.Name}-区域各平均灰度详细");
            new CommonUse().SaveMat(percentMat, $"{this.Name}-标准阀门值180后面积占比");

        }
        public void DrawDetail(Image<Gray, byte> image,int grayValue, string fileName = "")
        {
            var colorMat = image.Convert<Bgr, byte>().Mat;
            
            var fontMat = Mat.Ones(colorMat.Rows * 2 + 30, colorMat.Cols * 2, colorMat.Depth, colorMat.NumberOfChannels);
            this.CVQuestionList.ForEach(q =>
            {
                q.OptionRectList.ForEach(o =>
                {
                    CvInvoke.PutText(fontMat,o.GetHistPercentLessThan(grayValue).ToString("f2"), new Point(o.Rectangle.Location.X * 2, o.Rectangle.Location.Y * 2+o.Rectangle.Height), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                    CvInvoke.PutText(fontMat, $"The Percent Of Less than {grayValue} ", new Point(6, fontMat.Height - 30), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                });
            });
            //VectorOfMat matList = new VectorOfMat(colorMat, fontMat);
            //var mergeMat = new Mat();// Mat.Ones(colorMat.Rows*2, colorMat.Cols*2, colorMat.Depth, colorMat.NumberOfChannels); ;
            //CvInvoke.Add( fontMat, fontMat,mergeMat);

            new CommonUse().SaveMat(fontMat, $"{fileName}-灰度小于{grayValue}统计");

        }

    }

    public class CVQuestion
    {
        public static double lowestAreaPercent = 0.18;
        public CVQuestion(int id, List<CVRectangle> optionRectList)
        {
            this.Id = id;
            this.OptionRectList = optionRectList;

        }
        public int Id { get; private set; }
        public List<CVRectangle> OptionRectList { get; private set; }
        /// <summary>
        /// 答案标引 HashSet
        /// </summary>
        public List<int> Results { get; set; } = new List<int>();

        public QuestionResultStatus ResultStatus { get; set; } = QuestionResultStatus.Right;

        public QuestionResultStatus Check(OptionType optionType)
        {
            //先初步智能处理
            //1、没有选中的，取面积最大的，并大于0.18的
            //2、单选中多选的，去掉小于最大面积0.3的，，多选，去掉小于最大面积0.4的

            if (this.Results.Count == 0)
            {
                this.Results = GetAreaPerGreatThan18(optionType);

                if (this.Results.Count == 0)
                {
                    this.ResultStatus = QuestionResultStatus.Absence;
                }
                //return QuestionResultStatus.Absence;
            }
            else if (optionType == OptionType.Single && this.Results.Count > 1)
            {
                //多选时排除太小的，留下最大；//最大的0.25以下
                //var max = this.OptionRectList.Select(o => o.AreaPercent).Max();

                this.Results = this.Remove(optionType);
                if (this.Results.Count > 1)
                {
                    this.ResultStatus = QuestionResultStatus.MultiAnwser;
                }

                //return QuestionResultStatus.MultiAnwser;
            }

            return this.ResultStatus;
        }

        /// <summary>
        /// 没有选中的，取面积最大的，并大于0.18的,或者平均灰度230以下的在0.9以上，
        /// </summary>
        /// <param name="optionType"></param>
        /// <returns></returns>
        private List<int> GetAreaPerGreatThan18(OptionType optionType)
        {
            List<int> results = new List<int>();

            for (int i = 0; i < this.OptionRectList.Count; i++)
            {
                var r = this.OptionRectList[i];
                if (r.IsUnchanged())
                {
                    continue;
                }else if(r.GetHistPercentLessThan(230) >= 0.9)
                {
                    results.Add(i);
                    continue;
                }else if(r.AreaPercent < CVQuestion.lowestAreaPercent)
                {
                    continue;
                }
                    results.Add(i);
            }
            

            if (results.Count <= 1 || optionType == OptionType.Multi)
            {
                return results;
            }

            var maxPercent = this.OptionRectList.Select(o => o.AreaPercent).Max();
            var maxIndex = this.OptionRectList.FindIndex(c => c.AreaPercent == maxPercent);

            return new List<int>() { maxIndex };

        }

        /// <summary>
        /// 单选中多选的，去掉小于最大面积0.3的，，多选，去掉小于最大面积0.4的,180的面积占0.5以上算
        /// </summary>
        /// <param name="optionType"></param>
        /// <returns></returns>
        private List<int> Remove(OptionType optionType)
        {
            double disAreaPercent = optionType == OptionType.Single ? 0.3 : 0.4;
            var maxPercent = this.OptionRectList.Select(o => o.AreaPercent).Max();
            var lowestPercent = maxPercent - disAreaPercent;
            lowestPercent = Math.Max(lowestPercent, lowestAreaPercent);
            lowestPercent = Math.Min(lowestPercent, 0.5);

            var results = new List<int>();
            for (int i = 0; i < this.OptionRectList.Count; i++)
            {
                if (this.OptionRectList[i].IsUnchanged())
                {
                    continue;
                }
                if (this.OptionRectList[i].AreaPercent >= lowestPercent)
                {
                    results.Add(i);
                }
            }
            return results;
        }

        public void IntelligentChose(List<CVRectangle> otherOptionList)
        {
            if (this.ResultStatus == QuestionResultStatus.Absence)
            {
                IntelligentChoseByAbsence(otherOptionList);
            }
            else if (this.ResultStatus == QuestionResultStatus.MultiAnwser)
            {
                IntelligentChoseByMulti(otherOptionList);
            }
        }
        private void IntelligentChoseByAbsence(List<CVRectangle> otherOptionList)
        {
            //挑选两次面积和最大的，并且必须大于0.5,且第二次大于0.5
            int count = Math.Min(this.OptionRectList.Count, otherOptionList.Count);
            int maxIndex = 0;
            double maxPercent = 0;
            for (int i = 0; i < count; i++)
            {
                if (this.OptionRectList[i].IsUnchanged())
                {
                    continue;
                }
                var currentIndex = i;
                var currentPercent = this.OptionRectList[i].AreaPercent + otherOptionList[i].AreaPercent;
                if (currentPercent > maxPercent)
                {
                    maxPercent = currentPercent;
                    maxIndex = currentIndex;
                }
            }

            if (maxPercent > 0.6 && otherOptionList[maxIndex].AreaPercent>0.5)
            {
                this.Results = new List<int>() { maxIndex };
            }

        }
        private void IntelligentChoseByMulti(List<CVRectangle> otherOptionList)
        {
            //挑选两次面积和最大的，挑选面积最大的和她0.5以内的或者平均灰度在170一下的
            var newResults = new List<int>();
            List<double> percentSumList = new List<double>();
            int count = Math.Min(this.OptionRectList.Count, otherOptionList.Count);
            for (int i = 0; i < count; i++)
            {
                
                var currentSum = this.OptionRectList[i].AreaPercent + otherOptionList[i].AreaPercent;
                
                percentSumList.Add(currentSum);
            }
            double maxPercent = percentSumList.Max();

            
            for (int i = 0; i < percentSumList.Count; i++)
            {
                var r = this.OptionRectList[i];
                if (r.IsUnchanged())
                {
                    continue;
                }
                if (maxPercent - percentSumList[i] < 0.5 || r.GetHistPercentLessThan(220)>0.8)
                {
                    newResults.Add(i);
                }
            }
           
            //求交集
            this.Results = this.Results.Intersect(newResults).ToList();
            //this.Results = newResults;
        }

        public List<CVRectangle> NewOptionList(int threshold)
        {
            List<CVRectangle> newOptions = new List<CVRectangle>();
            this.OptionRectList.ForEach(o =>
            {
                newOptions.Add(new CVRectangle(o.Rectangle) { Threshold=threshold});
            });
            return newOptions;
        }

        public List<Point> GetResultPoints()
        {
            List<Point> points = new List<Point>();
            this.Results.ForEach(r =>
            {
                var rect = this.OptionRectList[r].Rectangle;
                points.Add(new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
            });

            return points;
        }
    }
    public class CVRectangle
    {
        public CVRectangle(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
        }
        public Rectangle Rectangle { get; private set; }
        public double AreaPercent { get; set; }
        public double AvgGrayValue { get; private set; }
        public List<int> HistDic { get; private set; } = new List<int>();
        public int Threshold { get; set; } = CVArea.defatulThreshold;
        //public bool IsAnwser { get; set; }

        public static List<CVRectangle> NewList(List<Rectangle> rectList, Point offset)
        {
            List<CVRectangle> cVRectangles = new List<CVRectangle>();
            rectList?.ForEach(r =>
            {
                r.Offset(-offset.X, -offset.Y);
                cVRectangles.Add(new CVRectangle(r));
            });

            return cVRectangles;
        }

        /// <summary>
        /// 计算占用比
        /// </summary>
        /// <param name="image">所在区域的图片</param>
        public void CalAreaPercent(Image<Gray,byte> image, string saveName="")
        {

            var newRect = new Rectangle(Math.Max(0, this.Rectangle.X), Math.Max(0, this.Rectangle.Y), this.Rectangle.Width, this.Rectangle.Height);
            using (var optionRectImage = image.Copy(newRect))
            {
                var thresholdMat = optionRectImage.CopyBlank().Mat;

                CvInvoke.Threshold(optionRectImage, thresholdMat, this.Threshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

                Mat mat_dilate = CommonUse.MyDilateS(thresholdMat, Emgu.CV.CvEnum.MorphOp.Open);
                mat_dilate = CommonUse.MyDilateS(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);
                //new CommonUse().SaveMat(mat_dilate, $"异常选项原图行学后-{saveName}");

                this.AreaPercent = CommonUse.GetWhiteColorPercenterS(mat_dilate.ToImage<Gray, byte>());
                //Console.WriteLine($"白色所占比：{this.AreaPercent}");

            }
            
        }

        /// <summary>
        /// 计算平均灰度
        /// </summary>
        /// <param name="img">所在区域的图片</param>
        public void CalVagGrayValueAndHist(Image<Gray,byte> img, string saveName = "")
        {
            var newRect = new Rectangle(Math.Max(0, this.Rectangle.X), Math.Max(0, this.Rectangle.Y), this.Rectangle.Width, this.Rectangle.Height);

            List<int> grayValueList = new List<int>();
            using (var tmpImage = img.Copy(newRect))
            {
                var size = tmpImage.Size;
                for (int i = 0; i < size.Height; i++)
                {
                    for (int j = 0; j < size.Width; j++)
                    {
                        var tmpValue = tmpImage.Data[i, j, 0];
                        grayValueList.Add(tmpValue);
                       
                    }
                }
            }

            this.HistDic = grayValueList;
            this.AvgGrayValue = grayValueList.Average();
            Console.WriteLine(this.AvgGrayValue);
        }

        /// <summary>
        /// 获取小于该灰度值的，也就是黑的占比
        /// </summary>
        /// <param name="grayValue"></param>
        /// <returns></returns>
        public double GetHistPercentLessThan(int grayValue)
        {
            if(grayValue>255 || grayValue < 0)
            {
                return 0;
            }

            int total = 0;
            int lessThanGray = 0;
            foreach (var item in this.HistDic)
            {
                total += item;
                if (item <= grayValue)
                {
                    lessThanGray += item;
                }
            }

            return lessThanGray * 1.0 / total;
        }

        /// <summary>
        /// 是否为涂改，或者涂改的很小
        /// 平均灰度大于210或200就是空白的，没有涂的
        ///230灰度的以上的超过0.21就是空白
        /// </summary>
        /// <returns></returns>
        public bool IsUnchanged()
        {
            return this.AvgGrayValue > 200 || this.GetHistPercentLessThan(230) < 0.79;
        }

    }

    public enum OptionType
    {
        /// <summary>
        /// 单选
        /// </summary>
        Single,
        /// <summary>
        /// 多选
        /// </summary>
        Multi
    }
    /// <summary>
    /// 区域内选项状态
    /// </summary>
    public enum QuestionResultStatus
    {
        /// <summary>
        /// 正确
        /// </summary>
        Right,
        /// <summary>
        /// 多选了
        /// </summary>
        MultiAnwser,
        /// <summary>
        /// 没有任何选择
        /// </summary>
        Absence,
        /// <summary>
        /// 既有多选的也有漏选的
        /// </summary>
        Double
    }
}
