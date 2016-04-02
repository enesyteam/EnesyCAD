using System.Runtime.InteropServices;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;

namespace Enesy.EnesyCAD.Forms
{
    public class Form : System.Windows.Forms.Form
    {
        [DllImport("user32.dll")]
        private static extern System.IntPtr SetFocus(System.IntPtr hwnd);

        /// <summary>
        /// Help link. It is link to if F1 key is pressed
        /// </summary>
        [System.ComponentModel.DefaultValue("")]
        public string Help { get; set; }

        /// <summary>
        /// ShowModal Dialog method
        /// </summary>
        public virtual void ShowModal()
        {
            acApp.ShowModalDialog(this);
            this.Icon = Enesy.Drawing.Icons.enesyIcon;
        }

        public virtual void ShowModeless()
        {
            acApp.ShowModelessDialog(this);
            this.Icon = Enesy.Drawing.Icons.enesyIcon;
        }

        public virtual void ShowModeless(bool displayImpliedSelection)
        {
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            PromptSelectionResult psr = ed.SelectImplied();
            this.ShowModeless();
            if (psr.Status == PromptStatus.OK && displayImpliedSelection)
            {
                ed.SetImpliedSelection(psr.Value.GetObjectIds());
                System.IntPtr hwnd = Autodesk.AutoCAD.ApplicationServices
                                                    .Application.MainWindow.Handle;
                SetFocus(hwnd);
                SetFocus(this.Handle);
            }
        }

        /// <summary>
        /// Perform Close() method if Esc key is pressed
        /// Visit help page if F1 is pressed
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }

            if (keyData == Keys.F1)
            {
                if (Help != "")
                {
                    System.Diagnostics.Process.Start(this.Help);
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)
        {
            base.OnHelpButtonClicked(e);

            System.Diagnostics.Process.Start(this.Help);
        }
    }
}
