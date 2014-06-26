open System

open SharpRaven
open SharpRaven.RavenWorkflow

let client = new RavenClient("YOUR_DSN_HERE")

let logToSentry () = raven(client) {
        printfn "A message should be logged to Sentry, pls check your Sentry console"
        failwith "Error from F#"
    }

let wrapper f = try f() with | _ -> () 

[<EntryPoint>]
let main argv = 
    wrapper <| logToSentry

    Console.ReadKey() |> ignore
    0 // return an integer exit code