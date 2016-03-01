using System;
using System.Collections.Generic;
using System.Text;

namespace Enesy.EnesyCAD.CommandManager
{
    public class CommandInfo
    {
        public string GlobalName { get; private set; }
        public string Tag { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public string Email { get; private set; }
        public string WebLink { get; private set; }

        public CommandInfo(string globalName,
                            string tab,
                            string description,
                            string author,
                            string email,
                            string webLink
            )
        {
            GlobalName = globalName;
            Tag = tab;
            Description = description;
            Author = author;
            Email = email;
            WebLink = webLink;
        }
    }
}
