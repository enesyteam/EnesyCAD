namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class GroupButton : ToolTipButton
    {
        public cmnBtnData mBtnData;
        public int mXindex;
        public int mYindex;

        public GroupButton(cmnBtnData data)
        {
            this.mBtnData = data;
            if (data == null)
            {
                this.mXindex = -1;
                this.mYindex = -1;
            }
            else
            {
                this.mXindex = data.mHIndex;
                this.mYindex = data.mVIndex;
            }
        }
    }
}
