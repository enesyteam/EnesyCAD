using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EnesyCAD.Runtime;
using EnesyCAD.Utilities;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace EnesyCAD.Utilities.Text
{
    public partial class TextParagraphDialog : EnesyCAD.Forms.Form
    {
        /// <summary>
        /// For store ObjID of text
        /// </summary>
        ObjectIdCollection m_objIdColl = new ObjectIdCollection();
        
        /// <summary>
        /// Constructor of form
        /// </summary>
        public TextParagraphDialog()
        {
            InitializeComponent();
            this.Help = EnesyCAD.CommandsHelp.TextParagraph;
        }

        private void butSelectText_Click(object sender, EventArgs e)
        {
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            m_objIdColl = Utilities.SelectText(db, ed);
        }
    }
}
