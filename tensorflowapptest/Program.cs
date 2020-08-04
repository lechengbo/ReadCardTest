using System;
using TensorFlowNET.Examples;

namespace tensorflowapptest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var lineRes = new LinearRegression();
            //var result= lineRes.Run();
            var imgc = new RetrainClassifierWithInceptionV3();
            var result = imgc.Run();
            Console.WriteLine($"result:{result}");
            Console.ReadKey();

        }
    }
}
