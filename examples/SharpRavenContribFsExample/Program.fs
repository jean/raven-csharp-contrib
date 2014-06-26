open System
open System.Threading

open SharpRaven

let client = new RavenClient("YOUR_DSN_HERE")

// you can use the 'raven' workflow to log any unhandled exceptions to Sentry as Error
let logToSentry () = raven(client) {
        printfn "A message should be logged to Sentry, pls check your Sentry console"
        failwith "Error from F#"
    }

// alternatively, try using the 'logTime' workflow to log slow functions to Sentry as Warning
let slowFunc () = logTime(client, TimeSpan.FromMilliseconds 1.0, "slowFunc") {
        Thread.Sleep(2)
    }

let wrapper f = try f() with | _ -> () 

[<EntryPoint>]
let main argv = 
    wrapper <| logToSentry
    do slowFunc()

    Console.ReadKey() |> ignore
    0 // return an integer exit code