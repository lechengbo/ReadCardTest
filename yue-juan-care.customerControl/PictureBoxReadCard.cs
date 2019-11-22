using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yue_juan_care.customerControl
{
    public class PictureBoxReadCard:PictureBox
    {
        public Point dragStartPoint;
        public Point centerPointOffset = new Point();//picBox中心位置偏移量
        public bool canDragPicBox = false;
        public Bitmap orignalBitmap;

        Point drawStart; //画框的起始点
        Point drawEnd;//画框的结束点<br>
        bool blnDraw;//判断是否绘制<br>
        Rectangle rect;//pictureBox控件的矩形位置
        Rectangle destRect;//pictureBox中图片的矩形

        public int MinWidth { get; set; }
        /// <summary>
        /// pictureBox的容器
        /// </summary>
        public Panel Container { get; private set; }

        /// <summary>
        /// 矩形框信息
        /// </summary>
        public RegionInfo RegionInfo { get; set; } 
        /// <summary>
        /// 当前选中的区域框
        /// </summary>
        public RegionInfo CurrentSelectedRect { get; set; }
        

        public PictureBoxReadCard()
        {

            this.Initial();
            this.InitializeComponent();
        }
        public void SetContainer(Panel container)
        {
            this.Container = container;
            this.MinWidth = this.Container.Width / 2;
            //this.Container.Controls.Add(this);
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
            this.MinWidth = tmpWidth / 2;
        }

        /// <summary>
        /// 从新调整宽高
        /// </summary>
        /// <param name="step"></param>
        public void ResizeWH(int step)
        {
            Console.WriteLine("开始缩放");
            var lastWidth =Math.Max(this.MinWidth, this.Width + step);
            var lastHeight = Convert.ToInt32(Math.Round(this.Height * (lastWidth * 1.0 / this.Width), MidpointRounding.AwayFromZero));
            this.Size = new Size(lastWidth, lastHeight);
            //this.SetCenterInContainer();


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

        public void LoadImage(Bitmap image)
        {
            this.ResizePictureBoxSizeByBitmap(image.Width, image.Height);
            this.FillPictureBoxSizeToPanel();
            SetCenterInContainer();
            this.orignalBitmap = (Bitmap)image.Clone();
            this.Image = image;
            this.RegionInfo = new RegionInfo(image.Width, image.Height, new List<Rectangle>());
        }
        private void DrawRectList(Graphics g)
        {

            //var g = this.CreateGraphics();
            if (this.RegionInfo==null ||this.RegionInfo.RectList.Count <= 0)
            {
                return;
            }
            g.DrawRectangles(new Pen(Color.Red, 1),this.RegionInfo.GetRelativeRectanleList(this.Width,this.Height).ToArray());
        }

        public void MoveRegion(MoveDirection direction , int step=1) {
            //相对当前pictureBox的宽度
            step = Convert.ToInt32(step * (this.Width * 1f / 100) * (this.Image.Width * 1f / this.Width));


            int offsetX = 0, offsetY = 0;
            switch (direction)
            {
                case MoveDirection.Left:
                    offsetX -= step;
                    break;
                case MoveDirection.Up:
                    offsetY -= step;
                    break;
                case MoveDirection.Right:
                    offsetX += step;
                    break;
                case MoveDirection.Down:
                    offsetY += step;
                    break;
                default:
                    break;
            }
            //tmpX = Math.Max(0, tmpX);
            //tmpY = Math.Max(0, tmpY);
            List<Rectangle> destList = new List<Rectangle>();
            foreach (var item in this.CurrentSelectedRect?.RectList)
            {
                int tmpX = item.X + offsetX;
                int tmpY = item.Y + offsetY;
                if(tmpX<0 || tmpY < 0)
                {
                    return;
                }

                var tmpRect = new Rectangle(new Point(tmpX, tmpY), item.Size);
                destList.Add(tmpRect);
            }

            this.RegionInfo.RectList = destList;
            //重绘
            this.Invalidate();
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

                this.MoveLocation(e.Location);

            }
        }
        private void PicBox_Paint(object sender, PaintEventArgs e)
        {
            if (this.Image == null)
            {
                return;
            }
            
            //e.Graphics.DrawRectangle(new Pen(Color.Red, 1), new Rectangle(0, 0, 100, 50));
            this.DrawRectList(e.Graphics);

        }
        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void Initial()
        {
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseDown);
            this.MouseMove += new MouseEventHandler(this.PicBox_MouseMove);
            this.MouseUp += new MouseEventHandler(this.PicBox_MouseUp);
            this.Paint += new PaintEventHandler(this.PicBox_Paint);
            this.MouseWheel += (object picBSender, MouseEventArgs picBE) =>
            {
                this.ResizeWH(picBE.Delta);

            };
        }


        #endregion

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxReadCard
            // 
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxReadCard_MouseDown_Draw);
            this.MouseMove += new MouseEventHandler(this.PictureBoxReadCard_MouseMove_Draw);
            this.MouseUp += new MouseEventHandler(this.PictureBoxReadCard_MouseUp_Draw);
            this.Paint += new PaintEventHandler(this.PictureBoxReadCard_Paint_Draw);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void PictureBoxReadCard_MouseDown_Draw(object sender, MouseEventArgs e)
        {
            
            drawStart = e.Location;
            //this.Invalidate();
            blnDraw = true;
        }
        private void PictureBoxReadCard_MouseMove_Draw(object sender, MouseEventArgs e)
        {
            if (blnDraw)
            {
                if (e.Button != MouseButtons.Left)//判断是否按下左键
                    return;
                Point tempEndPoint = e.Location; //记录框的位置和大小
                rect.Location = new Point(
                Math.Min(drawStart.X, tempEndPoint.X),
                Math.Min(drawStart.Y, tempEndPoint.Y));
                rect.Size = new Size(
                Math.Abs(drawStart.X - tempEndPoint.X),
                Math.Abs(drawStart.Y - tempEndPoint.Y));
                this.Invalidate();
            }
        }
        private void PictureBoxReadCard_MouseUp_Draw(object sender, MouseEventArgs e)
        {
            blnDraw = false; //结束绘制
            //把该绘制的矩形框退换现有区域，
            //计算出比例picbox 和里面图片的长宽比例
            if (this.Image == null)
            {
                return;
            }
            var currentBitMap = (Bitmap)this.Image;
            double widthPercent = currentBitMap.Width * 1.0 / this.Width;
            double heightPercent = currentBitMap.Height * 1.0 / this.Height;
            var destRect = new Rectangle(Convert.ToInt32(rect.X * widthPercent), Convert.ToInt32(rect.Y * heightPercent),
                Convert.ToInt32(rect.Width * widthPercent), Convert.ToInt32(rect.Height * heightPercent));
            //this.RegionInfo.RectList.Clear();
            this.RegionInfo.RectList.Insert(0,destRect);

            
        }
        private void PictureBoxReadCard_Paint_Draw(object sender, PaintEventArgs e)
        {

            if (blnDraw)
            {
                if (this.Image != null)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rect);//重新绘制颜色为红色
                    }
                }
            }
        }


        public Bitmap GetFirstRegionRect() {
            var firstRect = this.RegionInfo.RectList.FirstOrDefault();
            if (firstRect == null || firstRect.Width==0 || firstRect.Height==0)
            {
                return this.orignalBitmap;
            }
            return Cut(this.orignalBitmap, firstRect.X, firstRect.Y, firstRect.Width, firstRect.Height);
        }
        /// <summary>
            /// 剪裁 -- 用GDI+
            /// </summary>
            /// <param name="b">原始Bitmap</param>
            /// <param name="StartX">开始坐标X</param>
            /// <param name="StartY">开始坐标Y</param>
            /// <param name="iWidth">宽度</param>
            /// <param name="iHeight">高度</param>
            /// <returns>剪裁后的Bitmap</returns>
        public static Bitmap Cut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap Cut(Bitmap b,Rectangle rect)
        {
            return Cut(b, rect.X, rect.Y, rect.Width, rect.Height);
        }


    }
    public enum MoveDirection
    {
        Left,
        Up,
        Right,
        Down
    }
    public class RegionInfo {
        /// <summary>
        /// 原始模板图片宽度
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// 原始模板图片高度
        /// </summary>
        public int Height { get; private set; }
        public List<Rectangle> RectList { get; set; } 

        public RegionInfo(int width,int height,List<Rectangle> rectList)
        {
            this.Width = width;
            this.Height = height;
            this.RectList = rectList;
        }

        /// <summary>
        /// 获取相对矩形大小和位置
        /// </summary>
        /// <param name="relativeWidth">相对宽度，一般指现在pictureBox的宽度</param>
        /// <param name="relativeHeight">相对高度,一搬指picturebox的高度</param>
        /// <returns></returns>
        public List<RectangleF> GetRelativeRectanleList(int relativeWidth,int relativeHeight) {
            //计算出比例picbox 和里面图片的长宽比例
            float widthPercent = this.Width * 1.0f / relativeWidth;
            float heightPercent = this.Height * 1.0f / relativeHeight;

            var destRectList = new List<RectangleF>();
            foreach (var item in this.RectList)
            {
                RectangleF tmpRect = new RectangleF(item.X / widthPercent, item.Y / heightPercent, item.Width / widthPercent, item.Height / heightPercent);
                destRectList.Add(tmpRect);
            }

            return destRectList;
        }

        
    }
}
