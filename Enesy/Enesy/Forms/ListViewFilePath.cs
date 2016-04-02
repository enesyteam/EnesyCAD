using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Enesy.Forms
{
    public partial class ListViewFilePath : System.Windows.Forms.ListView
    {
        private bool Resizing = false;

        public ListViewFilePath()
        {
            InitializeComponent();

            // Add 2 columns: File & Path
            this.Columns.Add("File");
            this.Columns.Add("Path");
            this.View = View.Details;
            this.FullRowSelect = true;

            // Events
            this.SizeChanged += ListViewFilePath_SizeChanged;
        }

        void ListViewFilePath_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing)
            {
                // Set the resizing flag
                Resizing = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    listView.Columns[0].Width = (int)(0.3 * listView.ClientRectangle.Width);
                    listView.Columns[1].Width = (int)(0.7 * listView.ClientRectangle.Width);
                }
            }

            // Clear the resizing flag
            Resizing = false;
        }

        /// <summary>
        /// Add method
        /// </summary>
        /// <param name="path">Full path of file</param>
        public void Add(string path)
        {
            ListViewItem lvi = new ListViewItem(Path.GetFileName(path));
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, path);
            lvi.SubItems.Add(lvsi);
            this.Items.Add(lvi);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
