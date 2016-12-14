//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.Runtime;
//using Enesy.EnesyCAD.Runtime;

//namespace Enesy.EnesyCAD.CoreTeamCommands.Others.ObjectSnap
//{
//    // Kean Wislam
//    [CommandMethod("FORSNAP")]
//    public static void ForceObjectSnapping()
//    {
//      var doc = Application.DocumentManager.MdiActiveDocument;
//      var ed = doc.Editor;     
//      ed.PointFilter += new PointFilterEventHandler(OnPointFilter);
//    }
 
//    [CommandMethod("UNFORSNAP")]
//    public static void DisableForcedObjectSnapping()
//    {
//      var doc = Application.DocumentManager.MdiActiveDocument;
//      var ed = doc.Editor;
//      ed.PointFilter -= new PointFilterEventHandler(OnPointFilter);
//    }
 
//    static void OnPointFilter(object sender, PointFilterEventArgs e)
//    {
//      // Only if a command is active
 
//      bool cmdActive =
//        (short)Application.GetSystemVariable("CMDACTIVE") == 1;
//      if (e.Context.PointComputed && cmdActive)
//      {
//        // Check whether the point has been computed via an
//        // object snap
 
//        if (
//          (e.Context.History & PointHistoryBits.ObjectSnapped) == 0
//        )
//        {
//          // If not, don't accept it
 
//          e.Result.Retry = true;
//        }
//      }
//    }
//}
