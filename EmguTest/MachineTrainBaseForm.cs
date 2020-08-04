using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using EmguTest.Service;
using Newtonsoft.Json;
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

namespace EmguTest
{
    public partial class MachineTrainBaseForm : Form
    {
        private string positiveDataPath = ".\\ANN\\positiveData.json";
        private string negativeDataPath = ".\\ANN\\negativeData.json";

        private string positiveImgPath = ".\\ANN\\positive";
        private string negativeImgPath = ".\\ANN\\negative";

        private String annFileName = ".\\ANN\\ann_mlp_model.xml";
        public HashSet<CVRectangle> PositiveSet { get; set; } = new HashSet<CVRectangle>();
        public HashSet<CVRectangle> NegativeSet { get; set; } = new HashSet<CVRectangle>();
   
        public MachineTrainBaseForm()
        {
            InitializeComponent();

        }

        private void Bt_EM_Click(object sender, EventArgs e)
        {
            int N = 4; //number of clusters
            int N1 = (int)Math.Sqrt((double)4);

            Bgr[] colors = new Bgr[] {
               new Bgr(0, 0, 255),
               new Bgr(0, 255, 0),
               new Bgr(0, 255, 255),
               new Bgr(255, 255, 0)};

            int nSamples = 100;

            Matrix<float> samples = new Matrix<float>(nSamples, 2);
            Matrix<Int32> labels = new Matrix<int>(nSamples, 1);
            Image<Bgr, Byte> img = new Image<Bgr, byte>(500, 500);
            Matrix<float> sample = new Matrix<float>(1, 2);

            CvInvoke.cvReshape(samples.Ptr, samples.Ptr, 2, 0);
            //for (int i = 0; i < N; i++)
            //{
            //    Matrix<float> rows = samples.GetRows(i * nSamples / N, (i + 1) * nSamples / N, 1);
            //    double scale = ((i % N1) + 1.0) / (N1 + 1);
            //    MCvScalar mean = new MCvScalar(scale * img.Width, scale * img.Height);
            //    MCvScalar sigma = new MCvScalar(30, 30);
            //    ulong seed = (ulong)DateTime.Now.Ticks;
            //    CvInvoke.cvRandArr(ref seed, rows.Ptr, Emgu.CV.CvEnum.RAND_TYPE.CV_RAND_NORMAL, mean, sigma);
            //}
            //CvInvoke.cvReshape(samples.Ptr, samples.Ptr, 1, 0);

            //using (EM emModel1 = new EM())
            //using (EM emModel2 = new EM())
            //{
            //    EMParams parameters1 = new EMParams();
            //    parameters1.Nclusters = N;
            //    parameters1.CovMatType = Emgu.CV.ML.MlEnum.EM_COVARIAN_MATRIX_TYPE.COV_MAT_DIAGONAL;
            //    parameters1.StartStep = Emgu.CV.ML.MlEnum.EM_INIT_STEP_TYPE.START_AUTO_STEP;
            //    parameters1.TermCrit = new MCvTermCriteria(10, 0.01);
            //    emModel1.Train(samples, null, parameters1, labels);

            //    EMParams parameters2 = new EMParams();
            //    parameters2.Nclusters = N;
            //    parameters2.CovMatType = Emgu.CV.ML.MlEnum.EM_COVARIAN_MATRIX_TYPE.COV_MAT_GENERIC;
            //    parameters2.StartStep = Emgu.CV.ML.MlEnum.EM_INIT_STEP_TYPE.START_E_STEP;
            //    parameters2.TermCrit = new MCvTermCriteria(100, 1.0e-6);
            //    parameters2.Means = emModel1.GetMeans();
            //    parameters2.Covs = emModel1.GetCovariances();
            //    parameters2.Weights = emModel1.GetWeights();

            //    emModel2.Train(samples, null, parameters2, labels);

            //    #region Classify every image pixel
            //    for (int i = 0; i < img.Height; i++)
            //        for (int j = 0; j < img.Width; j++)
            //        {
            //            sample.Data[0, 0] = i;
            //            sample.Data[0, 1] = j;
            //            int response = (int)emModel2.Predict(sample, null);

            //            Bgr color = colors[response];

            //            img.Draw(
            //               new CircleF(new PointF(i, j), 1),
            //               new Bgr(color.Blue * 0.5, color.Green * 0.5, color.Red * 0.5),
            //               0);
            //        }
            //    #endregion

            //    #region draw the clustered samples
            //    for (int i = 0; i < nSamples; i++)
            //    {
            //        img.Draw(new CircleF(new PointF(samples.Data[i, 0], samples.Data[i, 1]), 1), colors[labels.Data[i, 0]], 0);
            //    }
            //    #endregion

            //    Emgu.CV.UI.ImageViewer.Show(img);
            //}

        }

