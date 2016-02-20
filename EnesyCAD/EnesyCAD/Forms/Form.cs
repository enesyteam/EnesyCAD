using System.Windows.Forms;

namespace EnesyCAD.Forms
{
    public class Form : System.Windows.Forms.Form
    {
        /// <summary>
        /// Help link. It is link to if F1 key is pressed
        /// </summary>
        [System.ComponentModel.DefaultValue("")]
        public string Help { get; set; }

        /// <summary>
        /// Show Dialog method
        /// </summary>
        public virtual void ShowModal()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(this);
            this.Icon = Properties.Resources.enesyIcon;
        }

        public virtual void ShowModeless()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(this);
            this.Icon = Properties.Resources.enesyIcon;
        }

        /// <summary>
        /// Perform Close() method if Esc key is pressed
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
