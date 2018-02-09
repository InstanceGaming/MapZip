using System;
namespace mapzip
{
    public partial class LoggerEventArgs : EventArgs
    {
        public enum LogPriority
        {
            BOTH,
            CONSOLE,
            LOGFILE
        }
    }
}