        private void Bt_ANN_Click(object sender, EventArgs e)
        {
            int trainSampleCount = 100;

            #region Generate the traning data and classes
            Matrix<float> trainData = new Matrix<float>(trainSampleCount, 2);
            Matrix<float> trainClasses = new Matrix<float>(trainSampleCount, 1);

            Image<Bgr, Byte> img = new Image<Bgr, byte>(500, 500);

            Matrix<float> sample = new Matrix<float>(1, 2);
            Matrix<float> prediction = new Matrix<float>(1, 1);

            Matrix<float> trainData1 = trainData.GetRows(0, trainSampleCount >> 1, 1);
            trainData1.SetRandNormal(new MCvScalar(200), new MCvScalar(50));
            Matrix<float> trainData2 = trainData.GetRows(trainSampleCount >> 1, trainSampleCount, 1);
            trainData2.SetRandNormal(new MCvScalar(300), new MCvScalar(50));

            Matrix<float> trainClasses1 = trainClasses.GetRows(0, trainSampleCount >> 1, 1);
            trainClasses1.SetValue(1);
            Matrix<float> trainClasses2 = trainClasses.GetRows(trainSampleCount >> 1, trainSampleCount, 1);
            trainClasses2.SetValue(2);
            #endregion

            using (Matrix<int> layerSize = new Matrix<int>(new int[] { 2, 5, 1 }))
            using (Mat layerSizeMat = layerSize.Mat)

            using (TrainData td = new TrainData(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, trainClasses))
            using (ANN_MLP network = new ANN_MLP())
            {
                network.SetLayerSizes(layerSizeMat);
                network.SetActivationFunction(ANN_MLP.AnnMlpActivationFunction.SigmoidSym, 0, 0);
                network.TermCriteria = new MCvTermCriteria(10, 1.0e-8);
                network.SetTrainMethod(ANN_MLP.AnnMlpTrainMethod.Backprop, 0.1, 0.1);
                network.Train(td, (int)Emgu.CV.ML.MlEnum.AnnMlpTrainingFlag.Default);

#if !NETFX_CORE
                String fileName = "ann_mlp_model.xml"; //Path.Combine(Path.GetTempPath(), "ann_mlp_model.xml");
                network.Save(fileName);
                //if (File.Exists(fileName))
                //    File.Delete(fileName);
#endif

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        sample.Data[0, 0] = j;
                        sample.Data[0, 1] = i;
                        network.Predict(sample, prediction);

                        // estimates the response and get the neighbors' labels
                        float response = prediction.Data[0, 0];

                        // highlight the pixel depending on the accuracy (or confidence)
                        img[i, j] = response < 1.5 ? new Bgr(90, 0, 0) : new Bgr(0, 90, 0);
                    }
                }
            }

            // display the original training samples
            for (int i = 0; i < (trainSampleCount >> 1); i++)
            {
                PointF p1 = new PointF(trainData1[i, 0], trainData1[i, 1]);
                img.Draw(new CircleF(p1, 2), new Bgr(255, 100, 100), -1);
                PointF p2 = new PointF((int)trainData2[i, 0], (int)trainData2[i, 1]);
                img.Draw(new CircleF(p2, 2), new Bgr(100, 255, 100), -1);
            }

