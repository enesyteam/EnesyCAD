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

        public ClipboardPalette()
        {
            InitializeComponent();

            // Register ourselves to handle clipboard modifications

            _nxtCbVwrHWnd = SetClipboardViewer(Handle);
        }

        private void AddDataToGrid()
        {
            DataObject currentClipboardData = Clipboard.GetDataObject() as DataObject;

            // If the clipboard contents are AutoCAD-related


            if (IsAutoCAD(currentClipboardData.GetFormats()))
            {
                // Create a new row for our grid and add our clipboard
                // data stored in the "tag"

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Tag = currentClipboardData;

                // Increment our counter

                _clipboardCounter += 1;

                // Create and add a cell for the name, using our counter

                DataGridViewTextBoxCell newNameCell = new DataGridViewTextBoxCell();
                newNameCell.Value = "Clipboard " + _clipboardCounter;
                newRow.Cells.Add(newNameCell);

                // Get the current time and place that in another cell

                DataGridViewTextBoxCell newTimeCell = new DataGridViewTextBoxCell();
                newTimeCell.Value = DateTime.Now.ToLongTimeString();
                newRow.Cells.Add(newTimeCell);

                // Add our row to the data grid and select it

                clipboardDataGridView.Rows.Add(newRow);
                clipboardDataGridView.FirstDisplayedScrollingRowIndex = clipboardDataGridView.Rows.Count - 1;
                newRow.Selected = true;

            }

        }
        // Move the selected item's data into the clipboard

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
                Clipboard.SetDataObject(clipboardDataGridView.SelectedRows[0].Tag);
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

        // Our context-menu command handlers


        private void PasteToolStripButton_Click(object sender, EventArgs e)
        {
            // Swap the data from the selected item in the grid into the
            // clipboard and use the internal AutoCAD command to paste it

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteclip ");
            }
        }


        private void PasteAsBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Swap the data from the selected item in the grid into the
            // clipboard and use the internal AutoCAD command to paste it
            // as a block

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteblock ");
            }
        }


        private void PasteToOriginalCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Swap the data from the selected item in the grid into the
            // clipboard and use the internal AutoCAD command to paste it
            // at the original location

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteorig ");
            }
        }


        private void RemoveAllToolStripButton_Click(object sender, EventArgs e)
        {
            // Remove all the items in the grid

            clipboardDataGridView.Rows.Clear();
        }


        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Rename the selected row by editing the name cell

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                clipboardDataGridView.BeginEdit(true);
            }
        }


        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Remove the selected grid item

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                clipboardDataGridView.Rows.Remove(clipboardDataGridView.SelectedRows[0]);
            }
        }

        // Our grid view event handlers


        private void ClipboardDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            // On right-click display the row as selected and show
            // the context menu at the location of the cursor

            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = clipboardDataGridView.HitTest(e.X, e.Y);

                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    clipboardDataGridView.ClearSelection();
                    clipboardDataGridView.Rows[hti.RowIndex].Selected = true;

                    ContextMenuStrip.Show(clipboardDataGridView, e.Location);
                }
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

        private void clipboardDataGridView_CellMouseDown_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clipboardDataGridView.CurrentCell = clipboardDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void clipboardDataGridView_MouseDown_1(object sender, MouseEventArgs e)
        {
            // On right-click display the row as selected and show
            // the context menu at the location of the cursor

            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = clipboardDataGridView.HitTest(e.X, e.Y);

                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    clipboardDataGridView.ClearSelection();
                    clipboardDataGridView.Rows[hti.RowIndex].Selected = true;

                    rightClickMenu.Show(clipboardDataGridView, e.Location);

                }
            }
        }

        private void PasteAsBlockToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteblock ");
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Swap the data from the selected item in the grid into the
            // clipboard and use the internal AutoCAD command to paste it

            if (clipboardDataGridView.SelectedRows.Count == 1)
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

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                PasteToClipboard();
                SendAutoCADCommand("_pasteorig ");
            }
        }

        private void RenameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Rename the selected row by editing the name cell

            if (clipboardDataGridView.SelectedRows.Count == 1)
            {
                clipboardDataGridView.BeginEdit(true);
            }
        }

        private void RemoveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            clipboardDataGridView.Rows.Clear();
        }

        private void clipboardDataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (clipboardDataGridView.SelectedRows.Count > 0)
            {
                DataGridView selected = sender as DataGridView;
                DataObject obj = selected.SelectedRows[0].Tag as DataObject;
                if (obj.GetDataPresent("Bitmap"))
                {
                    System.Drawing.Bitmap b = obj.GetData("Bitmap") as System.Drawing.Bitmap;
                    double ratio = b.Height / b.Width;
                    //Img.Height = _img.Width * ratio
                    Split.SplitterDistance = (int)(Split.Height - (Img.Width * ratio));
                    Img.Image = b;
                }
            }
        }

        private void RemoveAllToolStripButton_Click_1(object sender, EventArgs e)
        {
            clipboardDataGridView.Rows.Clear();
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
