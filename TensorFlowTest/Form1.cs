using NumSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;

namespace TensorFlowTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //// Create a tensor holds a scalar value
            //var t1 = new Tensor(3);

            //// Init from a string
            //var t2 = new Tensor("Hello! TensorFlow.NET");

            //// Tensor holds a ndarray
            //var nd = new NDArray(new int[] { 3, 1, 1, 2 });
            //var t3 = new Tensor(nd);

            //Console.WriteLine($"t1: {t1}, t2: {t2}, t3: {t3}");
            var tf = new tensorflow();
            //var x = tf.Variable(10, name: "x");
            //using (var session = tf.Session())
            //{
            //    session.run(x.initializer);
            //    var result = session.run(x);
            //    //Console.Write(result.flatten()); // should be 10
            //}

           

            
        }
    }
}
