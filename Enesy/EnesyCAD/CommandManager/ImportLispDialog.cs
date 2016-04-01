using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Enesy.EnesyCAD.IO;
using Enesy.EnesyCAD.DatabaseServices;

namespace Enesy.EnesyCAD.CommandManager
{
    public partial class ImportLispDialog : System.Windows.Forms.Form
    {
        /// <summary>
        /// Control board
        /// </summary>
        private LspImporter LImporter = new LspImporter();

        /// <summary>
        /// Link for help
        /// </summary>
        [DefaultValue(Enesy.Page.CadYoutube)]
        private string Help { get; set; }

        /// <summary>
        /// Storing displayMember of dataSource for restore searchBox in switch tab action
        /// </summary>
        string m_funcDisplayMember = "";
        string m_errDisplayMember = "";
        string m_funcStr = "";
        string m_errStr = "";

        /// <summary>
        /// DataSource of Command Manager, store all valid command
        /// For avoid command duplicated
        /// </summary>
        private DataTable m_dataSource = new DataTable();
        public DataTable DataSource 
        {
            get { return m_dataSource; }
            set
            {
                m_dataSource = value;
            }
        }

        /// <summary>
        /// Constructor for UI
        /// </summary>
        public ImportLispDialog()
        {
            InitializeComponent();
            this.Help = CommandsHelp.ImportLisp;

            // Init tabControl
            this.tabMain.TabsVisible = false;

            // Init dataGridView
            dgrvFunction.DataSource = LImporter.ValidFunction;
            dgrvFunction.AllowUserToAddRows = false;
            dgrvFunction.CellValueChanged += dgrvFunction_CellValueChanged;

            dgrvError.DataSource = LImporter.ErrorFunction;
            dgrvError.AllowUserToAddRows = false;
            dgrvError.CellValueChanged += dgrvError_CellValueChanged;

            // Init searchBox
            searchBox.DataSource = LImporter.ValidFunction;
            m_funcDisplayMember = searchBox.DisplayMember;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.LImporter.ExportToDatabase();
            base.OnClosing(e);
            //if (dgrvFunction.Rows.Count == 0)
            //{
            //    base.OnClosing(e);
            //}
            //else
            //{
            //    DialogResult diaRes = MessageBox.Show(
            //        "Are you want to save user function to database?",
            //        "Question",
            //        MessageBoxButtons.YesNoCancel
            //        );
            //    if (diaRes == DialogResult.OK)
            //    {
            //        this.LImporter.ExportToDatabase();
            //        base.OnClosing(e);
            //    }
            //    else if (diaRes == DialogResult.No)
            //    {
            //        base.OnClosing(e);
            //    }
            //    else
            //    {
            //        // Do nothing
            //    }
            //}
        }

        void dgrvFunction_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //throw new NotImplementedException();
            dgrvFunction.DataSource = null;
            dgrvFunction.DataSource = LImporter.ValidFunction;
        }

        void dgrvError_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //throw new NotImplementedException();
            dgrvError.DataSource = null;
            dgrvError.DataSource = LImporter.ErrorFunction;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                butOpen.PerformClick();
            }
            else if(keyData == (Keys.F1))
            {
                System.Diagnostics.Process.Start(Help);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void butFunction_Click(object sender, EventArgs e)
        {
            this.tabMain.SelectedIndex = 0;
            this.butFunction.BackColor = Color.White;
            this.butError.BackColor = SystemColors.Control;
        }

        private void butError_Click(object sender, EventArgs e)
        {
            this.tabMain.SelectedIndex = 1;
            this.butError.BackColor = Color.White;
            this.butFunction.BackColor = SystemColors.Control;
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Lisp file|*.lsp" + "|Xml file|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] files = ofd.FileNames;
                if (files.Length > 0)
                {
                    // Init progressBar & status
                    pnlProgress.Visible = true;
                    prgImportFiles.Maximum = 100;
                    prgImportFiles.Step = 100 / files.Length;
                    prgImportFiles.Value = 0;
                    lblStatus.Text = "Importing ...";

                    // Importing lisp function
                    LImporter.AddFiles(ofd.FileNames);

                    // Invisible progress bar
                    pnlProgress.Visible = false;
                    lblStatus.Text = "Found " + 
                        dgrvFunction.Rows.Count.ToString() + " function & "
                        + dgrvError.Rows.Count.ToString() + " error(s)";

                    // Press Function / Error button
                    if (dgrvError.Rows.Count > 0) butError.PerformClick();
                    else butFunction.PerformClick();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                EnesyCAD.ApplicationServices.EneApplication.EneDatabase.CmdTableRecord.
                Contains("LENH").ToString());
        }
    }
}
