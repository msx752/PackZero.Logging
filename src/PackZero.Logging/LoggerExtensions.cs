namespace PackZero.Logging;

public static class LoggerExtensions
{
    public static void Exception(this Serilog.ILogger logger
        , string exceptionMessage
        , Serilog.Events.LogEventLevel logLevel = Serilog.Events.LogEventLevel.Error
        )
    {
        var format = exceptionMessage;
        logger.Write(logLevel, format);
    }

    public static void Exception(this Serilog.ILogger logger
        , Exception exception
        , Serilog.Events.LogEventLevel logLevel = Serilog.Events.LogEventLevel.Error
        , string exceptionNote = null
        , string logType = "AppException"
        )
    {
        string LogType = logType;
        _Exception(exception, out string message, out string stackTrace, out string source, out string innerMessage, out string innerStackTrace, out string innerSource, out string format);
        logger.Write(logLevel, format, LogType, message, stackTrace, source, innerMessage, innerStackTrace, innerSource, exceptionNote);
    }

    public static void Exception(this Microsoft.Extensions.Logging.ILogger logger
        , string exceptionMessage
        , LogLevel logLevel = LogLevel.Error
        )
    {
        var format = exceptionMessage;
        logger.Log(logLevel, format);
    }

    public static void Exception(this Microsoft.Extensions.Logging.ILogger logger
        , Exception exception
        , LogLevel logLevel = LogLevel.Error
        , string exceptionNote = null
        , string logType = "AppException"
        )
    {
        string LogType = logType;
        _Exception(exception, out string message, out string stackTrace, out string source, out string innerMessage, out string innerStackTrace, out string innerSource, out string format);
        logger.Log(logLevel, format, LogType, message, stackTrace, source, innerMessage, innerStackTrace, innerSource, exceptionNote);
    }

    private static void _Exception(
        Exception exception
        , out string message
        , out string stackTrace
        , out string source
        , out string innerMessage
        , out string innerStackTrace
        , out string innerSource
        , out string format
        )
    {
        format = "{lt}: {Message} {StackTrace} {Source} {InnerMessage} {InnerStackTrace} {InnerSource} {ExceptionNote}";
        message = exception.Message;
        stackTrace = exception?.StackTrace ?? ".";
        source = exception?.Source ?? ".";
        innerMessage = (exception?.InnerException?.Message) ?? ".";
        innerStackTrace = (exception?.InnerException?.StackTrace) ?? ".";
        innerSource = (exception?.InnerException?.Source) ?? ".";
    }
}