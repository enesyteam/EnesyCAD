using System;
using Autodesk.AutoCAD.Runtime;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD.Plot;


namespace Enesy.EnesyCAD.Utilities
{
    public partial class Command
    {
        /// <summary>
        /// Property for GUI - PaletteSet in AutoCAD
        /// </summary>
        public static Autodesk.AutoCAD.Windows.PaletteSet m_ps;

        /// <summary>
        /// AutoCAD command (is called directly at commandLine)
        /// </summary>
        [EnesyCAD.Runtime.EnesyCADCommandMethod("MP",
            "Plot",
            "Multiple ploting",
            "EnesyCAD",
            "quandt@enesy.vn",
            Enesy.Page.CadYoutube
            )]
        public void QuickPlot()
        {
            if (m_ps == null)
            {
                //use constructor with Guid so that we can save/load user data
                m_ps = new Autodesk.AutoCAD.Windows.PaletteSet("",
                    new Guid("63B8DB5B-10E4-4924-B8A2-A9CF9158E4F6"));

                m_ps.Style = Autodesk.AutoCAD.Windows.PaletteSetStyles.NameEditable |
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowPropertiesMenu |
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowAutoHideButton |
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowCloseButton;

                m_ps.MinimumSize = new System.Drawing.Size(340, 200);
                
                m_ps.Add("", new PaletteControl());

                m_ps.Visible = true;
            }
            else
            {
                if (m_ps.Visible) m_ps.Visible = false;

                else m_ps.Visible = true;
            }
        }
    }
}
