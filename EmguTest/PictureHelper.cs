using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguTest
{
    public class PictureHelper
    {
        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="pb">PictureBox</param>
        /// <param name="angle">角度</param>
        public static void RotatePictureFormCenter(PictureBox pb, float angle)
        {

            Image img = (Bitmap)pb.Image;
            int newWidth = Math.Max(img.Height, img.Width);
            Bitmap bmp = new Bitmap(newWidth, newWidth);
            Graphics g = Graphics.FromImage(bmp);
            Matrix x = new Matrix();
            PointF point = new PointF(img.Width / 2f, img.Height / 2f);
            x.RotateAt(angle, point);
            g.Transform = x;
            g.DrawImage(img, 0, 0);
            g.Dispose();
            img = bmp;
            pb.Image = bmp;
            //SaveBitmap(bmp);

            //var newBitmap = (Bitmap)pb.Image;
            //newBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //pb.Image = newBitmap;
            //SaveBitmap(newBitmap);
        }
        public static void RotatePictureFormCenter1(PictureBox pb, float angle)
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

        /// <summary>
        /// 重新调整PictureBox宽高，根据位图的长宽比例
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="bitmapWidth"></param>
        /// <param name="bitmapHeight"></param>
        public static void ResizePictureBoxSizeByBitmap(PictureBox picBox, int bitmapWidth, int bitmapHeight)
        {
            //计算出比例picbox 和里面图片的长宽比例
            double widthPercent = bitmapWidth * 1.0 / picBox.Width;
            double heightPercent = bitmapHeight * 1.0 / picBox.Height;
            double resultPercent = Math.Max(widthPercent, heightPercent);
            picBox.Width = Convert.ToInt32(Math.Round(bitmapWidth / resultPercent, MidpointRounding.AwayFromZero));
            picBox.Height = Convert.ToInt32(Math.Round(bitmapHeight / resultPercent, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// 重新调整PictureBox宽高，使得box的宽或者高等于panel的宽高
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="panel"></param>
        public static void FillPictureBoxSizeToPanel(PictureBox picBox, Panel panel)
        {
            double widthPercent = panel.Width * 1.0 / picBox.Width;
            double heightPercent = panel.Height * 1.0 / picBox.Height;
            double resultPercent = Math.Min(widthPercent, heightPercent);
            var tmpWidth = Convert.ToInt32(Math.Floor(picBox.Width * resultPercent * 0.99));
            var tmpHeight = Convert.ToInt32(Math.Floor(picBox.Height * resultPercent * 0.99));

            picBox.Width = tmpWidth;
            picBox.Height = tmpHeight;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="bitmap"></param>
        public static void SaveBitmap(Bitmap bitmap)
        {
            string directoryPath = Application.StartupPath + "\\upload\\image\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fileName = $"{directoryPath}{DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo)}旋转后的.jpg";

            bitmap.Save(fileName);

        }
    }

    public class MovePictureBox : PictureBox
    {
        public Point dragStartPoint;
        public Point centerPointOffset = new Point();//picBox中心位置偏移量
        public bool canDragPicBox = false;
        public Panel Container { get; private set; }
        public MovePictureBox()
        {
           
            this.Initial();
        }
        public void SetContainer(Panel container) {
            this.Container = container;

            this.Container.Controls.Add(this);
        }

        /// <summary>
        /// 把中心点和它的容器Panel重合
        /// </summary>
        public void SetCenterInContainer()
        {
            Point panelCenterPoint = new Point()
            {
                X = this.Container.Width / 2,
                Y = this.Container.Height / 2
            };
            this.Location = new Point()
            {
                X = panelCenterPoint.X - this.Width / 2 + centerPointOffset.X,
                Y = panelCenterPoint.Y - this.Height / 2 + centerPointOffset.Y
            };
        }

        /// <summary>
        /// 重新调整PictureBox宽高，根据位图的长宽比例
        /// </summary>
        /// <param name="bitmapWidth"></param>
        /// <param name="bitmapHeight"></param>
        public void ResizePictureBoxSizeByBitmap(int bitmapWidth, int bitmapHeight)
        {
            //计算出比例picbox 和里面图片的长宽比例
            double widthPercent = bitmapWidth * 1.0 / this.Width;
            double heightPercent = bitmapHeight * 1.0 / this.Height;
            double resultPercent = Math.Max(widthPercent, heightPercent);
            this.Width = Convert.ToInt32(Math.Round(bitmapWidth / resultPercent, MidpointRounding.AwayFromZero));
            this.Height = Convert.ToInt32(Math.Round(bitmapHeight / resultPercent, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// 重新调整PictureBox宽高，使得box的宽或者高等于panel的宽高
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="panel"></param>
        public void FillPictureBoxSizeToPanel()
        {
            double widthPercent = this.Container.Width * 1.0 / this.Width;
            double heightPercent = this.Container.Height * 1.0 / this.Height;
            double resultPercent = Math.Min(widthPercent, heightPercent);
            var tmpWidth = Convert.ToInt32(Math.Floor(this.Width * resultPercent * 0.99));
            var tmpHeight = Convert.ToInt32(Math.Floor(this.Height * resultPercent * 0.99));

            this.Width = tmpWidth;
            this.Height = tmpHeight;
        }

        /// <summary>
        /// 从新调整宽高
        /// </summary>
        /// <param name="step"></param>
        public void ResizeWH(int step)
        {
            Console.WriteLine("开始缩放");
            var lastWidth = this.Width + step;
            var lastHeight = Convert.ToInt32(Math.Round(this.Height * (lastWidth * 1.0 / this.Width), MidpointRounding.AwayFromZero));
            this.Size = new Size(lastWidth, lastHeight);
            this.SetCenterInContainer();


        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="endPoint"></param>
        public void MoveLocation(Point endPoint)
        {
            var offsetX = endPoint.X - this.dragStartPoint.X;
            var offsetY = endPoint.Y - this.dragStartPoint.Y;
            this.Left += offsetX;
            this.Top += offsetY;

            this.centerPointOffset = new Point()
            {
                X = this.centerPointOffset.X + offsetX,
                Y = this.centerPointOffset.Y + offsetY
            };
        }

        public void LoadImage(Image image) {
            this.ResizePictureBoxSizeByBitmap(image.Width, image.Height);
            this.FillPictureBoxSizeToPanel();
            SetCenterInContainer();

            this.Image = image;
        }

        #region 初始化 事件
        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.canDragPicBox)
            {
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

                this.MoveLocation( e.Location);

            }
        }
        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void Initial()
        {
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseDown);
            this.MouseMove += new MouseEventHandler(this.PicBox_MouseMove);
            this.MouseUp += new MouseEventHandler(this.PicBox_MouseUp);
            this.MouseWheel += (object picBSender, MouseEventArgs picBE) =>
            {
                this.ResizeWH( picBE.Delta);

            };
        }
        
        
        #endregion
    }
}
