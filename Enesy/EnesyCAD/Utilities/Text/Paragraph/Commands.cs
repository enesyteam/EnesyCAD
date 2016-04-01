using Enesy.EnesyCAD.Runtime;
using Enesy.EnesyCAD;
using Enesy.EnesyCAD.Utilities.Text;

namespace Enesy.EnesyCAD.Utilities
{
    public partial class Commands
    {
        /// <summary>
        /// AutoCAD command method
        /// </summary>
        [EnesyCADCommandMethod(globalName: "TTP", tag: "Text", description: "Text space",
            author: CommandsHelp.EnesyAuthor, email: "quandt@enesy.vn",
            webLink: CommandsHelp.TextParagraph)]
        public void TextParagraph()
        {
            TextParagraphDialog tpd = new TextParagraphDialog();
            tpd.ShowModal();
        }
    }
}
