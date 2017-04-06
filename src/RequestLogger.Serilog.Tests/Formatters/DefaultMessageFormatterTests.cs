using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RequestLogger.Serilog.Formatters;

namespace RequestLogger.Serilog.Tests.Formatters
{
    [TestFixture]
    [Parallelizable]
    public class DefaultMessageFormatterTests
    {
        [SetUp]
        public void SetUp()
        {
            _formatter = new DefaultMessageFormatter();
        }

        private IMessageFormatter _formatter;

        [Test]
        public void Format_ShouldReturn_FormattedMessage_With_MessageTemplate()
        {
            var requestData = new RequestData();
            var responseData = new ResponseData();

            var result = _formatter.Format(requestData, responseData);

            Assert.AreEqual(
                "{HttpMethod} {Url} {RequestHeader} {RequestContent} {StatusCode} {ReasonPhrase} {ResponseHeader} {ResponseContent}",
                result.MessageTemplate);
        }

        [Test]
        public void Format_ShouldReturn_FormattedMessage_With_Properties()
        {
            const string requestMessage = "Ping";
            const string responseMessage = "Pong";
            var requestData = new RequestData
            {
                HttpMethod = "GET",
                Url = new Uri("http://ekm.com"),
                Header = new Dictionary<string, string[]>
                {
                    {"Content-Type", new[] {"application/json"}}
                },
                Content = Encoding.UTF8.GetBytes(requestMessage)
            };
            var responseData = new ResponseData
            {
                StatusCode = 400,
                ReasonPhrase = "Bad Request",
                Header = new Dictionary<string, string[]>(),
                Content = Encoding.UTF8.GetBytes(responseMessage)
            };

            var result = _formatter.Format(requestData, responseData);

            Assert.AreEqual(8, result.Properties.Length);
            Assert.AreEqual(requestData.HttpMethod, result.Properties[0]);
            Assert.AreEqual(requestData.Url, result.Properties[1]);
            Assert.AreEqual(requestData.Header, result.Properties[2]);
            Assert.AreEqual(requestMessage, result.Properties[3]);
            Assert.AreEqual(responseData.StatusCode, result.Properties[4]);
            Assert.AreEqual(responseData.ReasonPhrase, result.Properties[5]);
            Assert.AreEqual(responseData.Header, result.Properties[6]);
            Assert.AreEqual(responseMessage, result.Properties[7]);
        }

        [Test]
        public void Format_WithException_ShouldReturn_FormattedMessage_With_MessageTemplate()
        {
            var requestData = new RequestData();
            var responseData = new ResponseData();
            var ex = new Exception();

            var result = _formatter.Format(requestData, responseData, ex);

            Assert.AreEqual(
                "{HttpMethod} {Url} {RequestHeader} {RequestContent} {StatusCode} {ReasonPhrase} {ResponseHeader} {ResponseContent} {errorSource} {errorMessage} {errorStackTrace}",
                result.MessageTemplate);
        }

        [Test]
        public void Format_WithException_ShouldReturn_FormattedMessage_With_Properties()
        {
            const string requestMessage = "Ping";
            const string responseMessage = "Pong";
            var requestData = new RequestData
            {
                HttpMethod = "GET",
                Url = new Uri("http://ekm.com"),
                Header = new Dictionary<string, string[]>
                {
                    {"Content-Type", new[] {"application/json"}}
                },
                Content = Encoding.UTF8.GetBytes(requestMessage)
            };
            var responseData = new ResponseData
            {
                StatusCode = 400,
                ReasonPhrase = "Bad Request",
                Header = new Dictionary<string, string[]>(),
                Content = Encoding.UTF8.GetBytes(responseMessage)
            };
            var ex = new Exception("Test Error") { Source = "Test" };

            var result = _formatter.Format(requestData, responseData, ex);

            Assert.AreEqual(11, result.Properties.Length);
            Assert.AreEqual(requestData.HttpMethod, result.Properties[0]);
            Assert.AreEqual(requestData.Url, result.Properties[1]);
            Assert.AreEqual(requestData.Header, result.Properties[2]);
            Assert.AreEqual(requestMessage, result.Properties[3]);
            Assert.AreEqual(responseData.StatusCode, result.Properties[4]);
            Assert.AreEqual(responseData.ReasonPhrase, result.Properties[5]);
            Assert.AreEqual(responseData.Header, result.Properties[6]);
            Assert.AreEqual(responseMessage, result.Properties[7]);
            Assert.AreEqual(ex.Source, result.Properties[8]);
            Assert.AreEqual(ex.Message, result.Properties[9]);
            Assert.AreEqual(ex.StackTrace, result.Properties[10]);
        }
    }
}
