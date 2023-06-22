#r "nuget: Fun.Build"

open Fun.Build

let round = 5
let connections = [5;10;20]

pipeline "bomb" {
    description "start server and test bomb"
    stage "build axum server" {
        workingDir "hello-axum"
        run "cargo build -r"
    }
    stage "build actix server" {
        workingDir "hello-actix"
        run "cargo build -r"
    }
    stage "start bomb" {
        paralle
        stage "run axum server" {
            whenCmdArg "--axum"
            workingDir "hello-axum"
            run "cargo run -r"
        }
        stage "run actix server" {
            whenNot { cmdArg "--axum" }
            workingDir "hello-actix"
            run "cargo run -r"
        }
        stage "run load tools" {
            run (Async.Sleep 5000)
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
        }
    }
    runIfOnlySpecified false
}

tryPrintPipelineCommandHelp()
