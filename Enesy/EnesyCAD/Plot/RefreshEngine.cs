using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

using Enesy.EnesyCAD.Utilities;

namespace Enesy.EnesyCAD.Plot
{
    class RefreshEngine
    {
        #region Properties and Fields

        private RadioButton m_rdoBlock = new RadioButton();
        public RadioButton RadioBlock
        {
            get { return m_rdoBlock; }
            private set { m_rdoBlock = value; }
        }

        private RadioButton m_rdoRectangle = new RadioButton();
        public RadioButton RadioRectangle
        {
            get { return m_rdoRectangle; }
            private set { m_rdoRectangle = value; }
        }

        private RadioButton m_rdoLine = new RadioButton();
        public RadioButton RadioLine
        {
            get { return m_rdoLine; }
            private set { m_rdoLine = value; }
        }

        private RadioButton m_rdoName = new RadioButton();
        public RadioButton RadioName
        {
            get { return m_rdoName; }
            private set { m_rdoName = value; }
        }

        private RadioButton m_rdoFileName = new RadioButton();
        public RadioButton RadioFileName
        {
            get { return m_rdoFileName; }
            private set { m_rdoFileName = value; }
        }

        private RadioButton m_rdoPath = new RadioButton();
        public RadioButton RadioPath
        {
            get { return m_rdoPath; }
            private set { m_rdoPath = value; }
        }

        /// <summary>
        /// BlockTableRecord of frame object
        /// For get information of frame (scale X,Y; origin;...)
        /// </summary>
        private BlockTableRecord m_frame = null;
        public BlockTableRecord FrameBlock
        {
            get { return m_frame; }
            set { m_frame = value; }
        }

        /// <summary>
        /// Collection of frame object in case of frame is rectangle or line
        /// </summary>
        private ObjectIdCollection m_objIdColl = new ObjectIdCollection();
        public ObjectIdCollection FrameIdCollection
        {
            get { return m_objIdColl; }
            set { m_objIdColl = value; }
        }

        /// <summary>
        /// Left-Lower & Right-Upper points of base plot area
        /// </summary>
        private Point3d[] m_points = new Point3d[2];
        public Point3d[] BasePoints
        {
            get { return m_points; }
            set { m_points = value; }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RefreshEngine()
        {
            // Do nothing
        }

        /// <summary>
        /// Setting reference for class
        /// </summary>
        /// <param name="Block"></param>
        /// <param name="Rectangle"></param>
        /// <param name="Line"></param>
        /// <param name="Name"></param>
        /// <param name="Filename"></param>
        /// <param name="Path"></param>
        /// <param name="Frame">BlkTabRec of base frame block</param>
        /// <param name="IdCollection">Collection of frame id (rec or lines)</param>
        /// <param name="Points">MinPoint and MaxPoint</param>
        public void Set(
            RadioButton Block, RadioButton Rectangle, RadioButton Line,
            RadioButton Name, RadioButton Filename, RadioButton Path
            )
        {
            RadioBlock = Block;
            RadioRectangle = Rectangle;
            RadioLine = Line;
            RadioName = Name;
            RadioFileName = Filename;
            RadioPath = Path;
        }

        /// <summary>
        /// Check refresh state (valid or invalid)
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (m_rdoBlock.Checked)
            {
                if (FrameBlock == null || BasePoints == new Point3d[2])
                {
                    return false;
                }
                else
                    return true;
            }
            else
            {
                if (FrameIdCollection == null)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Refreshing - Main method
        /// </summary>
        public System.Data.DataTable Refresh()
        {
            System.Data.DataTable sheetsData = new System.Data.DataTable();
            if (this.IsValid())
            {
                Document doc = acApp.DocumentManager.MdiActiveDocument;
                if (m_rdoBlock.Checked)
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    sheetsData = this.RefreshBlock(doc, db, ed);
                }
                if (m_rdoRectangle.Checked)
                {
                    MessageBox.Show("loi o Refresh");
                }
                if (m_rdoLine.Checked)
                {
                    MessageBox.Show("loi o Refresh");
                }
            }
            else
            {
                MessageBox.Show("Input is invalid...");
            }
            return sheetsData;
        }

        private System.Data.DataTable RefreshBlock(Document doc, Database db, Editor ed)
        {
            System.Data.DataTable sheetsData = new System.Data.DataTable();
            sheetsData.Columns.Add("Order", typeof(int));
            sheetsData.Columns.Add("Xmin", typeof(double));
            sheetsData.Columns.Add("Ymin", typeof(double));
            sheetsData.Columns.Add("Xmax", typeof(double));
            sheetsData.Columns.Add("Ymax", typeof(double));

            // Get base information
            Point3d origin, minPoint, maxPoint;
            origin = FrameBlock.Origin;
            minPoint = BasePoints[0];
            maxPoint = BasePoints[1];

            // Get distance and angle between origin and min&max Points
            double[] distAndAngle = new double[4];
            distAndAngle[0] = origin.DistanceTo(minPoint);
            distAndAngle[1] = Utils.AngleFromXAxisInXYPlane(origin, minPoint);
            distAndAngle[2] = origin.DistanceTo(maxPoint);
            distAndAngle[3] = Utils.AngleFromXAxisInXYPlane(origin, maxPoint);

            // Get all block in current layout of active document
            //                                  then computing plot area
            List<BlockTableRecord> blkTabRecs = this.GetAllBlocks(doc, db, ed);

            foreach (BlockTableRecord blkTabRec in blkTabRecs)
            {
                origin = blkTabRec.Origin;
                minPoint = Utils.Polar(origin, distAndAngle[0], distAndAngle[1]);
                maxPoint = Utils.Polar(origin, distAndAngle[2], distAndAngle[3]);
                int i = 0;
                sheetsData.Rows.Add(i, minPoint.X, minPoint.Y, maxPoint.X, maxPoint.Y);
                i++;
            }
            return sheetsData;
        }

        private void RefreshRectangle()
        {

        }

        private void RefreshLine()
        {

        }

        /// <summary>
        /// Get all blocks/xrefs in active layout of active document
        /// Blocks is filtered based on blockChar var: Name || Filename || Path
        /// </summary>
        /// <param name="doc">Active document</param>
        /// <param name="blockChar">Name || Filename || Path</param>
        /// <returns></returns>
        private List<BlockTableRecord> GetAllBlocks(Document doc,Database db,Editor ed)
        {
            List<BlockTableRecord> blkTabRecs = new List<BlockTableRecord>();

            SelectionSet ss = null;
            string blockChar = this.GetBlockChar(doc);

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // Create filter
                TypedValue[] tp = new TypedValue[2];
                tp.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                SelectionFilter sf = new SelectionFilter(tp);

                // Select
                PromptSelectionResult psRes;
                psRes = ed.SelectAll(sf);
                if (psRes.Status == PromptStatus.OK)
                {
                    ss = psRes.Value;
                    foreach (SelectedObject obj in ss)
                    {
                        BlockTableRecord block = null;
                        BlockReference blockRef = tr.GetObject(obj.ObjectId,
                                                OpenMode.ForRead) as BlockReference;

                        if (blockRef.IsDynamicBlock)
                        {
                            //get the real dynamic block name
                            block = tr.GetObject(blockRef.DynamicBlockTableRecord,
                                                OpenMode.ForRead) as BlockTableRecord;
                        }
                        else
                        {
                            block = tr.GetObject(blockRef.BlockTableRecord,
                                                OpenMode.ForRead) as BlockTableRecord;
                        }
                        if (block != null)
                        {
                            if (GetBlockChar(doc) == blockChar)
                            {
                                blkTabRecs.Add(block);
                            }
                        }
                    }
                }
                tr.Commit();
            }
            return blkTabRecs;
        }

