using EnesyCAD.Runtime;

namespace EnesyCAD.Utilities.Text
{
    public partial class Commands
    {
        /// <summary>
        /// AutoCAD command method
        /// </summary>
        [EnesyCADCommandMethod(globalName: "TTP", tag: "Text", description: "Text space",
            author: "Enesy", email: "quandt@enesy.vn", webLink: Enesy.WebPageLink.EnesyCadYoutube)]
        public void TextParagraph()
        {
            TextParagraphDialog tpd = new TextParagraphDialog();
            tpd.ShowModal();
        }
    }
}
