using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class W32Util
    {
        public static int DLGC_WANTALLKEYS = 4;
        public static int WS_HSCROLL = 1048576;
        public static int WS_VSCROLL = 2097152;

        public static bool VisualStylesEnabled
        {
            get
            {
                if (OSFeature.Feature.IsPresent(OSFeature.Themes) && W32Util.IsAppThemed())
                    return W32Util.IsThemeActive();
                return false;
            }
        }

        [DllImport("User32.dll")]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        public static bool EnableAcadMainFrame(bool enable)
        {
            return W32Util.EnableWindow(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Handle, enable);
        }

        [DllImport("User32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);

        public static IntPtr SetFocusToAcadMainFrame()
        {
            return W32Util.SetFocus(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Handle);
        }

        [DllImport("User32.dll")]
        public static extern int GetSysColor(int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();

        public static Control GetFocusedControl()
        {
            Control control = (Control)null;
            IntPtr focus = W32Util.GetFocus();
            if (focus != IntPtr.Zero)
                control = Control.FromHandle(focus);
            return control;
        }

        [DllImport("User32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, W32Util.WinPosIndex nIndex, int dwNewLong);

        [DllImport("User32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, W32Util.WinPosIndex nIndex);

        public static int GetWin32Style(Control control)
        {
            return W32Util.GetWindowLong(control.Handle, W32Util.WinPosIndex.GWL_STYLE);
        }

        public static int SetWin32Style(Control control, int newStyle)
        {
            return W32Util.SetWindowLong(control.Handle, W32Util.WinPosIndex.GWL_STYLE, newStyle);
        }

        public static IntPtr SetWin32Focus(IntPtr hWnd)
        {
            return W32Util.SetFocus(hWnd);
        }

        [DllImport("UxTheme")]
        private static extern bool IsThemeActive();

        [DllImport("UxTheme")]
        private static extern bool IsAppThemed();

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public enum WinPosIndex
        {
            GWL_USERDATA = -21,
            GWL_EXSTYLE = -20,
            GWL_STYLE = -16,
            GWL_ID = -12,
            GWL_HWNDPARENT = -8,
            GWL_HINSTANCE = -6,
            GWL_WNDPROC = -4,
        }

        public enum WM
        {
            WM_NULL = 0,
            WM_SIZE = 5,
            WM_SETFOCUS = 7,
            WM_KILLFOCUS = 8,
            WM_PAINT = 15,
            WM_ERASEBKGND = 20,
            WM_DRAWITEM = 43,
            WM_MEASUREITEM = 44,
            WM_HELP = 83,
            WM_CONTEXTMENU = 123,
            WM_GETDLGCODE = 135,
            WM_KEYDOWN = 256,
            WM_CHAR = 258,
            WM_SYSKEYDOWN = 260,
            WM_SYSCHAR = 262,
            WM_PARENTNOTIFY = 528,
            WM_REFLECT = 8192,
        }

        public enum ODS
        {
            ODS_SELECTED = 1,
            ODS_FOCUS = 16,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DRAWITEMSTRUCT
        {
            public int ctrlType;
            public int ctrlID;
            public int itemID;
            public int itemAction;
            public int itemState;
            public IntPtr hwnd;
            public IntPtr hdc;
            public W32Util.RECT rcItem;
            public IntPtr itemData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MEASUREITEMSTRUCT
        {
            public int ctrlType;
            public int ctrlID;
            public int itemID;
            public int itemWidth;
            public int itemHeight;
            public IntPtr itemData;
        }
    }
}
