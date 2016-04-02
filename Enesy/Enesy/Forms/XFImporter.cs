using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class XFImporter : Form
    {
        public XFImporter(string path)
        {
            InitializeComponent();
            this.DataStructure();
            this.grvMain.DataSource = this.functionInfos;
            this.schFilter.DataSource = this.functionInfos;
            this.rchdDescription.DataSource = this.functionInfos;
            this.rchdDescription.DisplayMember = "Description";
            Enesy.EnesyCAD.IO.LispFileReader lfr =
                                    new Enesy.EnesyCAD.IO.LispFileReader(path);
            SendToDataSource(lfr.GetFunctions());
        }

        /// <summary>
        /// DataSource
        /// </summary>
        DataTable functionInfos = new DataTable();
        public DataTable FunctionInfos
        {
            get { return this.functionInfos; }
        }

        private void mnuiOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Lisp file |*.lsp";
            ofd.ShowDialog();
            string lispPath = ofd.FileName;

            Enesy.EnesyCAD.IO.LispFileReader lfr = 
                                    new Enesy.EnesyCAD.IO.LispFileReader(lispPath);
            SendToDataSource(lfr.GetFunctions());
        }

        private void DataStructure()
        {
            functionInfos.Columns.Add("Type", typeof(string));
            functionInfos.Columns.Add("Command", typeof(string));
            functionInfos.Columns.Add("Tab", typeof(string));
            functionInfos.Columns.Add("Author", typeof(string));
            functionInfos.Columns.Add("Help", typeof(string));
            functionInfos.Columns.Add("Description", typeof(string));
        }

        private void SendToDataSource(List<string> funcList)
        {
            foreach (string func in funcList)
            {
                DataRow dr = functionInfos.NewRow();
                dr["Command"] = func;
                functionInfos.Rows.Add(dr);
            }
        }
    }
}