using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class XFManager : Form
    {
        /// <summary>
        /// Datasource
        /// This database contains Command, Author, Tab, Help (link), Description
        /// </summary>
        private DataTable dataSource = new DataTable();

        public XFManager()
        {
            InitializeComponent();
            DataStructure();
            grvMain.DataSource = this.dataSource;
            schSearch.DataSource = this.dataSource;
            rchDescription.DataSource = this.dataSource;
            rchDescription.DisplayMember = "Description";
            lldHelp.DataSource = this.dataSource;
            lldHelp.DisplayMember = "Help";
        }

        private void mnuiImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Lisp file|*.lsp";
            ofd.ShowDialog();

            XFImporter xfI = new XFImporter(ofd.FileName);
            xfI.ShowDialog();

            ImportFunction(xfI.FunctionInfos);
        }

        private void ImportFunction(object data)
        {
            DataTable dt = data as DataTable;
            DataRowCollection rows = dt.Rows;
            foreach (DataRow r in rows)
            {
                dataSource.ImportRow(r);
            }
        }

        private void DataStructure()
        {
            dataSource.Columns.Add("Type", typeof(string));
            dataSource.Columns.Add("Command", typeof(string));
            dataSource.Columns.Add("Tab", typeof(string));
            dataSource.Columns.Add("Author", typeof(string));
            dataSource.Columns.Add("Help", typeof(string));
            dataSource.Columns.Add("Description", typeof(string));
        }
    }
}