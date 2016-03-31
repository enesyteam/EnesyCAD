using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegisterAutoCADpzo
{
    public partial class AssembliesManager : Form
    {
        public List<string> Assemblies = new List<string>();
        public AutoCADVersion AutoCADVersion = AutoCADVersion.AutoCAD2007;
        public AssembliesManager()
        {
            InitializeComponent();
        }
        public void Setup()
        {
            foreach (string s in Assemblies)
                lstAssemblies.Items.Add(s);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            RegistryKey currentKey = Registry.CurrentUser.OpenSubKey(RegisteryHelper.KeyFromAutoCADVersion(this.AutoCADVersion), true);
            if (!IsReadonly(lstAssemblies.SelectedItem.ToString()))
            {
                DialogResult rs = MessageBox.Show("Do you want to unload: " + lstAssemblies.SelectedItem.ToString(), "Unload Assembly?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == System.Windows.Forms.DialogResult.Yes)
                {
                    currentKey.DeleteSubKey(lstAssemblies.SelectedItem.ToString());
                    currentKey.Close();

                    for (int x = lstAssemblies.SelectedIndices.Count - 1; x >= 0; x--)
                    {
                        int idx = lstAssemblies.SelectedIndices[x];
                        lstAssemblies.Items.RemoveAt(idx);
                    }
                }
            }
            else
            {
                MessageBox.Show("This assembly is readonly!", "Readonly!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private bool IsReadonly(string item)
        {
            List<string> ReadOnlyList = new List<string>();
            ReadOnlyList.Add("AcadAcLaunchNFW");
            ReadOnlyList.Add("AcadAppload");
            ReadOnlyList.Add("AcadPlotStamp");
            ReadOnlyList.Add("Interop.WIA");
            ReadOnlyList.Add("System");
            ReadOnlyList.Add("WSCommCntrAcCon");
            ReadOnlyList.Add("AcMr");
            //ReadOnlyList.Add("AutoCADpzo");
            if (ReadOnlyList.Contains(item))
                return true;
            else
                return false;
        }
    }
}
