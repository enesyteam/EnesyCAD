using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AcApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Enesy.Forms;

namespace Enesy.EnesyCAD.Utilities.ClipboardManager
{
    public partial class ClipboardPalette : UserControl
    {
        private const int WM_DRAWCLIPBOARD = 0x308;

        private const int WM_CHANGECBCHAIN = 0x30d;
        // Handle for next clipboard viewer


        private IntPtr _nxtCbVwrHWnd;
        // Boolean to control access to clipboard data


        private bool _internalHold = false;
        // Counter for our visible clipboard name


        private int _clipboardCounter = 0;
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        // Windows API declarations

        public static extern IntPtr SetClipboardViewer(IntPtr HWnd);
        [DllImport("User32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern long SendMessage(IntPtr HWnd, int Msg, IntPtr wParam, IntPtr lParam);

        // Class constructor
        public PictureBoxZoom ClipboardImage { get; set; }
        public ClipboardPalette()
        {
            InitializeComponent();
            // Register ourselves to handle clipboard modifications
            _nxtCbVwrHWnd = SetClipboardViewer(Handle);
            ClipboardImage = new PictureBoxZoom(pictureBox);
            ClipboardImage.OnZoomChange += UpdateZoomFactorLabel;
            UpdateZoomFactorLabel();
        }

        private void UpdateZoomFactorLabel()
        {
            previewInfoText.Text = "Zoom: " + ClipboardImage.ZoomFactor;
        }

        private void AddDataToGrid()
        {
            DataObject currentClipboardData = Clipboard.GetDataObject() as DataObject;
            // If the clipboard contents are AutoCAD-related
            if (IsAutoCAD(currentClipboardData.GetFormats()))
            {
                // Create a new row for our grid and add our clipboard
                // data stored in the "tag"
                ListViewItem lvi = new ListViewItem("Clipboard " + _clipboardCounter); // { Tag = currentClipboardData };
                lvi.Tag = currentClipboardData;
                lvi.SubItems.Add(DateTime.Now.ToLongTimeString());
                clbList.Items.Add(lvi);
                clbList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                _clipboardCounter++;
            }

        }
        // Check whether the clipboard data was created by AutoCAD
        private bool IsAutoCAD(string[] Formats)
        {
            foreach (string item in Formats)
            {
                if (item.Contains("AutoCAD") & !(item.Contains("MText") | item.Contains("DText")))
                {
                    return true;
                }
            }
            return false;
        }
        private void PasteToClipboard()
        {
            // Use a variable to make sure we don't edit the
            // clipboard contents at the wrong time
            _internalHold = true;
            try
            {
                Clipboard.SetDataObject(clbList.SelectedItems[0].Tag);
            }
            catch
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Unable to place entry onto the clipboard.");
            }
            _internalHold = false;
        }

        // Send a command to AutoCAD


        private void SendAutoCADCommand(string cmd)
        {
            AcApp.DocumentManager.MdiActiveDocument.SendStringToExecute(cmd, true, false, true);
        }


        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clbList.SelectedItems.Count == 1)
            {
                clbList.SelectedItems[0].BeginEdit();
            }
        }

        // Override WndProc to get messages
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // The clipboard has changed

                case  // ERROR: Case labels with binary operators are unsupported : Equality
    WM_DRAWCLIPBOARD:

                    if (!_internalHold)
                        AddDataToGrid();

                    //SendMessage(_nxtCbVwrHWnd, m.Msg, m.WParam, m.LParam);

                    break;
                // Another clipboard viewer has removed itself

                case  // ERROR: Case labels with binary operators are unsupported : Equality
    WM_CHANGECBCHAIN:

                    if (m.WParam == (IntPtr)_nxtCbVwrHWnd)
                    {
                        _nxtCbVwrHWnd = m.LParam;
                    }
                    else
                    {
                       // SendMessage(_nxtCbVwrHWnd, m.Msg, m.WParam, m.LParam);
                    }

                    break;
            }
            base.WndProc(ref m);
        }

        private void PasteAsBlockToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (clbList.SelectedItems.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteblock ");
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Swap the data from the selected item in the grid into the
            // clipboard and use the internal AutoCAD command to paste it

            if (clbList.SelectedItems.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteclip ");
            }
        }

        private void PasteToOriginalCoordinatesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Swap the data from the selected item in the grid into the
            // clipboard and use the internal AutoCAD command to paste it
            // at the original location

            if (clbList.SelectedItems.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteorig ");
            }
        }

        private void RenameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (clbList.SelectedItems.Count == 1)
            {
                clbList.SelectedItems[0].BeginEdit();
            }
        }

        private void RemoveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //clipboardDataGridView.Rows.Clear();
            clbList.Items.Clear();
        }


        private void RemoveAllToolStripButton_Click_1(object sender, EventArgs e)
        {
            clbList.Items.Clear();
        }

        private void clbList_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                if (clbList.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    rightClickMenu.Show(Cursor.Position);
                }
            } 
        }

        private void clbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView selected = sender as ListView;
            if (selected.SelectedItems.Count == 1)
            {
                DataObject obj = selected.SelectedItems[0].Tag as DataObject;
                if (obj.GetDataPresent("Bitmap"))
                {
                    System.Drawing.Bitmap b = obj.GetData("Bitmap") as System.Drawing.Bitmap;
                    double ratio = b.Height / b.Width;
                    //Img.Height = _img.Width * ratio
                    //Split.SplitterDistance = (int)(Split.Height - (Img.Width * ratio));
                    pictureBox.Image = b;
                    //previewInfoText.Text = "Preview " + selected.SelectedItems[0].Text +  " (Use MouseWheel to Zoom in and Zoom out.)";
                    previewInfoText.Text = "Zoom: " + ClipboardImage.ZoomFactor;
                }
            }
        }
    }

    


    public class PaletteToolStrip : ToolStrip
    {

        public PaletteToolStrip()
            : base()
        {
        }

        public PaletteToolStrip(params ToolStripItem[] Items)
            : base(Items)
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x21 && CanFocus && !Focused)
            {
                Focus();
            }
            base.WndProc(ref m);
        }
        
    }
}
