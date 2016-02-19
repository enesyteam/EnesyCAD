using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class LinkLabelData : System.Windows.Forms.LinkLabel
    {
        public LinkLabelData()
        {
            InitializeComponent();
            positionChangedHandler = new EventHandler(dataManager_PositionChanged);
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
                    tryDataBinding();
                }
            }
        }

        /// <summary>
        /// DataMember
        /// </summary>
        private string dataMember;

        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
            "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [DefaultValue("")]
        public string DataMember
        {
            get
            {
                return this.dataMember;
            }
            set
            {
                if (this.dataMember != value)
                {
                    this.dataMember = value;
                    tryDataBinding();
                }
            }
        }

        /// <summary>
        /// Currency Manager from BlindingContext
        /// When BlindingContext changed, currencyManager is got to method tryDataBinding
        /// </summary>
        private CurrencyManager dataManager;

        /// <summary>
        /// DisplayMember: specify column that is displayed
        /// </summary>
        /// <param name="e"></param>
        private string displayMember = "";
        public string DisplayMember
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

        /// <summary>
        /// To wire the CurrencyManager with your control 
        /// (get the information Position is changed), we will need below handlers:
        /// </summary>
        /// <param name="e"></param>
        private EventHandler positionChangedHandler;

        #endregion

        /// <summary>
        /// The next code shows the method tryDataBinding. 
        /// It gets the CurrencyManager by the BindingContext (see above), 
        /// unwires the old CurrencyManager (if needed), and wires the new CurrencyManager. 
        /// If the data source changes its position, you get the PositionChanged event.
        /// </summary>
        private void tryDataBinding()
        {
            if (this.DataSource == null ||
                base.BindingContext == null)
                return;

            CurrencyManager cm;
            try
            {
                cm = (CurrencyManager)
                      base.BindingContext[this.DataSource,
                                          this.DataMember];
            }
            catch (System.ArgumentException)
            {
                // If no CurrencyManager was found
                return;
            }

            if (this.dataManager != cm)
            {
                // Unwire the old CurrencyManager
                if (this.dataManager != null)
                {
                    this.dataManager.PositionChanged -=
                        positionChangedHandler;
                }
                this.dataManager = cm;
                // Wire the new CurrencyManager
                if (this.dataManager != null)
                {
                    this.dataManager.PositionChanged +=
                                positionChangedHandler;
                }
            }
        }


        #region Events
        protected override void OnBindingContextChanged(EventArgs e)
        {
            this.tryDataBinding();
            base.OnBindingContextChanged(e);
        }

        /// <summary>
        /// Get the Current Item
        /// The information on which row is current is provided by the CurrencyManager. 
        /// Position contains the numbered index (zero based), 
        /// and Current contains the row-object.
        /// When current position changed, infomation (description - text) of this control
        /// (richTextBox) should be changed.
        /// </summary>
        private void dataManager_PositionChanged(object sender, EventArgs e)
        {
            this.Text = "";
            DataTable dt = dataSource as DataTable;
            DataRowCollection rows = dt.Rows;
            DataRow row = rows[dataManager.Position];
            this.Text = row[displayMember] as string;
        }
        #endregion
    }
}
