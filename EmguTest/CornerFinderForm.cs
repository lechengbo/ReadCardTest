using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public partial class CornerFinderForm : Form
    {
        public CornerFinderForm()
        {
            InitializeComponent();
        }

        private void CornerFinderForm_Load(object sender, EventArgs e)
        {
            this.picSrc.SetContainer(this.panel1);
            this.picTarget.SetContainer(this.panel2);
            this.picCircle.SetContainer(this.panel3);
            this.picRect.SetContainer(this.panel4);
        }

        private void Btn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "所有图片文件|*.png;*.jpg;*.jpeg|所有文件 (*.*)|*.*"; ;
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);

                this.picSrc.LoadImage(bitmap);

            }
        }

        private void Btn_findCorner_Click(object sender, EventArgs e)
        {
            var bitmap = this.picSrc.GetFirstRegionRect();
            var image = new Image<Bgr, byte>(bitmap);

            //Mat mat_threshold = new Mat();
            //int myThreshold = 200;
            //CvInvoke.Threshold(image, mat_threshold, myThreshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
            //new CommonUse().SaveMat(mat_threshold, "角检测的二值化前期");
            //LineSegment2D
            BriefDescriptorExtractor extractor = new BriefDescriptorExtractor();
            FastDetector detector = new FastDetector((int)this.numericUpDown1.Value);
            var points= detector.Detect(image);

            //detector.

            //CvInvoke.DrawChessboardCorners(image, new Size(1, 1), points, true);
            for (int i = 0; i < points.Length; i++)
            {
                //if (points[i].Angle < 60)
                //{
                //    continue;
                //}
                var tmpPoint = new Point((int)points[i].Point.X, (int)points[i].Point.Y);
                CvInvoke.Circle(image,tmpPoint , 1, new MCvScalar(0, 0, 255));
                
            }

            this.picTarget.LoadImage(image.ToBitmap());
        }

        private void Btn_findLine_Click(object sender, EventArgs e)
        {
            //LineSegment2D line1 = new LineSegment2D(new Point(1, 1), new Point(1, 20));
            //LineSegment2D line2 = new LineSegment2D(new Point(1, 1), new Point(20, 1));

            //MessageBox.Show(line1.GetExteriorAngleDegree(line2).ToString());

            var bitmap = this.picSrc.GetFirstRegionRect();
            var image = new Image<Bgr, byte>(bitmap);

            UMat grayImage = new UMat();
            CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

            //使用高斯滤波去除噪声
            //CvInvoke.GaussianBlur(grayImage, grayImage, new Size(5, 5), 3);
            //CvInvoke.Imshow("Blur Image", grayImage);

            UMat cannyEdges = new UMat();
            CvInvoke.Canny(grayImage, cannyEdges, 100, 120);
            CvInvoke.Imshow("Canny Image", cannyEdges);
            CvInvoke.WaitKey(2);


            var lines= CvInvoke.HoughLinesP(cannyEdges, 1, Math.PI / 40.0, 20, 8, 3);

            for (int i = 0; i < lines.Length; i++)
            {
                
                var line = lines[i];
                CvInvoke.Line(image, line.P1, line.P2, new MCvScalar(0, 0, 255));
            }

            this.picTarget.LoadImage(image.ToBitmap());
        }

        private void Btn_shapFind_Click(object sender, EventArgs e)
        {
            StringBuilder msgBuilder = new StringBuilder("Performance: ");

            //Load the image from file and resize it for display
            var bitmap = this.picSrc.GetFirstRegionRect();
            Image<Bgr, Byte> img =
               new Image<Bgr, byte>(bitmap)
               .Resize(400, 400, Emgu.CV.CvEnum.Inter.Linear, true);

            //Convert the image to grayscale and filter out the noise
            UMat uimage = new UMat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);

            CvInvoke.Imshow("Image", uimage);
            CvInvoke.WaitKey(2);

            //二值化
            //UMat cannyEdges = new UMat();
            CvInvoke.Threshold(uimage, uimage, 230, 255, ThresholdType.Binary);
            CvInvoke.Imshow("After Threshold", uimage);
            CvInvoke.WaitKey(2);

            //use image pyr to remove noise
            //UMat pyrDown = new UMat();
            //CvInvoke.PyrDown(uimage, pyrDown);
            //CvInvoke.PyrUp(pyrDown, uimage);

            //CvInvoke.Imshow("pyrDownUp", uimage);
            //CvInvoke.WaitKey(2);

            //Image<Gray, Byte> gray = img.Convert<Gray, Byte>().PyrDown().PyrUp();

            #region circle detection
            Stopwatch watch = Stopwatch.StartNew();
            double cannyThreshold = 180.0;
            double circleAccumulatorThreshold = 100;
            CircleF[] circles = CvInvoke.HoughCircles(uimage, HoughType.Gradient, 2.0, 20.0, cannyThreshold, circleAccumulatorThreshold, 5);

            watch.Stop();
            msgBuilder.Append(String.Format("Hough circles - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            #region Canny and edge detection
            watch.Reset(); watch.Start();
            double cannyThresholdLinking = 120.0;
            UMat cannyEdges = new UMat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);

            LineSegment2D[] lines = CvInvoke.HoughLinesP(
               cannyEdges,
               1, //Distance resolution in pixel-related units
               Math.PI / 45.0, //Angle resolution measured in radians.
               20, //threshold
               30, //min Line width
               10); //gap between lines

            watch.Stop();
            msgBuilder.Append(String.Format("Canny & Hough lines - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            #region Find triangles and rectangles
            watch.Reset(); watch.Start();
            List<Triangle2DF> triangleList = new List<Triangle2DF>();
            List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (CvInvoke.ContourArea(approxContour, false) > 200) //only consider contours with area greater than 250
                        {
                            if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                            {
                                Point[] pts = approxContour.ToArray();
                                triangleList.Add(new Triangle2DF(
                                   pts[0],
                                   pts[1],
                                   pts[2]
                                   ));
                            }
                            else if (approxContour.Size == 4) //The contour has 4 vertices.
                            {
                                #region determine if all the angles in the contour are within [80, 100] degree
                                bool isRectangle = true;
                                Point[] pts = approxContour.ToArray();
                                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                for (int j = 0; j < edges.Length; j++)
                                {
                                    double angle = Math.Abs(
                                       edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                    if (angle < 80 || angle > 100)
                                    {
                                        isRectangle = false;
                                        break;
                                    }
                                }
                                #endregion

                                if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                            }
                        }
                    }
                }
            }

            watch.Stop();
            msgBuilder.Append(String.Format("Triangles & Rectangles - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            //originalImageBox.Image = img;
            Console.WriteLine(msgBuilder.ToString()); 

            #region draw triangles and rectangles
            Image<Bgr, Byte> triangleRectangleImage = img.CopyBlank();
            foreach (Triangle2DF triangle in triangleList)
                triangleRectangleImage.Draw(triangle, new Bgr(Color.DarkBlue), 2);
            foreach (RotatedRect box in boxList)
                triangleRectangleImage.Draw(box, new Bgr(Color.DarkOrange), 2);
            this.picRect.LoadImage(triangleRectangleImage.ToBitmap());
            #endregion

            #region draw circles
            Image<Bgr, Byte> circleImage = img.CopyBlank();
            foreach (CircleF circle in circles)
                circleImage.Draw(circle, new Bgr(Color.Brown), 2);
            this.picCircle.LoadImage(circleImage.Bitmap);
            #endregion

            #region draw lines
            Image<Bgr, Byte> lineImage = img.CopyBlank();
            foreach (LineSegment2D line in lines)
                lineImage.Draw(line, new Bgr(Color.Green), 2);
            this.picTarget.LoadImage(lineImage.Bitmap);
            #endregion
        }

        private void Btn_hog_Click(object sender, EventArgs e)
        {
            HOGDescriptor hog = new HOGDescriptor();
        }

        private void Btn_svm_Click(object sender, EventArgs e)
        {
            int trainSampleCount = 150;
            int sigma = 60;

            #region Generate the training data and classes

            Matrix<float> trainData = new Matrix<float>(trainSampleCount, 2);
            Matrix<float> trainClasses = new Matrix<float>(trainSampleCount, 1);

            Image<Bgr, Byte> img = new Image<Bgr, byte>(500, 500);

            Matrix<float> sample = new Matrix<float>(1, 2);

            Matrix<float> trainData1 = trainData.GetRows(0, trainSampleCount / 3, 1);
            trainData1.GetCols(0, 1).SetRandNormal(new MCvScalar(100), new MCvScalar(sigma));
            trainData1.GetCols(1, 2).SetRandNormal(new MCvScalar(300), new MCvScalar(sigma));

            Matrix<float> trainData2 = trainData.GetRows(trainSampleCount / 3, 2 * trainSampleCount / 3, 1);
            trainData2.SetRandNormal(new MCvScalar(400), new MCvScalar(sigma));

            Matrix<float> trainData3 = trainData.GetRows(2 * trainSampleCount / 3, trainSampleCount, 1);
            trainData3.GetCols(0, 1).SetRandNormal(new MCvScalar(300), new MCvScalar(sigma));
            trainData3.GetCols(1, 2).SetRandNormal(new MCvScalar(100), new MCvScalar(sigma));

            Matrix<float> trainClasses1 = trainClasses.GetRows(0, trainSampleCount / 3, 1);
            trainClasses1.SetValue(1);
            Matrix<float> trainClasses2 = trainClasses.GetRows(trainSampleCount / 3, 2 * trainSampleCount / 3, 1);
            trainClasses2.SetValue(2);
            Matrix<float> trainClasses3 = trainClasses.GetRows(2 * trainSampleCount / 3, trainSampleCount, 1);
            trainClasses3.SetValue(3);

            #endregion

            using (SVM model = new SVM())
            {
                //SVMParams p = new SVMParams();
                //p.KernelType = Emgu.CV.ML.MlEnum.SVM_KERNEL_TYPE.LINEAR;
                //p.SVMType = Emgu.CV.ML.MlEnum.SVM_TYPE.C_SVC;
                //p.C = 1;
                //p.TermCrit = new MCvTermCriteria(100, 0.00001);

                //bool trained = model.Train(trainData, trainClasses, null, null, p);
                //bool trained = model.TrainAuto(trainData, trainClasses, null, null, p.MCvSVMParams, 5);

                //for (int i = 0; i < img.Height; i++)
                //{
                //    for (int j = 0; j < img.Width; j++)
                //    {
                //        sample.Data[0, 0] = j;
                //        sample.Data[0, 1] = i;

                //        float response = model.Predict(sample);

                //        img[i, j] =
                //           response == 1 ? new Bgr(90, 0, 0) :
                //           response == 2 ? new Bgr(0, 90, 0) :
                //           new Bgr(0, 0, 90);
                //    }
                //}

                //int c = model.GetSupportVectorCount();
                //for (int i = 0; i < c; i++)
                //{
                //    float[] v = model.GetSupportVector(i);
                //    PointF p1 = new PointF(v[0], v[1]);
                //    img.Draw(new CircleF(p1, 4), new Bgr(128, 128, 128), 2);
                //}
            }

            // display the original training samples
            for (int i = 0; i < (trainSampleCount / 3); i++)
            {
                PointF p1 = new PointF(trainData1[i, 0], trainData1[i, 1]);
                img.Draw(new CircleF(p1, 2.0f), new Bgr(255, 100, 100), -1);
                PointF p2 = new PointF(trainData2[i, 0], trainData2[i, 1]);
                img.Draw(new CircleF(p2, 2.0f), new Bgr(100, 255, 100), -1);
                PointF p3 = new PointF(trainData3[i, 0], trainData3[i, 1]);
                img.Draw(new CircleF(p3, 2.0f), new Bgr(100, 100, 255), -1);
            }

            Emgu.CV.UI.ImageViewer.Show(img);
        }
    }
}
