using Owin;
using RequestLogger.Owin;
using RequestLogger.Serilog;
using Serilog;

namespace OwinExample
{
    public partial class Startup
    {
        private static void ConfigureLogging(IAppBuilder app)
        {
            var logger = new LoggerConfiguration().WriteTo.Trace().CreateLogger();
            var requestLogger = new SerilogRequestLogger(logger);

            app.UseRequestLoggerMiddleware(requestLogger);
        }
    }
}