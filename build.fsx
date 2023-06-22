#r "nuget: Fun.Build"

open Fun.Build

let round = 5
let connections = [5;10;20]

pipeline "bomb" {
    description "start server and test bomb"
    stage "run server" {
        workingDir "hello-axum"
        run "cargo build -r"
        run (fun ctx -> async {
            ctx.RunCommand("cargo run -r") |> ignore
            do! Async.Sleep 5000
            printfn "hello axum server should be up"
            return Ok()
        })
    }
    stage "warm up" {
        stage "fun-bomb-cs" {
            workingDir "fun-bomb-cs"
            run "dotnet run -c Release -- http://127.0.0.1:3000/hello 10"
        }
        stage "fun-bomb-rs" {
            workingDir "fun-bomb-rs"
            run "cargo run -r -q -- http://127.0.0.1:3000/hello 10"
        }
    }
    stage "fun-bomb-cs" {
        workingDir "fun-bomb-cs"
        run (fun ctx -> async {
            for connection in connections do
                for i in 1..round do
                    printfn $"Connection {connection}, Round {i} (csharp)"
                    let! _ = ctx.RunCommand($"dotnet run -c Release -- http://127.0.0.1:3000/hello {connection}")
                    do! Async.Sleep 10_000
        })
    }
    stage "fun-bomb-rs" {
        workingDir "fun-bomb-rs"
        run (fun ctx -> async {
            for connection in connections do
                for i in 1..round do
                    printfn $"Connection {connection}, Round {i} (rust)"
                    let! _ = ctx.RunCommand($"cargo run -r -q -- http://127.0.0.1:3000/hello {connection}")
                    do! Async.Sleep 10_000
        })
    }
    runIfOnlySpecified false
}

tryPrintPipelineCommandHelp()
