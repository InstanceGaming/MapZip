using System;
using System.Collections.Generic;
using System.Text;
using static mapzip.LoggerEventArgs;

namespace mapzip
{
    public class LogService
    {
        private string m_LineSequence = Environment.NewLine;
        private ConsoleColor m_DefaultColor = ConsoleColor.Gray;

        public bool Debug { private get; set; }
        public bool Silent { private get; set; }

        internal ConsoleColor GetColorFromType(LogFlags flags)
        {
            if (flags.HasFlag(LogFlags.APP))
            {
                return ConsoleColor.DarkGray;
            }
            if (flags.HasFlag(LogFlags.DEBUG))
            {
                return ConsoleColor.DarkCyan;
            }
            if (flags.HasFlag(LogFlags.ERROR))
            {
                return ConsoleColor.Red;
            }
            if (flags.HasFlag(LogFlags.NOTICE))
            {
                return ConsoleColor.Yellow;
            }
            if (flags.HasFlag(LogFlags.IMPORANT))
            {
                return ConsoleColor.Magenta;
            }
            if (flags.HasFlag(LogFlags.INFO))
            {
                return m_DefaultColor;
            }
            return m_DefaultColor;
        }

        internal List<string> GetFlagNames(Enum flags)
        {
            List<string> names = new List<string>();
            foreach (Enum value in Enum.GetValues(flags.GetType()))
                if (flags.HasFlag(value))
                    names.Add(Enum.GetName(value.GetType(),value));
            return names;
        }

        internal string FormatMessage(LogFlags flags, string message, bool timestamp)
        {
            StringBuilder final = new StringBuilder();
            StringBuilder tags = new StringBuilder();
            foreach (string fname in GetFlagNames(flags))
            {
                tags.AppendFormat("[{0}]", fname);
            }
            final.AppendFormat("{0}{1}: {2}", timestamp ? String.Format("[{0}]",new DateTime().ToLocalTime().ToString()) : String.Empty, tags.ToString(), flags, message);
            return final.ToString();
        }

        internal void Write(LogPriority priority, ConsoleColor color, string text)
        {
            if (priority == LogPriority.BOTH || priority == LogPriority.CONSOLE)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ForegroundColor = m_DefaultColor;
            }
        }

        public virtual void OnLoggerLog(object sender, LoggerEventArgs e)
        {
            if (!Debug && e.Flags == LogFlags.DEBUG || Silent)
            {
                return;
            }
            StringBuilder final = new StringBuilder();
            final.AppendFormat("[{0}]: {1}", Enum.GetName(typeof(LogFlags), e.Flags), e.Message);
            if (Debug && e.Exception != null)
            {
                final.AppendLine(">> Debug Output (Debug=TRUE); Exception Details:");
                final.AppendFormat("--[SOURCE OBJ]: {0}", e.Exception.Source);
                final.AppendFormat("--[METHOD TARGET]: {0}", e.Exception.TargetSite);
                final.AppendFormat("--[MSG]: {0}{1}", e.Exception.Message);
                final.AppendFormat("--[STACKTRACE]: {0}[/STACKTRACE]", e.Exception.StackTrace);
                final.AppendLine("<<");
            }
            Write(e.Priority, GetColorFromType(e.Flags), final.ToString());
        }
    }
}
