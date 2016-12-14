using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.CommandManager.Ver2;
using System.ComponentModel;

namespace Enesy.EnesyCAD.CommandManager
{
    public class Commands
    {
        private static bool firstInvoke_ = true;
        private PropertyDescriptor sortProperty_;
        private ListSortDirection direction_;
        private static Commands current_;

        public PropertyDescriptor SortProperty
        {
            get
            {
                return this.sortProperty_;
            }
            set
            {
                this.sortProperty_ = value;
            }
        }

        public ListSortDirection SortDirection
        {
            get
            {
                return this.direction_;
            }
            set
            {
                this.direction_ = value;
            }
        }

        public static Commands Current
        {
            get
            {
                return Commands.current_;
            }
        }

        private void LoadExtensions()
        {
        }

        [CommandMethod("CE", CommandFlags.Transparent)]
        public void commandmanager()
        {
            Commands.current_ = this;
            try
            {
                if (Commands.firstInvoke_)
                {
                    this.LoadExtensions();
                    Commands.firstInvoke_ = false;
                }
                try
                {
                    Application.DocumentManager.MdiActiveDocument.TransactionManager.EnableGraphicsFlush(true);
                }
                catch (Exception ex)
                {
                    if (ex.ErrorStatus != ErrorStatus.LockViolation)
                        throw ex;
                }
                int num = int.Parse(Application.GetSystemVariable("CMDACTIVE").ToString());
                if ((num & 3) == 3 || (num & 36) != 0)
                {
                    string sStatusString = Autodesk.AutoCAD.Internal.Utils.GetCommandAtLevelForDocument(1);
                    if (!sStatusString.Equals(""))
                        sStatusString = ResHandler.GetResStringByName("#Active Command:") + " " + sStatusString.ToUpper();
                    //CalcResult calcResult = QCalcApplication.ShowModalCalculator(sStatusString, "0", true);
                    //if (!((DisposableWrapper)calcResult != (DisposableWrapper)null))
                    //    return;
                    //string message = calcResult.ResultString.Replace(" ", "-");
                    //if (calcResult.Type == ValueTypeEnum.VectorType)
                    //    Autodesk.AutoCAD.Internal.Utils.WriteToCommandLine(message + " ");
                    //else
                    //    Autodesk.AutoCAD.Internal.Utils.WriteToCommandLine(message);
                    Autodesk.AutoCAD.Internal.Utils.WriteToCommandLine("CE");
                }
                else
                {
                    //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nEnesyCAD version " + CMNApplication.CurrentVersion);
                    CMNApplication.ShowESWCmn();
                }
            }
            catch
            {
            }
            finally
            {
                Commands.current_ = null;
            }
        }

        [CommandMethod("CEC", CommandFlags.Transparent)]
        public void qcclose()
        {
            Commands.current_ = this;
            try
            {
                CMNApplication.HideESWCmn(true);
            }
            finally
            {
                Commands.current_ = null;
            }
        }
        [CommandMethod("CEINIT", CommandFlags.Transparent)]
        public void reloadcommandlist()
        {
            if (CMNApplication.ESWCmn.CmnControl == null) return;
            CMNApplication.ESWCmn.CmnControl.ReloadUILanguage();
        }
    }
}
