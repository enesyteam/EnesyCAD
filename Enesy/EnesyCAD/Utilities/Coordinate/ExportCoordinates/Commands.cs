using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD.Runtime;

namespace Enesy.EnesyCAD.Utilities
{
    public partial class Commands
    {
        [EnesyCADCommandMethod(
            "EID",
            "Coordinate",
            "Get and export coordinate of vertex pline",
            CommandsHelp.EnesyAuthor,
            "quandt@enesy.vn",
            CommandsHelp.CoordinatePicker
            )]
        public void ExportCoordinates()
        {
            CoordinatePickerDialog cpd = new CoordinatePickerDialog();
            cpd.Help = CommandsHelp.CoordinatePicker;
            cpd.ShowModal();
        }
    }
}