            this.ib_result.Image = img;
            //CvInvoke.WaitKey();

            //Emgu.CV.UI.ImageViewer.Show(img);
        }

        private void Btn_CNN2_Click(object sender, EventArgs e)
        {
            var positiveData = GetPositiveData();
            var negativeData = GetNegativeData();
            if(positiveData?.Count ==0 || negativeData?.Count == 0)
            {
                MessageBox.Show("训练数据不能为空");
                return;
            }
            int trainSampleCount = positiveData.Count + negativeData.Count;

            Matrix<float> trainData = new Matrix<float>(trainSampleCount, 2);
            Matrix<float> trainClasses = new Matrix<float>(trainSampleCount, 1);

            Matrix<float> sample = new Matrix<float>(1, 2);
            Matrix<float> prediction = new Matrix<float>(1, 1);


            for (int i = 0; i < positiveData.Count; i++)
            {
                var item = positiveData[i];
                trainData.Data[i, 0] = item.Percent;
                trainData.Data[i, 1] = item.Avg;

                trainClasses.Data[i,0] = 1;
            }
            for (int i = 0; i < negativeData.Count; i++)
            {
                var item = negativeData[i];
                int row = positiveData.Count + i;
                trainData.Data[row, 0] = item.Percent;
                trainData.Data[row, 1] = item.Avg;

                trainClasses.Data[row, 0] = 0;
            }

            Image<Bgr, Byte> img = new Image<Bgr, byte>(765, 300);

            using (Matrix<int> layerSize = new Matrix<int>(new int[] { 2, 5, 1 }))
            using (Mat layerSizeMat = layerSize.Mat)
            using (TrainData td = new TrainData(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, trainClasses))
            using (ANN_MLP network = new ANN_MLP())
            {
                network.SetLayerSizes(layerSizeMat);
                network.SetActivationFunction(ANN_MLP.AnnMlpActivationFunction.SigmoidSym, 0, 0);
                network.TermCriteria = new MCvTermCriteria(10, 1.0e-8);
                network.SetTrainMethod(ANN_MLP.AnnMlpTrainMethod.Backprop, 0.1, 0.1);
                network.Train(td, (int)Emgu.CV.ML.MlEnum.AnnMlpTrainingFlag.Default);


                //String fileName = "ann_mlp_model.xml"; //Path.Combine(Path.GetTempPath(), "ann_mlp_model.xml");
                network.Save(annFileName);
                //if (File.Exists(fileName))
                //    File.Delete(fileName);

                //画图

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        sample.Data[0, 0] =i*1.0f/(100*3);
                        sample.Data[0, 1] = 255-j*1.0f/3.0f;
                        network.Predict(sample, prediction);

                        // estimates the response and get the neighbors' labels
                        float response = prediction.Data[0, 0];

                        // highlight the pixel depending on the accuracy (or confidence)
                        img[i, j] = response < 0.5 ? new Bgr(90, 0, 0) : new Bgr(0, 90, 0);
                    }
                }

            }

            // display the original training samples
            for (int i = 0; i < positiveData.Count; i++)
            {
                var d = positiveData[i];
                PointF p1 = new PointF((255 - d.Avg) * 3,d.Percent*300);
                img.Draw(new CircleF(p1, 2), new Bgr(255, 100, 100), -1);
            }
            for (int i = 0; i < negativeData.Count; i++)
            {
                var d = negativeData[i];
                PointF p1 = new PointF((255 - d.Avg) * 3,d.Percent * 300);
                img.Draw(new CircleF(p1, 2), new Bgr(100, 255, 100), -1);
            }
            this.ib_result.Image = img;
            MessageBox.Show("训练完毕");
        }
        private List<(float Percent, float Avg)> GetPositiveData()
        {
            List<(float Percent, float Avg)> list = new List<(float Percent, float Avg)>();
            foreach(var r in this.PositiveSet)
            {
                list.Add(((float)r.AreaPercent, (float)r.AvgGrayValue));
            }
            return list;
        }
        private List<(float Percent, float Avg)> GetNegativeData()
        {
            List<(float Percent, float Avg)> list = new List<(float Percent, float Avg)>();
            foreach (var r in this.NegativeSet)
            {
                list.Add(((float)r.AreaPercent, (float)r.AvgGrayValue));
            }
            return list;
        }

        private void Btn_reg_Click(object sender, EventArgs e)
        {
            using (Matrix<int> layerSize = new Matrix<int>(new int[] { 2, 5, 1 }))
            using (Mat layerSizeMat = layerSize.Mat)
            using (ANN_MLP network = new ANN_MLP())
            {
                network.Load(annFileName);
                //network.SetLayerSizes(layerSizeMat);
                //network.SetActivationFunction(ANN_MLP.AnnMlpActivationFunction.SigmoidSym, 0, 0);
                //network.TermCriteria = new MCvTermCriteria(10, 1.0e-8);
                //network.SetTrainMethod(ANN_MLP.AnnMlpTrainMethod.Backprop, 0.1, 0.1);
                float[,] testData = new float[1,2] { { float.Parse(this.txb_percent.Text), float.Parse(this.txb_avg.Text) } };
                Matrix<float> sample = new Matrix<float>(testData);
                Matrix<float> prediction = new Matrix<float>(1, 1);

                network.Predict(sample, prediction);
                float response = prediction.Data[0, 0];

                MessageBox.Show($"判断结果：{response}");

            }
        }

        private void MachineTrainBaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
             string positiveJson= JsonConvert.SerializeObject(this.PositiveSet);
            File.WriteAllText(this.positiveDataPath, positiveJson);

            string negativeJson = JsonConvert.SerializeObject(this.NegativeSet);
            File.WriteAllText(this.negativeDataPath, negativeJson);
        }

        private void MachineTrainBaseForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(this.positiveDataPath))
            {
                string positiveJson = File.ReadAllText(this.positiveDataPath);
                this.PositiveSet = JsonConvert.DeserializeObject<HashSet<CVRectangle>>(positiveJson);
            }

            if (File.Exists(this.negativeDataPath))
            {
                string negativeJson = File.ReadAllText(this.positiveDataPath);
                this.NegativeSet = JsonConvert.DeserializeObject<HashSet<CVRectangle>>(negativeJson);
            }
        }


        private CVHelper cVHelper = new CVHelper();
        private const string negImgDirtory= ".\\samples\\neg";
        private const string actImgDirtory = ".\\samples\\act";
        private const string negRegErrorDir = ".\\samples\\negAssertError";
        private const string actRegErrorDir = ".\\samples\\actAssertError";
        private const int width = 22, height = 13;
        private List<List<float>> trainNegData;
        private List<List<float>> testNegData;

        private List<List<float>> trainActData;
        private List<List<float>> testActData;



        private void Btn_ann2_Click(object sender, EventArgs e)
        {
            this.prepareData();

            if ( trainNegData?.Count == 0 || trainActData?.Count==0)
            {
                MessageBox.Show("训练数据不能为空");
                return;
            }
            int trainSampleCount = trainActData.Count + trainNegData.Count;
            int colCount = width * height;
            Matrix<float> trainData = new Matrix<float>(trainSampleCount, colCount);
            Matrix<float> trainClasses = new Matrix<float>(trainSampleCount, 1);
            
            Matrix<float> sample = new Matrix<float>(1, colCount);
            Matrix<float> prediction = new Matrix<float>(1, 1);

            //准备正面数据
            var actCount = trainActData.Count;
            //Matrix<float> trainActDataMatr = new Matrix<float>(actCount, width * height);
            //Matrix<float> trainActClassesMatr = new Matrix<float>(actCount, 1);

            for (int i = 0; i < actCount; i++)
            {
                var colData = trainActData[i];
                var colCount1 = colData.Count;
                for (int j = 0; j < colCount1; j++)
                {
                    trainData.Data[i, j] = trainActData[i][j];
                }

                trainClasses.Data[i, 0] = 1;
                //trainClasses.Data[i, 1] = 0;
            }

            //准备未涂答数据
            var negCount = trainNegData.Count;
            //Matrix<float> trainNegDataMatr = new Matrix<float>(negCount, width * height);
            //Matrix<float> trainNegClassesMatr = new Matrix<float>(negCount, 1);
            for (int i = 0; i < negCount; i++)
            {
                var colData = trainNegData[i];
                var colCount1 = colData.Count;
                for (int j = 0; j < colCount1; j++)
                {
                    trainData.Data[i+actCount, j] = trainNegData[i][j];
                }

                trainClasses.Data[i+actCount, 0] = 0;
                //trainClasses.Data[i + actCount, 1] = 1;
            }

            //训练
            using (Matrix<int> layerSize = new Matrix<int>(new int[] { 286, 10,10, 1 }))
            using (Mat layerSizeMat = layerSize.Mat)
            using (TrainData td = new TrainData(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, trainClasses))
            using (ANN_MLP network = new ANN_MLP())
            {
                network.SetLayerSizes(layerSizeMat);
                network.SetActivationFunction(ANN_MLP.AnnMlpActivationFunction.SigmoidSym, 0, 0);
                network.TermCriteria = new MCvTermCriteria(10, 1.0e-8);
                network.SetTrainMethod(ANN_MLP.AnnMlpTrainMethod.Backprop, 0.01, 0.01);
                network.Train(td, (int)Emgu.CV.ML.MlEnum.AnnMlpTrainingFlag.Default);


                //String fileName = "ann_mlp_model.xml"; //Path.Combine(Path.GetTempPath(), "ann_mlp_model.xml");
                network.Save(annFileName);
                //if (File.Exists(fileName))
                //    File.Delete(fileName);

                //测试
                //1测试正面数据
                var testActCount = testActData.Count;
                var rightActCount = 0;//正确act识别数量
                for (int i = 0; i < testActCount; i++)
                {
                    var testData = testActData[i];
                    for (int j = 0; j < testData.Count; j++)
                    {
                        sample[0, j] = testData[j];
                    }
                    network.Predict(sample, prediction);
                    float response = prediction.Data[0, 0];
                    if (response >0.5)
                    {
                        rightActCount++;
                        Console.WriteLine($"该数据是涂答的，正确识别{response}");
                    }
                    else
                    {
                        Console.WriteLine($"该数据是涂答的，错误识别{response}");
                    }
                   
                }

                //2测试负面数据
                var testNegCount = testNegData.Count;
                var rightNegCount = 0;//正确neg识别数量
                for (int i = 0; i < testNegCount; i++)
                {
                    var testData = testNegData[i];
                    for (int j = 0; j < testData.Count; j++)
                    {
                        sample[0, j] = testData[j];
                    }
                    network.Predict(sample, prediction);
                    float response = prediction.Data[0, 0];
                    if (response <=0.5)
                    {
                        rightNegCount++;
                        Console.WriteLine($"该数据是未涂答的，正确识别{response}");
                    }
                    else
                    {
                        Console.WriteLine($"该数据是未涂答的，错误识别{response}");
                    }

                }
                MessageBox.Show("训练完毕，并测试");
            }


        }

        private List<float>[] Create(int rowCount,int colCount,int defaultValue = 1)
        {
            var list = new List<List<float>>();
            for (int i = 0; i < rowCount; i++)
            {
                var listcol = new List<float>();
                for (int j = 0; j < colCount; j++)
                {
                    listcol.Add(defaultValue);
                }
                list.Add(listcol);
            }

            return list.ToArray();
        }
        private void prepareData()
        {
            var negFiles= Directory.GetFiles(negImgDirtory,"*.jpg");
            var actFiles = Directory.GetFiles(actImgDirtory, "*.jpg");

            var negData = new List<List<float>>();
            for (int i = 0; i < negFiles.Length; i++)
            {
                var path = negFiles[i];
                Image<Gray, byte> img = new Image<Gray, byte>(path);
                negData.Add(getImgData(img));
            }

            var actData = new List<List<float>>();
            for (int i = 0; i < actFiles.Length; i++)
            {
                var path = actFiles[i];
                Image<Gray, byte> img = new Image<Gray, byte>(path);
                actData.Add(getImgData(img));
            }

            trainNegData = new List<List<float>>();
            testNegData = new List<List<float>>();
            int negCount = negData.Count, trainnegCount = Convert.ToInt32(negCount * 0.9);
            for (int i = 0; i < negCount; i++)
            {
                trainNegData.Add(negData[i]);
                if (i<= trainnegCount)
                {
                    
                }
                else
                {
                    testNegData.Add(negData[i]);
                }
            }

            trainActData = new List<List<float>>();
            testActData = new List<List<float>>();
            int actCount = actData.Count, trainactCount = Convert.ToInt32(actCount * 0.9);
            for (int i = 0; i < actCount; i++)
            {
                trainActData.Add(actData[i]);
                if (i <= trainactCount)
                {
                    
                }
                else
                {
                    testActData.Add(actData[i]);
                }
            }
        }

        
        private void btn_ANNReg_Click(object sender, EventArgs e)
        {
            var regPath = txbregPath.Text;
            if (string.IsNullOrEmpty(regPath))
            {
                MessageBox.Show("待识别文件夹不能空");
                return;
            }
            var isAct = ckbIsAct.Checked;

            var files = Directory.GetFiles(regPath);
            var testData = new List<List<float>>();
            for (int i = 0; i < files.Length; i++)
            {
                var path = files[i];
                Image<Gray, byte> img = new Image<Gray, byte>(path);
                testData.Add(getImgData(img));
            }

            
            using (ANN_MLP network = new ANN_MLP())
            {
                network.Load(annFileName);

                int colCount = width * height;
                Matrix<float> sample = new Matrix<float>(1, colCount);
                Matrix<float> prediction = new Matrix<float>(1, 1);

                //1测试数据
                var testCount = testData.Count;
                var rightCount = 0;//正确act识别数量
                for (int i = 0; i < testCount; i++)
                {
                    var testColData = testData[i];
                    for (int j = 0; j < testColData.Count; j++)
                    {
                        sample[0, j] = testColData[j];
                    }
                    network.Predict(sample, prediction);
                    float response = prediction.Data[0, 0];

                    if (isAct && response > 0.5)
                    {
                        rightCount++;
                        Console.WriteLine($"该数据是涂答的，正确识别{response}");
                    }else if(isAct && response<=0.5)
                    {
                        Console.WriteLine($"该数据是涂答的，错误识别{response}");
                        File.Copy(files[i], Path.Combine(actRegErrorDir, Path.GetFileName(files[i])),true);
                    }else if(!isAct && response <= 0.5)
                    {
                        rightCount++;
                        Console.WriteLine($"该数据是未涂答的，正确识别{response}");
                    }else if(!isAct && response > 0.5)
                    {
                        Console.WriteLine($"该数据是未涂答的，错误识别{response}");
                        File.Copy(files[i], Path.Combine(negRegErrorDir, Path.GetFileName(files[i])),true);
                    }
                    else
                    {
                        Console.WriteLine("未知识别结果");
                    }

                }

                var result = $"测试数量：{testCount},正确数量：{rightCount},正确率：{rightCount * 1.0 / testCount}";
                Console.WriteLine(result);
                MessageBox.Show(result);


            }


        }

        private List<float> getImgData(Image<Gray,byte> img)
        {
            img = img.Resize(width, height, Emgu.CV.CvEnum.Inter.Linear);

            var list = new List<float>();
            var size = img.Size;
            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    var tmpValue = img.Data[i, j, 0];
                    list.Add((int)tmpValue/255f);

                }
            }
            
            return list;
        }
    }
}
