using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD
{
    partial class Utils
    {
        /// <summary>
        /// Get 2 point which is 2 corner of a rectangle
        /// </summary>
        /// <param name="ed">Editor of active CAD document</param>
        /// <returns>2D Array of point 3D</returns>
        public static Point3d[] GetCorners()
        {
            Document ac = Application.DocumentManager.MdiActiveDocument;
            Editor ed = ac.Editor;

            Point3d[] result = new Point3d[2];
            PromptPointResult prPntRes1;
            PromptPointOptions prPntOpts1 = new PromptPointOptions(
                "\nSpecify the first corner: \n"
                );

            // Set attributes for selection function
            prPntOpts1.AllowArbitraryInput = true;
            prPntOpts1.AllowNone = false;
            prPntOpts1.LimitsChecked = false;
            prPntRes1 = ed.GetPoint(prPntOpts1);

            if (prPntRes1.Status != PromptStatus.Cancel)
            {
                PromptPointResult prPntRes2;
                PromptCornerOptions prCorOpts2 = new PromptCornerOptions(
                    "\nSpecify the opposite corner: \n",
                    prPntRes1.Value
                    );
                prPntRes2 = ed.GetCorner(prCorOpts2);

                if (prPntRes2.Status != PromptStatus.Cancel)
                {
                    result[0] = prPntRes1.Value;
                    result[1] = prPntRes2.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// Select object that filter type of objects
        /// </summary>
        /// <param name="zeroDxfCodes">String that is 0 dxf code</param>
        /// <param name="ed">Editor of active document</param>
        /// <returns></returns>
        public static ObjectIdCollection SelectionFilter(List<string> zeroDxfCodes, Editor ed)
        {
            ObjectIdCollection objIdColl = null;
            try
            {
                // Define the filter criteria
                TypedValue[] tpVals = new TypedValue[zeroDxfCodes.Count + 2];
                tpVals.SetValue(new TypedValue((int)DxfCode.Operator, "<or"), 0);
                for (int i = 0; i < zeroDxfCodes.Count; i++)
                {
                    tpVals.SetValue(new TypedValue((int)DxfCode.Start, zeroDxfCodes[i]), i+1);
                }
                tpVals.SetValue(new TypedValue((int)DxfCode.Operator, "or>"), tpVals.Length - 1);
                
                // Assign the filter criteria to a SelectionFilter object
                SelectionFilter selFtr = new SelectionFilter(tpVals);

                // Request for objects to be selected in the drawing area
                PromptSelectionResult acSSPrompt;
                acSSPrompt = ed.GetSelection(selFtr);
                
                // If the prompt status is OK, objects were selected
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    // Init ObjectIdCollection
                    objIdColl = new ObjectIdCollection();

                    // Pass objectId to objIdColl
                    SelectionSet acSSet = acSSPrompt.Value;
                    foreach (SelectedObject so in acSSet)
                    {
                        objIdColl.Add(so.ObjectId);
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error select:\n" + ex.Message);
            }
            return objIdColl;
        }
    }
}
