using System;
using System.Collections.Generic;
using System.Text;

namespace Enesy.EnesyCAD.IO
{
    class LispFunction
    {
        public string GlobalName { get; set; }
        public string FileName { get; private set; }
        public int Line { get; private set; }

        public LispFunction(string globalName, string fileName, int line)
        {
            GlobalName = globalName;
            FileName = fileName;
            Line = line;
        }
    }
}
