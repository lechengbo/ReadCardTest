using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yue_juan_care.customerControl
{
    
    public partial class SunButton : Button
    {
        public ClickType ButtonType { get; set; } = ClickType.Title;
        public bool  IsExpand { get; set; }
        public SunMenu SunMenu { get; set; }
        //public object Param { get; set; }
        public SunButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }

    /// <summary>
    /// 点击类型
    /// </summary>
    public enum ClickType
    {
        /// <summary>
        /// 点击标题
        /// </summary>
        Title,
        /// <summary>
        /// 点击删除
        /// </summary>
        Delete,
        /// <summary>
        /// 点击忽略
        /// </summary>
        Ignore,
    }
}
