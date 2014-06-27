namespace SharpRaven

open System
open System.Diagnostics

open SharpRaven.Data

[<AutoOpen>]
module LogTimeWorkflow =
    let inline (!>) (b : ^b) : ^a = (^a : (static member op_Implicit : ^b -> ^a) (b))

    [<Sealed>]
    type LogTimeBuilder internal (client    : RavenClient, 
                                  threshold : TimeSpan, 
                                  funcName  : string) =
        let stopwatch = new Stopwatch()

        member b.Delay f = 
            stopwatch.Start()
            let res = f()
            stopwatch.Stop()

            if (stopwatch.Elapsed > threshold) then
                let message : SentryMessage = !> (sprintf "[%s] took [%A] to execute!" funcName stopwatch.Elapsed)
                client.CaptureMessage(message, ErrorLevel.Warning) |> ignore

            res

        member b.Zero () = ()

    let logTime (client, threshold, funcName) = new LogTimeBuilder(client, threshold, funcName)