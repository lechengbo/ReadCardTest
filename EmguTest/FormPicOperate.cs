using Emgu.CV;
using Emgu.CV.Structure;
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
    public partial class FormPicOperate : Form
    {
        public FormPicOperate()
        {
            InitializeComponent();
        }

        private void btnmulop_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = true;
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileNames = op.FileNames;
                for (int i = 0; i < fileNames.Length; i++)
                {
                    var filePath = fileNames[i];
                
                    Image<Bgr, Byte> img = new Image<Bgr, byte>(filePath);
                    var middle = img.Resize(400, 532, Emgu.CV.CvEnum.Inter.Linear);
                    middle.Save("./template/template" + i + "-middle.png");
                    var small= img.Resize(60, 80, Emgu.CV.CvEnum.Inter.Linear);
                    small.Save("./template/template" + i + "-small.png");
                }
                
                
            }
        }
    }
}
