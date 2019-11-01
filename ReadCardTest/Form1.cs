using Saraff.Twain;
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

namespace ReadCardTest
{
    public partial class Form1 : Form
    {
        Twain32 _twain = new Twain32();
        public SunMenu CurrentSunMenu1 { get; set; }
        public SunMenu CurrentSunMenu3 { get; set; }

        public int Count { get; set; }
        public Form1()
        {
            InitializeComponent();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                this._twain.Acquire();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SAMPLE1", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Twain321_AcquireCompleted(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            this.flowLayoutPanel3.Visible = false;
            //this.flowLayoutPanel3.Enabled = false;
        }

        private void TableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel5.Visible = !this.flowLayoutPanel5.Visible;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel6.Visible = !this.flowLayoutPanel6.Visible;
        }

        private void Btn_addMenu_Click(object sender, EventArgs e)
        {
            List<SunMenu> sunMenus = GetMenuList();
            
            //sunMenus.InsertRange(0,GetMenuList());
            //sunMenus.InsertRange(0,GetMenuList());
            //sunMenus.InsertRange(0, GetMenuList());
            this.sunMenuCtrl1.AddMenu(sunMenus);
            

            //this.sunMenuCtrl1.panelContainer.VerticalScroll.Value = 100;
            //this.sunMenuCtrl1.panelContainer.AutoScrollPosition = new Point(0, 100);

        }

        private List<SunMenu> GetMenuList()
        {
            List<SunMenu> sunMenus = new List<SunMenu>();
            var menuLevel1 = new SunMenu("答题卡图像不合格"+(++Count))
            {
                Level = 1,
                Count=4,
                Expand=true
            };
            var menuLevel2 = new SunMenu("准考证无法识别") { Level = 2 ,Count=4,Expand=true};
            var menuLevel3_1 = new SunMenu("生物考试1.jpg") { Level = 3, OperateButtonList = new List<ClickType>() { ClickType.Ignore, ClickType.Delete } };
            var menuLevel3_2 = new SunMenu("生物考试2.jpg") { Level = 3, OperateButtonList = new List<ClickType>() { ClickType.Ignore, ClickType.Delete } };
            var menuLevel3_3 = new SunMenu("生物考试3.jpg") { Level = 3, OperateButtonList = new List<ClickType>() { ClickType.Ignore, ClickType.Delete } };
            var menuLevel3_4 = new SunMenu("生物考试4.jpg") { Level = 3, OperateButtonList = new List<ClickType>() { ClickType.Ignore, ClickType.Delete } };
            menuLevel2.AddChild(menuLevel3_1);
            menuLevel2.AddChild(menuLevel3_2);
            menuLevel2.AddChild(menuLevel3_3);
            menuLevel2.AddChild(menuLevel3_4);

            var menuLevel2_1 = new SunMenu("准考证重复") { Level = 2,Count=0 };
            var menuLevel3_5 = new SunMenu("生物考试5.jpg")
            {
                Level = 3,
                OperateButtonList = new List<ClickType>() { ClickType.Delete },
                Param = "我是传递的参数",
                ClickHandler = (type, param) =>
                {
                    Console.WriteLine($"你点击了{type.ToString()},{param}");
                }
            };
            menuLevel2_1.AddChild(menuLevel3_5);

            menuLevel1.AddChild(menuLevel2);
            menuLevel1.AddChild(menuLevel2_1);

            sunMenus.Add(menuLevel1);

            this.CurrentSunMenu1 = menuLevel1;
            this.CurrentSunMenu3 = menuLevel3_4;
            return sunMenus;
        }

        private void Button4_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void Button4_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Enter(object sender, EventArgs e)
        {
            Console.WriteLine("我进入了");
        }

        private void Button4_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("我离开了");
        }

        private void Btn_fresh_Click(object sender, EventArgs e)
        {
            List<SunMenu> sunMenus = GetMenuList();
            sunMenus.AddRange(GetMenuList());
            sunMenus.AddRange(GetMenuList());
            sunMenus.AddRange(GetMenuList());
            this.sunMenuCtrl1.RefreshMenu(sunMenus);
        }

        private void Btn_getExpanInfo_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var item in this.sunMenuCtrl1.GetExpandInfo())
            {
                sb.Append(item).Append(";\n");
                
            }
            MessageBox.Show(sb.ToString()) ;
        }
    }
}
