using System;
using System.Collections.Generic;
using System.Text;

namespace Enesy.EnesyCAD.DatabaseServices
{
    internal struct LispFunction
    {
        public string GlobalName;
        public string Tag;
        public string Description;
        public string Author;
        public string Email;
        public string Help;
        public string FileName;
        public int Line;

        public LispFunction(string globalName, string tag, string description,
            string author, string email, string help, string fileName, int line)
        {
            GlobalName = globalName;
            Tag = tag;
            Description = description;
            Author = author;
            Email = email;
            Help = help;
            FileName = fileName;
            Line = line;
        }
    }
}
