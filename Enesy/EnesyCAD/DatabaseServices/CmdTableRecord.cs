using System.Data;

namespace Enesy.EnesyCAD.DatabaseServices
{
    internal class CmdTableRecord : System.Data.DataTable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CmdTableRecord()
        {
            // Init Command Table Record
            this.Columns.Add("Commands", typeof(string));
            this.Columns.Add("Tag", typeof(string));
            this.Columns.Add("Description", typeof(string));
            this.Columns.Add("Author", typeof(string));
            this.Columns.Add("Email", typeof(string));
            this.Columns.Add("Help", typeof(string));
        }

        /// <summary>
        /// Add a cmdRecord to table
        /// </summary>
        /// <param name="cmd"></param>
        public void Add(CmdRecord cmd)
        {
            if (!this.Contains(cmd.GlobalName))
            {
                DataRow dr = this.NewRow();
                dr["Commands"] = cmd.GlobalName;
                dr["Tag"] = cmd.Tag;
                dr["Description"] = cmd.Description;
                dr["Author"] = cmd.Author;
                dr["Email"] = cmd.Email;
                dr["Help"] = cmd.Help;
                this.Rows.Add(dr);
            }
        }

        public void Add(string name, string tag, string description, string author,
            string email, string help)
        {
            if (!this.Contains(name))
            {
                DataRow dr = this.NewRow();
                dr["Commands"] = name;
                dr["Tag"] = tag;
                dr["Description"] = description;
                dr["Author"] = author;
                dr["Email"] = email;
                dr["Help"] = help;
                this.Rows.Add(dr);
            }
        }

        /// <summary>
        /// Clearing all rows
        /// </summary>
        public void ClearCmd()
        {
            this.Rows.Clear();
        }

        /// <summary>
        /// Check if CmdTblRec contains cmd name
        /// </summary>
        /// <param name="cmdName"></param>
        /// <returns></returns>
        public bool Contains(string cmdName)
        {
            bool flag = false;
            try
            {
                DataRow[] found = this.Select("Commands ='" + cmdName + "'");
                flag = (found.Length > 0 ? true : false);
            }
            catch
            {
            }
            return flag;
        }
    }
}
