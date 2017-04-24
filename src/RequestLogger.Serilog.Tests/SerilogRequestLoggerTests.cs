using System;
using Moq;
using NUnit.Framework;
using RequestLogger.Serilog.Formatters;
using Serilog;

namespace RequestLogger.Serilog.Tests
{
    [TestFixture]
    [Parallelizable]
    public class SerilogRequestLoggerTests
    {
        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger>();
            _messageFormatter = new Mock<IMessageFormatter>();
            _requestLogger = new SerilogRequestLogger(_logger.Object, _messageFormatter.Object);
        }

        private Mock<ILogger> _logger;
        private Mock<IMessageFormatter> _messageFormatter;
        private IRequestLogger _requestLogger;

        [Test]
        public void Log_ShouldCall_Logger_Information()
        {
            var requestData = new RequestData();
            var responseData = new ResponseData();
            var formattedMessage = new FormattedMessage {MessageTemplate = "1"};

            _messageFormatter
                .Setup(x => x.Format(requestData, responseData))
                .Returns(formattedMessage);

            _requestLogger.Log(requestData, responseData);

            _logger.Verify(x => x.Information(formattedMessage.MessageTemplate, formattedMessage.Properties), Times.Once);
        }

        [Test]
        public void Log_When_FormattedMessageHasNoContent_ShouldNotCall_Logger_Information()
        {
            var requestData = new RequestData();
            var responseData = new ResponseData();

            _messageFormatter
                .Setup(x => x.Format(requestData, responseData))
                .Returns(new FormattedMessage());

            _requestLogger.Log(requestData, responseData);

            _logger.Verify(x => x.Information(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public void LogError_ShouldCall_Logger_Error()
        {
            var requestData = new RequestData();
            var responseData = new ResponseData();
            var ex = new Exception();
            var formattedMessage = new FormattedMessage {MessageTemplate = "1"};

            _messageFormatter
                .Setup(x => x.Format(requestData, responseData, ex))
                .Returns(formattedMessage);

            _requestLogger.LogError(requestData, responseData, ex);

            _logger.Verify(x => x.Error(formattedMessage.MessageTemplate, formattedMessage.Properties), Times.Once);
        }

        [Test]
        public void LogError_When_FormattedMessageHasNoContent_ShouldNotCall_Logger_Error()
        {
            var requestData = new RequestData();
            var responseData = new ResponseData();
            var ex = new Exception();

            _messageFormatter
                .Setup(x => x.Format(requestData, responseData))
                .Returns(new FormattedMessage());

            _requestLogger.LogError(requestData, responseData, ex);

            _logger.Verify(x => x.Error(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }
    }
}
