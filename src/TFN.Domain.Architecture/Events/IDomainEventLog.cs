using System;

namespace TFN.Domain.Architecture.Events
{
    public interface IDomainEventLog
    {
        void LogFatal(string context, string message);

        void LogFatal(string context, string message, Exception exception);
    }
}