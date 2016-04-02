using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD.Utilities
{
    public partial class CoordinatePickerDialog : Enesy.EnesyCAD.Forms.Form
    {
        ObjectIdCollection m_objIdColl = new ObjectIdCollection();

        EntityType m_objType = EntityType.None;

        Point3dCollection m_vertices = new Point3dCollection();

        public CoordinatePickerDialog()
        {
            InitializeComponent();            

            // Init combobox - List name of all UCS
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            if (Utils.GetUCSName(db) != null)
            {
                cboUcs.Items.AddRange(Utils.GetUCSName(db).ToArray());
                cboUcs.SelectedIndex = 0;
                rdoUcsName.Enabled = true;
            }
            else
            {
                rdoUcsName.Enabled = false;
            }

            // Init combobox of options
            cboOption.SelectedIndex = 0;

            // Init radioButton
            Editor ed = doc.Editor;
            CoordinateSystem3d cur = ed.CurrentUserCoordinateSystem.CoordinateSystem3d;
            // Check if current UCS is WCS
            if (cur.Origin == Point3d.Origin && cur.Xaxis == Vector3d.XAxis
                    && cur.Yaxis == Vector3d.YAxis && cur.Zaxis == Vector3d.ZAxis)
            {
                if (rdoCurrent.Checked) rdoWorld.Checked = true;
                rdoCurrent.Enabled = false;
            }
            else
            {
                rdoCurrent.Enabled = true;
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdoUcsName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoUcsName.Checked) cboUcs.Enabled = true;
            else cboUcs.Enabled = false;
        }

        private void butSelect_Click(object sender, EventArgs e)
        {
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            // Select lwPolyline only
            List<string> typ = new List<string>() { "LWPOLYLINE", "POLYLINE" };
            m_objIdColl = Utils.SelectionFilter(typ, ed);

            if (m_objIdColl != null)
            {
                Transaction tr = doc.Database.TransactionManager.StartTransaction();
                m_vertices = Utils.ListVertices(tr, m_objIdColl);
                lblStatus.Text = m_objIdColl.Count.ToString() + " pline selected, " +
                    (m_vertices != null ? (m_vertices.Count.ToString() + " points") : "0 point");
            }
        }

        private void butPick_Click(object sender, EventArgs e)
        {

        }

        private void butBlock_Click(object sender, EventArgs e)
        {

        }

        private void butExport_Click(object sender, EventArgs e)
        {
            if (m_objIdColl.Count == 0)
            {
                MessageBox.Show("Not founds any objects!");
                return;
            }

            if (m_vertices.Count == 0)
            {
                MessageBox.Show("It has no point!");
                return;
            }

            if (rdoCurrent.Checked)
            {
                m_vertices = Utils.Wcs2Ucs(m_vertices);
            }
            if (rdoUcsName.Checked)
            {
                m_vertices = Utils.Wcs2Ucs(m_vertices, cboUcs.Text);
            }
            
            if (cboOption.SelectedIndex == 0)
            {
                string s = "";
                foreach (Point3d p in m_vertices)
                {
                    s += p.X + "\t" + p.Y + "\n";
                }
                System.Windows.Forms.Clipboard.SetText(s);
            }
            this.Close();
        }
    }
}
