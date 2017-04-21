# RequestLogger.Serilog

RequestLogger that can be used with Serilog.

[![Build status](https://ci.appveyor.com/api/projects/status/6xkttukd6q4qoy9c/branch/master?svg=true)](https://ci.appveyor.com/project/mrstebo/requestlogger-serilog/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/ekmsystems/RequestLogger.Serilog/badge.svg?branch=master)](https://coveralls.io/github/ekmsystems/RequestLogger.Serilog?branch=master)
[![NuGet](https://img.shields.io/nuget/v/RequestLogger.Serilog.svg)](https://www.nuget.org/packages/RequestLogger.Serilog/)

RequestLogger.Serilog is available via [NuGet](https://www.nuget.org/packages/RequestLogger.Serilog/):

```powershell
Install-Package RequestLogger.Serilog
```

## Quick Start

There are some example projects in this repository that will show you how to use this package:

- [ASP.NET](src/Examples/AspNetExample)
- [Owin](src/Examples/OwinExample)

## Custom Message Format

If you want to customize the message that gets sent to the serilog logger, then you need to create a custom `IMessageFormatter`. This package comes with a [default formatter](src/RequestLogger.Serilog/Formatters/DefaultMessageFormatter.cs)
