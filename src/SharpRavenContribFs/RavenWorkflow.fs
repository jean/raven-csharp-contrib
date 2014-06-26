namespace SharpRaven

[<AutoOpen>]
module RavenWorkflow =
    [<Sealed>]
    type RavenBuilder internal (client : RavenClient) =
        member b.Delay f = 
            try
                f()
            with
            | ex -> client.CaptureException(ex) |> ignore
                    reraise()

        member b.Zero () = ()

    let raven client = new RavenBuilder(client)