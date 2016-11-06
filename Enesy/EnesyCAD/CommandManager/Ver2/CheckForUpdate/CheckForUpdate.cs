using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class CheckForUpdate
    {
        public string appname = string.Empty;
        public Version version = new Version();
        public string newdownloadlink = string.Empty;

        public CheckForUpdate(string uri)
        {
            WebClient client = new WebClient();
            string content = string.Empty;
            Stream stream;

            try
            {
                stream = client.OpenRead(uri);
                StreamReader reader = new StreamReader(stream);
                content = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string[] strContent = content.Split(';');
            if (strContent.Length != 3)
            {
                MessageBox.Show("text file must be in this format \"appname;version;newurl\"");
                return;
            }

            appname = strContent[0];
            version = new Version(strContent[1]);
            newdownloadlink = strContent[2];
        }
    }
}
