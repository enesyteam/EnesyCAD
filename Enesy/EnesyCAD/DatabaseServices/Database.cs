using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autodesk.AutoCAD.Runtime;
using System.Data;

namespace Enesy.EnesyCAD.DatabaseServices
{
    internal partial class Database
    {
        #region Field and Properties
        /// <summary>
        /// Store all command method
        /// </summary>
        private CmdTableRecord m_cmdTableRecord = new CmdTableRecord();
        public CmdTableRecord CmdTableRecord
        {
            get { return m_cmdTableRecord; }
            set { m_cmdTableRecord = value; }
        }
        #endregion

        #region Constructor
        public Database()
        {
            LoadingECM(false);
            LoadingULF();
        }
        #endregion

        /// <summary>
        /// Update all user lisp function if any changing
        /// </summary>
        public void ReloadULF()
        {
            this.CmdTableRecord.ClearCmd();
            this.LoadingECM(false);
            this.LoadingULF();
        }
    }
}
