namespace PackZero.Logging;

public static class LoggingExtensions
{
    public static IHostBuilder UseAppZeroLogging(this IHostBuilder builder, Action<LoggerConfiguration> actionLoggerConfiguration, Action<HostBuilderContext, ILoggingBuilder> configureLogging)
    {
        builder.ConfigureLogging((hostBuilderContext, loggingBuilder) =>
        {
            loggingBuilder
                .ClearProviders()
                .AddSerilog()
                ;

            configureLogging?.Invoke(hostBuilderContext, loggingBuilder);
        });

        builder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .WriteTo.Console()
                .MinimumLevel.Information()
                .ReadFrom.Configuration(hostBuilderContext.Configuration)
                .Enrich.WithProperty("Application", hostBuilderContext.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("Environment", hostBuilderContext.HostingEnvironment)
            ;
            actionLoggerConfiguration?.Invoke(loggerConfiguration);
        });
        return builder;
    }

    public static IHostBuilder UseAppZeroLogging(this IHostBuilder builder, Action<LoggerConfiguration> actionLoggerConfiguration)
    {
        return builder.UseAppZeroLogging(actionLoggerConfiguration, null);
    }

    public static IHostBuilder UseAppZeroLogging(this IHostBuilder builder, Action<HostBuilderContext, ILoggingBuilder> configureLogging)
    {
        return builder.UseAppZeroLogging(null, configureLogging);
    }

    public static IHostBuilder UseAppZeroLogging(this IHostBuilder builder)
    {
        return builder.UseAppZeroLogging(null, null);
    }
}