using System;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            if (msg.Msg == 135)
            {
                IntPtr result = msg.Result;
                msg.Result = (IntPtr)(result.ToInt32() | W32Util.DLGC_WANTALLKEYS);
            }
        }
    }
}