using Emgu.CV;
using Emgu.CV.CvEnum;
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
using yue_juan_care.customerControl;

namespace EmguTest
{
    public partial class TemplateValidate : Form
    {
        public TemplateValidate()
        {
            InitializeComponent();
        }

        private void TemplateValidate_Load(object sender, EventArgs e)
        {
            this.picSrc.SetContainer(this.panel1);
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

        private void Btn_reg_Click(object sender, EventArgs e)
        {
            if (this.picSrc.Image == null)
            {
                MessageBox.Show("请先加载图片");
                return;
            }

            CommonUse commonUse = new CommonUse();
            List<Rectangle> posRectList = new List<Rectangle>();

            for (int i = 0; i < this.picSrc.RegionInfo.RectList.Count; i++)
            {

                var tmpRect = this.picSrc.RegionInfo.RectList[i];

                var cutedBitmap = PictureBoxReadCard.Cut(this.picSrc.orignalBitmap,tmpRect.X,tmpRect.Y,tmpRect.Width,tmpRect.Height);

                if (cutedBitmap == null)
                {
                    MessageBox.Show("将要识别的图片不能为空");
                    return;
                }

              

                var rectList = commonUse.GetRectListFromBitmap(cutedBitmap, 200, 6000, tmpRect.X,tmpRect.Y, false, 0);

                posRectList.AddRange(rectList);

                
            }

            //获取定位点信息
            var dic = commonUse.OrderRectList(posRectList, true, false);
            var tempList1 = dic[1].OrderBy(r => r.X).ToList();
            var tempList2 = dic[2].OrderBy(r => r.X).ToList();

            //四个点
            var p1 = tempList1[0];
            var p2 = new Point(tempList1[1].X, p1.Y);

            lbl1.Text = $"左上角：X：{tempList1[0].X},Y：{tempList1[0].Y},Width:{tempList1[0].Width},Height:{tempList1[0].Height}";
            lbl2.Text = $"右上角：X：{tempList1[1].X},Y：{tempList1[1].Y},Width:{tempList1[1].Width},Height:{tempList1[1].Height}";
            lbl3.Text = $"左下角：X：{tempList2[0].X},Y：{tempList2[0].Y},Width:{tempList2[0].Width},Height:{tempList2[0].Height}";
            lbl4.Text = $"右下角：X：{tempList2[1].X},Y：{tempList2[1].Y},Width:{tempList2[1].Width},Height:{tempList2[1].Height}";
            
            var width = tempList1[1].X - tempList1[0].X;
            var height = tempList2[0].Y - tempList1[0].Y;
            lbl5.Text = $"W:{width}，H:{height}";

            Console.WriteLine($"{lbl1.Text};{lbl5.Text}");
           
            //画出所有定位点
            //排序
            //var rectListDic = commonUse.OrderRectList(rectList);

            using (Mat src = new Image<Bgr, byte>((Bitmap)this.picSrc.orignalBitmap.Clone()).Mat)
            {
                foreach (var item in posRectList)
                {
                    CvInvoke.Rectangle(src, item, new MCvScalar(0, 0, 255));
                }

                CvInvoke.Line(src, tempList1[0].Location, tempList1[1].Location, new MCvScalar(0, 0, 255),2);
                CvInvoke.Line(src, tempList2[0].Location, tempList1[0].Location, new MCvScalar(0, 0, 255),2);
                CvInvoke.PutText(src, lbl5.Text, tempList1[0].Location, FontFace.HersheyComplex, 3, new MCvScalar(0, 0, 255),2);


                commonUse.SaveMat(src, "定位点画出后");
            }

           


        }
    }
}
