using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using app = Autodesk.AutoCAD.ApplicationServices.Application;
using Enesy.EnesyCAD;
using Enesy.EnesyCAD.Utilities;

namespace Enesy.EnesyCAD.Plot
{
    public partial class PaletteControl : UserControl
    {
        #region Properties and Fields
        /// <summary>
        /// Refresh sheets info engine
        /// </summary>
        private RefreshEngine m_refresher = new RefreshEngine();

        /// <summary>
        /// Datatable to store & sync data between viewer & frame
        /// </summary>
        private System.Data.DataTable m_sheetsData = null;

        /// <summary>
        /// BlockTableRecord of frame
        /// </summary>
        private BlockTableRecord m_frame = null;

        /// <summary>
        /// Collection of id of rectangles or lines
        /// </summary>
        private ObjectIdCollection m_objIdColl = null;

        /// <summary>
        /// Left-Lower & Right-Upper points of base plot area
        /// </summary>
        private Point3d[] m_points = new Point3d[2];

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public PaletteControl()
        {
            InitializeComponent();
            Functions.RefreshPloter(cboPlotter);
            Functions.RefreshStyles(cboStyle);
            Functions.RefreshPaper(cboPaper, cboPlotter.Text);
            m_refresher.Set(
                rdoBlock, rdoRectangle, rdoLine,
                rdoName, rdoFileName, rdoPath
            );
        }        

        private void butSpecify_Click(object sender, EventArgs e)
        {
            if (rdoBlock.Checked)
            {
                m_refresher.FrameBlock = Functions.GetBlock();
                if (m_refresher.FrameBlock != null)
                {
                    m_refresher.BasePoints = Utils.GetCorners();
                }
                if (m_refresher.FrameBlock != null && 
                    m_refresher.BasePoints != new Point3d[2])
                {
                    // Get Name || Filename || Path of block
                    string p = m_refresher.FrameBlock.PathName;
                    txtBlockName.Text = m_refresher.FrameBlock.Name;
                    txtBlockFileName.Text = Path.GetFileName(p);
                    if (txtBlockFileName.Text == "")
                    {
                        p = "";
                        rdoName.Checked = true;
                        rdoFileName.Enabled = false;
                        rdoPath.Enabled = false;
                    }
                    else
                    {
                        rdoFileName.Enabled = true;
                        rdoPath.Enabled = true;
                        if (!(Path.IsPathRooted(p)))
                        {
                            Document doc = Autodesk.AutoCAD.ApplicationServices.
                                Application.DocumentManager.MdiActiveDocument;
                            HostApplicationServices hs = HostApplicationServices.Current;
                            string acPath = hs.FindFile(
                                doc.Name, doc.Database, FindFileHint.Default);
                            p = Path.Combine(acPath, p);
                            p = Path.GetFullPath(p);
                        }
                    }
                    txtBlockPath.Text = p;
                }
            }

            if (rdoRectangle.Checked)
            {
                
            }

            if (rdoLine.Checked)
            {
                
            }
        }

        private void chkCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCenter.Checked)
            {
                txtCenterX.Enabled = false;
                txtCenterY.Enabled = false;
            }
            else
            {
                txtCenterX.Enabled = true;
                txtCenterY.Enabled = true;
            }
        }

        private void chkFit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFit.Checked)
            {
                txtScaleB.Enabled = false;
                txtScaleO.Enabled = false;
            }
            else
            {
                txtScaleB.Enabled = true;
                txtScaleO.Enabled = true;
            }
        }

        private void chkAutoFit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoFit.Checked)
            {
                txtAutoFit.Enabled = true;
            }
            else
            {
                txtAutoFit.Enabled = false;
            }
        }

        private void butEditStyle_Click(object sender, EventArgs e)
        {
            UserConfigurationManager ucm = app.UserConfigurationManager;
            IConfigurationSection ics = ucm.OpenCurrentProfile();
            IConfigurationSection general = ics.OpenSubsection("General");
            string path = (string)general.ReadProperty(
                "PrinterStyleSheetDir",
                string.Empty
            );
            string styleName;
            styleName = (string)cboStyle.Items[cboStyle.SelectedIndex];
            path = path + "\\" + styleName;
            System.Diagnostics.Process.Start(path);
        }

        private void cboStyle_DropDown(object sender, EventArgs e)
        {
            if (!(Command.m_ps.Dock.Equals(0)))
            {
                Command.m_ps.KeepFocus = true;
            }
        }

        private void cboStyle_DropDownClosed(object sender, EventArgs e)
        {
            if (!(Command.m_ps.Dock.Equals(0)))
            {
                Command.m_ps.KeepFocus = false;
            }
        }

        private void cboPlotter_DropDown(object sender, EventArgs e)
        {
            if (!(Command.m_ps.Dock.Equals(0)))
            {
                Command.m_ps.KeepFocus = true;
            }
        }

        private void cboPlotter_DropDownClosed(object sender, EventArgs e)
        {
            if (!(Command.m_ps.Dock.Equals(0)))
            {
                Command.m_ps.KeepFocus = false;
            }
        }

        private void cboPaper_DropDown(object sender, EventArgs e)
        {
            if (!(Command.m_ps.Dock.Equals(0)))
            {
                Command.m_ps.KeepFocus = true;
            }
        }

        private void cboPaper_DropDownClosed(object sender, EventArgs e)
        {
            if (!(Command.m_ps.Dock.Equals(0)))
            {
                Command.m_ps.KeepFocus = false;
            }
        }

        private void cboPlotter_Click(object sender, EventArgs e)
        {
            Functions.RefreshPloter(cboPlotter);
        }

        private void cboStyle_Click(object sender, EventArgs e)
        {
            Functions.RefreshStyles(cboStyle);
        }

        private void cboPlotter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Functions.RefreshPaper(cboPaper, cboPlotter.Text);
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            dgvrSheets.DataSource = m_refresher.Refresh();
        }
    }
}
