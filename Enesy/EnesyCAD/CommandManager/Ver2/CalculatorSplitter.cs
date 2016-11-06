using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class CalculatorSplitter : Splitter
    {
        public CalculatorSplitter(Control control1, Control control2)
        {
            Control parent = control1.Parent;
            if (parent != control2.Parent)
                return;
            parent.SuspendLayout();
            if (parent.Controls.IndexOf(control2) > parent.Controls.IndexOf(control1))
                parent.Controls.SetChildIndex(control2, 0);
            control2.Dock = DockStyle.Fill;
            control1.Dock = DockStyle.Top;
            this.Dock = DockStyle.Top;
            this.Height = 4;
            this.Cursor = Cursors.HSplit;
            this.SplitterMoved += new SplitterEventHandler(this.SplitterLocationChanged);
            parent.Controls.Add((Control)this);
            parent.Controls.SetChildIndex((Control)this, 1);
            parent.ResumeLayout();
        }

        protected override void OnSplitterMoving(SplitterEventArgs e)
        {
            int num;
            if ((num = ((CMNControl)this.Parent).CommandListResizeAmount(e.Y)) <= 0)
                return;
            if (e.Y != num)
                base.OnSplitterMoving(new SplitterEventArgs(e.X, num, e.SplitX, num));
            else
                base.OnSplitterMoving(e);
        }

        private void SplitterLocationChanged(object sender, SplitterEventArgs e)
        {
            ((CMNControl)this.Parent).UpdateControlSizes();
        }
    }
}