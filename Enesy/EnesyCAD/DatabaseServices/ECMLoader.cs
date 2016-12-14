using System;
using System.Reflection;
using System.Data;
using Enesy.EnesyCAD.Runtime;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.DatabaseServices;
using Enesy.EnesyCAD.StringResources;

namespace Enesy.EnesyCAD.DatabaseServices
{
    internal partial class Database
    {
        /// <summary>
        /// Loading Enesy Command Method (ECM) information in this assembly
        /// </summary>
        /// <param name="markedOnly">Default value: false</param>
        internal void LoadingECM(bool markedOnly)
        {
            // Write message to command window
            Autodesk.AutoCAD.EditorInput.Editor ed = 
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.
                                                            MdiActiveDocument.Editor;
            ed.WriteMessage("\nLoading ECM Database ...");

            // Just get the commands for this assembly
            Assembly asm = Assembly.GetExecutingAssembly();

            // for command category
            object[] categorys = asm.GetCustomAttributes(typeof(CommandGroup), true);
            Type[] catTypes;
            int catNumtypes = categorys.Length;




            // Get EnesyCommandMethod attributes
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
                // If we're only looking for specifically marked CommandClasses, then use an
                // empty list
                if (markedOnly)
                    tps = new Type[0];
                else
                    tps = asm.GetExportedTypes();
            }

            // Append valid value into Database
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
                            // get command category
                            object[] cats = meth.GetCustomAttributes(typeof(CommandGroup), true);
                            string category = "";
                            int index = 0;
                            foreach (object c in cats)
                            {
                                index++;
                                CommandGroup commandCat = c as CommandGroup;
                                //GLOBAL.WriteMessage("Command " + attb.GlobalName + " - Category: " + commandCat.Category);
                                category = commandCat.Category;
                            }
                            //GLOBAL.WriteMessage("\nCategory " + index + "");
                            //
                            CmdRecord cmd = new CmdRecord(attb.GlobalName,
                                                        attb.Tag,
                                                        attb.Description,
                                                        attb.Author,
                                                        attb.Email,
                                                        attb.WebLink,
                                                        category
                                                        );
                            // Check if Database contains this cmd
                            if (!this.CmdTableRecord.Contains(attb.GlobalName))
                            {
                                this.CmdTableRecord.Add(attb.GlobalName,
                                    attb.Tag,
                                    !String.IsNullOrEmpty(attb.Tag) ? "[" + attb.Tag + "] " + GetCommandDescription(attb.Description) : GetCommandDescription(attb.Description),
                                    attb.Author,
                                    attb.Email,
                                    attb.WebLink,
                                    category
                                    );
                                //if (!String.IsNullOrEmpty(cat.Category))
                                //{
                                //    GLOBAL.WriteMessage("Category: " + cat.Category);
                                //}
                            }
                        }
                    }
                }
            }
            //
        }
        private string GetCommandDescription(string des)
        {
            string s = CommandMethodResources.ResourceManager.GetString(des, GLOBAL.CurrentCulture);
            return !string.IsNullOrEmpty(s) ? s : des;
        }
    } // end class
}
