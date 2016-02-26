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

        List<Point3d> m_vertices = new List<Point3d>();

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
    }
}
