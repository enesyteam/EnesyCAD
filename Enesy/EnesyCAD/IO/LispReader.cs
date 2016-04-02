using System.IO;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace Enesy.EnesyCAD.IO
{
    internal class LispReader
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
        public LispReader(string lispFilePath)
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
                    lspLine = lspLine.Trim();
                    lspLine = lspLine.Trim('\t');
                    if (lspLine == "" || lspLine[0] == ';') continue;
                    lspLine = lspLine.ToLower();
                    lspLine = lspLine.Replace('(', ' ');
                    lspLine = lspLine.Replace(')', ' ');

                    int i;
                    while ((i = lspLine.IndexOf("defun")) != -1)
                    {
                        lspLine = lspLine.Substring(i + 5);
                        lspLine = lspLine.Trim();
                        lspLine = lspLine.Trim('\t');
                        funcs.Add(new LispFunction(lspLine.Split(' ')[0],
                                                            m_lispFileName, line));
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
        #endregion
    }
}
