raven-csharp-contrib
====================

Extensions for the C# client for Sentry (https://www.getsentry.com) for C# and F#.

## C# ##

Two [PostSharp](http://www.postsharp.net/) powered attributes are provided:

- **RavenLogException** - this attribute captures any unhandled exceptions from your methods and logs them as errors to your *Sentry* account.
- **RavenLogExecutionTime** - this attribute captures the execution time of your methods and if they exceed your specified threshold then it logs them as warnings to your *Sentry* account. 

(Note: these attributes do not use *PostSharp* professional edition features, so you don't need to go out and pay for a professional license)

Both attributes can be applied at `assembly`, `class` or `method` level, so you can capture and log any unhandled exceptions to *Sentry* in your assembly with one line of code! If you rather use a custom appender for log4net instead, there are a couple of open source alternatives floating around, such as [this one](https://github.com/themotleyfool/RavenLog4NetAppender).

See [**Example**](examples/SharpRavenContribExample/Program.cs).

## F# ##

For all you F# lovers we have two workflows instead:

-  **raven** - this workflow captures any unhandled exceptions from within the body of the workflow and logs them as errors to your *Sentry* account.
- **logTime** - this workflow captures the execution time of the computation within the body of the workflow and if it exceeds your specified threshold then it logs it as a warning to your *Sentry* account. 

See [**Example**](examples/SharpRavenContribFsExample/Program.fs).

## Nuget ##

The C# and F# extensions are provided through 2 separate Nuget packages.

To get the C# custom attributes:
[![Nuget package](https://raw.githubusercontent.com/theburningmonk/raven-csharp-contrib/develop/nuget/SharpRaven-Contrib%20logo.png)](https://www.nuget.org/packages/SharpRaven-Contrib/)

To get the F# workflows:
[![Nuget package](https://raw.githubusercontent.com/theburningmonk/raven-csharp-contrib/develop/nuget/SharpRaven-Contribfs%20logo.png)](https://www.nuget.org/packages/SharpRaven-Contribfs/)


## How to Build ##

The preferred way to build the solution is with the provided build script using [Fake](https://github.com/fsharp/FAKE), simply double click `**build.cmd**` to execute.

To build manually, open the `SharpRavenContrib.sln` solution file at the root folder and build it in Visual Studio (don't forget to enable Nuget restore).