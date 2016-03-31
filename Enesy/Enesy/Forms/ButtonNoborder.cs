using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Controls
{
    /// <summary>
    /// Button with noborder
    /// </summary>
    public class ButtonNoBorder : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.DrawRectangle(new Pen(Brushes.White, 1), this.ClientRectangle);

            int arrowX = ClientRectangle.Width - 10;
            int arrowY = ClientRectangle.Height / 2 - 1;

            Brush brush = Enabled ? Brushes.DimGray : SystemBrushes.ButtonShadow;
            Point[] arrows = new Point[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
            pevent.Graphics.FillPolygon(brush, arrows);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            base.BackColor = Color.White;
        }

    }
}
