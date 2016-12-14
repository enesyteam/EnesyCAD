using System.Xml;
using System.Data;
using System.Collections.Generic;
using Enesy.EnesyCAD.DatabaseServices;

namespace Enesy.EnesyCAD.IO
{
    public class LspImporter
    {
        /// <summary>
        /// Store valid user commands
        /// </summary>
        public DataTable ValidFunction = new DataTable();

        /// <summary>
        /// Store error user commands
        /// </summary>
        public DataTable ErrorFunction = new DataTable();

        /// <summary>
        /// For store fileNames that has been imported
        /// </summary>
        private List<string> m_fileNames = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public LspImporter()
        {
            // Init ValidFunction property
            ValidFunction.Columns.Add("Commands");          //0
            ValidFunction.Columns.Add("Tag");               //1
            ValidFunction.Columns.Add("Description");       //2
            ValidFunction.Columns.Add("Author");            //3
            ValidFunction.Columns.Add("Email");             //4
            ValidFunction.Columns.Add("Help");              //5
            ValidFunction.Columns.Add("File");              //6 Invisible column
            ValidFunction.Columns["File"].ColumnMapping = MappingType.Hidden;
            ValidFunction.Columns.Add("Line");              //7 Invisible column
            ValidFunction.Columns["Line"].ColumnMapping = MappingType.Hidden;
            
            // Init ErrorFunction property
            ErrorFunction.Columns.Add("Commands");          //0
            ErrorFunction.Columns.Add("Error");             //1
            ErrorFunction.Columns["Error"].ReadOnly = true;
            ErrorFunction.Columns.Add("Tag").
                ColumnMapping = MappingType.Hidden;         //2
            ErrorFunction.Columns.Add("Description").
                ColumnMapping = MappingType.Hidden;         //3
            ErrorFunction.Columns.Add("Author").
                ColumnMapping = MappingType.Hidden;         //4
            ErrorFunction.Columns.Add("Email").
                ColumnMapping = MappingType.Hidden;         //5
            ErrorFunction.Columns.Add("Help").
                ColumnMapping = MappingType.Hidden;         //6
            ErrorFunction.Columns.Add("File");              //7
            ErrorFunction.Columns["File"].ReadOnly = true;
            ErrorFunction.Columns.Add("Line");              //8
            ErrorFunction.Columns["Line"].ReadOnly = true;
        }

        void ValidFunction_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            //throw new System.NotImplementedException();
            ErrorFunction.RowChanged -= ErrorFunction_RowChanged;
            ValidFunction.RowChanged -= ValidFunction_RowChanged;
            try
            {
                string name = e.Row["Commands"].ToString();
                Database db = EnesyCAD.ApplicationServices.EneApplication.EneDatabase;

                LispFunction lspFunc = new LispFunction(
                        name,
                        e.Row["Tag"].ToString(),
                        e.Row["Description"].ToString(),
                        e.Row["Author"].ToString(),
                        e.Row["Email"].ToString(),
                        e.Row["Help"].ToString(),
                        e.Row["File"].ToString(),
                        System.Convert.ToInt32(e.Row["Line"])
                        );

                // Check if lspFunc exists in databse
                if (db.CmdTableRecord.Contains(name))
                {
                    ValidFunction.Rows.Remove(e.Row);
                    InsertErrorFunc(lspFunc, "Duplicated to database commands");
                }

                // Check if lspFunc exists in ValidFunction
                else if (CheckExist(name, ValidFunction))
                {
                    ValidFunction.Rows.Remove(e.Row);
                    InsertErrorFunc(lspFunc, "Duplicated to valid commands");
                }
            }
            catch { }
            ErrorFunction.RowChanged += ErrorFunction_RowChanged;
            ValidFunction.RowChanged += ValidFunction_RowChanged;
        }

