using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Internal;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Autodesk.AutoCAD.Customization
{
    internal class DefaultGroupEditor : UITypeEditor
    {
        private const string kToolPaletteScheme = "ToolPaletteScheme";

        private static StringCollection m_defaultGroups;

        private IWindowsFormsEditorService m_editorService;

        private ListBox m_listbox;

        private static string m_defaultGroup;

        public static string DefaultGroup
        {
            get
            {
                return DefaultGroupEditor.m_defaultGroup;
            }
        }

        public DefaultGroupEditor()
        {
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                this.m_editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (this.m_editorService != null)
                {
                    if (DefaultGroupEditor.m_defaultGroups == null && null != HostApplicationServices.Current)
                    {
                        Utils.GetToolPaletteGroups("ToolPaletteScheme", out DefaultGroupEditor.m_defaultGroups);
                    }
                    this.m_listbox = new ListBox()
                    {
                        SelectionMode = SelectionMode.One,
                        BorderStyle = BorderStyle.None
                    };
                    foreach (string mDefaultGroup in DefaultGroupEditor.m_defaultGroups)
                    {
                        this.m_listbox.Items.Add(mDefaultGroup);
                    }
                    DefaultGroupEditor.m_defaultGroup = (string)value;
                    if ((0 == DefaultGroupEditor.m_defaultGroup.Length ? false : this.m_listbox.Items.Contains(DefaultGroupEditor.m_defaultGroup)))
                    {
                        this.m_listbox.SelectedIndex = this.m_listbox.FindStringExact(DefaultGroupEditor.m_defaultGroup);
                    }
                    this.m_listbox.SelectedIndexChanged += new EventHandler(this.m_listbox_SelectedIndexChanged);
                    int itemHeight = this.m_listbox.ItemHeight * (this.m_listbox.Items.Count + 1);
                    if (itemHeight < this.m_listbox.Height)
                    {
                        this.m_listbox.Height = itemHeight;
                    }
                    this.m_editorService.DropDownControl(this.m_listbox);
                    if (this.m_listbox.Text == DefaultGroupEditor.m_defaultGroup)
                    {
                        return value;
                    }
                    return this.m_listbox.Text;
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private void m_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_editorService != null)
            {
                this.m_editorService.CloseDropDown();
            }
        }

        public static void Reset()
        {
            DefaultGroupEditor.m_defaultGroups = null;
        }
    }
}