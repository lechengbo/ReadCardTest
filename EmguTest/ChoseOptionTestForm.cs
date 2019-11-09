using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public partial class ChoseOptionTestForm : Form
    {
        private string[] multSelectPicPathList;
        private int successNum = 0;
        private int failNum = 0;
        public ChoseOptionTestForm()
        {
            InitializeComponent();
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

                this.ib_original.LoadImage(bitmap);

            }
        }

        private void ChoseOptionTestForm_Load(object sender, EventArgs e)
        {
            this.ib_original.SetContainer(this.panel1);
            this.ib_result.SetContainer(this.panel2);
        }

        private void Btn_reg_Click(object sender, EventArgs e)
        {
            if (ib_original.Image == null)
            {
                MessageBox.Show("请先加载图片");
                return;
            }

            var cutedBitmap = this.ib_original.GetFirstRegionRect();

            if (cutedBitmap == null) {
                MessageBox.Show("将要识别的图片不能为空");
                return;
            }

            CommonUse commonUse = new CommonUse();

            var rectList = commonUse.GetRectListFromBitmap(cutedBitmap, Convert.ToInt32(this.minNum.Value), Convert.ToInt32(this.maxNum.Value), 0, 0, true, 2);

            //排序
            //var rectListDic = commonUse.OrderRectList(rectList);

            Mat src = new Image<Bgr, byte>(cutedBitmap).Mat;

            foreach (var item in rectList)
            {
                CvInvoke.Rectangle(src, item, new MCvScalar(0, 0, 255));
            }

            commonUse.SaveMat(src, "获取裁剪图中的所有轮廓边界提取");

            this.ib_result.Image = src.Bitmap;
        }

        private void Btn_loadMult_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = true;
            op.Filter = "所有图片文件|*.png;*.jpg;*.jpeg|所有文件 (*.*)|*.*"; 
            if (op.ShowDialog() == DialogResult.OK)
            {
                this.successNum = 0;
                this.failNum = 0;
                this.multSelectPicPathList = op.FileNames;
                this.lbl_regResul.Text = "";
                this.lbl_picNumInfo.Text = $"选择张数：{this.successNum}/{this.multSelectPicPathList?.Length}";
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                //var bitmap = new Bitmap(fileName);

                //this.ib_original.LoadImage(bitmap);

            }
        }

        private void Btn_regMult_Click(object sender, EventArgs e)
        {
            CommonUse commonUse = new CommonUse();

            foreach (var filePath in this.multSelectPicPathList)
            {
                try
                {
                    var bitmap = new Bitmap(filePath);
                    var rectList = commonUse.GetRectListFromBitmap(bitmap, Convert.ToInt32(this.minNum.Value), Convert.ToInt32(this.maxNum.Value), 0, 0, true, 2);
                    Mat src = new Image<Bgr, byte>(bitmap).Mat;

                    foreach (var item in rectList)
                    {
                        CvInvoke.Rectangle(src, item, new MCvScalar(0, 0, 255));
                    }

                    commonUse.SaveMat(src, Path.GetFileName(filePath),false) ;

                    this.successNum++;

                }
                catch (Exception)
                {

                    this.failNum++;
                }
                finally
                {
                    this.lbl_regResul.Text = this.failNum==0?"":$"失败数量：{this.failNum}";
                    this.lbl_picNumInfo.Text = $"选择张数：{this.successNum}/{this.multSelectPicPathList?.Length}";
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var directoryPath = Application.StartupPath + "\\upload\\image\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            Process.Start("explorer.exe", directoryPath);
        }
    }
}
