using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class ButtonGroupCtrl : GroupControl
    {
        public static int GROUP_LEFT_PADDING = 10;
        public static int GROUP_RIGHT_PADDING = 10;
        public static int GROUP_BOTTOM_PADDING = 8;
        public static int GROUP_TOP_PADDING = GroupControl.GROUP_TITLE_HEIGHT + ButtonGroupCtrl.GROUP_BOTTOM_PADDING;
        public static int BUTTON_H_SPACE = 8;
        public static int BUTTON_V_SPACE = 4;
        private GroupButton[] mButtons;
        private int mRows;
        private int mColumns;

        public ButtonGroupCtrl(GroupsPane parent)
            : base(parent)
        {
            this.DrawBoundary = true;
        }
        private static FlatStyle GetBtnStyle()
        {
            return W32Util.VisualStylesEnabled ? FlatStyle.System : FlatStyle.Flat;
        }

        public override bool Populate(cmnGroupData data)
        {
            if (!base.Populate(data) || data.GetType() != typeof(CMNBtnGroupData))
                return false;
            CMNBtnGroupData calcBtnGroupData = (CMNBtnGroupData)data;
            if (calcBtnGroupData.mButtons == null || calcBtnGroupData.mButtons.Length == 0)
                return true;
            this.mButtons = new GroupButton[calcBtnGroupData.mButtons.Length];
            this.mRows = 0;
            this.mColumns = 0;
            foreach (cmnBtnData mButton in calcBtnGroupData.mButtons)
            {
                if (mButton.mVIndex >= this.mRows)
                    ++this.mRows;
                if (mButton.mHIndex >= this.mColumns)
                    ++this.mColumns;
            }
            if (this.mRows == 0 || this.mColumns == 0)
                return true;
            this.mHeight = ButtonGroupCtrl.GROUP_TOP_PADDING + this.mRows * (calcBtnGroupData.mBtnSize.Height + ButtonGroupCtrl.BUTTON_V_SPACE) - ButtonGroupCtrl.BUTTON_V_SPACE + ButtonGroupCtrl.GROUP_BOTTOM_PADDING;
            FlatStyle btnStyle = ButtonGroupCtrl.GetBtnStyle();
            for (int index = 0; index < calcBtnGroupData.mButtons.Length; ++index)
            {
                cmnBtnData data1 = (cmnBtnData)calcBtnGroupData.mButtons.GetValue(index);
                GroupButton groupButton = new GroupButton(data1);
                groupButton.FlatStyle = btnStyle;
                groupButton.ForeColor = Color.FromName(data1.msColor);
                groupButton.Location = new Point(ButtonGroupCtrl.GROUP_LEFT_PADDING + data1.mHIndex * (calcBtnGroupData.mBtnSize.Width + ButtonGroupCtrl.BUTTON_H_SPACE), ButtonGroupCtrl.GROUP_TOP_PADDING + data1.mVIndex * (calcBtnGroupData.mBtnSize.Height + ButtonGroupCtrl.BUTTON_V_SPACE));
                groupButton.Size = calcBtnGroupData.mBtnSize;
                groupButton.Name = data1.msExpression;
                groupButton.TabIndex = index;
                groupButton.Text = ResHandler.GetResStringByName(data1.msLabel);
                string msToolTip = data1.msToolTip;
                if (msToolTip != null && !msToolTip.Equals(""))
                    groupButton.ToolTip = ResHandler.GetResStringByName(data1.msToolTip);
                groupButton.Click += new EventHandler(this.OnButton_Click);
                this.mButtons.SetValue((object)groupButton, index);
                this.Controls.Add((Control)groupButton);
            }
            return true;
        }

        public void ResizeButtons(int dx)
        {
            if (this.mColumns <= 0)
                return;
            int width = ((CMNBtnGroupData)this.Data).mBtnSize.Width;
            int num1 = (dx - ButtonGroupCtrl.GROUP_LEFT_PADDING - ButtonGroupCtrl.GROUP_RIGHT_PADDING - (this.mColumns - 1) * ButtonGroupCtrl.BUTTON_H_SPACE) / this.mColumns;
            double num2 = (double)num1 / (double)width;
            if (num2 > 1.0)
            {
                double mBtnMaxHratio = ((CMNBtnGroupData)this.Data).mBtnMaxHRatio;
            }
            if (num2 <= 1.0)
                num1 = width;
            else if (num2 > ((CMNBtnGroupData)this.Data).mBtnMaxHRatio)
                num1 = (int)((CMNBtnGroupData)this.Data).mBtnMaxHRatio * width;
            this.SuspendLayout();
            foreach (GroupButton mButton in this.mButtons)
            {
                mButton.Width = num1;
                mButton.Left = ButtonGroupCtrl.GROUP_LEFT_PADDING + mButton.mXindex * (num1 + ButtonGroupCtrl.BUTTON_H_SPACE);
            }
            this.ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ResizeButtons(this.ClientSize.Width);
        }

        public override void ThemeChanged()
        {
            base.ThemeChanged();
            FlatStyle btnStyle = ButtonGroupCtrl.GetBtnStyle();
            foreach (GroupButton mButton in this.mButtons)
            {
                if (mButton.FlatStyle != btnStyle)
                    mButton.FlatStyle = btnStyle;
            }
        }

        private void CreateMemoryVariable(string sValue)
        {
            //if (this.CalcControl.VariableExists(this.msMemoryString))
            //    return;
            //this.CalcControl.UIAddedGlobal(ref new AcCalcVarItem()
            //{
            //  mName = this.msMemoryString,
            //  msValue = sValue,
            //  msDescription = ResHandler.GetResStringByName("#Calculator Current Memory"),
            //  msType = AcCalcVarItem.varType.double_constant
            //}, "");
        }

        private void SetMemoryVariable(string sValue)
        {
            //AcCalcVarItem var = new AcCalcVarItem();
            //var.mName = this.msMemoryString;
            //var.msValue = sValue;
            //if (!this.CalcControl.VariableExists(this.msMemoryString))
            //{
            //    var.msDescription = ResHandler.GetResStringByName("#Calculator Current Memory");
            //    var.msType = AcCalcVarItem.varType.double_constant;
            //    this.CalcControl.UIAddedGlobal(ref var, "");
            //}
            //else
            //    this.CalcControl.UIChangedGlobal(ref var);
        }

        private void OnButton_Click(object sender, EventArgs e)
        {
            cmnBtnData.Action msAction = ((GroupButton)sender).mBtnData.msAction;
            ExpressionType type = ((GroupButton)sender).mBtnData.msExpressionType;
            switch (msAction)
            {
                case cmnBtnData.Action.append:
                    //this.CalcControl.AppendToInputField(((GroupButton)sender).mBtnData.msExpression);
                    MessageBox.Show("a");
                    break;
                case cmnBtnData.Action.clear:
                    //this.CalcControl.ClearInputField();
                    MessageBox.Show("a");
                    break;
                case cmnBtnData.Action.clear_history:
                    //this.CalcControl.ClearHistory();
                    MessageBox.Show("a");
                    break;
                case cmnBtnData.Action.backspace:
                    //this.CalcControl.BackspaceToInputField();
                    MessageBox.Show("a");
                    break;
                case cmnBtnData.Action.evaluate:
                    //this.CalcControl.EvaluateExpression(this.CalcControl.mEquationTextBox.Text, true, ExpressionEvaluationAction.Replace);
                    MessageBox.Show("a");
                    type = ExpressionType.Operand;
                    break;
                case cmnBtnData.Action.mem_store:
                    //if (this.CalcControl.mEquationTextBox.Text == null || this.CalcControl.mEquationTextBox.Text.Equals(""))
                    //{
                    //    int num = (int)MessageBox.Show(ResHandler.GetResStringByName("#EmptyExpressionError"), ResHandler.GetResStringByName("#EmptyExpressionErrorTitle"));
                    //    break;
                    //}
                    //this.SetMemoryVariable(this.CalcControl.mEquationTextBox.Text);
                    break;
                case cmnBtnData.Action.mem_plus:
                    //if (this.CalcControl.mEquationTextBox.Text == null || this.CalcControl.mEquationTextBox.Text.Equals(""))
                    //{
                    //    int num = (int)MessageBox.Show(ResHandler.GetResStringByName("#EmptyExpressionError"), ResHandler.GetResStringByName("#EmptyExpressionErrorTitle"));
                    //    break;
                    //}
                    //if (!this.CalcControl.VariableExists(this.msMemoryString))
                    //    this.CreateMemoryVariable("0");
                    //this.CalcControl.EvaluateExpression("$" + this.msMemoryString + "= $" + this.msMemoryString + " + " + this.CalcControl.mEquationTextBox.Text, false, ExpressionEvaluationAction.Replace);
                    break;
                case cmnBtnData.Action.mem_minus:
                    //if (this.CalcControl.mEquationTextBox.Text == null || this.CalcControl.mEquationTextBox.Text.Equals(""))
                    //{
                    //    int num = (int)MessageBox.Show(ResHandler.GetResStringByName("#EmptyExpressionError"), ResHandler.GetResStringByName("#EmptyExpressionErrorTitle"));
                    //    break;
                    //}
                    //if (!this.CalcControl.VariableExists(this.msMemoryString))
                    //    this.CreateMemoryVariable("0");
                    //this.CalcControl.EvaluateExpression("$" + this.msMemoryString + "=$" + this.msMemoryString + "-" + this.CalcControl.mEquationTextBox.Text, false, ExpressionEvaluationAction.Replace);
                    break;
                case cmnBtnData.Action.mem_recall:
                    //if (this.CalcControl.VariableExists(this.msMemoryString))
                    //{
                    //    this.CalcControl.EvaluateExpression("$" + this.msMemoryString, false, ExpressionEvaluationAction.AppendResult);
                    //    break;
                    //}
                    //this.CalcControl.mEquationTextBox.ApplyExpressionChange(ExpressionType.Result, "0");
                    break;
                case cmnBtnData.Action.mem_clear:
                    this.SetMemoryVariable("0");
                    break;
                default:
                    //this.CalcControl.mEquationTextBox.ApplyExpressionChange(type, ((GroupButton)sender).mBtnData.msExpression);
                    break;
            }
            //this.CalcControl.SetLastExpressionType(type);
        }

        public override void RepairToolTips()
        {
            base.RepairToolTips();
            foreach (Control control in (ArrangedElementCollection)this.Controls)
            {
                if (control.GetType() == typeof(GroupButton))
                    ((ToolTipButton)control).ToolTip = ((ToolTipButton)control).ToolTip;
            }
        }
    }
}
