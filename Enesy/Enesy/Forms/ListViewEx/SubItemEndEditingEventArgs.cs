using System;
using System.Windows.Forms;

namespace Enesy.Form
{
    public class SubItemEndEditingEventArgs : SubItemEventArgs
    {
        private string _text = string.Empty;

        private bool _cancel = true;

        private bool _RetainEditor;

        public bool Cancel
        {
            get
            {
                return this._cancel;
            }
            set
            {
                this._cancel = value;
            }
        }

        public string DisplayText
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public bool RetainEditor
        {
            get
            {
                return this._RetainEditor;
            }
            set
            {
                this._RetainEditor = value;
            }
        }

        public SubItemEndEditingEventArgs(ListViewItem item, int subItem, string display, bool cancel)
            : base(item, subItem)
        {
            this._text = display;
            this._cancel = cancel;
        }
    }
}