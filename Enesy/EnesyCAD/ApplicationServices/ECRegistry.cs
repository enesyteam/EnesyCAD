using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Enesy.EnesyCAD.ApplicationServices
{
    internal class ECRegistry
    {
        /// <summary>
        /// For getting user command files, that stored in UserCommand key
        /// </summary>
        public string[] UserCommandFiles
        {
            get
            {
                string[] files;
                RegistryKey HCU = Registry.CurrentUser;
                RegistryKey sk = HCU.OpenSubKey(@"Software\Enesy\EnesyCAD\UserCommand");
                if (sk != null)
                {
                    files = new string[sk.SubKeyCount];
                    try
                    {
                        string[] subK = sk.GetSubKeyNames();
                        for (int i = 0; i < files.Length; i++)
                        {
                            RegistryKey vl = sk.OpenSubKey(subK[i]);
                            files[i] = vl.GetValue("path") as string;
                        }
                    }
                    catch
                    {
                        files = new string[0];
                    }
                }
                else
                {
                    files = new string[0];
                }
                sk.Close();
                HCU.Close();
                return files;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ECRegistry()
        {
            this.CreateEnesyCADSubKey();
            this.CreateUserCommandSubKey();
        }

        /// <summary>
        /// Check if key "HKEY_CURRENT_USER\Software\Enesy\EnesyCAD" exists,
        /// if key is not existed, create it
        /// </summary>
        private void CreateEnesyCADSubKey()
        {
            // Check if Enesy subkey exists
            if (Reg.SubKeyExists(Registry.CurrentUser, @"Software\Enesy"))
            {
                // If subkey exists, check if EnesyCAD subkey exists
                if (!Reg.SubKeyExists(Registry.CurrentUser, @"Software\Enesy\EnesyCAD"))
                {
                    RegistryKey HCU = Registry.CurrentUser;
                    HCU.CreateSubKey(@"Software\Enesy\EnesyCAD");
                    HCU.Close();
                }
            }
            else
            {
                // If subkey don't exists, create it
                RegistryKey HCU = Registry.CurrentUser;
                HCU.CreateSubKey(@"Software\Enesy");
                HCU.CreateSubKey(@"Software\Enesy\EnesyCAD");
                HCU.Close();
            }
        }

        /// <summary>
        /// Check if key "HKEY_CURRENT_USER\Software\Enesy\EnesyCAD\UserCommand" exists,
        /// if key is not existed, create it
        /// </summary>
        private void CreateUserCommandSubKey()
        {
            if (!Reg.SubKeyExists(Registry.CurrentUser,
                        @"Software\Enesy\EnesyCAD\UserCommand"))
            {
                RegistryKey HCU = Registry.CurrentUser;
                HCU.CreateSubKey(@"Software\Enesy\EnesyCAD\UserCommand");
                HCU.Close();
            }
        }

        /// <summary>
        /// Add a user command collection to registry
        /// </summary>
        /// <param name="path">Path of user command file (xml)</param>
        public bool AddUserCommand(string path)
        {
            if (File.Exists(path))
            {
                string name = Path.GetFileNameWithoutExtension(path);
                
                RegistryKey HCU = Registry.CurrentUser;
                RegistryKey sk = HCU.OpenSubKey(@"Software\Enesy\EnesyCAD\UserCommand", true);
                if (sk != null)
                {
                    string[] uc = sk.GetSubKeyNames();
                    
                    // Make sure that this file is the only
                    if (Enesy.Utilities.Contains(this.UserCommandFiles, path))
                    {
                        return false;
                    }

                    // Rename if key is duplicated
                    int i = 1;
                    while (Enesy.Utilities.Contains(uc, name))
                    {
                        name += i.ToString();
                        i++;
                    }
                    
                    // Add user command
                    RegistryKey key;
                    try
                    {
                        key = sk.CreateSubKey(name);
                        key.SetValue("path", path);
                    }
                    catch
                    {
                        return false;
                    }

                    // Closing all key is used
                    key.Close();
                    sk.Close();
                    HCU.Close();
                }
                else return false;
            }
            else return false;
            return true;
        }

        /// <summary>
        /// Delete UC key
        /// </summary>
        /// <param name="xmlPath"></param>
        public bool DeleteUserCommand(string xmlPath)
        {
            bool flag = false;
            try
            {
                RegistryKey HCU = Registry.CurrentUser;
                RegistryKey sk = HCU.OpenSubKey(@"Software\Enesy\EnesyCAD\UserCommand", true);
                if (sk != null)
                {
                    string[] subK = sk.GetSubKeyNames();
                    for (int i = 0; i < subK.Length; i++)
                    {
                        RegistryKey vl = sk.OpenSubKey(subK[i]);
                        if (xmlPath == (string)vl.GetValue("path"))
                        {
                            sk.DeleteSubKeyTree(subK[i]);
                            flag = true;
                        }
                        vl.Close();
                    }
                    sk.Close();
                }
                HCU.Close();
            }
            catch
            {
                return false;
            }
            return flag;
        }
    }
}
