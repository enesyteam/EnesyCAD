using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enesy.Controls;

namespace Enesy.Forms
{
    public partial class SearchBox : UserControl
    {
        

        #region Properties & Field
        /// <summary>
        /// For control column when displayMember changed
        /// </summary>
        bool m_isInit = true;

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
                //if (m_isInit)
                //    this.txtFilter.Text = "Search " + this.txtFilter.DisplayMember;
            }
        }

        #endregion


        private ButtonNoBorder filterButton = new ButtonNoBorder();

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchBox()
        {
            InitializeComponent();
            // add custom controls
            filterButton.BackColor = Color.White;
            filterButton.FlatStyle = FlatStyle.Flat;
            filterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            filterButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            filterButton.Cursor = Cursors.Hand;
            filterButton.FlatAppearance.BorderSize = 0;
            filterButton.Size = new Size(33, this.Height);
            //filterButton.Text = "Columns";
            filterButton.Image = Enesy.Properties.Resources.Filter;
            filterButton.ImageAlign = ContentAlignment.MiddleLeft;
            //filterButton.Location = new Point(0, preferredSize.Height / 2 - filterButton.Height/2);
            filterButton.Click += filterButton_Click;
            this.Controls.Add(filterButton);
            filterButton.Dock = DockStyle.Left;
            //
            this.m_isInit = true;
           // txtFilter.Text = SearchWaterMark;
            this.ContextMenuStrip = this.mnusColumn;
            this.mnusColumn.ItemClicked += 
                new ToolStripItemClickedEventHandler(mnusColumn_ItemClicked);
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            //mnusColumn.Show(Cursor.Position);
            Control ctr = sender as Control;
            Point p = ctr.Location;
            mnusColumn.Show(ctr.PointToScreen(new Point(p.X, p.Y + ctr.Height)));
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
        //private void butColumn_Click(object sender, EventArgs e)
        //{
        //    //mnusColumn.Show(Cursor.Position);
        //    Control ctr = sender as Control;
        //    Point p = ctr.Location;
        //    mnusColumn.Show(ctr.PointToScreen(new Point(p.X, p.Y + ctr.Height)));
        //}
        
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

        }

    }
}
