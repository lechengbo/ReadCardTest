using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yue_juan_care.customerControl
{
    public class SunMenu
    {

        private static int width = 235;
        private static int height = 35;
        private static string familyName = "Microsoft YaHei";

        private int level = 1;
        public string Title { get; private set; }
        public SunMenu Parent { get; set; }
        public int Count { get; set; }
        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = Math.Max(1, value);
            }
        }
        public bool Expand { get; set; }

        public object Param { get; set; }
        public List<ClickType> OperateButtonList { get; set; } = new List<ClickType>();
        public List<SunMenu> Children { get; private set; } = new List<SunMenu>();

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Action<ClickType, object> ClickHandler;

        public SunMenu(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("创建SunMenu的标题不能为空");

            }
            this.Title = title;
        }

        /// <summary>
        /// 添加子或孙类，默认为子类，
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="childLevel">1：表示子类，2表示孙，以此类推</param>
        public void AddChild(SunMenu menu, int childLevel = 1)
        {
            menu.Level = this.Level + childLevel;
            this.Children.Add(menu);
            menu.Parent = this;
        }

        public FlowLayoutPanel CreatePanel(int index = 0)
        {
            var panel = new FlowLayoutPanel();

            panel.AutoSize = true;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            panel.Margin = new System.Windows.Forms.Padding(0);
            panel.Name = MenuHelper.CreateName("panel");
            panel.TabIndex = index;
            panel.Padding = new Padding(0);
            panel.Margin = new Padding(0);
            //添加button
            panel.Controls.Add(this.CreateSunButton());
            //添加功能型button
            foreach (var item in this.OperateButtonList)
            {
                panel.Controls.Add(this.CreateOperateButton(item));
            }

            //添加子panel
            for (int i = 0; i < this.Children.Count; i++)
            {
                var lowerPanel = this.Children[i].CreatePanel(i);
                lowerPanel.Visible = this.Expand;
                panel.Controls.Add(lowerPanel);
            }

            return panel;
        }

        public SunButton CreateSunButton()
        {
            float fontSize = 10.5F;
            int leftPadding = 20;

            var fontStyle = FontStyle.Regular;

            SunButton sunButton = new SunButton() { IsExpand = this.Expand, SunMenu = this };

            switch (this.Level)
            {
                case 1:
                    sunButton.Image = sunButton.IsExpand ? Properties.Resources.up : Properties.Resources.down;
                    sunButton.BackgroundImage = Properties.Resources.backFull2x;
                    fontStyle = System.Drawing.FontStyle.Bold;
                    break;
                case 2:
                    fontSize = 9F;
                    sunButton.Image = sunButton.IsExpand ? Properties.Resources.upFull : Properties.Resources.downFull;
                    //sunButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
                    break;
                case 3:
                    fontSize = 9F;
                    leftPadding = 22;
                    break;
                default:
                    break;
            }


            sunButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            sunButton.AutoSize = true;
            sunButton.Cursor = System.Windows.Forms.Cursors.Hand;
            sunButton.FlatAppearance.BorderSize = 0;
            sunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            sunButton.Font = new System.Drawing.Font(familyName, fontSize, fontStyle);
            sunButton.Location = new System.Drawing.Point(0, 0);
            sunButton.Margin = new System.Windows.Forms.Padding(0);
            sunButton.Name = MenuHelper.CreateName("btn");
            sunButton.Padding = new System.Windows.Forms.Padding(leftPadding, 0, 0, 0);
            sunButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            sunButton.Size = new System.Drawing.Size(width, height);
            sunButton.Text = this.Title + (this.level != 3 ? $"({this.Count})" : "");
            sunButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //margin和padding
            if (this.Level == 3)
            {
                sunButton.Size = new System.Drawing.Size(0, height);
                int offset = 4;
                int marginRight = 0;
                sunButton.Margin = new Padding(leftPadding + offset, 0, marginRight, 0);
                sunButton.Padding = new System.Windows.Forms.Padding(0);
            }
            sunButton.UseVisualStyleBackColor = true;
            sunButton.Click += new System.EventHandler((object sender, EventArgs e) =>
            {
                var button = (SunButton)sender;
                button.SunMenu.Expand = button.IsExpand = !button.IsExpand;

                //显示或者隐藏
                switch (button.SunMenu.Level)
                {
                    case 1:
                    case 2:
                        var tmpParent = button.Parent;
                        if (tmpParent.Parent == null)
                        {
                            return;
                        }
                        foreach (var item in tmpParent.Controls)
                        {
                            if (item is FlowLayoutPanel)
                            {
                                var tmpPanel = ((FlowLayoutPanel)item);
                                tmpPanel.Visible = !tmpPanel.Visible;
                            }
                        }
                        break;
                    default:
                        break;
                }

                //改变图标
                switch (button.SunMenu.Level)
                {
                    case 1:
                        button.Image = button.IsExpand ? Properties.Resources.up : Properties.Resources.down;
                        break;
                    case 2:
                        button.Image = button.IsExpand ? Properties.Resources.upFull : Properties.Resources.downFull;
                        break;
                    default:
                        break;

                }

                //针对 菜单的点击事件
                this.ClickHandler?.Invoke(ClickType.Title, this.Param);


            });
            //给最后一级 布局，自动填充中间的空隙宽度
            if (this.Level == 3)
            {
                sunButton.SizeChanged += new EventHandler((object sender, EventArgs e) =>
                {
                    var button = (SunButton)sender;
                    //Console.WriteLine(button.Width);
                    int marginRight = width - 4 - button.Width - button.Margin.Left - button.Padding.Left - button.SunMenu.OperateButtonList.Count * 40;
                    var tmpMargin = button.Margin;
                    tmpMargin.Right = Math.Max(0, marginRight);
                    button.Margin = tmpMargin;
                });

            }
            //给 第二级 菜单 点击前后的颜色变化
            if (this.Level == 2)
            {
                sunButton.Enter += new EventHandler((object sender, EventArgs e) =>
                {
                    var button = (SunButton)sender;
                    button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(188)))), ((int)(((byte)(135)))));

                });
                sunButton.Leave += new EventHandler((object sender, EventArgs e) =>
                {
                    var button = (SunButton)sender;
                    button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
                });
            }

            return sunButton;
        }

        public SunButton CreateOperateButton(ClickType clickType)
        {
            float fontSize = 9F;
            int leftPadding = 0;

            var fontStyle = FontStyle.Regular;

            SunButton sunButton = new SunButton() { IsExpand = this.Expand, SunMenu = this };
            sunButton.AutoSize = true;
            sunButton.Cursor = System.Windows.Forms.Cursors.Hand;
            sunButton.FlatAppearance.BorderSize = 0;
            sunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            sunButton.Font = new System.Drawing.Font(familyName, fontSize, fontStyle);
            sunButton.Location = new System.Drawing.Point(0, 0);
            sunButton.Margin = new System.Windows.Forms.Padding(0);
            sunButton.Name = MenuHelper.CreateName("btn");
            sunButton.Padding = new System.Windows.Forms.Padding(leftPadding, 0, 0, 0);
            sunButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            sunButton.Size = new System.Drawing.Size(0, height);
            sunButton.Text = MenuHelper.GetNameByClickType(clickType);
            sunButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            sunButton.UseVisualStyleBackColor = true;

            switch (clickType)
            {
                case ClickType.Ignore:
                    sunButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(102)))));
                    break;
                default:
                    sunButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
                    break;
            }
            sunButton.Click += new System.EventHandler((object sender, EventArgs e) =>
            {
                var button = (SunButton)sender;
                //针对 菜单的点击事件
                this.ClickHandler?.Invoke(clickType, this.Param);



            });
            return sunButton;
        }

    }

    public class MenuHelper
    {
        public static string CreateName(string pre)
        {
            string guid = Guid.NewGuid().ToString("N"); ;
            return guid.Substring(guid.Length / 2);
        }

        public static string GetNameByClickType(ClickType clickType)
        {
            string name = string.Empty;
            switch (clickType)
            {
                case ClickType.Title:
                    break;
                case ClickType.Delete:
                    name = "删除";
                    break;
                case ClickType.Ignore:
                    name = "忽略";
                    break;
                default:
                    break;
            }

            return name;
        }

        public static List<string> GetExpandInfo(List<SunMenu> sunMenuList)
        {
            var expandMsg = new List<string>();
            foreach (var item in sunMenuList)
            {
                if (item.Expand&& item.Level!=3)
                {
                    expandMsg.Add(item.Title);
                }
                foreach (var child in item.Children)
                {
                    if (child.Expand && child.Level != 3)
                    {
                        expandMsg.Add($"{item.Title}-{child.Title}");
                    }
                }
            }

            return expandMsg;

        }
    }
}
