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
            m_textBeforeTheChange = this.Text;
            this.KeyDown += TextBoxFilter_KeyDown;
        }

        void TextBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                m_textBeforeTheChange = this.Text;
                m_ctrlPressed = true;
            }
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
            try
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
            catch
            {
                // Do nothing
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