        void ErrorFunction_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            //throw new System.NotImplementedException();
            ValidFunction.RowChanged -= ValidFunction_RowChanged;
            ErrorFunction.RowChanged -= ErrorFunction_RowChanged;
            try
            {
                string name = e.Row["Commands"].ToString();
                Database db = EnesyCAD.ApplicationServices.EneApplication.EneDatabase;

                LispFunction lspFunc = new LispFunction(
                        name,
                        e.Row["Tag"].ToString(),
                        e.Row["Description"].ToString(),
                        e.Row["Author"].ToString(),
                        e.Row["Email"].ToString(),
                        e.Row["Help"].ToString(),
                        e.Row["File"].ToString(),
                        System.Convert.ToInt32(e.Row["Line"])
                        );

                // Check if lspFunc exists in databse
                if (db.CmdTableRecord.Contains(name))
                {
                    ErrorFunction.Rows.Remove(e.Row);
                    InsertErrorFunc(lspFunc, "Duplicated to database commands");
                }

                // Check if lspFunc exists in ValidFunction
                else if (CheckExist(name, ValidFunction))
                {
                    ErrorFunction.Rows.Remove(e.Row);
                    InsertErrorFunc(lspFunc, "Duplicated to valid commands");
                }

                // If it has not any duplication
                else
                {
                    ErrorFunction.Rows.Remove(e.Row);
                    InsertValidFunc(lspFunc);
                }
            }
            catch { }
            ValidFunction.RowChanged += ValidFunction_RowChanged;
            ErrorFunction.RowChanged += ErrorFunction_RowChanged;
        }

        /// <summary>
        /// Adding .lsp files
        /// </summary>
        /// <param name="fileNames"></param>
        public void AddFiles(string[] fileNames)
        {
            ValidFunction.RowChanged -= ValidFunction_RowChanged;
            ErrorFunction.RowChanged -= ErrorFunction_RowChanged;

            foreach (string file in fileNames)
            {
                // If this file has been imported, continue
                if (m_fileNames.Contains(file)) continue;

                // Check if file exists
                if (!System.IO.File.Exists(file)) continue;

                // Get database
                Database db = EnesyCAD.ApplicationServices.EneApplication.EneDatabase;

                // Read .lsp file
                LspReader lReader = new LspReader(file);
                List<LispFunction> lspFuncs = lReader.ListMainFunction();
                foreach (LispFunction lspFunc in lspFuncs)
                {
                    // Check if lspFunc exists in databse
                    if (db.CmdTableRecord.Contains(lspFunc.GlobalName))
                    {
                        InsertErrorFunc(lspFunc, "Duplicated to database commands");
                    }
                    // Check if lspFunc exists in ValidFunction
                    else if (CheckExist(lspFunc.GlobalName, ValidFunction))
                    {
                        InsertErrorFunc(lspFunc, "Duplicated to valid commands");
                    }
                    else InsertValidFunc(lspFunc);
                }
            }
            ValidFunction.RowChanged += ValidFunction_RowChanged;
            ErrorFunction.RowChanged += ErrorFunction_RowChanged;
        }

        /// <summary>
        /// Insert a row to valid function table
        /// </summary>
        /// <param name="lspFunc"></param>
        private void InsertValidFunc(LispFunction lspFunc)
        {
            DataRow r = ValidFunction.NewRow();
            if (lspFunc.GlobalName.Contains("c:"))
            {
                r[0] = lspFunc.GlobalName.Substring(
                    lspFunc.GlobalName.IndexOf("c:") + 2).ToUpper();
            }
            else
            {
                r[0] = lspFunc.GlobalName;
            }
            r[6] = lspFunc.FileName;
            r[7] = lspFunc.Line;
            ValidFunction.Rows.Add(r);
        }

        /// <summary>
        /// Insert a row to error function table
        /// </summary>
        /// <param name="lspFunc"></param>
        private void InsertErrorFunc(LispFunction lspFunc, string error)
        {
            DataRow r = ErrorFunction.NewRow();
            if (lspFunc.GlobalName.Contains("c:"))
            {
                r[0] = lspFunc.GlobalName.Substring(
                    lspFunc.GlobalName.IndexOf("c:") + 2).ToUpper();
            }
            else
            {
                r[0] = lspFunc.GlobalName;
            }
            r[1] = error;
            r[7] = lspFunc.FileName;
            r[8] = lspFunc.Line;
            ErrorFunction.Rows.Add(r);
        }

        /// <summary>
        /// Check if Datatable contains specifiedcommand
        /// </summary>
        /// <param name="lFunc"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool CheckExist(string lFunc, DataTable source)
        {
            bool flag = false;
            try
            {
                DataRow[] found = source.Select("Commands ='" + lFunc + "'");
                flag = (found.Length > 1 ? true : false);
            }
            catch
            {
            }
            return flag;
        }

        /// <summary>
        /// Export valid function without xml root
        /// </summary>
        /// <returns>xml contents</returns>
        public void ExportToDatabase()
        {
            Database db = Enesy.EnesyCAD.ApplicationServices.EneApplication.EneDatabase;
            try
            {
                for (int i = 0; i < ValidFunction.Rows.Count; i++)
                {
                    DataRow r = ValidFunction.Rows[i];
                    db.CmdTableRecord.Add(
                        r["Commands"].ToString(),
                        r["Tag"].ToString(),
                        r["Description"].ToString(),
                        r["Author"].ToString(),
                        r["Email"].ToString(),
                        r["Help"].ToString(),
                        r["Group"].ToString()
                        );
                }
            }
            catch { }
        }
    }
}
