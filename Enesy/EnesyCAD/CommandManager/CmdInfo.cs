

namespace Enesy.EnesyCAD.CommandManager
{
    internal struct CmdInfo
    {
        string GlobalName, Tab, Description, Author, Email, Help;

        bool NameEditable;

        string FileName;

        int Line;

        public CmdInfo(string globalName, string tab, string description, string author, string email, string help,
                        bool nameEditable, string fileName, int line)
        {
            GlobalName = globalName;
            Tab = tab;
            Description = description;
            Author = author;
            Email = email;
            Help = help;
            NameEditable = nameEditable;
            FileName = fileName;
            Line = line;
        }
    }
}
