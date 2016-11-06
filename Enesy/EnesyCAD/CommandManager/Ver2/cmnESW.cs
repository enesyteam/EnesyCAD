using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using System.Drawing;
using System.Linq;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class cmnESW
    {
        private PaletteSet mESW;
        private CMNControl mCmnControl;
        private bool _mSuspendAutoRollUp;
        private bool _mbConfigurationLoaded;

        public PaletteSet ESW
        {
            get
            {
                return this.mESW;
            }
        }

        public CMNControl CmnControl
        {
            get
            {
                return this.mCmnControl;
            }
        }
        public bool SuspendAutoRollUp
        {
            get
            {
                return this._mSuspendAutoRollUp;
            }
            set
            {
                if (this.mESW.Dock != DockSides.None)
                    return;
                if (value)
                {
                    if (this.mESW.AutoRollUp)
                    {
                        this.mESW.AutoRollUp = false;
                        this._mSuspendAutoRollUp = true;
                    }
                    else
                        this._mSuspendAutoRollUp = false;
                }
                else
                {
                    if (!this._mSuspendAutoRollUp)
                        return;
                    this.mESW.AutoRollUp = true;
                    this._mSuspendAutoRollUp = false;
                }
            }
        }
        public bool Configured
        {
            get
            {
                return this._mbConfigurationLoaded;
            }
            set
            {
                this._mbConfigurationLoaded = value;
            }
        }
        public cmnESW()
        {
            this.mESW = new PaletteSet("Command Manager", new Guid("673C2600-D8CC-44C7-932B-2A205AA45DD7"));
            this.mESW.Name = "Command Manager";
            this.mESW.DockEnabled = DockSides.Right | DockSides.Left | DockSides.Top | DockSides.Bottom;
            this.mESW.Dock = DockSides.None;
            this.mESW.Load += new PalettePersistEventHandler(this.OnLoad);
            this.mESW.Save += new PalettePersistEventHandler(this.OnSave);
            this.mESW.StateChanged += new PaletteSetStateEventHandler(this.OnStateChange);
            this.mESW.Icon = (Icon)GlobalResource.ResourceManager.GetObject("calc_icon");
            this.mCmnControl = new CMNControl(this);
            this.mESW.Size = CMNControl.UIData.mESWDefaultSize;
            this.mESW.MinimumSize = CMNControl.UIData.mESWMinSize;
            this.mCmnControl.BackColor = CMNApplication.Theme.ESWBackground;
            this.Add("Command Manager", this.mCmnControl);
            this.mCmnControl.CurrentDocData = (PerDocData)CMNApplication.mDocDataCollection[(object)Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument];
            //this.mCmnControl.RestoreFromCurrentData(false);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentActivated += new DocumentCollectionEventHandler(this.documentActivated);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentToBeDestroyed += new DocumentCollectionEventHandler(this.docToBeDestroyed);
            this.addEdiotrReactors(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument);
        }
        private void OnSave(object sender, PalettePersistEventArgs e)
        {
            if (this.mCmnControl == null)
            {
                return;
            }
            this.mCmnControl.SaveConfiguration(e.ConfigurationSection);
        }
        private void OnLoad(object sender, PalettePersistEventArgs e)
        {
            if (this.mCmnControl == null)
                return;
            if (CMNApplication.ESWShouldSyncWithModal)
            {
                //using (IConfigurationSection parentSection = Autodesk.AutoCAD.ApplicationServices.Application.UserConfigurationManager.OpenDialogSection((object)new CalculatorForm()))
                //    this.mCmnControl.LoadConfiguration(parentSection);
            }
            else
                this.mCmnControl.LoadConfiguration(e.ConfigurationSection);
            this._mbConfigurationLoaded = true;
        }
        private void OnStateChange(object sender, PaletteSetStateEventArgs e)
        {
            //if (e.NewState == StateEventIndex.Show)
            //    this.mCmnControl.Context.HostServices.SetQCState(true);
            //else if (e.NewState == StateEventIndex.Hide)
            //    this.mCmnControl.Context.HostServices.SetQCState(false);
            /*else*/ if (e.NewState == StateEventIndex.ThemeChange)
            {
                if (CMNApplication.Theme != null)
                    CMNApplication.Theme.Update();
                this.mCmnControl.SyncTheme();
            }
            this.mCmnControl.RepairToolTips();
        }
        public Palette Add(string name, Control control)
        {
            return this.mESW.Add(name, control);
        }
        public void addEdiotrReactors(Document doc)
        {
            if ((DisposableWrapper)doc == (DisposableWrapper)null)
                return;
            doc.CommandWillStart += new CommandEventHandler(this.commandWillStart);
            doc.CommandEnded += new CommandEventHandler(this.commandEnded);
            doc.CommandCancelled += new CommandEventHandler(this.commandEnded);
            doc.CommandFailed += new CommandEventHandler(this.commandEnded);
        }
        public void removeEdiotrReactors(Document doc)
        {
            if ((DisposableWrapper)doc == (DisposableWrapper)null)
                return;
            doc.CommandWillStart -= new CommandEventHandler(this.commandWillStart);
            doc.CommandEnded -= new CommandEventHandler(this.commandEnded);
            doc.CommandCancelled -= new CommandEventHandler(this.commandEnded);
            doc.CommandFailed -= new CommandEventHandler(this.commandEnded);
        }
        public void commandWillStart(object sender, CommandEventArgs e)
        {
            this.mCmnControl.UpdateToolBar(false);
            if (e.GlobalCommandName == "YYY")
            {
                MessageBox.Show("Will Check if Palette is Exist!");
            }
        }
        public void commandEnded(object sender, CommandEventArgs e)
        {
            this.mCmnControl.UpdateToolBar(true);
        }
        private void documentActivated(object sender, DocumentCollectionEventArgs e)
        {
            this.addEdiotrReactors(e.Document);
            this.mCmnControl.UpdateToolBar(e.Document.CommandInProgress.Equals(string.Empty));
        }

        private void docToBeDestroyed(object sender, DocumentCollectionEventArgs e)
        {
            this.removeEdiotrReactors(e.Document);
        }
    }
}
