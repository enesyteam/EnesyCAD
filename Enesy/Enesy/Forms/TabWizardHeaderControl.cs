using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class TabWizardHeaderControl : System.Windows.Forms.TabControl
    {
        public TabWizardHeaderControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// Indicating mode of show/hide header dynamic
        /// true : header will be showed when mouse over header
        /// </summary>
        [DefaultValue(false)]
        public bool HeaderDynamicMode { get; set; }

        /// <summary>
        /// Property indicating show/hide state of tab header
        /// </summary>
        private bool tabsVisible;
        [DefaultValue(true)]
        public bool TabsVisible
        {
            get { return tabsVisible; }
            set
            {
                if (tabsVisible == value) return;
                tabsVisible = value;
                RecreateHandle();
            }
        }

        /// <summary>
        /// Method that control header apperance
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328)
            {
                if (!tabsVisible && !DesignMode)
                {
                    m.Result = (IntPtr)1;
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
