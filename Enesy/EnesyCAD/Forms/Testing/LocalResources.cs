using System;
using System.Resources;

namespace Autodesk.AutoCAD.Customization
{
    internal class LocalResources
    {
        private static ResourceManager rm_;

        public LocalResources()
        {
        }

        public static string GetString(string name)
        {
            if (LocalResources.rm_ == null)
            {
                LocalResources.rm_ = new ResourceManager(typeof(LocalResources));
            }
            return LocalResources.rm_.GetString(name);
        }
    }
}