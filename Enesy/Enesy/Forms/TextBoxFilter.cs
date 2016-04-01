using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class TextBoxFilter : System.Windows.Forms.TextBox
    {
        private string searchWaterMark = "";
        public string SearchWaterMark
        {
            get { return searchWaterMark; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    searchWaterMark = "Search in:" + value;
                    // Call OnPropertyChanged whenever the property is updated
                    OnPropertyChanged("SearchWaterMark");
                    //MessageBox.Show(SearchWaterMark);
                    this.Text = SearchWaterMark;
                }
            }
        }
        // Declare the event 
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        #region Properties & Field
        /// <summary>
        /// Store previous text
        /// For fixing error when keyPressed event occur, text changed even when ctrl pressed
        /// </summary>
        string m_textBeforeTheChange = String.Empty;
        bool m_ctrlPressed = false;

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
                    if (DataSource != null)
                    {
                        this.TextChanged -= new EventHandler(TextboxFilter_TextChanged);
                        this.TextChanged += new EventHandler(TextboxFilter_TextChanged);
                    }
                    else
                    {
                        this.TextChanged -= new EventHandler(TextboxFilter_TextChanged);
                    }
                }
            }
        }

        /// <summary>
        /// DisplayMember: specify column that is displayed
        /// </summary>
        /// <param name="e"></param>
        private string displayMember = "";
        public virtual string DisplayMember
        {
            get { return this.displayMember; }
            set
            {
                if (displayMember != value)
                {
                    this.displayMember = value;
                    SearchWaterMark = displayMember;
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public TextBoxFilter()
        {
            InitializeComponent();
            base.AutoSize = false;
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;

            m_textBeforeTheChange = this.Text;
           // this.KeyDown += TextBoxFilter_KeyDown;
            this.Enter += TextBoxFilter_Enter;
            this.Leave +=TextBoxFilter_Leave;
            this.Click +=TextBoxFilter_Click;
        }


        private void TextBoxFilter_Click(object sender, EventArgs e)
        {
            if (this.Text == SearchWaterMark)
            {
                this.Text = "";
            }
            else
            {
                SelectAll();
                this.ForeColor = Color.Gray;
            }
        }

        private void TextBoxFilter_Leave(object sender, EventArgs e)
        {
            if (this.Text.Trim() == "")
            {
                this.Text = SearchWaterMark;
                this.ForeColor = Color.Gray;
            }
        }

        private void TextBoxFilter_Enter(object sender, EventArgs e)
        {
            if (this.Text == SearchWaterMark)
            {
                this.Text = "";
            }
            else
            {
                SelectAll();
                this.ForeColor = Color.Gray;
            }
        }

        void TextBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                m_textBeforeTheChange = this.Text;
                m_ctrlPressed = true;
            }
            else if (e.KeyCode == Keys.Escape)
                this.Text = "";
            //throw new NotImplementedException();            
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        #region Event
        void TextboxFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.Text != SearchWaterMark)
            {
                if (m_ctrlPressed)
                {
                    this.Text = m_textBeforeTheChange;
                    return;
                }
                if ((this.dataSource != null) &&
                    (ColumnExist(this.displayMember, this.dataSource)))
                {
                    Filter();
                }
            }
        }
        #endregion

        public void Filter()
        {
            DataTable dt = this.dataSource as DataTable;
            DataView dv = dt.DefaultView;
            dv.RowFilter = CreateExpression(dt, displayMember, this.Text);
        }

        private string CreateExpression(DataTable dt, string colname, string value)
        {
            if (value == "") return "";

            string expression = null;

            if (colname != null && dt.Columns[colname] != null)
            {
                if ("Byte,Decimal,Double,Int16,Int32,Int64,SByte,Single,UInt16,UInt32,UInt64,".Contains(dt.Columns[colname].DataType.Name + ","))
                {
                    expression = colname + "=" + value;
                }
                else if (dt.Columns[colname].DataType == typeof(string))
                {
                    expression = string.Format(colname + " LIKE '%{0}%'", value);
                }
                else if (dt.Columns[colname].DataType == typeof(DateTime))
                {
                    expression = colname + " = #" + value + "#";
                }
            }

            return expression;
        }

        private bool ColumnExist(string column, object datasource)
        {
            DataTable dt = datasource as DataTable;
            return dt.Columns.Contains(column);
        }
    }
}
