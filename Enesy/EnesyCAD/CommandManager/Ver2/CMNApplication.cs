using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    /// <summary>
    /// Command Manager Application
    /// </summary>
    public class CMNApplication : IExtensionApplication
    {
        public static string CurrentVersion
        {
            get {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                Version currentVersion = new Version(fvi.ProductVersion);
                return currentVersion.ToString();
            }
        }

        public static Hashtable mDocDataCollection;
        private static cmnTheme theme_;
        public static cmnTheme Theme
        {
            get
            {
                if (CMNApplication.theme_ == null)
                    CMNApplication.theme_ = new cmnTheme();
                return CMNApplication.theme_;
            }
        }
        private static bool eswNeedsToSyncSeetingsWithModal_;
        public static bool ESWShouldSyncWithModal
        {
            get
            {
                return CMNApplication.eswNeedsToSyncSeetingsWithModal_;
            }
        }
        public static cmnESW ESWCmn;
        public void Terminate()
        {
        }
        public void Initialize()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SystemObjects.DynamicLinker.ProductLcid, true);
            //ResourceManager resourceManager = new ResourceManager(typeof(CalculatorForm));
            //resourceManager.GetString("panel_.AccessibleDescription", new CultureInfo(SystemObjects.DynamicLinker.ProductLcid, true));
            if (CMNControl.UIData == null)
            {
                CMNControl.DeserializeUiLayout();
            }
            CMNApplication.mDocDataCollection = new Hashtable();
            IEnumerator enumerator = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.GetEnumerator();
            while (enumerator.MoveNext())
            {
                PerDocData perDocDatum = new PerDocData((Document)enumerator.Current);
                CMNApplication.mDocDataCollection.Add((Document)enumerator.Current, perDocDatum);
            }
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentToBeDestroyed += new DocumentCollectionEventHandler(this.docToBeDestroyed);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentToBeDeactivated += new DocumentCollectionEventHandler(this.documentToBeDeactivated);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentActivated += new DocumentCollectionEventHandler(this.documentActivated);
            Autodesk.AutoCAD.ApplicationServices.Application.SystemVariableChanged += new SystemVariableChangedEventHandler(this.sysvarChanged);
            //CalcDialogCreator.SetCreatorFunc(new ShowModalFunc(CMNApplication.ShowModalCalculator));
        }
        public void sysvarChanged(object sender, SystemVariableChangedEventArgs e)
        {
            if (!e.Changed)
            {
                return;
            }
            if (CMNApplication.ESWCmn == null || string.CompareOrdinal(e.Name, "CleanScreenState") != 0)
            {
                if (string.CompareOrdinal(e.Name, "LUNITS") == 0 || string.CompareOrdinal(e.Name, "LUPREC") == 0)
                {
                    if (CMNApplication.ESWCmn != null)
                    {
                        CMNApplication.ESWCmn.CmnControl.RestoreFromCurrentData(false);
                    }
                    if (CMNApplication.ESWCmn != null)
                    {
                        CMNApplication.ESWCmn.CmnControl.RestoreFromCurrentData(false);
                    }
                }
            }
            else if (int.Parse(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CLEANSCREENSTATE").ToString()) == 1)
            {
                if (CMNApplication.ESWCmn.ESW.Visible)
                {
                    CMNApplication.HideESWCmn(true);
                    CMNApplication.ESWCmn.CmnControl.mbShouldRestore = true;
                    return;
                }
            }
            else if (CMNApplication.ESWCmn.CmnControl.mbShouldRestore)
            {
                //CMNApplication.ShowESWCalculator();
                return;
            }
        }
        private void docToBeDestroyed(object sender, DocumentCollectionEventArgs e)
        {
            if (CMNApplication.mDocDataCollection.Contains(e.Document))
            {
                CMNApplication.mDocDataCollection.Remove(e.Document);
            }
            if (CMNApplication.ESWCmn != null && CMNApplication.ESWCmn.ESW.Visible && Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Count == 1)
            {
                CMNApplication.ESWCmn.ESW.Visible = false;
                CMNApplication.ESWCmn.CmnControl.mbShouldRestore = true;
            }
        }

        private void documentActivated(object sender, DocumentCollectionEventArgs e)
        {
            PerDocData perDocDatum = null;
            if (!CMNApplication.mDocDataCollection.Contains(e.Document))
            {
                perDocDatum = new PerDocData(e.Document);
                CMNApplication.mDocDataCollection.Add(e.Document, perDocDatum);
            }
            else
            {
                perDocDatum = (PerDocData)CMNApplication.mDocDataCollection[e.Document];
            }
            if (CMNApplication.ESWCmn != null)
            {
                CMNApplication.ESWCmn.CmnControl.CurrentDocData = perDocDatum;
                CMNApplication.ESWCmn.CmnControl.RestoreFromCurrentData(true);
                if (Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Count == 1 && CMNApplication.ESWCmn.CmnControl.mbShouldRestore)
                {
                    CMNApplication.ESWCmn.ESW.Visible = true;
                    CMNApplication.ESWCmn.CmnControl.mbShouldRestore = false;
                }
            }
            W32Util.SetFocusToAcadMainFrame();
        }

        private void documentToBeDeactivated(object sender, DocumentCollectionEventArgs e)
        {
            if (CMNApplication.ESWCmn == null)
            {
                return;
            }
            PerDocData item = (PerDocData)CMNApplication.mDocDataCollection[e.Document];
            if (item == null)
            {
                return;
            }
            item.mCurrentExpression = CMNApplication.ESWCmn.CmnControl.mSearchTextBox.Text;
        }
        public static void ShowESWCmn()
        {
            if (CMNApplication.ESWCmn == null)
            {
                //GLOBAL.WriteMessage("sdfsdfsdf");
                CMNApplication.ESWCmn = new cmnESW();
                CMNApplication.ESWCmn.CmnControl.SetStatusRegionText("Command Information");
            }
            CMNApplication.ESWCmn.CmnControl.SyncTheme();
            CMNApplication.ESWCmn.ESW.Visible = true;
            //CMNApplication.ESWCmn.CmnControl.ReloadUILanguage();
            if (!CMNApplication.ESWCmn.Configured)
            {
                CMNApplication.ESWCmn.ESW.Dock = DockSides.None;
                CMNApplication.ESWCmn.Configured = true;
            }
            if (CMNApplication.ESWCmn.ESW.Dock != DockSides.None || CMNApplication.ESWCmn.ESW.Anchored)
                CMNApplication.ESWCmn.CmnControl.Enabled = true;
            if (!CMNApplication.ESWCmn.ESW.RolledUp)
                CMNApplication.ESWCmn.CmnControl.SetFocusToInputArea();
            else
                CMNApplication.ESWCmn.ESW.RolledUp = false;
           // CMNApplication.ESWCmn.CmnControl.Context.HostServices.SetQCState(true);
        }
        public static void HideESWCmn(bool isClose)
        {
            if (CMNApplication.ESWCmn == null)
                return;
            if ((CMNApplication.ESWCmn.ESW.Dock != DockSides.None || CMNApplication.ESWCmn.ESW.Anchored) && !isClose)
            {
                CMNApplication.ESWCmn.CmnControl.Enabled = false;
            }
            else
            {
                CMNApplication.ESWCmn.ESW.Visible = false;
                //CMNApplication.ESWCmn.CmnControl.Context.HostServices.SetQCState(false);
            }
        }
    }
}
