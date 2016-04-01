using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Specialized;
using System.Net;
using System.IO;

using Enesy.Forms;
using Enesy.EnesyCAD.Runtime;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.ApplicationServices;

namespace Enesy.EnesyCAD.CommandManager
{
    public partial class CommandsManagerDialog : System.Windows.Forms.Form
    {
        /// <summary>
        /// Link for help
        /// </summary>
        [DefaultValue(Enesy.Page.CadYoutube)]
        private string Help { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CommandsManagerDialog()
        {
            InitializeComponent();

            // Help topic
            this.Help = CommandsHelp.CommandsManager;
            
            // Init info for viewer
            this.dgrvCommands.DataSource = EneApplication.EneDatabase.CmdTableRecord;
            this.dgrvCommands.AllowUserToAddRows = false;
            this.searchBox.DataSource = EneApplication.EneDatabase.CmdTableRecord;
            this.searchBox.Enabled = true;
            
            // Init treeView
            this.trvSubCommands.Nodes[0].Expand(); // Expand All node

            // Init contextMenu for treeView
            // For root node: "All"
            cmnuRoot.Items.Add("Add new subset ...");
            cmnuRoot.Items.Add("Export to autoCAD menu item ...");
            trvSubCommands.Nodes[0].ContextMenuStrip = cmnuRoot;
            
            // For subset of root
            cmnuSub.Items.Add("Rename ...");
            cmnuSub.Items.Add("Add new subset ...");
            cmnuSub.Items.Add("Delete");
            cmnuSub.Items.Add("Export to autoCAD menu item ...");
            
            
            // Description richTextBox
            this.txtDescription.DataSource = EneApplication.EneDatabase.CmdTableRecord;
            this.txtDescription.DisplayMember = "Description";
            this.txtDescription.Regen();

            // Link label
            lblWebLink.DataSource = EneApplication.EneDatabase.CmdTableRecord;
            lblWebLink.DisplayMember = "Help";
            lblWebLink.Regen();
        }

        /// <summary>
        /// Override ProcessCmdKey method
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.searchBox.Text = "";
                searchBox.Regen();
            }
            if (keyData == Keys.F1)
            {
                System.Diagnostics.Process.Start(Help);
            }
            if (keyData == (Keys.F | Keys.Control))
            {
                searchBox.Focus();
            }
            if (keyData == (Keys.I | Keys.Control))
            {
                mnuImport.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Override OnFormClosing of this form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Calling command when row/cell is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrvCommands_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
            Autodesk.AutoCAD.ApplicationServices.Document doc =
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            string s = dgrvCommands.Rows[e.RowIndex].Cells[0].Value.ToString();
            doc.SendStringToExecute(s + " ", false, false, true);
        }

        private void dgrvCommands_MouseHover(object sender, EventArgs e)
        {
            this.lblStatus.Text = "Double click to calling command";
        }

        private void searchBox_MouseHover(object sender, EventArgs e)
        {
            if (searchBox.DisplayMember == "")
            {
                this.lblStatus.Text = "Select search column...";
            }
            else
            {
                this.lblStatus.Text = "Search " + searchBox.DisplayMember;
            }
        }

        private void butYoutube_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Enesy.Page.CadYoutube);
            }
            catch
            {
                // Do nothing
            }
        }

        private void butHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Enesy.Page.HomePage);
            }
            catch { }
        }

        private void butFanPage_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Enesy.Page.FanPage);
            }
            catch { }
        }

        private void mnuDisplayFavorite_Click(object sender, EventArgs e)
        {
            if (mnuDisplayFavorite.Checked)
            {
                mnuDisplayFavorite.Checked = false;
                splitContainer1.Panel1Collapsed = true;
            }
            else
            {
                mnuDisplayFavorite.Checked = true;
                splitContainer1.Panel1Collapsed = false;
            }
        }

        private void mnuDisplayDescription_Click(object sender, EventArgs e)
        {
            if (mnuDisplayDescription.Checked)
            {
                mnuDisplayDescription.Checked = false;
                pnlDescription.Visible = false;
            }
            else
            {
                mnuDisplayDescription.Checked = true;
                pnlDescription.Visible = true;
            }
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            ImportLispDialog ild = new ImportLispDialog();
            ild.DataSource = EneApplication.EneDatabase.CmdTableRecord;
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ild);
        }

        private void mnuLoadUC_Click(object sender, EventArgs e)
        {
            UCSuite uc = new UCSuite();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(uc);
        }
    }
}