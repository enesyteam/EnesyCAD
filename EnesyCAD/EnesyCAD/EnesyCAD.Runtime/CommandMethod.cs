/*
    Copyright (c) 2016, Enesy.vn, congnv@enesy.vn
*/

using System;
using Autodesk.AutoCAD.Runtime;

namespace Enesy.CAD.Framework.Runtime
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class EnesyCADCommandMethodAttribute : Attribute, ICommandLineCallable
    {
        private string m_groupName;

        private string m_globalName;

        private string m_localizedNameId;

        private CommandFlags m_flags;

        private Type m_contextMenuExtensionType;

        private string m_helpFileName;

        private string m_helpTopic;


        private string m_weblink = "";
        public string Weblink
        {
            get
            {
                return this.m_weblink;
            }
        }
        public string Email { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public Type ContextMenuExtensionType
        {
            get
            {
                return this.m_contextMenuExtensionType;
            }
        }

        public CommandFlags Flags
        {
            get
            {
                return this.m_flags;
            }
        }

        public string GlobalName
        {
            get
            {
                return this.m_globalName;
            }
        }

        public string GroupName
        {
            get
            {
                return this.m_groupName;
            }
        }

        public string HelpFileName
        {
            get
            {
                return this.m_helpFileName;
            }
        }

        public string HelpTopic
        {
            get
            {
                return this.m_helpTopic;
            }
        }

        public string LocalizedNameId
        {
            get
            {
                return this.m_localizedNameId;
            }
        }

        public EnesyCADCommandMethodAttribute(string globalName)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.m_localizedNameId = null;
            this.m_flags = CommandFlags.Modal;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        #region AutoCADpzo
        public EnesyCADCommandMethodAttribute(string globalName, string weblink)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.m_weblink = weblink;
            this.m_localizedNameId = null;
            this.m_flags = CommandFlags.Modal;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }
        public EnesyCADCommandMethodAttribute(string groupName, string globalName, CommandFlags flags, string weblink)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_weblink = weblink;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }
        public EnesyCADCommandMethodAttribute(string globalName, CommandFlags flags, string weblink)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.m_weblink = weblink;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }
        #endregion

        public EnesyCADCommandMethodAttribute(string globalName, CommandFlags flags)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethodAttribute(string groupName, string globalName, CommandFlags flags)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = contextMenuExtensionType;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, string helpTopic)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = helpTopic;
        }

        public EnesyCADCommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType, string helpFileName, string helpTopic)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = contextMenuExtensionType;
            this.m_helpFileName = helpFileName;
            this.m_helpTopic = helpTopic;
        }
    }
}