        private void FindBlockInLayout(Database db, string layoutName)
        {
            // Switch to specified layout
            acApp.SetSystemVariable("CTAB", layoutName);

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTable bt = tr.GetObject(
                        db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    foreach (ObjectId id in bt)
                    {
                        BlockTableRecord btr;
                        btr = tr.GetObject(id, OpenMode.ForRead) as BlockTableRecord;

                        // If this is a layout
                        if (btr.IsLayout)
                        {
                            ObjectId lid = btr.ObjectId;
                            Layout lt = tr.GetObject(lid, OpenMode.ForRead) as Layout;
                            List<object> objs = new List<object>();

                            if (lt.LayoutName != layoutName)
                            {
                                continue;
                            }
                            else
                            {
                                RXClass etype = RXObject.GetClass(typeof(BlockReference));
                                foreach (ObjectId eid in btr)
                                {
                                }
                            }
                        }
                    }
                    tr.Commit();
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        /// <summary>
        /// Get charactericstic of block (Name || Filename || Path)
        /// </summary>
        /// <param name="btr"></param>
        /// <returns></returns>
        private string GetBlockChar(Document doc)
        {
            string n = null;
            
            BlockName nType = BlockName.Name;
            if (m_rdoFileName.Checked) nType = BlockName.FileName;
            if (m_rdoPath.Checked) nType = BlockName.Path;

            switch (nType)
            {
                case BlockName.Name:
                    n = FrameBlock.Name;
                    break;
                case BlockName.FileName:
                    n = Path.GetFileName(FrameBlock.PathName);
                    break;
                case BlockName.Path:
                    string p = FrameBlock.PathName;
                    if (!(Path.IsPathRooted(p)))
                    {
                        HostApplicationServices hs = HostApplicationServices.Current;
                        string acPath = hs.FindFile(
                            doc.Name, doc.Database, FindFileHint.Default);
                        p = Path.Combine(acPath, p);
                        n = Path.GetFullPath(p);
                    }
                    break;
            }
            return n;
        }

        /// <summary>
        /// Override ToString() method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
