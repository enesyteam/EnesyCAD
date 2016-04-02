using System;
using System.Globalization;
using System.Resources;

namespace Autodesk.AutoCAD.Customization
{
    internal class GlobalResources
    {
        private static ResourceManager rm_;

        public GlobalResources()
        {
        }

        public static object GetObject(string name)
        {
            if (GlobalResources.rm_ == null)
            {
                GlobalResources.rm_ = new ResourceManager(typeof(GlobalResources));
            }
            return GlobalResources.rm_.GetObject(name, CultureInfo.InvariantCulture);
        }
    }
}