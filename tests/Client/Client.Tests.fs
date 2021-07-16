module Client.Tests

open Fable.Mocha

open Index
open Shared

let client = testList "Client" [
    testCase "Added todo" <| fun _ ->
        //let newTodo = { ServerResponse = None; ValidationError = None; Text = "WC1 2PF"; ServerState = ServerState.Idle }
        let model, _ = init ()

        let model, _ = update (TextChanged (0, "1011")) model

        Expect.equal 1 model.Destinations.Length "There should be 1 todo"
        Expect.equal "1011" model.Destinations.[0].Text "text should be correct"
]

let all =
    testList "All"
        [
#if FABLE_COMPILER // This preprocessor directive makes editor happy
            Shared.Tests.shared
#endif
            client
        ]

[<EntryPoint>]
let main _ = Mocha.runTests all