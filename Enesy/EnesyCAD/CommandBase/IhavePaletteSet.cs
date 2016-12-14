using Autodesk.AutoCAD.Windows;
using Enesy.EnesyCAD.CommandManager.Ver2;
using System;
using System.Windows.Forms;

namespace Enesy.EnesyCAD
{
    public interface IhavePaletteSet
    {
        UserControl MyControl { get; set; }
        string MyPaletteHeader { get; set; }
        void Active();
    }
}
