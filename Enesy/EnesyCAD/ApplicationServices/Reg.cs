using Microsoft.Win32;

namespace Enesy.EnesyCAD.ApplicationServices
{
    public class Reg
    {
        /// <summary>
        /// Check if sub key is existed
        /// </summary>
        /// <param name="subKeyPath">Ex:@"Software\Enesy"</param>
        /// <returns></returns>
        public static bool SubKeyExists(RegistryKey rKey, string subKeyPath)
        {
            using (rKey)
            {
                return (rKey.OpenSubKey(subKeyPath, false) == null) ? false : true;
            }
        }

        /// <summary>
        /// Check if a key is existed
        /// </summary>
        /// <param name="subKeyPath">Ex:@"HKEY_LOCAL_MACHINE\System"</param>
        /// <param name="valueName">ex: "Start"</param>
        /// <returns></returns>
        public static bool ValueExists(string subKeyPath, string valueName)
        {
            return (Registry.GetValue(subKeyPath, valueName, null) == null) ? false : true;
        }

        /// <summary>
        /// Write a key, Ex: HKLM\SOFTWARE\Enesy\
        ///                                 path, value = 1
        /// rKey=HKLM; subKeyPath=@"SOFTWARE\Enesy; key=path; value=1;
        /// </summary>
        /// <param name="rKey"></param>
        /// <param name="subKeyPath"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteKey(RegistryKey rKey, string subKeyPath,
            string key, object value)
        {
            try
            {
                RegistryKey rk = rKey.OpenSubKey(subKeyPath, true);
                rk.SetValue(key, value);
                rk.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
