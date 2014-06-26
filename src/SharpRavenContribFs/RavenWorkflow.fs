namespace SharpRaven

[<AutoOpen>]
module RavenWorkflow =
    let delay f (client : RavenClient) =
        try
            f()
        with
        | ex -> client.CaptureException(ex) |> ignore
                reraise()

    [<Sealed>]
    type RavenBuilder internal (client : RavenClient) =
        member b.Delay f        = delay f client
        member b.Zero ()        = ()

    let raven client = new RavenBuilder(client)