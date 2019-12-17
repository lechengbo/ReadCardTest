using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCardTest
{
    public partial class ClassifierForm : Form
    {
        public ClassifierForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Bgr[] colors = new Bgr[] {
                               new Bgr(0, 0, 255),
                               new Bgr(0, 255, 0),
                               new Bgr(255, 0, 0)
                               };
            int trainSampleCount = 150;

            #region Generate the training data and classes
            Matrix<float> trainData = new Matrix<float>(trainSampleCount, 2);
            Console.WriteLine(trainData.ToString());
            CvInvoke.Imshow("trainData", trainData);
            CvInvoke.WaitKey(2);
            Matrix<int> trainClasses = new Matrix<int>(trainSampleCount, 1);
            CvInvoke.Imshow("trainClasses", trainClasses);
            CvInvoke.WaitKey(2);

            Image<Bgr, Byte> img = new Image<Bgr, byte>(500, 500);
            CvInvoke.Imshow("img", img);
            CvInvoke.WaitKey(2);

            Matrix<float> sample = new Matrix<float>(1, 2);

            Matrix<float> trainData1 = trainData.GetRows(0, trainSampleCount / 3, 1);
            trainData1.GetCols(0, 1).SetRandNormal(new MCvScalar(100), new MCvScalar(50));
            trainData1.GetCols(1, 2).SetRandNormal(new MCvScalar(300), new MCvScalar(50));

            Matrix<float> trainData2 = trainData.GetRows(trainSampleCount / 3, 2 * trainSampleCount / 3, 1);
            trainData2.SetRandNormal(new MCvScalar(400), new MCvScalar(50));

            Matrix<float> trainData3 = trainData.GetRows(2 * trainSampleCount / 3, trainSampleCount, 1);
            trainData3.GetCols(0, 1).SetRandNormal(new MCvScalar(300), new MCvScalar(50));
            trainData3.GetCols(1, 2).SetRandNormal(new MCvScalar(100), new MCvScalar(50));

            Console.WriteLine(trainData.ToString());
            CvInvoke.Imshow("trainData", trainData);
            CvInvoke.WaitKey(2);

            Matrix<int> trainClasses1 = trainClasses.GetRows(0, trainSampleCount / 3, 1);
            trainClasses1.SetValue(1);
            Matrix<int> trainClasses2 = trainClasses.GetRows(trainSampleCount / 3, 2 * trainSampleCount / 3, 1);
            trainClasses2.SetValue(2);
            Matrix<int> trainClasses3 = trainClasses.GetRows(2 * trainSampleCount / 3, trainSampleCount, 1);
            trainClasses3.SetValue(3);
            CvInvoke.Imshow("trainClasses", trainData);
            CvInvoke.WaitKey(2);
            #endregion

            using (TrainData td = new TrainData(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, trainClasses))
            using (NormalBayesClassifier classifier = new NormalBayesClassifier())
            {
                //ParamDef[] defs = classifier.GetParams();
                classifier.Train(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, trainClasses);
                classifier.Clear();
                classifier.Train(td);
#if !NETFX_CORE
                String fileName = Path.Combine(Path.GetTempPath(), "normalBayes.xml");
                classifier.Save(fileName);
                if (File.Exists(fileName))
                    File.Delete(fileName);
#endif

                #region Classify every image pixel
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        sample.Data[0, 0] = i;
                        sample.Data[0, 1] = j;
                        int response = (int)classifier.Predict(sample, null);

                        Bgr color = colors[response - 1];

                        img[j, i] = new Bgr(color.Blue * 0.5, color.Green * 0.5, color.Red * 0.5);
                    }
                }

                #endregion
            }

            // display the original training samples
            for (int i = 0; i < (trainSampleCount / 3); i++)
            {
                PointF p1 = new PointF(trainData1[i, 0], trainData1[i, 1]);
                img.Draw(new CircleF(p1, 2.0f), colors[0], -1);
                PointF p2 = new PointF(trainData2[i, 0], trainData2[i, 1]);
                img.Draw(new CircleF(p2, 2.0f), colors[1], -1);
                PointF p3 = new PointF(trainData3[i, 0], trainData3[i, 1]);
                img.Draw(new CircleF(p3, 2.0f), colors[2], -1);
            }

            CvInvoke.Imshow("img", img);
            CvInvoke.WaitKey();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int K = 10;
            int trainSampleCount = 100;

            #region Generate the training data and classes

            Matrix<float> trainData = new Matrix<float>(trainSampleCount, 2);
            Matrix<float> trainClasses = new Matrix<float>(trainSampleCount, 1);

            Image<Bgr, Byte> img = new Image<Bgr, byte>(500, 500);

            Matrix<float> sample = new Matrix<float>(1, 2);

            Matrix<float> trainData1 = trainData.GetRows(0, trainSampleCount >> 1, 1);
            trainData1.SetRandNormal(new MCvScalar(200), new MCvScalar(50));
            Matrix<float> trainData2 = trainData.GetRows(trainSampleCount >> 1, trainSampleCount, 1);
            trainData2.SetRandNormal(new MCvScalar(300), new MCvScalar(50));

            Matrix<float> trainClasses1 = trainClasses.GetRows(0, trainSampleCount >> 1, 1);
            trainClasses1.SetValue(1);
            Matrix<float> trainClasses2 = trainClasses.GetRows(trainSampleCount >> 1, trainSampleCount, 1);
            trainClasses2.SetValue(2);
            #endregion

            Matrix<float> results, neighborResponses;
            results = new Matrix<float>(sample.Rows, 1);
            neighborResponses = new Matrix<float>(sample.Rows, K);
            //dist = new Matrix<float>(sample.Rows, K);

            using (KNearest knn = new KNearest())
            {
                knn.DefaultK = K;
                knn.IsClassifier = true;
                knn.Train(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, trainClasses);

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        sample.Data[0, 0] = j;
                        sample.Data[0, 1] = i;

                        // estimates the response and get the neighbors' labels
                        float response = knn.Predict(sample); //knn.FindNearest(sample, K, results, null, neighborResponses, null);

                        int accuracy = 0;
                        // compute the number of neighbors representing the majority
                        for (int k = 0; k < K; k++)
                        {
                            if (neighborResponses.Data[0, k] == response)
                                accuracy++;
                        }
                        // highlight the pixel depending on the accuracy (or confidence)
                        img[i, j] =
                           response == 1 ?
                              (accuracy > 5 ? new Bgr(90, 0, 0) : new Bgr(90, 40, 0)) :
                              (accuracy > 5 ? new Bgr(0, 90, 0) : new Bgr(40, 90, 0));
                    }
                }

            }

            // display the original training samples
            for (int i = 0; i < (trainSampleCount >> 1); i++)
            {
                PointF p1 = new PointF(trainData1[i, 0], trainData1[i, 1]);
                img.Draw(new CircleF(p1, 2.0f), new Bgr(255, 100, 100), -1);
                PointF p2 = new PointF(trainData2[i, 0], trainData2[i, 1]);
                img.Draw(new CircleF(p2, 2.0f), new Bgr(100, 255, 100), -1);
            }

            CvInvoke.Imshow("K-mean", img);
            CvInvoke.WaitKey();
        }
    }
}
