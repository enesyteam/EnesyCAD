using System;
using System.ComponentModel;
using Autodesk.AutoCAD.Runtime;

namespace Enesy.EnesyCAD.Runtime
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class EnesyCADCommandMethod : Attribute, ICommandLineCallable
    {
        #region Implement interface member
        private string m_groupName;

        private string m_globalName;

        private string m_localizedNameId;

        private CommandFlags m_flags;

        private Type m_contextMenuExtensionType;

        private string m_helpFileName;

        private string m_helpTopic;

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

        public Type ContextMenuExtensionType
        {
            get
            {
                return this.m_contextMenuExtensionType;
            }
        }  
        #endregion

        [DefaultValue("")]
        public string Tag { get; private set; }

        [DefaultValue("")]
        public string Description { get; private set; }

        [DefaultValue("")]
        public string Author { get; private set; }

        [DefaultValue("")]
        public string Email { get; private set; }

        [DefaultValue("")]
        public string WebLink { get; private set; }

        // If command is function for test, don't show on command manager
        [DefaultValue(false)]
        public bool IsTest { get; private set; }

        #region User constructor by EnesyCAD
        public EnesyCADCommandMethod(string globalName,
            string helpTopic)
        {
            // Interface member
            this.m_globalName = globalName;
            this.m_groupName = null;
            this.m_localizedNameId = null;
            this.m_flags = CommandFlags.Modal;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = helpTopic;

            // Custom member
        }

        public EnesyCADCommandMethod(   string globalName,
                                        string tag,
                                        string description,
                                        string author,
                                        string email,
                                        string webLink
            )
        {
            // Interface member
            this.m_globalName = globalName;
            this.m_groupName = null;
            this.m_localizedNameId = null;
            this.m_flags = CommandFlags.Modal;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;

            // Custom member
            this.Tag = tag;
            this.Description = description;
            this.Author = author;
            this.Email = email;
            this.WebLink = webLink;
        }

        public EnesyCADCommandMethod(string globalName,
                                        string tag,
                                        string description,
                                        string author,
                                        string email,
                                        string webLink,
                                        CommandFlags flags
            )
        {
            // Interface member
            this.m_globalName = globalName;
            this.m_groupName = null;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;

            // Custom member
            this.Tag = tag;
            this.Description = description;
            this.Author = author;
            this.Email = email;
            this.WebLink = webLink;
        }

        public EnesyCADCommandMethod(CommandInfo comInfo, CommandFlags flag)
        {
            // Interface member
            this.m_globalName = comInfo.GlobalName;
            this.m_groupName = null;
            this.m_localizedNameId = null;
            this.m_flags = flag;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;

            // Custom member
            this.Tag = comInfo.Tag;
            this.Description = comInfo.Description;
            this.Author = comInfo.Author;
            this.Email = comInfo.Email;
            this.WebLink = comInfo.WebLink;
        }

        public EnesyCADCommandMethod(string globalName,
                                    bool isTest,
                                    string description
            )
        {
            // Interface member
            this.m_globalName = globalName;
            this.m_groupName = null;
            this.m_localizedNameId = null;
            this.m_flags = CommandFlags.Modal;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;

            // Custom member
            this.Description = description;
            this.IsTest = isTest;
        }

        public EnesyCADCommandMethod(string groupName, string globalName, CommandFlags flags, string weblink)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.WebLink = weblink;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethod(string globalName, CommandFlags flags, string weblink)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.WebLink = weblink;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }
        #endregion

        #region Implement interface method
        public EnesyCADCommandMethod(string globalName)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.m_localizedNameId = null;
            this.m_flags = CommandFlags.Modal;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethod(string globalName, CommandFlags flags)
        {
            this.m_groupName = null;
            this.m_globalName = globalName;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethod(string groupName, string globalName, CommandFlags flags)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = null;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethod(string groupName, string globalName, string localizedNameId, CommandFlags flags)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethod(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = contextMenuExtensionType;
            this.m_helpFileName = null;
            this.m_helpTopic = null;
        }

        public EnesyCADCommandMethod(string groupName, string globalName, string localizedNameId, CommandFlags flags, string helpTopic)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = null;
            this.m_helpFileName = null;
            this.m_helpTopic = helpTopic;
        }

        public EnesyCADCommandMethod(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType, string helpFileName, string helpTopic)
        {
            this.m_groupName = groupName;
            this.m_globalName = globalName;
            this.m_localizedNameId = localizedNameId;
            this.m_flags = flags;
            this.m_contextMenuExtensionType = contextMenuExtensionType;
            this.m_helpFileName = helpFileName;
            this.m_helpTopic = helpTopic;
        }
        #endregion
    }
}