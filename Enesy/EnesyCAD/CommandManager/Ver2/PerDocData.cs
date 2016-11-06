using Autodesk.AutoCAD.AcCalc;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class PerDocData
    {
        public Document mDocument;
        public ArrayList mHistoryList;
        public string mCurrentExpression;

        public PerDocData(Document document)
        {
            this.mDocument = document;
            this.mHistoryList = new ArrayList();
        }

        private PerDocData()
        {
        }

        public void AddHistory(string sExpr, CalcResult result)
        {
           // this.mHistoryList.Add((object)new ExpressionResultPair(sExpr, result));
        }

        public void ClearHistoryData()
        {
            this.mHistoryList.Clear();
        }
    }
}
