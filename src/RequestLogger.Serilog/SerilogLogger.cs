using System;
using RequestLogger.Serilog.Formatters;
using Serilog;

namespace RequestLogger.Serilog
{
    public class SerilogLogger  : IRequestLogger
    {
        private readonly ILogger _logger;
        private readonly IMessageFormatter _formatter;

        public SerilogLogger(ILogger logger, IMessageFormatter formatter = null)
        {
            _logger = logger;
            _formatter = formatter ?? new DefaultMessageFormatter();
        }

        public void Log(RequestData requestData, ResponseData responseData)
        {
            var message = _formatter.Format(requestData, responseData) ?? new FormattedMessage();

            if (!ShouldSendMessage(message))
                return;

            _logger.Information(message.MessageTemplate, message.Properties);
        }

        public void LogError(RequestData requestData, ResponseData responseData, Exception ex)
        {
            var message = _formatter.Format(requestData, responseData, ex) ?? new FormattedMessage();

            if (!ShouldSendMessage(message))
                return;

            _logger.Error(message.MessageTemplate, message.Properties);
        }

        private static bool ShouldSendMessage(FormattedMessage message)
        {
            return !string.IsNullOrEmpty(message.MessageTemplate);
        }
    }
}
