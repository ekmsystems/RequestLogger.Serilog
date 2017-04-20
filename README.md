# RequestLogger.Serilog

[![Build status](https://ci.appveyor.com/api/projects/status/iu6dojwpm61rmh8j/branch/master?svg=true)](https://ci.appveyor.com/project/mrstebo/requestlogger-serilog-v1nn5/branch/master)
[![MyGet Prerelease](https://img.shields.io/myget/ekmsystems/v/RequestLogger.Serilog.svg?label=MyGet_Prerelease)](https://www.myget.org/feed/ekmsystems/package/nuget/RequestLogger.Serilog)
[![NuGet](https://img.shields.io/nuget/v/RequestLogger.Serilog.svg)](https://www.nuget.org/packages/RequestLogger.Serilog/)

RequestLogger that can be used with Serilog.

RequestLogger.Serilog is available via [NuGet](https://www.nuget.org/packages/RequestLogger.Serilog/):

```powershell
Install-Package RequestLogger.Serilog
```

## Quick Start

There are some example projects in this repository that will show you how to use this package:

- [Owin](src/Examples/OwinExample)
- [ASP.NET](src/Examples/AspNetExample)

## Custom Message Format

If you want to customize the message that gets sent to the serilog logger, then you need to create a custom `IMessageFormatter`. This package comes with a [default formatter](src/RequestLogger.Serilog/Formatters/DefaultMessageFormatter.cs)
