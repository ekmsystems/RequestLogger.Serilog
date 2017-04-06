using System;
using System.Text;

namespace RequestLogger.Serilog.Formatters
{
    public class DefaultMessageFormatter : IMessageFormatter
    {
        public FormattedMessage Format(RequestData requestData, ResponseData responseData)
        {
            return new FormattedMessage
            {
                MessageTemplate =
                    "{HttpMethod} {Url} {RequestHeader} {RequestContent} {StatusCode} {ReasonPhrase} {ResponseHeader} {ResponseContent}",
                Properties = new object[]
                {
                    requestData.HttpMethod,
                    requestData.Url,
                    requestData.Header,
                    ReadBytes(requestData.Content),
                    responseData.StatusCode,
                    responseData.ReasonPhrase,
                    responseData.Header,
                    ReadBytes(responseData.Content)
                }
            };
        }

        public FormattedMessage Format(RequestData requestData, ResponseData responseData, Exception ex)
        {
            return new FormattedMessage
            {
                MessageTemplate =
                    "{HttpMethod} {Url} {RequestHeader} {RequestContent} {StatusCode} {ReasonPhrase} {ResponseHeader} {ResponseContent} {errorSource} {errorMessage} {errorStackTrace}",
                Properties = new object[]
                {
                    requestData.HttpMethod,
                    requestData.Url,
                    requestData.Header,
                    ReadBytes(requestData.Content),
                    responseData.StatusCode,
                    responseData.ReasonPhrase,
                    responseData.Header,
                    ReadBytes(responseData.Content),
                    ex.Source,
                    ex.Message,
                    ex.StackTrace
                }
            };
        }

        private static string ReadBytes(byte[] content)
        {
            return Encoding.UTF8.GetString(content ?? new byte[] {});
        }
    }
}
