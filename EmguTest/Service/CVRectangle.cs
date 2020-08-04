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
        public static int maxThreshold = 225;
        public static int minThreshold = 150;

        public static int defatulThreshold = 175;
        public static int stepThreshold = 20;
        public static double valAreaPercent = 0.44;
        public static int extensionWBase = 5;//在宽度上的扩展的基数，也就是X轴 越大越小
        public static int extensionHBase = 4;//在宽度上的扩展的基数，也就是Y轴


        private Dictionary<int, List<Rectangle>> dicRectList;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="dicRectList">该区域的小框，相对图片的坐标，而不是区域的</param>
        /// <param name="optionType"></param>
        /// <param name="name"></param>
        public CVArea(Rectangle area, Dictionary<int, List<Rectangle>> dicRectList, OptionType optionType = OptionType.Single, string name = "")
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
        public void SetName(string last)
        {
            this.Name += $"-{last}-";// last;
        }
        public void SetQuestionList(Dictionary<int, List<Rectangle>> dicRectList)
        {
            this.CVQuestionList = new List<CVQuestion>();
            foreach (var key in dicRectList.Keys)
            {
                var question = new CVQuestion(key, CVRectangle.NewList(dicRectList[key], this.Area.Location));
                this.CVQuestionList.Add(question);
            }
        }

        /// <summary>
        /// 该区域的图片
        /// </summary>
        /// <param name="src"></param>
        /// <param name="myThreshold"></param>
        public void Recognition(Image<Gray, byte> src, int myThreshold = 180)
        {

            ////思路：第一次正常识别--->初步智能筛选---->再次智能筛选---》再次智能(如果未选，继续多次，如果多选)
            ////var src = new Image<Gray, byte>(bitmap);

            var thresholdMat = src.CopyBlank().Mat;

            CvInvoke.Threshold(src, thresholdMat, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

            new CVHelper().SaveMat(src.Mat, "原始的");
            //思路 open -膨胀
            //形态学膨胀
            Mat mat_dilate = CVHelper.MyDilateS(thresholdMat, Emgu.CV.CvEnum.MorphOp.Open);
            new CVHelper().SaveMat(mat_dilate, "Open行学");
            mat_dilate = CVHelper.MyDilateS(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);
            new CVHelper().SaveMat(mat_dilate, "Dilate行学");

            //补充偏移置换
            //src = Max(src);

            foreach (var question in this.CVQuestionList)
            {
                for (int i = 0; i < question.OptionRectList.Count; i++)
                {
                    var cvRect = question.OptionRectList[i];
                    //计算平均灰度值
                    //var fileName = $"{this.Name}第";
                    cvRect.CalVagGrayValueAndHist(src);

                   
                    //计算面积比
                    cvRect.CalAreaPercent(src);
                    if (cvRect.AreaPercent > CVArea.valAreaPercent)
                    {
                        question.Results.Add(i);
                    }

                }

            }
            //打印初步识别的
            //new CommonUse().DrawRectCircleAndSave(src.Mat.Clone(), this.GetRectList(), $"{this.Name}-初步识别的", -this.Area.X, -this.Area.Y, points: this.GetResultPointList(false));
            this.DrawDetail(src.Clone(), this.Name);
            //初步筛选
            this.Check();
            new CVHelper().DrawRectCircleAndSave(src.Mat.Clone(), this.GetRectList(false), $"{this.Name}-初步筛选结果", points: this.GetResultPointList(false));

            //再次智能处理异常的
            this.CVQuestionList.ForEach(q =>
            {
                this.IntelligentChose(src, q);

                int total = 11;
                for (int i = 2; i <= total; i++)
                {
                    if(q.ResultStatus== QuestionResultStatus.Right)
                    {
                        break;
                    }
                    else if (q.ResultStatus == QuestionResultStatus.Absence)
                    {
                        q.IntelligentChoseByAbsence(src, i);

                    }
                }

            });

            //再次通过神经网络识别

            //this.RecognitionByML();
            for (int i = 0; i < this.CVQuestionList.Count; i++)
            {
                var q = this.CVQuestionList[i];
                if (q.ResultStatus != QuestionResultStatus.Absence)
                {
                    continue;
                }
                //针对只有一个的选框的不适用人工智能网络)
                //if (this.GetOptionCount() == 1)
                //{
                //    continue;
                //}

                for (int j = 0; j < q.OptionRectList.Count; j++)
                {
                    var rect = q.OptionRectList[j];
                    if (rect.AreaPercent == 0 && rect.AvgGrayValue == 0)
                    {
                        continue;
                    }
                    //智能识别
                    var queRect = rect.Rectangle;
                    using(var copy = src.Copy(queRect))
                    {
                        var isAnswer = MLearner.IsAnswer(copy);
                        if (!q.Results.Contains(j) && isAnswer)
                        {
                            q.Results.Add(j);
                        }
                        new CVHelper().SaveMat(copy.Mat, "需要智能之别的" + isAnswer,dictory:"MLReg\\");
                    }
                    
                }
            }

            //脱离原rectangle，再次智能筛选
            try { 
             DoMLAgain(src);
            }
            catch (Exception ex)
            {
                Console.WriteLine("再次智能之别是，发生错误");
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 再次智能识别
        /// </summary>
        /// <param name="src"></param>
        private void DoMLAgain(Image<Gray, byte> src)
        {
            //正对使用了偏移块还不准的区域再次识别
            if (!this.IsAbsence())
            {
                return;
            }

            var cvHelper = new CVHelper();
            var rectList = cvHelper.GetRectListFromBitmap(src.Bitmap, isAutoFillFull: true);
            if (rectList.Count != this.GetOptionCount())// || rectList.Count==1 针对只有一个的选框的不适用
            {
                return;
            }
            var rectListDic = cvHelper.OrderRectList(rectList, this.IsOrderByRow());
            var anwserDir = new Dictionary<int, List<int>>();
            foreach (var key in rectListDic.Keys)
            {
                var queOptions = rectListDic[key];
                var tmpResult = new List<int>();
                for (int i = 0; i < queOptions.Count; i++)
                {
                    var queRect = queOptions[i];
                    using (var copy = src.Copy(queRect))
                    {
                        var isAnswer = MLearner.IsAnswer(copy);
                        if (isAnswer)
                        {
                            tmpResult.Add(i);
                        }

#if DEBUG
                        new CVHelper().SaveMat(copy.Mat, "再次智能之别的" + isAnswer, dictory: "MLReg2\\");
#endif
                    }
                }
                anwserDir.Add(key, tmpResult);
            }
            //合并答案
            
                foreach (var key in anwserDir.Keys)
                {
                    var result = anwserDir[key];
                    if(this.CVQuestionList[key - 1].ResultStatus== QuestionResultStatus.Absence){
                        this.CVQuestionList[key - 1].Results = result;
                    }
                    
                }
           


#if DEBUG
            //画出再次智能出来的框和识别出来的中心点
            var tmpRectList = new List<Rectangle>();
            var tmpPointList = new List<Point>();
            foreach (var key in rectListDic.Keys)
            {
                tmpRectList.AddRange(rectListDic[key]);
                anwserDir[key].ForEach(i =>
                {
                    var rect = rectListDic[key][i];
                    var point = rect.Location;
                    point.Offset(rect.Width / 2, rect.Height / 2);
                    tmpPointList.Add(point);
                });
            }

            cvHelper.DrawRectCircleAndSave(src.Mat, tmpRectList, "再次智能后", points: tmpPointList, dictory: "MLReg2\\");
#endif
        }

        private bool IsOrderByRow()
        {

            var que = this.CVQuestionList.FirstOrDefault();
            if (que == null || que.OptionRectList.Count == 0)
            {
                return true;
            }
            var first = que.OptionRectList.FirstOrDefault();
            var last = que.OptionRectList.LastOrDefault();
            return (last.Rectangle.X - first.Rectangle.X > last.Rectangle.Y - first.Rectangle.Y);

        }
        public bool IsAbsence()
        {
            for (int i = 0; i < this.CVQuestionList.Count; i++)
            {
                var que = CVQuestionList[i];
                if (que.ResultStatus == QuestionResultStatus.Absence)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 获取该区域内所有的小矩形框
        /// </summary>
        /// <returns></returns>
        private int GetOptionCount()
        {
            var first = this.CVQuestionList.FirstOrDefault();
            if (first == null)
            {
                return 0;
            }
            return this.CVQuestionList.Count * first.OptionRectList.Count;
        }

        public void RecognitionByML(Image<Gray,byte> src)
        {
            for (int i = 0; i < this.CVQuestionList.Count; i++)
            {
                var q = this.CVQuestionList[i];
                if (q.ResultStatus != QuestionResultStatus.Absence)
                {
                    continue;
                }

                for (int j = 0; j < q.OptionRectList.Count; j++)
                {
                    var rect = q.OptionRectList[j];
                    
                    //智能识别
                    var queRect = rect.Rectangle;
                    queRect.Offset(this.Area.Location);
                    using (var copy = src.Copy(queRect))
                    {
                        var isAnswer = MLearner.IsAnswer(copy);
                        if (!q.Results.Contains(j) && isAnswer)
                        {
                            q.Results.Add(j);
                        }
                        new CVHelper().SaveMat(copy.Mat, "需要智能之别的" + isAnswer);
                    }

                }
            }
        }

        /// <summary>
        /// 上下左右4分之一取最大
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image<Gray, byte> Max(Image<Gray, byte> image)
        {
            int maxX = image.Width, maxY = image.Height;

            for (int i = 0; i < this.CVQuestionList.Count; i++)
            {

                var q = this.CVQuestionList[i];

                if (q.OptionRectList.Count <= 1)
                {
                    continue;
                }
                q.OptionRectList.ForEach(o =>
                {
                    int width = o.Rectangle.Width, height = o.Rectangle.Height;
                    int startX = o.Rectangle.X, startY = o.Rectangle.Y,
                    endX = startX + width, endY = startY + height,
                    maxExtensionDisX = width / 5, maxExtensionDisY = height / 5;

                    //上下端
                    for (int x = startX; x < endX; x++)
                    {
                        //上端
                        for (int y = startY, oppositeY = y + height; y < startY + maxExtensionDisY && oppositeY < maxY; y++)
                        {
                            SetMaxColor(image, x, y, x, oppositeY);
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
                        for (int x = startX, oppositeX = x + width; x < startX + maxExtensionDisX && oppositeX < maxX; x++)
                        {
                            SetMaxColor(image, x, y, oppositeX, y);
                        }
                        //右端
                        for (int x = endX - maxExtensionDisX, oppositeX = x - width; x < endX; x++)
                        {
                            if (oppositeX < 0)
                            {
                                continue;
                            }
                            SetMaxColor(image, x, y, oppositeX, y);
                        }
                    }


                });
            };
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



            int disThreshold = question.ResultStatus == QuestionResultStatus.Absence ? stepThreshold + 5 : -stepThreshold;
            int currentThreshold = defatulThreshold + disThreshold;


            var optionList = question.NewOptionList(currentThreshold);
            //优化求结果和面积比
            for (int i = 0; i < optionList.Count; i++)
            {
                var currentCvRect = optionList[i];
                currentCvRect.CalAreaPercent(src);

                
            }

            question.IntelligentChose(optionList);

            question.Check();


        }

        public void DrawDetail(Image<Gray, byte> image, string fileName = "")
        {
            var colorMat = image.Convert<Bgr, byte>().Mat;
            new CVHelper().DrawRectCircleAndSave(colorMat, this.GetRectList(false), $"{this.Name}-标准识别结果", points: this.GetResultPointList(false));

            var fontMat = Mat.Ones(colorMat.Rows * 2 + 30, colorMat.Cols * 2, colorMat.Depth, colorMat.NumberOfChannels);
            var percentMat = Mat.Ones(colorMat.Rows * 2 + 30, colorMat.Cols * 2, colorMat.Depth, colorMat.NumberOfChannels);
            this.CVQuestionList.ForEach(q =>
            {
                q.OptionRectList.ForEach(o =>
                {
                    CvInvoke.PutText(fontMat, o.AvgGrayValue.ToString("f2"), new Point(o.Rectangle.Location.X * 2, o.Rectangle.Location.Y * 2 + o.Rectangle.Height), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                    CvInvoke.PutText(fontMat, "Average Gray Value ", new Point(6, fontMat.Height - 30), FontFace.HersheySimplex, 0.6, new MCvScalar(255, 255, 255));
                    //面积咱比显示
                    CvInvoke.PutText(percentMat, o.AreaPercent.ToString("f2"), new Point(o.Rectangle.Location.X * 2, o.Rectangle.Location.Y * 2 + o.Rectangle.Height), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                    CvInvoke.PutText(percentMat, "After  MorphOperated,the percent of area ", new Point(6, fontMat.Height - 30), FontFace.HersheySimplex, 0.6, new MCvScalar(255, 255, 255));
                });
            });
            //VectorOfMat matList = new VectorOfMat(colorMat, fontMat);
            //var mergeMat = new Mat();// Mat.Ones(colorMat.Rows*2, colorMat.Cols*2, colorMat.Depth, colorMat.NumberOfChannels); ;
            //CvInvoke.Add( fontMat, fontMat,mergeMat);

            new CVHelper().SaveMat(fontMat, $"{this.Name}-区域各平均灰度详细");
            new CVHelper().SaveMat(percentMat, $"{this.Name}-标准阀门值180后面积占比");

        }
        public void DrawDetail(Image<Gray, byte> image, int grayValue, string fileName = "")
        {
            var colorMat = image.Convert<Bgr, byte>().Mat;

            var fontMat = Mat.Ones(colorMat.Rows * 2 + 30, colorMat.Cols * 2, colorMat.Depth, colorMat.NumberOfChannels);
            this.CVQuestionList.ForEach(q =>
            {
                q.OptionRectList.ForEach(o =>
                {
                    CvInvoke.PutText(fontMat, o.GetHistPercentLessThan(grayValue).ToString("f2"), new Point(o.Rectangle.Location.X * 2, o.Rectangle.Location.Y * 2 + o.Rectangle.Height), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                    CvInvoke.PutText(fontMat, $"The Percent Of Less than {grayValue} ", new Point(6, fontMat.Height - 30), FontFace.HersheySimplex, 0.4, new MCvScalar(255, 255, 255));
                });
            });
            //VectorOfMat matList = new VectorOfMat(colorMat, fontMat);
            //var mergeMat = new Mat();// Mat.Ones(colorMat.Rows*2, colorMat.Cols*2, colorMat.Depth, colorMat.NumberOfChannels); ;
            //CvInvoke.Add( fontMat, fontMat,mergeMat);

            new CVHelper().SaveMat(fontMat, $"{fileName}-灰度小于{grayValue}统计");

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
                //this.Results = GetAreaPerGreatThan18(optionType);

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
        /// 没有选中的，取面积最大的，并大于0.18的,//或者平均灰度230以下的在0.9以上，
        /// </summary>
        /// <param name="optionType"></param>
        /// <returns></returns>
        private List<int> GetAreaPerGreatThan18(OptionType optionType)
        {
            List<int> results = new List<int>();

            for (int i = 0; i < this.OptionRectList.Count; i++)
            {
                var r = this.OptionRectList[i];
                //if (r.IsUnchanged())
                //{
                //    continue;
                //}else if(r.GetHistPercentLessThan(230) >= 0.9)
                //{
                //    results.Add(i);
                //    continue;
                //}else 
                if (r.AreaPercent < CVQuestion.lowestAreaPercent)
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
        /// 单选中多选的，去掉小于最大面积0.3的，，多选，去掉小于最大面积0.4的,且小于0.4
        /// </summary>
        /// <param name="optionType"></param>
        /// <returns></returns>
        private List<int> Remove(OptionType optionType)
        {
            double disAreaPercent = optionType == OptionType.Single ? 0.3 : 0.4;
            var maxPercent = this.OptionRectList.Select(o => o.AreaPercent).Max();
            var lowestPercent = maxPercent - disAreaPercent;
            lowestPercent = Math.Min(lowestPercent, 0.4);
            lowestPercent = Math.Max(lowestAreaPercent, lowestPercent);

            var results = new List<int>();
            for (int i = 0; i < this.OptionRectList.Count; i++)
            {
                //if (this.OptionRectList[i].IsUnchanged())
                //{
                //    continue;
                //}
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
                //return;
                IntelligentChoseByMulti(otherOptionList);
            }
        }
        private void IntelligentChoseByAbsence(List<CVRectangle> otherOptionList)
        {
            //挑选两次面积和最大的，并且必须大于0.55,且第二次大于0.5
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

            if (maxPercent >= 0.5 && otherOptionList[maxIndex].AreaPercent >= 0.5)
            {
                this.Results = new List<int>() { maxIndex };
            }

        }

        public void IntelligentChoseByAbsence(Image<Gray,byte> src,int times)
        {

            if (this.ResultStatus == QuestionResultStatus.Right)
            {
                return;
            }



            int disThreshold = CVArea.stepThreshold + 5 * times;
            int currentThreshold = CVArea.defatulThreshold + disThreshold;

            if (currentThreshold > CVArea.maxThreshold)
            {
                return;
            }

            var optionList = NewOptionList(currentThreshold);
            //优化求结果和面积比
            for (int i = 0; i < optionList.Count; i++)
            {
                if (this.OptionRectList[i].IsUnchanged())
                {
                    continue;
                }

                var currentCvRect = optionList[i];
                currentCvRect.CalAreaPercent(src);

                this.OptionRectList[i].AreaPercent = currentCvRect.AreaPercent;
                this.OptionRectList[i].Threshold = currentCvRect.Threshold;

                if (currentCvRect.AreaPercent > 0.7 && !this.Results.Contains(i))
                {
                    this.Results.Add(i);
                }

            }

            this.Check();
           
        }


        private void IntelligentChoseByMulti(List<CVRectangle> otherOptionList)
        {
            //挑选两次面积和最大的，挑选面积最大的和她0.5以内的或者平均灰度在170一下的
            //for (int i = 0; i < this.Results.Count; i++)
            //{
            //    int index = this.Results[i];
            //    if (this.OptionRectList[index].AreaPercent > 0.5 || otherOptionList[index].AreaPercent > 0.4)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        this.Results.RemoveAt(i);
            //        i--;
            //    }
            //}

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
                //如果平均灰度小于180且平均面积大于0.5也算选中
                if (((maxPercent - percentSumList[i] < 0.5 || r.AvgGrayValue < 180) && (maxPercent < percentSumList[i] * 2))
                    || r.AreaPercent > 0.5  )
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
                newOptions.Add(new CVRectangle(o.Rectangle) { Threshold = threshold });
            });
            return newOptions;
        }

        public List<Point> GetResultPoints()
        {
            List<Point> points = new List<Point>();
            var tmpcount = this.OptionRectList.Count;
            this.Results.ForEach(r =>
            {
                if (r < tmpcount)
                {
                    var rect = this.OptionRectList[r].Rectangle;
                    points.Add(new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
                }

            });

            return points;
        }
        public List<Rectangle> GetResultRect()
        {
            List<Rectangle> rectList = new List<Rectangle>();

            this.Results.ForEach(r =>
            {
                var rect = this.OptionRectList[r].Rectangle;
                rectList.Add(rect);
            });
            return rectList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isResult"></param>
        /// <returns></returns>
        public List<Rectangle> GetRect(Point offset, bool isResult = true)
        {
            List<Rectangle> rectList = new List<Rectangle>();

            for (int i = 0; i < this.OptionRectList.Count; i++)
            {
                var option = this.OptionRectList[i];
                if (this.Results.Contains(i) && isResult)
                {
                    var tmpRect = option.Rectangle;
                    tmpRect.Offset(offset.X, offset.Y);
                    rectList.Add(tmpRect);
                }
                else if (!this.Results.Contains(i) && !isResult)
                {
                    var tmpRect = option.Rectangle;
                    tmpRect.Offset(offset.X, offset.Y);
                    rectList.Add(tmpRect);
                }
            }
            return rectList;
        }



        public void Check()
        {

            int count = this.Results.Count;
            if (count == 0)
            {
                this.ResultStatus = QuestionResultStatus.Absence;
            }
            else if (count == 1)
            {
                this.ResultStatus = QuestionResultStatus.Right;
            }
            else if(count>=2)
            {
                this.ResultStatus = QuestionResultStatus.MultiAnwser;
            }
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
        public void CalAreaPercent(Image<Gray, byte> image, string saveName = "")
        {

            //image.Save("ssss.jpg");
            int maxX = image.Width, maxY = image.Height;
            int width = this.Rectangle.Width, height = this.Rectangle.Height;
            int xExtensionDis = width / CVArea.extensionWBase, yExtensionDis = height / CVArea.extensionHBase;
            var extensionRect = new Rectangle(Math.Max(0, this.Rectangle.X - xExtensionDis), Math.Max(0, this.Rectangle.Y - yExtensionDis), this.Rectangle.Width + 2 * xExtensionDis, this.Rectangle.Height + 2 * yExtensionDis);
            

            var newRect = new Rectangle(new Point(this.Rectangle.X - extensionRect.X, this.Rectangle.Y - extensionRect.Y), this.Rectangle.Size);

            if (extensionRect.Right > maxX)
            {
                extensionRect.Width -= extensionRect.Right - maxX;
            }
            if (extensionRect.Bottom > maxY)
            {
                extensionRect.Height -= extensionRect.Bottom - maxY;
            }
            using (var extOptionRectImage = image.Copy(extensionRect))
            {
                var thresholdMat = extOptionRectImage.CopyBlank().Mat;
                //CvInvoke.Threshold(CVHelper.Filter( extOptionRectImage.Mat), thresholdMat, this.Threshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

                CvInvoke.Threshold(extOptionRectImage, thresholdMat, this.Threshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);

                Mat mat_dilate = CVHelper.MyDilateS(thresholdMat, Emgu.CV.CvEnum.MorphOp.Open);
                mat_dilate = CVHelper.MyDilateS(mat_dilate, Emgu.CV.CvEnum.MorphOp.Dilate);

                new CVHelper().SaveMat(extOptionRectImage.Mat, $"原始选项原图行学后-{saveName}");
                new CVHelper().SaveMat(mat_dilate, $"异常选项原图行学后-{saveName}");
                using (var originalImage = this.Max(mat_dilate.ToImage<Gray, byte>(), newRect))
                {
                    this.AreaPercent = CVHelper.GetWhiteColorPercenterS(originalImage);
                    Console.WriteLine($"{this.Threshold}下的白色所占比：{this.AreaPercent}");

                }



            }

        }
        /// <summary>
        /// 上下左右4分之一取最大
        /// </summary>
        /// <param name="image">上下左右扩大后的</param>
        /// <param name="originalRect">原始的框，单起始点坐标是相对该image的</param>
        /// <returns></returns>
        private Image<Gray, byte> Max(Image<Gray, byte> image, Rectangle originalRect)
        {
            int maxX = image.Width, maxY = image.Height;

            int width = originalRect.Width, height = originalRect.Height;
            int startX = originalRect.X, startY = originalRect.Y,
            endX = startX + width, endY = startY + height,
            maxExtensionDisX = width / CVArea.extensionWBase, maxExtensionDisY = height / CVArea.extensionHBase;

            //上下端
            for (int x = startX; x < endX; x++)
            {
                //上端
                for (int y = startY, oppositeY = y + height; y < startY + maxExtensionDisY && oppositeY < maxY; y++)
                {
                    if (oppositeY >= maxY)
                    {
                        break;
                    }
                    SetMaxColor(image, x, y, x, oppositeY);
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
                for (int x = startX, oppositeX = x + width; x < startX + maxExtensionDisX && oppositeX < maxX; x++)
                {
                    if (oppositeX >= maxX)
                    {
                        break;
                    }
                    SetMaxColor(image, x, y, oppositeX, y);
                }
                //右端
                for (int x = endX - maxExtensionDisX, oppositeX = x - width; x < endX; x++)
                {
                    if (oppositeX < 0)
                    {
                        continue;
                    }
                    SetMaxColor(image, x, y, oppositeX, y);
                }
            }
            new CVHelper().DrawRectCircleAndSave(image.Mat, new List<Rectangle>() { originalRect }, "");
            return image.Copy(originalRect);
        }

        private static void SetMaxColor(Image<Gray, byte> image, int x, int y, int oppositeX, int oppositeY)
        {
            if (oppositeX < 0 || oppositeY < 0 || oppositeX >= image.Width || oppositeY >= image.Height)
            {
                return;
            }
            var val1 = image.Data[y, x, 0];
            var val2 = image.Data[oppositeY, oppositeX, 0];
            image.Data[y, x, 0] = Math.Max(val1, val2);
        }

        /// <summary>
        /// 计算平均灰度
        /// </summary>
        /// <param name="img">所在区域的图片</param>
        public void CalVagGrayValueAndHist(Image<Gray, byte> img, string saveName = "")
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
            //Console.WriteLine(this.AvgGrayValue);
        }

        /// <summary>
        /// 获取小于该灰度值的，也就是黑的占比
        /// </summary>
        /// <param name="grayValue"></param>
        /// <returns></returns>
        public double GetHistPercentLessThan(int grayValue)
        {
            if (grayValue > 255 || grayValue < 0)
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
            if (this.Threshold <= 180 && this.AreaPercent > 0.5)
            {
                return false;
            }
            return this.AvgGrayValue > 200 || this.GetHistPercentLessThan(230) < 0.8;
            //return this.AvgGrayValue > 200 || this.GetHistPercentLessThan(230) < 0.80;
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
