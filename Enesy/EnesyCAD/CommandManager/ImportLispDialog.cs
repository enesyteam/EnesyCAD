using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Enesy.EnesyCAD.IO;

namespace Enesy.EnesyCAD.CommandManager
{
    public partial class ImportLispDialog : System.Windows.Forms.Form
    {
        /// <summary>
        /// Link for help
        /// </summary>
        [DefaultValue(Enesy.Page.CadYoutube)]
        private string Help { get; set; }

        /// <summary>
        /// Store information of lisp function and errors
        /// </summary>
        DataTable m_function = new DataTable();
        DataTable m_error = new DataTable();

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
        public DataTable DataSource { get; set; }

        public ImportLispDialog()
        {
            InitializeComponent();
            this.Help = CommandsHelp.ImportLisp;

            // Init tabControl
            this.tabMain.TabsVisible = false;

            // Init dataSource
            m_function.Columns.Add("Commands");
            m_function.Columns.Add("Tab");
            m_function.Columns.Add("Description");
            m_function.Columns.Add("Author");
            m_function.Columns.Add("Email");
            m_function.Columns.Add("Help");
            m_function.Columns.Add("File");     // Invisible column
            m_function.Columns.Add("Line");     // Invisible column
            
            m_error.Columns.Add("Commands");
            m_error.Columns.Add("Error");
            m_error.Columns.Add("File");
            m_error.Columns.Add("Line");

            // Init dataGridView
            dgrvFunction.DataSource = m_function;
            dgrvError.DataSource = m_error;
            dgrvFunction.Columns["File"].Visible = false;
            dgrvFunction.Columns["Line"].Visible = false;

            // Init searchBox
            searchBox.DataSource = m_function;
            m_funcDisplayMember = searchBox.DisplayMember;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                butOpen.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Engine of this class
        /// Import lisp files and send valid & invalid lisp function to source
        /// </summary>
        /// <param name="fileNames"></param>
        private void ImportLispFiles(string[] fileNames)
        {
            if (DataSource == null)
            {
                MessageBox.Show("Error: Checking lisp function\nDataSource is null!");
                return;
            }
            foreach (string file in fileNames)
            {
                LispReader rd = new LispReader(file);
                List<LispFunction> lspFunc = rd.ListMainFunction();
                foreach (LispFunction func in lspFunc)
                {
                    if (!CheckExist(func, DataSource))
                    {
                        if (!CheckExist(func, m_function))
                        {
                            // If this function is not exist, add it to valid function
                            DataRow r = m_function.NewRow();
                            r[0] = func.GlobalName;
                            m_function.Rows.Add(r);
                        }
                        else
                        {
                            DataRow r = m_error.NewRow();
                            r[0] = func.GlobalName;
                            r[1] = "Duplicated to command in current lisp files";
                            r[2] = func.FileName;
                            r[3] = func.Line;
                            m_error.Rows.Add(r);
                        }
                    }
                    else
                    {
                        DataRow r = m_error.NewRow();
                        r[0] = func.GlobalName;
                        r[1] = "Duplicated to command in manager";
                        r[2] = func.FileName;
                        r[3] = func.Line;
                        m_error.Rows.Add(r);
                    }
                    prgImportFiles.PerformStep();
                }
            }
        }

        /// <summary>
        /// Check whether specified lisp function is exist in source or not
        /// </summary>
        /// <param name="lFunc"></param>
        /// <param name="source"></param>
        private bool CheckExist(LispFunction lFunc, DataTable source)
        {
            bool flag = false;
            try
            {
                DataRow[] found = source.Select("Commands ='" + lFunc.GlobalName + "'");
                flag = (found.Length > 0 ? true : false);
            }
            catch
            {
                // Do nothing
            }
            return flag;
        }

        private void butFunction_Click(object sender, EventArgs e)
        {
            this.tabMain.SelectedIndex = 0;
            this.butFunction.BackColor = Color.White;
            this.butError.BackColor = SystemColors.Control;
            this.m_errDisplayMember = searchBox.DisplayMember;
            this.m_errStr = searchBox.Text;
            searchBox.DisplayMember = m_funcDisplayMember;
            searchBox.Regen();
        }

        private void butError_Click(object sender, EventArgs e)
        {
            this.tabMain.SelectedIndex = 1;
            this.butError.BackColor = Color.White;
            this.butFunction.BackColor = SystemColors.Control;
            this.m_funcDisplayMember = searchBox.DisplayMember;
            this.m_funcStr = searchBox.Text;
            searchBox.DisplayMember = m_errDisplayMember;
            searchBox.Regen();
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
                    this.ImportLispFiles(files);

                    // Invisible progress bar
                    pnlProgress.Visible = false;
                    lblStatus.Text = "Found " + files.Length.ToString() + " function & "
                        + dgrvError.Rows.Count.ToString() + " error(s)";

                    // Press Function / Error button
                    if (dgrvError.Rows.Count > 0) butError.PerformClick();
                    else butFunction.PerformClick();
                }
            }
        }
    }
}
