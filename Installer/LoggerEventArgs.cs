using System;
namespace mapzip
{
    public partial class LoggerEventArgs : EventArgs
    {
        public LogPriority Priority { get; set; }
        public LogFlags Flags { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
