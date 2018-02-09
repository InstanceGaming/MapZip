using Mono.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapzip
{
    class ArgumentParser
    {
        private bool m_Debug = false;
        private bool m_Silent = false;
        private bool m_Uninstall = false;
        private bool m_Help = false;
        private string[] m_Arguments = null;
        private List<string> m_Errors = new List<string>();

        private OptionSet m_Options = null;

        public bool Debug { get { return m_Debug; } }
        public bool Silent { get { return m_Silent; } }
        public bool Uninstall { get { return m_Uninstall; } }
        public bool Help { get { return m_Help; } }
        public List<string> Errors { get { return m_Errors; } }

        public ArgumentParser(string[] arguments)
        {
            m_Arguments = arguments;
            m_Options = new OptionSet() {
                { "s|silent",
                    "Silent operation mode. No command output, no confirmation lines.",
                    v => m_Silent = true},
                { "d|debug",
                    "Debug output mode. Use this mode when developing.",
                    v => m_Debug = true},
                { "u|uninstall",
                    "Uninstaller mode. Removes the associated map from users app data directory if found.",
                    v => m_Uninstall = true},
                { "h|help",
                    "Shows this help text.",
                    v => m_Help = true},
            };
        }

        public void Parse()
        {
            m_Errors = m_Options.Parse(m_Arguments);
        }

        public string GetHelpDescriptions()
        {
            StringBuilder final = new StringBuilder();
            final.AppendLine("Argument Help and Descrptions:");
            foreach (Option o in m_Options)
            {
                final.AppendFormat(" - {0}: {1}{2}", o.Prototype, o.Description, Environment.NewLine);
            }
            return final.ToString();
        }
    }
}
