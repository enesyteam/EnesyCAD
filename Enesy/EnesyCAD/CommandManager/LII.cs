using System.Data;
using System.Collections.Generic;
using Enesy.EnesyCAD.IO;
using Enesy.EnesyCAD.DatabaseServices;
using eApp = Enesy.EnesyCAD.ApplicationServices.EneApplication;

namespace Enesy.EnesyCAD.CommandManager
{
    class LI
    {
        /// <summary>
        /// Store path of lisp files that will be imported
        /// </summary>
        List<string> _fileNames = new List<string>();

        /// <summary>
        /// Store valid user commands
        /// </summary>
        public DataTable ValidFunction=new DataTable();

        /// <summary>
        /// Store error user commands
        /// </summary>
        public DataTable ErrorFunction = new DataTable();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileNames">Path of lisp files</param>
        /// <param name="cmdTableRecord">That contains cmd list</param>
        public LI()
        {
            // Init ValidFunction property
            ValidFunction.Columns.Add("Commands");
            ValidFunction.Columns.Add("Tab");
            ValidFunction.Columns.Add("Description");
            ValidFunction.Columns.Add("Author");
            ValidFunction.Columns.Add("Email");
            ValidFunction.Columns.Add("Help");
            ValidFunction.Columns.Add("File");     // Invisible column
            ValidFunction.Columns.Add("Line");     // Invisible column

            // Init ErrorFunction property
            ErrorFunction.Columns.Add("Commands");
            ErrorFunction.Columns.Add("Error");
            ErrorFunction.Columns.Add("File");
            ErrorFunction.Columns.Add("Line");

            // Loading files
            //this.Import();
        }

        public void Import(string fileName)
        {
            if (!_fileNames.Contains(fileName))
            {
                // Store this fileName
                _fileNames.Add(fileName);

                // Read lsp file
                LspReader rd = new LspReader(fileName);
                List<LispFunction> lspFunc = rd.ListMainFunction();
                foreach (LispFunction func in lspFunc)
                {
                    if (IsValid(func.GlobalName))
                    {
                        // If this function is not exist, add it to valid function
                        DataRow r = ValidFunction.NewRow();
                        r[0] = func.GlobalName;
                        ValidFunction.Rows.Add(r);
                    }
                    else
                    {
                        DataRow r = ErrorFunction.NewRow();
                        r[0] = func.GlobalName;
                        r[1] = "Duplicated to existed commands";
                        r[2] = func.FileName;
                        r[3] = func.Line;
                        ErrorFunction.Rows.Add(r);
                    }
                }
            }
        }

        /// <summary>
        /// Check if a lisp function name is valid
        /// </summary>
        /// <param name="cmdName"></param>
        /// <returns></returns>
        private bool IsValid(string cmdName)
        {
            return (CheckExist(cmdName,eApp.EneDatabase.CmdTableRecord) && CheckExist(cmdName, ValidFunction))
                ? true
                : false;
        }

        /// <summary>
        /// Check whether specified lisp function is exist in source or not
        /// </summary>
        /// <param name="lFunc"></param>
        /// <param name="source"></param>
        private bool CheckExist(string lFunc, DataTable source)
        {
            bool flag = false;
            try
            {
                DataRow[] found = source.Select("Commands ='" + lFunc + "'");
                flag = (found.Length > 0 ? true : false);
            }
            catch
            {
                // Do nothing
            }
            return flag;
        }
    }
}
