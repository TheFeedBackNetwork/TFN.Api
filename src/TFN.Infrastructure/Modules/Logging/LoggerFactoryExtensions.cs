using Microsoft.Extensions.Logging;
using TFN.Infrastructure.Modules.Logging.Email;

namespace TFN.Infrastructure.Modules.Logging
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddEmail(
            this ILoggerFactory factory,
            string recipient,
            string sender,
            string smtpUsername,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            string environmentName,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new EmailLoggerProvider(recipient, sender, smtpUsername, smtpPassword, smtpHost,
                smtpPort, environmentName, minimumLevel));
            return factory;
        }
    }
}