#r "nuget: Fun.Build"

open Fun.Build

let round = 5
let connections = [10;20;30;50;70;100;200;300;500]

pipeline "bomb" {
    description "start server (actix-web by default) and test bomb"
    stage "build axum server" {
        workingDir "hello-axum"
        run "cargo build -r"
    }
    stage "build actix server" {
        workingDir "hello-actix"
        run "cargo build -r"
    }
    stage "build csharp server" {
        workingDir "hello-csharp"
        run "dotnet build -c Release"
    }
    stage "start bomb" {
        paralle
        stage "run axum server" {
            whenCmdArg "--axum" "" "use axum as the backend server"
            workingDir "hello-axum"
            run "cargo run -r"
        }
        stage "run csharp server" {
            whenCmdArg "--csharp" "" "use asp.net core as the backend server"
            workingDir "hello-csharp"
            run "dotnet run -c Release"
        }
        stage "run actix server" {
            whenNot { cmdArg "--axum"; cmdArg "--csharp" }
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
            run (fun _ -> raise (PipelineCancelledException "All bombing is finished, will cancel the pipeline."); ())
        }
    }
    runIfOnlySpecified false
}

tryPrintPipelineCommandHelp()
