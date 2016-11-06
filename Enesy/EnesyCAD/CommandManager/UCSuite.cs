using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using eApp = Enesy.EnesyCAD.ApplicationServices.EneApplication;

namespace Enesy.EnesyCAD.CommandManager
{
    public partial class UCSuite : EnesyCAD.Forms.EnesyCADForm
    {
        public UCSuite()
        {
            InitializeComponent();
            AddInfo(eApp.EneCadRegistry.UserCommandFiles);
            this.Help = CommandsHelp.UCSuite;
        }

        public void AddInfo(string[] files)
        {
            foreach (string file in files)
            {
                this.lstvFiles.Add(file);
            }
        }

        public void AddInfo(string file)
        {
            this.lstvFiles.Add(file);
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "xml file|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    if (eApp.EneCadRegistry.AddUserCommand(file))
                    {
                        this.lstvFiles.Add(file);
                    }
                }
            }
            EnesyCAD.ApplicationServices.EneApplication.EneDatabase.ReloadULF();
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lstvFiles.SelectedItems)
            {
                if (eApp.EneCadRegistry.DeleteUserCommand(lvi.SubItems[1].Text))
                {
                    lstvFiles.Items.Remove(lvi);
                }
            }
            EnesyCAD.ApplicationServices.EneApplication.EneDatabase.ReloadULF();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(CommandsHelp.UCSuite);
        }

        private void lstvFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lstvFiles.SelectedItems.Count == 0)
            {
                butRemove.Enabled = false;
            }
            else
            {
                butRemove.Enabled = true;
            }
        }
    }
}
