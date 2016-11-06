using Enesy.EnesyCAD.CommandManager.Ver2;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Enesy.EnesyCAD
{
    public class HavePaletteCommandBase : IhavePaletteSet
    {
        public ToolBar mToolbar { get; set; }
        public UserControl MyControl { get; set; }
        public string MyPaletteHeader { get; set; }
        public void DoCommand() {
            if (MyControl == null)
                return;
            //add toolbar
            Panel masterPanel = new Panel();

            mToolbar = new ToolBar() { 
                ImageList = new ImageList(),
                Location = new Point(20,20),
            Appearance = ToolBarAppearance.Flat,
            ButtonSize = new Size(16, 16),
            Divider = false,
            Dock = DockStyle.Top,
            DropDownArrows = true,
            };

            object obj = GlobalResource.ResourceManager.GetObject("gtk_cancel");
            if (obj != null)
            {
                if (obj is Bitmap)
                    this.mToolbar.ImageList.Images.Add((System.Drawing.Image)obj, Color.Magenta);
                else
                    this.mToolbar.ImageList.Images.Add((Icon)obj);
            }

            ToolBarButton closeButton = new ToolBarButton() { ImageIndex = 0, ToolTipText = "Close", Tag = "CLOSE" };
            mToolbar.Buttons.Add(closeButton);
            mToolbar.ButtonClick += mToolbar_ButtonClick;
            masterPanel.Controls.Add(mToolbar);
            MyControl.Dock = DockStyle.Fill;
            //MyControl.BackColor = Color.Red;
            masterPanel.Controls.Add(MyControl);

            if (!string.IsNullOrEmpty(MyPaletteHeader))
            {
                CMNApplication.ESWCmn.Add(MyPaletteHeader, masterPanel);
            }
        }

        private void mToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            string tag = (string)e.Button.Tag;
            switch (tag)
            {
                case "CLOSE":
                    CMNApplication.ESWCmn.ESW.Remove(1);
                    break;
            }
        }
        public HavePaletteCommandBase()
        {
            CMNApplication.ShowESWCmn();
        }
    }
}
