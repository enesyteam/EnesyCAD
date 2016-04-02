using System.IO;
using System.Collections.Generic;
using System;
using System.ComponentModel;

using Enesy.EnesyCAD.DatabaseServices;

namespace Enesy.EnesyCAD.IO
{
    internal class LspReader
    {
        #region Properties and Fields
        // Store path of lisp file
        string m_lispFileName = String.Empty;

        // Check whether file name is valid or invalid
        [DefaultValue(false)]
        public bool IsValid { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor with just one file
        /// </summary>
        /// <param name="lispFilePath"></param>
        public LspReader(string lispFilePath)
        {
            FileInfo fi = new FileInfo(lispFilePath);
            if (fi.Exists == true && fi.Extension == ".lsp")
            {
                IsValid = true;
                m_lispFileName = lispFilePath;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// Listing all function in a lisp file
        /// </summary>
        /// <returns></returns>
        public List<LispFunction> ListAllFuntion()
        {
            List<LispFunction> funcs = new List<LispFunction>();
            StreamReader sr = File.OpenText(m_lispFileName);
            try
            {
                string lspLine;
                int line = 1;
                while ((lspLine = sr.ReadLine()) != null)
                {
                    List<string> fs = this.ReadLine(lspLine);
                    foreach (string s in fs)
                    {
                        funcs.Add(new LispFunction(
                            s, "", "", "", "", "", m_lispFileName, line));
                    }
                    line++;
                }
            }
            catch
            {
            }
            sr.Dispose();
            return funcs;
        }

        /// <summary>
        /// List all main function of lisp file
        /// </summary>
        /// <returns></returns>
        public List<LispFunction> ListMainFunction()
        {
            List<LispFunction> allFunc = this.ListAllFuntion();
            List<LispFunction> mainFunc = new List<LispFunction>();
            foreach (LispFunction func in allFunc)
            {
                if (func.GlobalName.ToUpper().Contains("C:"))
                {
                    mainFunc.Add(func);
                }
            }
            return mainFunc;
        }

        /// <summary>
        /// List all sub function of lisp file
        /// </summary>
        /// <returns></returns>
        public List<LispFunction> ListSubFunction()
        {
            List<LispFunction> allFunc = this.ListAllFuntion();
            List<LispFunction> subFunc = new List<LispFunction>();
            foreach (LispFunction func in allFunc)
            {
                if (!func.GlobalName.ToUpper().Contains("C:"))
                {
                    subFunc.Add(func);
                }
            }
            return subFunc;
        }

        /// <summary>
        /// Find defined function (main or sub) in a line of lsp file
        /// </summary>
        /// <param name="line">A line of lsp file</param>
        /// <returns>A list of lisp function</returns>
        public List<string> ReadLine(string lspLine)
        {
            // List that for store and return
            List<string> funcs = new List<string>();

            try
            {
                // Trim comment part
                int i;
                if ((i = lspLine.IndexOf(";")) != -1)
                {
                    lspLine = lspLine.Substring(0, i);
                }

                // Find defun then get func
                lspLine = lspLine.ToLower();
                lspLine = lspLine.Replace('(', ' ');
                lspLine = lspLine.Replace(')', ' ');
                while ((i = lspLine.IndexOf("defun")) != -1)
                {
                    lspLine = lspLine.Substring(i + 5);
                    lspLine = lspLine.Trim();
                    lspLine = lspLine.Trim('\t');
                    funcs.Add(lspLine.Split(' ')[0]);
                }
            }
            catch
            {
            }
            return funcs;
        }
        #endregion
    }
}
