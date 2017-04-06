using System;

namespace RequestLogger.Serilog.Formatters
{
    public interface IMessageFormatter
    {
        FormattedMessage Format(RequestData requestData, ResponseData responseData);
        FormattedMessage Format(RequestData requestData, ResponseData responseData, Exception ex);
    }
}
