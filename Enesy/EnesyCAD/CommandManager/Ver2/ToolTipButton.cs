using System;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class ToolTipButton : Button
    {
        private System.Windows.Forms.ToolTip _mToolTip;
        private bool fixed_;

        public string ToolTip
        {
            get
            {
                return this._mToolTip.GetToolTip((Control)this);
            }
            set
            {
                this.SetToolTip(value);
            }
        }

        public ToolTipButton()
        {
            this._mToolTip = new System.Windows.Forms.ToolTip();
            this._mToolTip.ShowAlways = true;
        }

        private void SetToolTip(string tip)
        {
            this._mToolTip.Dispose();
            if (tip.Length == 0)
                return;
            this._mToolTip = new System.Windows.Forms.ToolTip();
            this._mToolTip.SetToolTip((Control)this, tip);
            this._mToolTip.ShowAlways = true;
            this.Click += new EventHandler(this.HandelClick);
            this.fixed_ = true;
        }

        private void HandelClick(object sender, EventArgs e)
        {
            if (this.fixed_)
                return;
            this.ToolTip = this.ToolTip;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.fixed_ = false;
        }
    }
}
