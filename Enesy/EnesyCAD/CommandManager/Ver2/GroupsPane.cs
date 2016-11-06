using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class GroupsPane : UserControl
    {
        public static int GROUP_VERT_SPACE = 8;
        public CMNControl mParent;
        private Container components;
        private ArrayList _mGroups;

        public ArrayList Groups
        {
            get
            {
                return this._mGroups;
            }
        }

        public int RestoredHeight
        {
            get
            {
                int num = 0;
                foreach (GroupControl control in (ArrangedElementCollection)this.Controls)
                    num += control.Minimized ? GroupControl.GROUP_TITLE_HEIGHT : control.RestoredHeight;
                if (this.Controls.Count > 1)
                    num += (this.Controls.Count - 1) * GroupsPane.GROUP_VERT_SPACE;
                return num;
            }
        }

        public GroupsPane(CMNControl parent)
        {
            this.InitializeComponent();
            this.mParent = parent;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GroupsPane
            // 
            this.AutoScroll = true;
            this.Name = "GroupsPane";
            this.Size = new System.Drawing.Size(1230, 560);
            this.ResumeLayout(false);

        }

        public bool Populate(cmnControlData cdata)
        {
            if (cdata == null)
            {
                return false;
            }
            this._mGroups = new ArrayList();
            this.SuspendLayout();
            int num = 0;
            foreach (cmnGroupData group in cdata.GroupList)
            {
                System.Type type = group.GetType();
                GroupControl groupControl = (GroupControl)new ButtonGroupCtrl(this);
                if (groupControl.Populate(group))
                {
                    groupControl.Left = 20;
                    groupControl.Top = num;
                    groupControl.Width = this.Width;
                    groupControl.Height = groupControl.RestoredHeight;
                    this.Groups.Add(groupControl);
                    this.Controls.Add(groupControl);
                    num = groupControl.Bottom + GroupsPane.GROUP_VERT_SPACE;
                }
                else
                    break;
            }
            this.ReAlignControls();
            this.ResumeLayout(true);
            return true;
        }

        public bool LoadConfiguration(IConfigurationSection parentSection)
        {
            if (parentSection == null)
                return false;
            string name = "Groups";
            if (parentSection.ContainsSubsection(name))
            {
                IConfigurationSection parentSection1 = parentSection.OpenSubsection(name);
                if (parentSection1 != null)
                {
                    foreach (GroupControl group in this.Groups)
                        group.LoadConfiguration(parentSection1);
                    parentSection1.Close();
                    return true;
                }
            }
            return false;
        }

        public bool SaveConfiguration(IConfigurationSection parentSection)
        {
            if (parentSection == null)
                return false;
            string name = "Groups";
            if (parentSection.ContainsSubsection(name) && this.mParent.Host is cmnESW)
                parentSection.DeleteSubsection(name);
            IConfigurationSection subsection = parentSection.CreateSubsection(name);
            if (subsection == null)
                return false;
            foreach (GroupControl group in this.Groups)
                group.SaveConfiguration(subsection);
            subsection.Close();
            return true;
        }

        public void ThemeChanged()
        {
            foreach (GroupControl group in this.Groups)
                group.ThemeChanged();
        }

        public bool KeepFocus()
        {
            foreach (GroupControl control in (ArrangedElementCollection)this.Controls)
            {
                if (control.KeepFocus())
                    return true;
            }
            return false;
        }

        public bool OneGroupResized(GroupControl group)
        {
            if (!this.Controls.Contains((Control)group))
                return false;
            this.ReAlignControls();
            return true;
        }

        public bool AddNewGroup(GroupControl newGroup)
        {
            if (newGroup == null)
                return false;
            if (!this.Contains((Control)newGroup))
                this.Controls.Add((Control)newGroup);
            int newIndex = -1;
            foreach (GroupControl group in this.Groups)
            {
                if (this.Contains((Control)group))
                {
                    ++newIndex;
                    this.Controls.SetChildIndex((Control)group, newIndex);
                }
            }
            this.ReAlignControls();
            return true;
        }

        public void ReAlignControls()
        {
            this.SuspendLayout();
            int num1 = this.AutoScrollPosition.Y;
            foreach (GroupControl control in (ArrangedElementCollection)this.Controls)
            {
                control.Left = this.AutoScrollPosition.X + 16;
                control.Top = num1;
                //control.Width = this.Width;// -20;
                control.Height = control.Minimized ? GroupControl.GROUP_TITLE_HEIGHT : control.RestoredHeight;
                num1 = control.Bottom + GroupsPane.GROUP_VERT_SPACE;
            }
            //if (this.VarGroup != null)
            //{
            //    int num2 = this.Height - this.VarGroup.Top;
            //    bool flag = !this.VarGroup.Minimized && num2 > ((AcCalcVariableGroupData)this.VarGroup.Data).mMinHeight;
            //    if (flag)
            //    {
            //        this.AutoScroll = false;
            //        this.VarGroup.Height = flag ? num2 : ((AcCalcVariableGroupData)this.VarGroup.Data).mMinHeight;
            //        this.VarGroup.RestoredHeight = this.VarGroup.Height;
            //    }
            //    else
            //        this.AutoScroll = true;
            //    this.VarGroup.ResizeControls();
            //}
            if (!this.AutoScroll)
                this.AutoScroll = true;
            if (this.HScroll)
                this.HScroll = false;
            this.ResumeLayout();
        }

        public bool RemoveGroup(GroupControl group)
        {
            if (!this.Contains((Control)group))
                return false;
            this.Controls.Remove((Control)group);
            if (group.ContainsFocus)
                this.mParent.ActiveControl = this.mParent.mSearchTextBox;
            this.ReAlignControls();
            return true;
        }

        //public bool PopulateVarGroup(AcCalcVariablesData varData)
        //{
        //    return this.VarGroup.BuildVarTree(varData);
        //}

        public void RepairToolTips()
        {
            foreach (GroupControl control in (ArrangedElementCollection)this.Controls)
                control.RepairToolTips();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ReAlignControls();
        }
    }
}
