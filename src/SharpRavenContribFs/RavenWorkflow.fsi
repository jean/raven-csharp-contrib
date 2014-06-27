namespace SharpRaven

[<AutoOpen>]
module RavenWorkflow =
    [<Sealed>]
    type RavenBuilder = 
        member Delay    : (unit -> 'a) -> 'a
        member Zero     : unit -> unit

    val raven : RavenClient -> RavenBuilder