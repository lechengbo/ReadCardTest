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
    public partial class SunMenuCtrl: UserControl
    {
        private List<SunMenu> SunMenuList { get; set; } = new List<SunMenu>();
        public SunMenuCtrl()
        {
            InitializeComponent();
        }

        public void AddMenu(List<SunMenu> sunMenus)
        {
            
            for (int i = 0; i < sunMenus?.Count; i++)
            {
                var tmpPanel = sunMenus[i].CreatePanel(i);
                this.panelContainer.Controls.Add(tmpPanel);
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

        public List<string> GetExpandInfo() {
            return MenuHelper.GetExpandInfo(this.SunMenuList);
        }

        private void PanelContainer_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("滚动");
        }
    }
}
