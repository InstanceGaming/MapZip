using System;
namespace mapzip
{
    public partial class LoggerEventArgs : EventArgs
    {
        public enum LogFlags
        {
            INFO,
            IMPORANT,
            NOTICE,
            ERROR,
            DEBUG,
            INSTALLER,
            IO,
            CONFIGURATION,
            ARGUMENTS,
            APP
        }
    }
}
