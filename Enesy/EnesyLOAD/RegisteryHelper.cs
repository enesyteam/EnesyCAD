using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegisterAutoCADpzo
{
    public static class RegisteryHelper
    {
        public static bool Create(string assembly, AutoCADVersion version)
        {
            string fileName = Path.GetFileNameWithoutExtension(assembly);
            // Tạo
            RegistryKey subKey = null;
            try
            {
                switch (version)
                {
                    case AutoCADVersion.AutoCAD2007:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R17.0\\ACAD-5001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2008:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R17.1\\ACAD-6001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2009:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R17.2\\ACAD-7001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2010:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R18.0\\ACAD-8001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2011:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R18.1\\ACAD-9001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2012:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R18.2\\ACAD-A001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2013:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R19.0\\ACAD-B001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2014:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R19.1\\ACAD-D001:409\\Applications\\" + fileName);
                        break;
                    case AutoCADVersion.AutoCAD2015:
                        subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R20.0\\ACAD-E001:409\\Applications\\" + fileName);
                        break;
                }
                subKey.SetValue("DESCRIPTION", "AutoCADpzo - by Cá Cơm");
                subKey.SetValue("LOADCTRLS", 00000002, RegistryValueKind.DWord);
                subKey.SetValue("MANAGED", 00000001, RegistryValueKind.DWord);
                subKey.SetValue("LOADER", assembly);
                subKey.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Assemblies Register fail!");
            }
            return false;
            
        }
        public static List<string> ReadAssemblies(AutoCADVersion version)
        {
            RegistryKey subKey = null;
            List<string> result = new List<string>();
            switch (version)
            {
                case AutoCADVersion.AutoCAD2007:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R17.0\\ACAD-5001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2008:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R17.1\\ACAD-6001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2009:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R17.2\\ACAD-7001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2010:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R18.0\\ACAD-8001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2011:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R18.1\\ACAD-9001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2012:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R18.2\\ACAD-A001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2013:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R19.0\\ACAD-B001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2014:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R19.1\\ACAD-D001:409\\Applications\\");
                    break;
                case AutoCADVersion.AutoCAD2015:
                    subKey = Registry.CurrentUser.CreateSubKey("Software\\Autodesk\\AutoCAD\\R20.0\\ACAD-E001:409\\Applications\\");
                    break;
            }
            //subKey.SetValue("DESCRIPTION", "AutoCADpzo - by Cá Cơm");
            //subKey.SetValue("LOADCTRLS", 00000002, RegistryValueKind.DWord);
            //subKey.SetValue("MANAGED", 00000001, RegistryValueKind.DWord);
            //subKey.SetValue("LOADER", assembly);
            foreach (string s in subKey.GetSubKeyNames())
            {
                result.Add(s);
            }
            subKey.Close();
            return result;
        }

        //public static RegistryKey CurrentKey(AutoCADVersion version)
        //{ 
        
        //}

        public static string KeyFromAutoCADVersion(AutoCADVersion version)
        {
            string result = string.Empty;
            switch (version)
            {
                case AutoCADVersion.AutoCAD2007:
                    result = "Software\\Autodesk\\AutoCAD\\R17.0\\ACAD-5001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2008:
                    result = "Software\\Autodesk\\AutoCAD\\R17.1\\ACAD-6001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2009:
                    result = "Software\\Autodesk\\AutoCAD\\R17.2\\ACAD-7001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2010:
                    result = "Software\\Autodesk\\AutoCAD\\R18.0\\ACAD-8001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2011:
                    result = "Software\\Autodesk\\AutoCAD\\R18.1\\ACAD-9001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2012:
                    result = "Software\\Autodesk\\AutoCAD\\R18.2\\ACAD-A001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2013:
                    result = "Software\\Autodesk\\AutoCAD\\R19.0\\ACAD-B001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2014:
                    result = "Software\\Autodesk\\AutoCAD\\R19.1\\ACAD-D001:409\\Applications\\";
                    break;
                case AutoCADVersion.AutoCAD2015:
                    result = "Software\\Autodesk\\AutoCAD\\R20.0\\ACAD-E001:409\\Applications\\";
                    break;
            }
            return result;
        }
    }
    

    public enum AutoCADVersion
    { 
        AutoCAD2007,
        AutoCAD2008,
        AutoCAD2009,
        AutoCAD2010,
        AutoCAD2011,
        AutoCAD2012,
        AutoCAD2013,
        AutoCAD2014,
        AutoCAD2015,
    }
}
