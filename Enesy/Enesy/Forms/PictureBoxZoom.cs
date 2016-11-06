using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Enesy.Forms
{
    /// <summary>
    /// Used for PictureBox zooming or unzooming using the mouse wheel.
    /// </summary>
    public class PictureBoxZoom : IMessageFilter
    {
        private PictureBox _pictureBox;
        private Panel _scrollPanel;
        private float _zoom;
        public float ZoomFactor { get { return _zoom; } }

        public delegate void ZoomChange();
        /// <summary>
        /// Zoom change event
        /// </summary>
        public ZoomChange OnZoomChange;

        /// <summary>
        /// PictureBoxZoom contructor.
        /// </summary>
        /// <param name="pictureBox">PictureBox control to be 
        /// zoomed/unzoomed.</param>
        public PictureBoxZoom(PictureBox pictureBox)
        {
            if (pictureBox.Parent.GetType() != typeof(Panel))
            {
                throw new InvalidOperationException(
                    "pictureBox container has to be a panel type");
            }

            _scrollPanel = (Panel)pictureBox.Parent;
            _scrollPanel.AutoScroll = true;

            _pictureBox = pictureBox;
            _pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _pictureBox.MouseWheel += new MouseEventHandler(this.MouseWheel);
            _zoom = 1;
            Application.AddMessageFilter(this);
        }

        /// <summary>
        /// Reset the zoom to the 100% and fire a zoom event
        /// </summary>
        public void Reset()
        {
            Zoom(1);
            if (OnZoomChange != null)
            {
                OnZoomChange();
            }
        }

        /// <summary>
        /// Zoom/unzoom the control by a given factor.
        /// </summary>
        /// <param name="zoom">zoom factor between 0.1 and 8.0</param>
        public void Zoom(float zoom)
        {
            Zoom(zoom, _scrollPanel.AutoScrollPosition);
        }

        /// <summary>
        /// Zoom/unzoom the control by a given factor. 
        /// Factor is applied to the image size.
        /// </summary>
        /// <param name="zoom">zoom factor between 0.1 and 8.0</param>
        /// <param name="pos">position under the cursor which is to be 
        /// retained</param>
        private void Zoom(float zoom, PointF pos)
        {
            // make sure an image is set
            if (_pictureBox.Image != null)
            {
                float oldZoom = _zoom;
                SizeF imageSize = _pictureBox.Image.Size;
                PointF scrollPosition = _scrollPanel.AutoScrollPosition;
                PointF cursorOffset = new PointF(pos.X + scrollPosition.X,
                    pos.Y + scrollPosition.Y);

                _zoom = Math.Max(0.1f, Math.Min(8.0f, zoom));

                // disable the redraw to prevent flicker
                SetRedraw(_scrollPanel, false);

                // scale the zoom box
                _pictureBox.Width = (int)Math.Round(imageSize.Width * _zoom);
                _pictureBox.Height = (int)Math.Round(imageSize.Height * _zoom);

                // calculate the new scroll position
                _scrollPanel.AutoScrollPosition = new Point(
                    (int)Math.Round(_zoom * pos.X / oldZoom) -
                    (int)cursorOffset.X,
                    (int)Math.Round(_zoom * pos.Y / oldZoom) -
                    (int)cursorOffset.Y);
                _scrollPanel.PerformLayout();

                // reenable the redraw
                SetRedraw(_scrollPanel, true);
                _scrollPanel.Refresh();
            }
        }

        /// <summary>
        /// Called when user moves the mouse wheel. Used for 
        /// controling the zoom.
        /// </summary>
        /// <param name="sender">sender control</param>
        /// <param name="e">event</param>
        private void MouseWheel(object sender, MouseEventArgs e)
        {
            Zoom(_zoom + 0.1f * ((e.Delta > 0) ? 1 : -1), e.Location);
            ((HandledMouseEventArgs)e).Handled = true;
            if (OnZoomChange != null)
            {
                OnZoomChange();
            }
        }

        /// <summary>
        /// Since the PictureBox control does not recieve the mouse wheel 
        /// messages when it is in a scroll panel, we need to force the event 
        /// to be forwarded to the PictureBox, rather than the scroll bar.
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x20a;
            // make sure the event is the mouse wheel event
            if (m.Msg == WM_MOUSEWHEEL)
            {
                // get the mouse position
                Point pos = new Point(
                    m.LParam.ToInt32() & 0xffff, // X coord
                    m.LParam.ToInt32() >> 16);   // Y coord
                // determine the handle of the window under the mouse
                IntPtr hWnd = WindowFromPoint(pos);
                // ensure the window validity
                if (hWnd == _pictureBox.Handle)
                {
                    // forward the message to the window
                    SendMessage(_pictureBox.Handle, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Enable/disable redraw of a control. This is called by the internal 
        /// Paint function to prevent the jerking between the positioning 
        /// operations.
        /// </summary>
        /// <param name="ctl">In this case PictureBox control</param>
        /// <param name="enable">true to enable drawing, false to disable 
        /// drawing</param>
        private void SetRedraw(Control ctl, bool enable)
        {
            SendMessage(ctl.Handle, 0xb,
                enable ? (IntPtr)1 : IntPtr.Zero, IntPtr.Zero);
        }

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg,
            IntPtr wp, IntPtr lp);
    }
}
