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
using yue_juan_care.customerControl;

namespace EmguTest
{
    public partial class ZoomForm2 : Form
    {
        public ZoomForm2()
        {
            InitializeComponent();
        }

        private void Btn_loadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                var fileName = op.FileName;
                //Image<Bgr, Byte> img = new Image<Bgr, byte>(op.FileName);
                //var image = Image.FromFile(op.FileName);
                var bitmap = new Bitmap(fileName);

                this.picBox.LoadImage(bitmap);

            }
        }

        private void ZoomForm2_Load(object sender, EventArgs e)
        {
            this.picBox.SetContainer(this.panel_PicBox);
            this.picBox.RegionInfo = new RegionInfo(1200, 798, new List<Rectangle>() {
                new Rectangle(){X=20,Y=20,Width=100,Height=50},
                 new Rectangle(){X=20,Y=90,Width=100,Height=50},
                  new Rectangle(){X=20,Y=160,Width=100,Height=50},
                   new Rectangle(){X=140,Y=20,Width=100,Height=50},
                   new Rectangle(){X=260,Y=20,Width=100,Height=50},
                   new Rectangle(){X=140,Y=90,Width=100,Height=50},
                   new Rectangle(){X=260,Y=90,Width=100,Height=50},
                   new Rectangle(){X=140,Y=160,Width=100,Height=50},
                   new Rectangle(){X=260,Y=160,Width=100,Height=50},
            });
            this.picBox.CurrentSelectedRect = this.picBox.RegionInfo;
        }

        private void ZoomForm2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.KeyCode == Keys.ControlKey)
            {
                this.picBox.canDragPicBox = true;
                Cursor = Cursors.Hand;
                Console.WriteLine("Control key按下了");
            } 

        }

        private void ZoomForm2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control || e.KeyCode == Keys.ControlKey)
            {
                this.picBox.canDragPicBox = false;
                Cursor = Cursors.Default;
                Console.WriteLine("Control key没有按住了");
                return;
            }
            this.MoveDetect(e);

        }
        private void MoveDetect(KeyEventArgs e) {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Left)
            {
                this.picBox.MoveRegion(MoveDirection.Left, 5);
            }
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Up)
            {
                this.picBox.MoveRegion(MoveDirection.Up, 5);
            }
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Right)
            {
                this.picBox.MoveRegion(MoveDirection.Right, 5);
            }
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Down)
            {
                this.picBox.MoveRegion(MoveDirection.Down, 5);
            }
            else if (e.KeyCode == Keys.Left)
            {
                this.picBox.MoveRegion(MoveDirection.Left);
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.picBox.MoveRegion(MoveDirection.Up);
            }
            else if (e.KeyCode == Keys.Right)
            {
                this.picBox.MoveRegion(MoveDirection.Right);
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.picBox.MoveRegion(MoveDirection.Down);
            }
        }

        private void Btn_moveRect_Click(object sender, EventArgs e)
        {
            this.picBox.CurrentSelectedRect = this.picBox.RegionInfo;
        }

        private void Btn_paintFormRect_Click(object sender, EventArgs e)
        {

            
            //for (int i = 0; i < 6; i++)
            //{
            //    Random r = new Random();
            //    var index= r.Next(0, Form1.OrginalRectList.Count - 1);
            //    Form1.OrginalRectList.RemoveAt(index);
            //}
            Console.WriteLine(Form1.OrginalRectList.Count);
            //Form1.OrginalRectList.RemoveAt(0);
            //Form1.OrginalRectList.RemoveAt(Form1.OrginalRectList.Count - 1);
            CVHelper common = new CVHelper();
            var tempList= common.FillFull(Form1.OrginalRectList);
            //var tempList = Form1.OrginalRectList;
            this.picBox.RegionInfo= new RegionInfo(this.picBox.Image.Width, this.picBox.Image.Height, tempList);
            Console.WriteLine(tempList.Count);
            this.picBox.CurrentSelectedRect = this.picBox.RegionInfo;

            //画出
            Mat matOrginal=new Image<Bgr, byte>((Bitmap)this.picBox.Image).Mat;
            foreach (var item in this.picBox.CurrentSelectedRect.RectList)
            {
                CvInvoke.Rectangle(matOrginal, Rectangle.Round(item), new MCvScalar(0, 0, 255));

            }
            CVHelper commonUse = new CVHelper();
            commonUse.SaveMat(matOrginal, "获取裁剪图中的所有轮廓边在原始图中ZoomForm");
        }

        private void Btn_percentTest_Click(object sender, EventArgs e)
        {
            CVHelper common = new CVHelper();

            Rectangle r1 = new Rectangle(1, 1, 200, 200);
            Rectangle r2 = new Rectangle(2, 2, 190, 190);

            var percent=common.DecideOverlap(r2, r1, out Rectangle maxRect);
            MessageBox.Show(percent.ToString());
        }
    }
}
