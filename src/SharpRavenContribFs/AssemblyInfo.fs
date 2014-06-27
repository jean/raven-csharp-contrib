namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("raven-csharp-contrib")>]
[<assembly: AssemblyProductAttribute("raven-csharp-contrib")>]
[<assembly: AssemblyDescriptionAttribute("Extensions to C# client for Sentry")>]
[<assembly: AssemblyVersionAttribute("0.2.0")>]
[<assembly: AssemblyFileVersionAttribute("0.2.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.0"
