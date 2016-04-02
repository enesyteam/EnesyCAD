using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;
using System.ComponentModel;
using Autodesk.AutoCAD.Geometry;

namespace Enesy.EnesyCAD.DatabaseServices
{
    /// <summary>
    /// Composite Figure
    /// </summary>
    public class CompositeFigure : FigureBase, INotifyPropertyChanged
    {
        List<Entity> _children = null;
        [Browsable(false)]
        public List<Entity> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }
        public CompositeFigure()
        {
            Children = new List<Entity>();
        }
        [Browsable(false)]
        public virtual Point2d InsertPoint { get; set; }

        public override void Add(Document drawing)
        {
            this.Drawing = drawing;
            Autodesk.AutoCAD.DatabaseServices.Database acDb = Drawing.Database;
            Transaction = Drawing.TransactionManager.StartTransaction();
            BlockTableRecord = Transaction.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

            foreach (Entity f in Children)
            {
                BlockTableRecord.AppendEntity(f);
                Transaction.AddNewlyCreatedDBObject(f, true);
            }
            Transaction.Commit();
        }


#region INotyfi
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
#endregion
    }
}
