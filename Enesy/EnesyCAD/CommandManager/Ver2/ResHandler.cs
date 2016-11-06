namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class ResHandler
    {
        public static string GetResStringByName(string sName)
        {
            if (sName == null || sName.Equals(""))
                return "";
            if (!sName.StartsWith("#"))
                return sName;
            sName = sName.Remove(0, 1);
            return "";// LocalResources.GetString(sName) ?? sName;
        }
    }
}
