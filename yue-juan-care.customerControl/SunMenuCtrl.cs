using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yue_juan_care.customerControl
{
    public partial class SunMenuCtrl : UserControl
    {
        private List<SunMenu> SunMenuList { get; set; } = new List<SunMenu>();
        private SunButton CurrentTitleButton { get; set; }
        public SunMenuCtrl()
        {
            var startTime = DateTime.Now;
            InitializeComponent();
            var endTime = DateTime.Now;
            var timespan = endTime - startTime;
            Console.WriteLine($"总耗时耗时：{timespan.TotalMilliseconds}");
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |  //全部在窗口绘制消息中绘图
                ControlStyles.OptimizedDoubleBuffer, //使用双缓冲
                true);
        }
        ///<summary>
        /// 设置控件窗口创建参数的扩展风格
        ///</summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}


        public void AddMenu(List<SunMenu> sunMenus)
        {

            for (int i = 0; i < sunMenus?.Count; i++)
            {
                var tmpPanel = sunMenus[i].CreatePanel(i);
                this.panelContainer.Controls.Add(tmpPanel);
                //加入一级菜单的关闭同级菜单事件
                var currentMenu = sunMenus[i];
                if (currentMenu.Level == 1 && currentMenu.TitleButton != null)
                {
                    currentMenu.TitleButton.Click += (object sender, EventArgs e) =>
                     {
                         this.CloseLevel1Menu(currentMenu);
                     };
                }
                //加入颜色改变事件
                SetButtonClick(currentMenu);
            }

            this.SunMenuList.AddRange(sunMenus);
            this.panelContainer.Invalidate();
        }

        public void RefreshMenu(List<SunMenu> sunMenus)
        {
            int verticalValue = this.panelContainer.VerticalScroll.Value;

            this.panelContainer.Controls.Clear();
            //while (this.panelContainer.Controls.Count > 0)
            //{
            //    this.panelContainer.Controls.Clear();//.RemoveAt(0);
            //}
            this.SunMenuList = new List<SunMenu>();
            this.AddMenu(sunMenus);

            this.panelContainer.AutoScrollPosition = new Point(0, verticalValue);
        }

        public List<string> GetExpandInfo()
        {
            return MenuHelper.GetExpandInfo(this.SunMenuList);
        }

        public void CloseLevel1Menu(SunMenu menu)
        {
            if (!menu.Expand)
            {
                return;
            }
            this.SunMenuList.ForEach(m =>
            {
                if (m.Expand && m != menu)
                {
                    m.Toggle(false);
                }
            });
        }

        /// <summary>
        /// 颜色改变事件
        /// </summary>
        /// <param name="menu"></param>
        public void SetButtonClick(SunMenu menu)
        {
            //加入二三级菜单的点击颜色变化事件
            if (menu.Level > 1 && menu.TitleButton != null)
            {
                menu.TitleButton.Click += (object sender, EventArgs e) =>
                {
                    //恢复颜色
                    if (this.CurrentTitleButton != null)
                    {
                        CurrentTitleButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));

                    }
                    CurrentTitleButton = (SunButton)sender;
                    //设置点击后的颜色
                    CurrentTitleButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(188)))), ((int)(((byte)(135)))));
                };

            }
            menu.Children.ForEach(c =>
            {
                SetButtonClick(c);
            });
        }

        private void PanelContainer_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("滚动");
        }

        private void SunMenuCtrl_Layout(object sender, LayoutEventArgs e)
        {
            Console.WriteLine($"菜单控件布局完成,{DateTime.Now.Ticks}");
        }

        private void SunMenuCtrl_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine($"菜单控件绘制完成,{DateTime.Now.Ticks}");
        }
    }
}
