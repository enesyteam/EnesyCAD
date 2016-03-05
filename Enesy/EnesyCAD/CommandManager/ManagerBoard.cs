using System;
using System.Reflection;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Enesy.EnesyCAD.Runtime;
using Autodesk.AutoCAD.Runtime;

namespace Enesy.EnesyCAD.CommandManager
{
    class ManagerBoard
    {
        /// <summary>
        /// ManagerBoard (control center) <--> DataSource <--> UI
        /// </summary>
        public DataTable DataSource
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor, load or initilize database of application
        /// </summary>
        public ManagerBoard()
        {
            Initilize();
        }

        /// <summary>
        /// Create database, files, ... for first time of using
        /// </summary>
        private void Initilize()
        {
            // Get then pass list of enesy command to data source
            this.ListCommandsFromThisAssembly();

            // Get path of this assembly
            string assemPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Creating path of lisp function database file
            string data = System.IO.Path.GetDirectoryName(assemPath);
            data = Path.Combine(data, "UserLispFunction.xml");

            if (!File.Exists(data))
            {
                // If database file is not exist, create it
                File.Create(data);
            }
            else
            {
                // If database file is exist, load it
                LoadDatabase(data);
            }
        }

        /// <summary>
        /// Loading database, sending it to DataSource
        /// </summary>
        /// <param name="path"></param>
        private void LoadDatabase(string path)
        {

        }

        #region Get information of commands from EnesyCadCommand attribute
        /// <summary>
        /// Get information of commands from [EnesyCADCommandMethod] attribute
        /// </summary>
        /// <param name="asm">Assembly that contains .NET function</param>
        /// <param name="markedOnly"></param>
        /// <returns></returns>
        private static List<CommandInfo> GetCommands(Assembly asm, bool markedOnly)
        {
            {
                //StringCollection sc = new StringCollection();
                List<CommandInfo> cmdInfo = new List<CommandInfo>();
                object[] objs = asm.GetCustomAttributes(typeof(EnesyCADCommandMethod), true);
                Type[] tps;
                int numTypes = objs.Length;
                if (numTypes > 0)
                {
                    tps = new Type[numTypes];
                    for (int i = 0; i < numTypes; i++)
                    {
                        CommandClassAttribute cca = objs[i] as CommandClassAttribute;
                        if (cca != null)
                        {
                            tps[i] = cca.Type;
                        }
                    }
                }
                else
                {
                    // If we're only looking for specifically
                    // marked CommandClasses, then use an
                    // empty list
                    if (markedOnly)
                        tps = new Type[0];
                    else
                        tps = asm.GetExportedTypes();
                }

                foreach (Type tp in tps)
                {
                    MethodInfo[] meths = tp.GetMethods();
                    foreach (MethodInfo meth in meths)
                    {
                        objs = meth.GetCustomAttributes(typeof(EnesyCADCommandMethod), true);
                        foreach (object obj in objs)
                        {
                            EnesyCADCommandMethod attb = (EnesyCADCommandMethod)obj;
                            if (!attb.IsTest)
                            {
                                cmdInfo.Add(new CommandInfo(attb.GlobalName,
                                                            attb.Tag,
                                                            attb.Description,
                                                            attb.Author,
                                                            attb.Email,
                                                            attb.WebLink
                                                            )
                                );
                            }
                        }
                    }
                }
                return cmdInfo;
            }
        }

        /// <summary>
        /// List Commands in current Assembly
        /// </summary>
        public void ListCommandsFromThisAssembly()
        {
            if (DataSource != null) DataSource.Rows.Clear();

            // Just get the commands for this assembly
            Assembly asm = Assembly.GetExecutingAssembly();
            List<CommandInfo> cmds = GetCommands(asm, false);

            // Send info to dataSource (datatable)
            foreach (CommandInfo cmd in cmds)
            {
                System.Data.DataRow dr = DataSource.NewRow();

                dr[0] = cmd.GlobalName;
                dr[1] = cmd.Tag;
                dr[2] = cmd.Description;
                dr[3] = cmd.Author;
                dr[4] = cmd.Email;
                dr[5] = cmd.WebLink;

                DataSource.Rows.Add(dr);
            }
        }
        #endregion
    }
}
