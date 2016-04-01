using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using app = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.PlottingServices;

namespace Enesy.EnesyCAD.Plot
{
    class Functions
    {
        /// <summary>
        /// Get information of frame
        /// </summary>
        /// <returns></returns>
        public static BlockTableRecord GetBlock()
        {
            Document ac = app.DocumentManager.MdiActiveDocument;
            Editor ed = ac.Editor;
            Database db = ac.Database;

            BlockTableRecord block = null;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                PromptEntityOptions prEntOpts = new PromptEntityOptions(
                    "\nSpecify block:\n"
                    );

                prEntOpts.SetRejectMessage(
                    "\nYou are missing, try again!"
                    );

                prEntOpts.AddAllowedClass(typeof(BlockReference), false);

                PromptEntityResult prEntRes;
                prEntRes = ed.GetEntity(prEntOpts);

                if (prEntRes.Status != PromptStatus.OK)
                {
                    return block;
                }

                BlockReference blockRef = tr.GetObject(prEntRes.ObjectId,
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

                if (block == null)
                {
                    return block;
                }
                tr.Commit();
            }
            return block;
        }

        /// <summary>
        /// Refreshing list of plotter
        /// </summary>
        public static void RefreshPloter(ComboBox cboPlotter)
        {
            // Update devices name
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            PlotSettings ps = new PlotSettings(true);
            psv.RefreshLists(ps);

            // Store current device's name
            string curPlotter = cboPlotter.Text;

            //Append plotter/printer to combobox
            StringCollection devList = psv.GetPlotDeviceList();
            cboPlotter.Items.Clear();
            for (int i = 0; i < devList.Count; i++)
            {
                if (devList[i] == "None") continue;
                cboPlotter.Items.Add(devList[i]);
            }
            if (devList.Contains(curPlotter))
            {
                cboPlotter.Text = curPlotter;
            }
            else
            {
                //Setting default plotter/printer
                if (devList.Contains("PDF reDirect v2_A3.pc3"))
                {
                    cboPlotter.SelectedIndex =
                        cboPlotter.Items.IndexOf("PDF reDirect v2_A3.pc3");
                }
                else if (devList.Contains("PDF reDirect v2"))
                {
                    cboPlotter.SelectedIndex =
                        cboPlotter.Items.IndexOf("PDF reDirect v2");
                }
                else if (devList.Contains("DWG To PDF.pc3"))
                {
                    cboPlotter.SelectedIndex =
                        cboPlotter.Items.IndexOf("DWG To PDF.pc3");
                }
                else
                {
                    cboPlotter.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Refreshing list of plot style table
        /// </summary>
        /// <returns></returns>
        public static void RefreshStyles(ComboBox cboStyle)
        {
            // Update devices name
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            PlotSettings ps = new PlotSettings(true);
            psv.RefreshLists(ps);

            //Storing current style
            string curStyle = cboStyle.Text;

            // Append style to combobox's items
            StringCollection styleList = psv.GetPlotStyleSheetList();
            cboStyle.Items.Clear();
            for (int i = 0; i < styleList.Count; i++)
            {
                cboStyle.Items.Add(styleList[i]);
            }

            if (styleList.Contains(curStyle))
            {
                cboStyle.Text = curStyle;
            }
            else
            {
                // Setting default plot style table
                cboStyle.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Get list of paper size with specified device in cboPlotter
        ///     then append it to cboPaper
        /// </summary>
        /// <param name="cboPaper"></param>
        public static void RefreshPaper(ComboBox cboPaper, string printer)
        {
            // Storing current paper for selecting closest paper size with other printer
            string curPaper = cboPaper.Text;
            // Get size of current paper (if any)
            //int[] size = GetPaperSize(oldPrinter, curPaper);

            StringCollection sc = null;
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            PlotSettings ps = new PlotSettings(true);
            try
            {
                psv.SetPlotConfigurationName(ps, printer, null);
                sc = psv.GetCanonicalMediaNameList(ps);
                cboPaper.Items.Clear();
                if (sc != null)
                {
                    for (int i = 0; i < sc.Count; i++)
                    {
                        string s = (psv.GetLocaleMediaName(ps, i).ToString());
                        if (s != "") cboPaper.Items.Add(s);
                    }
                }
            }
            catch { }

            // Set default size
            if (cboPaper.Items.Contains(curPaper))
            {
                cboPaper.Text = curPaper;
            }
            else
            {
                cboPaper.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// Get size of paper of specified device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="paper"></param>
        private static int[] GetPaperSize(string printer, string paper)
        {
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            PlotSettings ps = new PlotSettings(true);
            int[] size = null;
            int w = 0;
            int h = 0;
            psv.SetPlotConfigurationName(ps, printer, null);
            StringCollection mediaL = psv.GetCanonicalMediaNameList(ps);
            for (int i = 0; i < mediaL.Count; i++)
            {
                string s = (psv.GetLocaleMediaName(ps, i).ToString());
                if (s == paper)
                {
                    paper = mediaL[i];
                    psv.SetPlotConfigurationName(ps, printer, paper);
                    w = Convert.ToInt32(
                        Math.Max(ps.PlotPaperSize.X, ps.PlotPaperSize.Y));
                    h = Convert.ToInt32(
                        Math.Min(ps.PlotPaperSize.X, ps.PlotPaperSize.Y));
                    break;
                }
            }
            if (w != 0 && h != 0) { size = new int[] { w, h }; }
            return size;
        }

        /// <summary>
        /// Get the paper that have size is the most closest specified size
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string GetClosestPaper(string printer, int[] size)
        {
            int w1 = size[0];
            int h1 = size[1];
            string paper = null;
            string tempPaper = null;

            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            PlotSettings ps = new PlotSettings(true);
            psv.SetPlotConfigurationName(ps, printer, null);
            StringCollection mediaL = psv.GetCanonicalMediaNameList(ps);
            if (mediaL != null)
            {
                for (int i = 0; i < mediaL.Count; i++)
                {
                    paper = psv.GetLocaleMediaName(ps, i).ToString();
                    psv.SetPlotConfigurationName(ps, printer, paper);
                    int w2 = Convert.ToInt32(
                        Math.Max(ps.PlotPaperSize.X, ps.PlotPaperSize.Y));
                    int h2 = Convert.ToInt32(
                        Math.Min(ps.PlotPaperSize.X, ps.PlotPaperSize.Y));
                    if (w1 == w2 && h1 == h2) { break; }
                    
                    int o1 = w1; int o2 = h1;
                    int tO1, tO2;
                    tO1 = Math.Abs(w1 - w2);
                    tO2 = Math.Abs(h2 - h1);
                    if (tO1 * tO2 < o1 * o2)
                    {
                        tempPaper = paper;
                    }
                    paper = tempPaper;
                }
            }
            return paper;
        }
    }
}
