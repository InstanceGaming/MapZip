using System;
using static mapzip.LoggerEventArgs;

namespace mapzip
{
    public class LoggableAction
    {
        public EventHandler<LoggerEventArgs> Output;

        public virtual void OnLogAction(LogPriority priority, LogFlags type, string message)
        {
            Output?.Invoke(this, new LoggerEventArgs() { Priority = priority, Flags = type, Message = message, Exception = null});
        }
    }
}
