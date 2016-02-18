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
        public TextBoxFilter()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        #region Properties & Field

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
                    OnDataSourceChanged(new FilterEventArgs(this.dataSource));
                }
            }
        }
        public delegate void OnDataSourceChangedHandler(object sender, FilterEventArgs e);

        public event OnDataSourceChangedHandler DataSourceChanged;


        protected virtual void OnDataSourceChanged(FilterEventArgs e)
        {
            if (DataSourceChanged != null)
            {
                DataSourceChanged(this, e);
            }
            this.TextChanged -= new EventHandler(TextboxFilter_TextChanged);
            this.TextChanged += new EventHandler(TextboxFilter_TextChanged);
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



        #region Event

        void TextboxFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
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

        protected virtual void Filter()
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

    public class FilterEventArgs : EventArgs
    {
        private object _dataSource;

        public FilterEventArgs()
        {

        }

        /// <summary>
        /// Constructor with parameters to initialize values
        /// </summary>
        /// <param name="DataSource">DataSource</param>
        public FilterEventArgs(object DataSource)
        {
            this._dataSource = DataSource;
        }

        /// <summary>
        /// Property to get dataSource
        /// </summary>
        public object DataSource
        {
            get { return this._dataSource; }
        }
    }
}
