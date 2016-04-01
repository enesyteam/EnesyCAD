using System;
using System.Reflection;
using System.Data;

using Enesy.EnesyCAD.Runtime;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.DatabaseServices;

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
                            CmdRecord cmd = new CmdRecord(attb.GlobalName,
                                                        attb.Tag,
                                                        attb.Description,
                                                        attb.Author,
                                                        attb.Email,
                                                        attb.WebLink
                                                        );
                            // Check if Database contains this cmd
                            if (!this.CmdTableRecord.Contains(attb.GlobalName))
                            {
                                //System.Data.DataRow dr = this.CmdTableRecord.NewRow();
                                //dr[0] = attb.GlobalName;
                                //dr[1] = attb.Tag;
                                //dr[2] = attb.Description;
                                //dr[3] = attb.Author;
                                //dr[4] = attb.Email;
                                //dr[5] = attb.WebLink;
                                //this.CmdTableRecord.Rows.Add(dr);
                                this.CmdTableRecord.Add(attb.GlobalName,
                                    attb.Tag,
                                    attb.Description,
                                    attb.Author,
                                    attb.Email,
                                    attb.WebLink
                                    );
                            }
                        }
                    }
                }
            }
        }
    }
}
