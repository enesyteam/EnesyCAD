using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

using Utility.ModifyRegistry;
using System.Collections.Generic;

namespace RegisterAutoCADpzo
{
    public partial class DemandLoading : Form
    {
        private string[] FileName;
        private AutoCADVersion autoCADVersion = AutoCADVersion.AutoCAD2007;
        public DemandLoading()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        { 
            // Versions
            string[] versions = new string[] 
            {
                "AutoCAD2007", 
                "AutoCAD2008",
                "AutoCAD2009",
                "AutoCAD2010",
                "AutoCAD2011",
                "AutoCAD2012",
                "AutoCAD2013",
                "AutoCAD2014",
                "AutoCAD2015",
            };
            cmbVersion.Items.AddRange(versions);
            cmbVersion.SelectedIndex = 0;

            this.btnRemove.Enabled = lstAssemblies.SelectedItem != null;
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool SelectFile()
        {
            System.Windows.Forms.OpenFileDialog openDialog = new System.Windows.Forms.OpenFileDialog();
            openDialog.DefaultExt = "dll";
            openDialog.Multiselect = true;
            openDialog.Filter = "Dynamic link library file (*.dll)|*.dll|ObjectARX file (*.arx)|*.arx|All files (*.*)|*.*";
            openDialog.RestoreDirectory = true;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                this.FileName =  openDialog.FileNames;
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectFile())
            {
                foreach (string s in this.FileName)
                {
                    if (lstAssemblies.FindString(s) == -1)
                        lstAssemblies.Items.Add(s);
                    else
                        MessageBox.Show("Duplicate assembly." + "\n" + "Please select another one!", "Existing Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void lstAssemblies_DisplayMemberChanged(object sender, EventArgs e)
        {
            MessageBox.Show(lstAssemblies.Items.Count.ToString());
        }

        private void lstAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnRemove.Enabled = lstAssemblies.SelectedItem != null;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (lstAssemblies.Items.Count == 0)
            {
                MessageBox.Show("Please select assemblies first!", "Empty items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (SelectFile())
                {
                    foreach (string s in this.FileName)
                    {
                        if (lstAssemblies.FindString(s) == -1)
                            lstAssemblies.Items.Add(s);
                        else
                            MessageBox.Show("Duplicate assembly." + "\n" + "Please select another one!", "Existing Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            foreach (var item in lstAssemblies.Items)
            {
                if (RegisteryHelper.Create(item.ToString(), autoCADVersion))
                {

                    //lstAssemblies.Items.Remove(item);

                    MessageBox.Show(Path.GetFileNameWithoutExtension(item.ToString()) + " was added to registery!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(Path.GetFileNameWithoutExtension(item.ToString()) + "was not added to registery!", "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int x = lstAssemblies.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = lstAssemblies.SelectedIndices[x];
                lstAssemblies.Items.RemoveAt(idx);
            } 
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbVersion.SelectedIndex)
            { 
                case 0:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2007;
                    break;
                case 1:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2008;
                    break;
                case 2:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2009;
                    break;
                case 3:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2010;
                    break;
                case 4:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2011;
                    break;
                case 5:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2012;
                    break;
                case 6:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2013;
                    break;
                case 7:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2014;
                    break;
                case 8:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2015;
                    break;
                default:
                    this.autoCADVersion = AutoCADVersion.AutoCAD2007;
                    break;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListBox list = new ListBox();
            List<string> assemblies = RegisteryHelper.ReadAssemblies(this.autoCADVersion);
            foreach (string s in assemblies) list.Items.Add(s);

            AssembliesManager assembliesManager = new AssembliesManager();
            assembliesManager.StartPosition = FormStartPosition.CenterParent;
            assembliesManager.Assemblies = assemblies;
            assembliesManager.AutoCADVersion = this.autoCADVersion;
            assembliesManager.Setup();
            assembliesManager.ShowDialog();
        }
    }
}
