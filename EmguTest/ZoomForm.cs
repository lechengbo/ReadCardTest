using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using EmguTest.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public partial class ZoomForm : Form
    {
        private Point panelCenterPoint;
        private Point dragStartPoint;
        private Point picBoxCenterPointOffset = new Point();//picBox中心位置偏移量
        private int zoonSetp = 50;
        private bool canDragPicBox = false;

        private MovePictureBox movePictureBox;
        public ZoomForm()
        {
            InitializeComponent();

        }

        private void ZoomForm_Load(object sender, EventArgs e)
        {
            this.picBox.MouseWheel += (object picBSender, MouseEventArgs picBE) =>
            {
                this.ResizePicBox((PictureBox)picBSender, picBE.Delta);

            };

            //movePictureBox = new MovePictureBox(this.panelImage);
            
        }

        private Point CaculatePanelCenterPoint(Panel panel) {
            Point centerPoint = new Point() {
                X = panel.Width / 2,
                Y=panel.Height/2
            };
            
            return centerPoint;
        }
        private void SetPicBoxCenterInOnePoint(PictureBox picture, Point point,Point centerOffset) {
            picture.Location = new Point()
            {
                X = point.X - picture.Width / 2 +centerOffset.X,
                Y = point.Y - picture.Height / 2 + centerOffset.Y
            };
        }

        private void Btn_loadPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                var image = Image.FromFile(op.FileName);
                //this.movePictureBox.LoadImage(image);
                PictureHelper.ResizePictureBoxSizeByBitmap(this.picBox, image.Width, image.Height);

                PictureHelper.FillPictureBoxSizeToPanel(this.picBox, this.panelImage);

                //计算中间点重合
                this.panelCenterPoint = CaculatePanelCenterPoint(this.panelImage);
                SetPicBoxCenterInOnePoint(this.picBox, this.panelCenterPoint, this.picBoxCenterPointOffset);


                this.picBox.Image = image;

            }
        }
        private void ResizePicBox(PictureBox picBox, int step)
        {
            Console.WriteLine("开始缩放");
            var lastWidth = picBox.Width + step;
            var lastHeight = Convert.ToInt32(Math.Round(picBox.Height * (lastWidth * 1.0 / picBox.Width), MidpointRounding.AwayFromZero));
            picBox.Size = new Size(lastWidth, lastHeight);
            SetPicBoxCenterInOnePoint(picBox, this.panelCenterPoint,this.picBoxCenterPointOffset);


        }

        private void Btn_showPicBoxLocation_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"X:{this.picBox.Location.X};Y:{this.picBox.Location.Y},宽:{this.picBox.Width},高：{this.picBox.Height}");
        }

        private void Btn_getPanelAutoInfo_Click(object sender, EventArgs e)
        {
            string info = $"X:{this.panelImage.AutoScrollPosition.X},Y:{this.panelImage.AutoScrollPosition.Y}";
            MessageBox.Show(info);
        }

        private void Btn_panelDragToggle_Click(object sender, EventArgs e)
        {
            this.canDragPicBox = !this.canDragPicBox;
            this.btn_panelDragToggle.Text = this.canDragPicBox ? "关闭拖动" : "开启拖动";
        }

        private void PanelImage_MouseMove(object sender, MouseEventArgs e)
        {
            //if (this.canDragPicBox)
            //{
            //    if (e.Button != MouseButtons.Left)//判断是否按下左键
            //        return;

            //    MovePictrueBox(this.panelImage, e.Location);
            //    //Point tempEndPoint = e.Location;
            //    //int offsetWidth = tempEndPoint.X - dragStartPoint.X;
            //    //int offsetHeight = tempEndPoint.Y - dragStartPoint.Y;

            //    //this.panelImage.AutoScrollPosition = new Point()
            //    //{
            //    //    X = this.panelImage.AutoScrollPosition.X + offsetWidth,
            //    //    Y = this.panelImage.AutoScrollPosition.Y + offsetHeight
            //    //};

            //}
        }

        private void MovePictrueBox(Panel panelImage, Point endPoint) {
            int offsetWidth = (endPoint.X - dragStartPoint.X);
            int offsetHeight = (endPoint.Y - dragStartPoint.Y);

            panelImage.AutoScrollPosition = new Point()
            {
                X = panelImage.AutoScrollPosition.X + offsetWidth,
                Y = panelImage.AutoScrollPosition.Y + offsetHeight
            };

            //this.picBoxCenterPointOffset = new Point()
            //{
            //    X = this.picBoxCenterPointOffset.X + offsetWidth,
            //    Y = this.picBoxCenterPointOffset.Y + offsetHeight
            //};
        }
        private void MovePictrueBox(PictureBox pictureBox, Point endPoint) {
            var offsetX= endPoint.X - this.dragStartPoint.X;
            var offsetY= endPoint.Y - this.dragStartPoint.Y; 
            pictureBox.Left += offsetX;
            pictureBox.Top += offsetY;

            this.picBoxCenterPointOffset = new Point()
            {
                X = this.picBoxCenterPointOffset.X + offsetX,
                Y = this.picBoxCenterPointOffset.Y + offsetY
            };
        }

        private void PanelImage_MouseDown(object sender, MouseEventArgs e)
        {
            //this.dragStartPoint = e.Location;
        }

        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.canDragPicBox) {
                this.dragStartPoint = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.canDragPicBox)
            {
                if (e.Button != MouseButtons.Left)//判断是否按下左键
                    return;

                //MovePictrueBox(this.panelImage, e.Location);
                MovePictrueBox((PictureBox)sender, e.Location);
                
            }
        }

        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void PanelImage_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Paint Restart");
        }

        private void ZoomForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.KeyCode==Keys.ControlKey)
            {
                this.canDragPicBox = true;
                Cursor = Cursors.Hand;
                Console.WriteLine("Control key按下了");
            }
        }

        private void ZoomForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control || e.KeyCode== Keys.ControlKey)
            {
                this.canDragPicBox = false;
                Cursor = Cursors.Default;
                Console.WriteLine("Control key没有按住了");
            }
        }

        private void PicBox_Paint(object sender, PaintEventArgs e)
        {
            if (((PictureBox)sender).Image == null)
            {
                return;
            }
            e.Graphics.DrawRectangle(new Pen(Color.Red, 1), new Rectangle(10,10,100,200));

        }

        private void RotatePictureFormCenter(PictureBox pb, float angle)
        {
            Graphics graphics = pb.CreateGraphics();
            graphics.Clear(pb.BackColor);
            //装入图片
            Bitmap image = new Bitmap(pb.Image);
            //获取当前窗口的中心点
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);
            PointF center = new PointF(rect.Width / 2, rect.Height / 2);
            float offsetX = 0;
            float offsetY = 0;
            offsetX = center.X - image.Width / 2;
            offsetY = center.Y - image.Height / 2;
            //构造图片显示区域:让图片的中心点与窗口的中心点一致
            RectangleF picRect = new RectangleF(offsetX, offsetY, image.Width, image.Height);
            PointF Pcenter = new PointF(picRect.X + picRect.Width / 2,
                picRect.Y + picRect.Height / 2);
            // 绘图平面以图片的中心点旋转
            graphics.TranslateTransform(Pcenter.X, Pcenter.Y);
            graphics.RotateTransform(angle);
            //恢复绘图平面在水平和垂直方向的平移
            graphics.TranslateTransform(-Pcenter.X, -Pcenter.Y);
            //绘制图片
            graphics.DrawImage(image, picRect);
        }
       

        private void Btn_savePictureBoxImg_Click(object sender, EventArgs e)
        {
            string directoryPath = Application.StartupPath + "\\upload\\image\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fileName = $"{directoryPath}{DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo)}旋转后的.jpg";

            this.picBox.Image.Save(fileName);
            PictureHelper.SaveBitmap((Bitmap)((PictureBox)this.picBox).Image);

        }

        private void Btn_rotate_Click(object sender, EventArgs e)
        {
            PictureHelper.RotatePictureFormCenter(this.picBox, (float)this.num_angle.Value);
        }

        private void Btn_orc_Click(object sender, EventArgs e)
        {
            var text = OCRHelper.Ocr((Bitmap)this.picBox.Image);
            Console.WriteLine(text);
            MessageBox.Show(text);
        }

        private void Btn_imShow_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>((Bitmap)this.picBox.Image);
            var color=img[1, 2];//img.Data
            //DepthType.Cv8U
            CvInvoke.Imshow("imshow", img);
        }
    }
}
