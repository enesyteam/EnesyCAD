using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class SearchBox : UserControl
    {

        #region Properties & Field

        /// <summary>
        /// Contextmenustrip to show & select column
        /// </summary>
        private ContextMenuStrip mnusColumn = new ContextMenuStrip();

        /// <summary>
        /// DataSource for filter
        /// </summary>
        private object dataSource = null;

        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [DefaultValue(null)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (this.dataSource != value)
                {
                    this.dataSource = value;
                    OnDataSourceChanged();
                }
            }
        }

        /// <summary>
        /// DisplayMember of search box
        /// </summary>
        public string DisplayMember
        {
            get
            {
                return this.txtFilter.DisplayMember;
            }
            set
            {
                this.txtFilter.DisplayMember = value;
            }
        }

        protected virtual void OnDataSourceChanged()
        {
            this.txtFilter.DataSource = this.dataSource;
            if (this.dataSource != null)
            {
                DataTable dt = this.dataSource as DataTable;
                DataColumnCollection dcc = dt.Columns;
                if (dcc.Count == 0) return;

                mnusColumn.Items.Clear();
                foreach (DataColumn dc in dcc)
                {
                    mnusColumn.Items.Add(dc.ColumnName);
                }
            }
        }

        /// <summary>
        /// Text of textBox
        /// </summary>
        public override string Text
        {
            get
            {
                return this.txtFilter.Text;
            }
            set
            {
                this.txtFilter.Text = value;
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchBox()
        {
            InitializeComponent();

            this.txtFilter.Text = "Search...";
            this.ContextMenuStrip = this.mnusColumn;
            this.mnusColumn.ItemClicked += 
                new ToolStripItemClickedEventHandler(mnusColumn_ItemClicked);
        }

        /// <summary>
        /// DataSource Filter
        /// </summary>
        public void Regen()
        {
            if (dataSource != null)
            {
                this.txtFilter.Filter();
            }
        }

        // Events -----------------------------------------------------------------------------
        private void butColumn_Click(object sender, EventArgs e)
        {
            //mnusColumn.Show(Cursor.Position);
            Control ctr = sender as Control;
            Point p = ctr.Location;
            mnusColumn.Show(ctr.PointToScreen(new Point(p.X, p.Y + ctr.Height)));
        }
        
        void mnusColumn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.txtFilter.DisplayMember = e.ClickedItem.Text;
        }

        private void SearchBox_SizeChanged(object sender, EventArgs e)
        {
            this.Height = this.txtFilter.Height + 10;
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {
            this.txtFilter.SelectAll();
        }
    }
}
