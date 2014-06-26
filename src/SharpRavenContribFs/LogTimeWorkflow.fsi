namespace SharpRaven

open System

[<AutoOpen>]
module LogTimeWorkflow =
    [<Sealed>]
    type LogTimeBuilder = 
        member Delay : (unit -> 'a) -> 'a
        member Zero  : unit -> unit

    val logTime : RavenClient * TimeSpan * string -> LogTimeBuilder