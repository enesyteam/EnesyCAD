using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using System;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CoreTeamCommands.Distance
{
    public class DistanceCommand : HavePaletteCommandBase
    {
        private DistanceType mDistanceType = DistanceType.Horizontal;
        private OutputMode mOutputMode = OutputMode.Insert;
        private double mBaseValue = 0;
        private Point3d mBasePoint = new Point3d(0,0,0);
        private double mInputScale = 1;
        private double mOutputScale = 1;
        private string mPrefix = "";
        private string mSurfix = "";


        public DistanceCommand()
        {
        }

 #region Properties
        public DistanceType DistanceType
        {
            get { return mDistanceType; }
            set
            {
                if (value != mDistanceType)
                {
                    mDistanceType = value;
                    //GLOBAL.WriteMessage("\nDistance Type was changed to: " + DistanceType);
                    OnPropertyChanged("DistanceType");
                }
            }
        }
        public OutputMode OutputMode
        {
            get { return mOutputMode; }
            set {
                if (value != mOutputMode)
                {
                    mOutputMode = value;
                    //GLOBAL.WriteMessage("\nOutput Mode was changed to: " + OutputMode);
                    OnPropertyChanged("OutputMode");
                }
            }
        }
        public double BaseValue
        {
            get { return mBaseValue; }
            set { 
                mBaseValue = value;
                //GLOBAL.WriteMessage("\nBase Value was changed to: " + BaseValue);
                OnPropertyChanged("BaseValue");
            }
        }
        public Point3d BasePoint
        {
            get { return mBasePoint; }
            set {
                if (value != mBasePoint)
                {
                    mBasePoint = value;
                    //GLOBAL.WriteMessage("\nBase Point was changed to: " + BasePoint);
                    OnPropertyChanged("BasePoint");
                }
            }
        }
        public double InputScale
        {
            get { return mInputScale; }
            set {
                if (value > 0)
                {
                    this.mInputScale = value;
                    OnPropertyChanged("InputScale");
                }
            }
        }
        public double OutputScale
        {
            get { return mOutputScale; }
            set
            {
                if (value > 0)
                {
                    this.mOutputScale = value;
                    OnPropertyChanged("OutputScale");
                }
            }
        }
        public string Prefix
        {
            get { return mPrefix; }
            set {
                if (!String.IsNullOrEmpty(value))
                {
                    this.mPrefix = value;
                    OnPropertyChanged("Prefix");
                }
            }
        }
        public string Surfix
        {
            get { return mSurfix; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.mSurfix = value;
                    OnPropertyChanged("Surfix");
                }
            }
        }
#endregion
#region CommandMethod
        [EnesyCAD.Runtime.EnesyCADCommandMethod("DIS",
        "Distance",
        "DIS.Desciption",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        [EnesyCAD.Runtime.CommandGroup("DIS")]
        public void ShowESWControl()
        {
            DistanceCommandControl disCtrl = new DistanceCommandControl() { DataContext = this };
            disCtrl.PopulateControls();

            MyControl = disCtrl;
            
            MyPaletteHeader = "Distance";
            Active();
        }
        [EnesyCAD.Runtime.EnesyCADCommandMethod("DISBP",
        "Distance",
        "DIS.Desciption",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        [EnesyCAD.Runtime.CommandGroup("DIS")]
        public void ChangeBasePoint()
        {
            PromptPointOptions ppo = new PromptPointOptions("\nPick new Base Point");
            PromptPointResult ppr = GLOBAL.CurrentEditor.GetPoint(ppo);
            if (ppr.Status == PromptStatus.OK)
            {
                this.BasePoint = ppr.Value;
            }
        }
        [EnesyCAD.Runtime.EnesyCADCommandMethod("DISBV",
        "Distance",
        "DIS.Desciption",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        [EnesyCAD.Runtime.CommandGroup("DIS")]
        public void ChangeBaseValue()
        {
            PromptDoubleOptions pdo = new PromptDoubleOptions("\nCurrent Base Value is " + BaseValue + ". Enter new Base Value");
            PromptDoubleResult pdr = GLOBAL.CurrentEditor.GetDouble(pdo);
            if (pdr.Status == PromptStatus.OK)
            {
                this.BaseValue = pdr.Value;
            }
        }
#if DEBUG
        [CommandMethod("DISCT")]
#endif
        public void ChangeDistanceType()
        {
            PromptKeywordOptions pko = new PromptKeywordOptions("\n" + String.Format(CommandStringResources.ResourceManager.GetString("CurrentIs", GLOBAL.CurrentCulture)
                + " {0}" + ". " + CommandStringResources.ResourceManager.GetString("ChangeTo", GLOBAL.CurrentCulture), this.DistanceType));
            pko.Keywords.Add("Horizontal");
            pko.Keywords.Add("Vertical");
            pko.Keywords.Add("Curve");
            pko.Keywords.Default = "Horizontal";
            PromptResult pr = GLOBAL.CurrentEditor.GetKeywords(pko);
            if (pr.Status == PromptStatus.OK)
            {
                switch (pr.StringResult)
                {
                    case "Horizontal":
                        this.DistanceType = Distance.DistanceType.Horizontal;
                        break;
                    case "Vertical":
                        this.DistanceType = Distance.DistanceType.Vertical;
                        break;
                    case "Curve":
                        this.DistanceType = Distance.DistanceType.Curve;
                        break;
                }
            }
        }
#endregion

#region Find
        //public double FindPoint(Point3d point)
        //{
        //    switch (this.DistanceType)
        //    { 
        //        case Distance.DistanceType.Horizontal:
        //            return FindHDistanceAtPoint(point);
        //        case Distance.DistanceType.Vertical:
        //            return FindVDistanceAtPoint(point);
        //        case Distance.DistanceType.ByCurve:
        //            // ??
        //            return 0;
        //        default: return 0;
        //    }
        //}

        private double FindDistanceAtPoint(Point3d point)
        {
            switch (this.DistanceType)
            { 
                case Distance.DistanceType.Horizontal:
                    return BaseValue + (point.Y - BasePoint.Y) / InputScale;
                case Distance.DistanceType.Vertical:
                    return BaseValue + (point.X - BasePoint.X) / InputScale;
                default: return 0;
            }
        }
#endregion
        [CommandMethod("TDIS")]
        public void Test()
        {
            PromptPointOptions ppo = new PromptPointOptions("\nPick Point");
            PromptPointResult ppr = GLOBAL.CurrentEditor.GetPoint(ppo);
            if (ppr.Status == PromptStatus.OK)
            {
                GLOBAL.WriteMessage(FindDistanceAtPoint(ppr.Value).ToString());
            }
        }
    }
}
