using System.Xml;
using System.Collections.Generic;
using eApp = Enesy.EnesyCAD.ApplicationServices.EneApplication;

namespace Enesy.EnesyCAD.DatabaseServices
{
    internal partial class Database
    {
        /// <summary>
        /// Loading CmdRecord from xml contents (for encrypted xml file)
        /// </summary>
        /// <param name="xmlContents"></param>
        private void LoadingULF(string xmlContents)
        {
            try
            {
                // Loading string as xml
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlContents);

                // Find all CmdRecord
                XmlNodeList xNodes = xDoc.GetElementsByTagName("CmdRecord");
                foreach (XmlNode node in xNodes)
                {
                    CmdRecord cmd = new CmdRecord(
                        node.ChildNodes.Item(0).InnerText.Trim(),   // name
                        node.ChildNodes.Item(1).InnerText.Trim(),   // tab
                        node.ChildNodes.Item(2).InnerText,         // description
                        node.ChildNodes.Item(3).InnerText,         // author
                        node.ChildNodes.Item(4).InnerText.Trim(),   // email
                        node.ChildNodes.Item(5).InnerText.Trim()   // help
                        );
                    this.CmdTableRecord.Add(cmd);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Loading fileNames of user command files
        /// </summary>
        /// <returns>Collection of files</returns>
        private void LoadingULF()
        {
            // Write message to command window
            Autodesk.AutoCAD.EditorInput.Editor ed =
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.
                                                            MdiActiveDocument.Editor;
            try
            {
                ed.WriteMessage("\nLoading ULF Database ...");

                // Get all userCommandFiles
                string[] ULFFiles;
                ULFFiles = eApp.EneCadRegistry.UserCommandFiles;
                foreach (string file in ULFFiles)
                {
                    string cont = System.IO.File.ReadAllText(file);
                    LoadingULF(cont);
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.Message);
                ed.WriteMessage("\nLoading ULF Database unsuccessfully!");
            }
        }
    }
}
