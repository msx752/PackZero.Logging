[![Nuget](https://img.shields.io/badge/package-PackZero.Logging-brightgreen.svg?maxAge=259200)](https://www.nuget.org/packages/PackZero.Logging)
[![CodeQL](https://github.com/msx752/PackZero.Logging/actions/workflows/codeql.yml/badge.svg?branch=main)](https://github.com/msx752/PackZero.Logging/actions/workflows/codeql.yml)
[![MIT](https://img.shields.io/badge/License-MIT-blue.svg?maxAge=259200)](https://github.com/msx752/PackZero.Logging/blob/main/LICENSE.md)

# PackZero.Logging
Serilog extension for the exception handling


# How to Use
``` c#
using PackZero.Logging;
```
exception format is `{logType}: {Message} {StackTrace} {Source} {InnerMessage} {InnerStackTrace} {InnerSource} {ExceptionNote}`
``` c#
public class AppHostedService : IHostedService
{
    private readonly ILogger<AppHostedService> logger;

    public AppHostedService(ILogger<AppHostedService> logger)
    {
        this.logger = logger;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            //actions
        }
        catch (Exception e)
        {
            logger.Exception(e);
        }
    }
}
```

# Features
 - IHostBuilder extension named `UseAppZeroLogging()` extends `ConfigureLogging()` and overrides Serilog LoggerConfiguration using internally `UseSerilog()`
 - Application Name and Environment information will be added as property on Serilo