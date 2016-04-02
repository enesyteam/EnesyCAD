using System;
using System.Collections.Generic;
using System.Text;

namespace Enesy.EnesyCAD.Plot
{
    class QPlotConfig
    {
        /// <summary>
        /// Printer (or Ploter)
        /// In command pallete, printer default is DWG To PDF.pc3
        /// </summary>
        public string Plotter;

        /// <summary>
        /// Size of paper (in printer)
        /// </summary>
        public string PaperSize;

        /// <summary>
        /// File plot style table (*.ctb)
        /// In command pallete, style table default is monochrome.ctb
        /// </summary>
        public string PlotStyleTable;

        /// <summary>
        /// Property (/field) indicated that plot window is fited to paper or not
        /// </summary>
        public bool IsFitToPaper;

        /// <summary>
        /// If IsFitToPaper is false, this command uses CustomScale to plot
        /// </summary>
        public double CustomScale;

        /// <summary>
        /// Center the plot option
        /// </summary>
        public bool IsCenterThePlot;

        /// <summary>
        /// Plot offset group (in plot form, autoCAD software)
        /// </summary>
        public double[] PlotOffset = new double[2];

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plotter"></param>
        /// <param name="paperSize"></param>
        /// <param name="plotStyleTable"></param>
        /// <param name="isFitToPaper"></param>
        /// <param name="customScale"></param>
        /// <param name="isCenterThePlot"></param>
        /// <param name="plotOffset">is 2 elements double array</param>
        public QPlotConfig(string plotter, string paperSize, string plotStyleTable,
            bool isFitToPaper, double customScale, bool isCenterThePlot,
            double[] plotOffset)
        {
            Plotter = plotter;
            PaperSize = paperSize;
            PlotStyleTable = plotStyleTable;
            IsFitToPaper = isFitToPaper;
            CustomScale = customScale;
            IsCenterThePlot = isCenterThePlot;
            PlotOffset = plotOffset;
        }
    }
}
