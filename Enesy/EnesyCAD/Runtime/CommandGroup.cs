using System;
using System.ComponentModel;
using Autodesk.AutoCAD.Runtime;

namespace Enesy.EnesyCAD.Runtime
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class CommandGroup : Attribute
    {
        private string mCategory = "Default";
        public string Category
        {
            get { return mCategory; }
            set { mCategory = value; }
        }
        public CommandGroup(string category)
        {
            this.mCategory = category;
        }
    }
}