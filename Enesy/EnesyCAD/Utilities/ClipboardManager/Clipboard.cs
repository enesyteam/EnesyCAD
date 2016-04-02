using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.EditorInput;

using Enesy.EnesyCAD.Utilities.ClipboardManager;

public class ClipBoard : IExtensionApplication
{

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]

    private static ClipboardPalette _cp = null;
    public static ClipboardPalette ClipboardPalette
    {
        get
        {
            if (_cp == null)
            {
                _cp = new ClipboardPalette();
            }
            return _cp;
        }
    }


    private static PaletteSet _ps = null;
    public static PaletteSet PaletteSet
    {
        get
        {
            if (_ps == null)
            {
                _ps = new PaletteSet("Clipboard", new System.Guid("ED8CDB2B-3281-4177-99BE-E1A46C3841AD"));
                _ps.Text = "Clipboard";
                _ps.DockEnabled = DockSides.Left;// +DockSides.Right + DockSides.None;
                _ps.MinimumSize = new System.Drawing.Size(200, 300);
                _ps.Size = new System.Drawing.Size(300, 500);
                _ps.Add("Clipboard", ClipboardPalette);
            }
            return _ps;
        }
    }


    public void Initialize()
    {
        DemandLoading.RegistryUpdate.RegisterForDemandLoading();
    }


    public void Terminate()
    {
    }

    [Enesy.EnesyCAD.Runtime.EnesyCADCommandMethod("CLB",
    "ClipBoard",
    "ClipBoard Manager",
    "EnesyCAD",
    "cad@enesy.vn",
    "https://www.youtube.com/watch?v=ma6t7cuxvNw",
    CommandFlags.Modal
    )]
    public static void ShowClipboard()
    {
        PaletteSet.Visible = true;

    }

    [Enesy.EnesyCAD.Runtime.EnesyCADCommandMethod("CLBREMOVE",
    "ClipBoard",
    "Unload ClipBoard Manager",
    "EnesyCAD",
    "cad@enesy.vn",
    "https://www.youtube.com/watch?v=ma6t7cuxvNw",
    CommandFlags.Modal
    )]
    public void RemoveClipboard()
    {
        DemandLoading.RegistryUpdate.UnregisterForDemandLoading();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        ed.WriteMessage("The Clipboard Manager will not be loaded" + " automatically in future editing sessions.");

    }
}