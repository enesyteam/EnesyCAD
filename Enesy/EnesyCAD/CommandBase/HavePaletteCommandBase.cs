using Autodesk.AutoCAD.Windows;
using Enesy.EnesyCAD.CommandManager.Ver2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Enesy.EnesyCAD
{
    public class HavePaletteCommandBase : IhavePaletteSet, INotifyPropertyChanged
    {
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private static bool firstInvoke_ = true;
        private int mIndex = 0;
        public int Index {
            get { return mIndex; }
            set { mIndex = value; }
        }
        public ToolBar mToolbar { get; set; }
        public UserControl MyControl { get; set; }
        public string MyPaletteHeader { get; set; }

        private void LoadExtensions()
        {
        }
        public void Active() {
            if (MyControl == null)
                return;
            

            if (!string.IsNullOrEmpty(MyPaletteHeader))
            {
                CMNApplication.ShowESWCmn();
                //add toolbar
                Panel masterPanel = new Panel() {Dock = DockStyle.Fill };

                mToolbar = new ToolBar()
                {
                    ImageList = new ImageList(),
                    Location = new Point(20, 20),
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
                SplitContainer sc = new SplitContainer()
                {
                    Orientation = Orientation.Horizontal,
                    IsSplitterFixed = true,
                    FixedPanel = FixedPanel.Panel1,
                    SplitterDistance = 20,
                    Dock = DockStyle.Fill
                };
                sc.Panel1.Controls.Add(mToolbar);
                MyControl.Dock = DockStyle.Fill;
                masterPanel.Controls.Add(MyControl);

                sc.Panel2.Controls.Add(masterPanel);
                List<string> paletteNames = new List<string>();
                for (int i = 0; i < CMNApplication.ESWCmn.ESW.Count; i++)
                {
                    paletteNames.Add(CMNApplication.ESWCmn.ESW[i].Name);
                }

                if (!paletteNames.Contains(this.MyPaletteHeader))
                {
                    CMNApplication.ESWCmn.Add(MyPaletteHeader, sc);
                }
                for (int i = 0; i < CMNApplication.ESWCmn.ESW.Count; i++)
                {
                    if (CMNApplication.ESWCmn.ESW[i].Name == this.MyPaletteHeader)
                    {
                        CMNApplication.ESWCmn.ESW.Activate(i);
                        return;
                    }
                }
                
            }
        }
        bool isQuitAlert = false;
        private void mToolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            string tag = (string)e.Button.Tag;
            switch (tag)
            {
                case "CLOSE":
                    {
                        for(int i = 0; i<CMNApplication.ESWCmn.ESW.Count; i++)
                        {
                            if (CMNApplication.ESWCmn.ESW[i].Name == this.MyPaletteHeader)
                            {
                                if (isQuitAlert)
                                {
                                    DialogResult dr = MessageBox.Show(String.Format(StringResources.GlobalStringResources.ResourceManager.GetString("CloseDialogWarningMessage", GLOBAL.CurrentCulture), this.MyPaletteHeader), "", MessageBoxButtons.OKCancel);
                                    if (dr == DialogResult.OK)
                                    {
                                        CMNApplication.ESWCmn.ESW.Remove(i);
                                    }
                                }
                                else
                                    CMNApplication.ESWCmn.ESW.Remove(i);
                            }
                        }
                        break;
                    }
            }
        }
        public HavePaletteCommandBase()
        {
            
        }
    }
}
