using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTest.Service
{
    public class MLearner
    {
        private static String annFileName = ".\\ANN\\ann_mlp_model.xml";
        private static ANN_MLP network;
        private const int width = 22, height = 13;
        static MLearner()
        {
            if (!File.Exists(annFileName))
            {
                return;
            }

            network = new ANN_MLP();

            network.Load(annFileName);
            

        }

        public static bool IsAnswer(Image<Gray,byte> img)
        {
            if (network == null)
            {
                return false;
            }

            Matrix<float> sample = new Matrix<float>(1, width * height);
            Matrix<float> prediction = new Matrix<float>(1, 1);

            var testData = getImgData(img);
            
            for (int j = 0; j < testData.Count; j++)
            {
                sample[0, j] = testData[j];
            }

            network.Predict(sample, prediction);
            float response = prediction.Data[0, 0];

            return response > 0.5;
        }

        
        private static List<float> getImgData(Image<Gray, byte> img)
        {
            img = img.Resize(width, height, Emgu.CV.CvEnum.Inter.Linear);

            var list = new List<float>();
            var size = img.Size;
            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    var tmpValue = img.Data[i, j, 0];
                    list.Add((int)tmpValue / 255f);

                }
            }

            return list;
        }
    }
}
