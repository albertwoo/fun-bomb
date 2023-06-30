#r "nuget: Fun.Build, 0.4.0"
#r "nuget: Plotly.NET"

open System
open System.Collections.Generic
open Fun.Build
open Plotly.NET

type BombType =
    | CSHARP
    | RUST

let round = 5
let connections = [ 10; 20; 30; 50; 70; 100; 200; 300; 500 ]
let bombData = Map.ofSeq [ CSHARP, List<int>(); RUST, List<int>() ]

let parseRPS (str: string) =
    try
        let startStr = "RPS "
        let startIndex = str.IndexOf startStr + startStr.Length
        let endIndex = str.IndexOf("/s", startIndex)
        let targetStr = str.Substring(startIndex, endIndex - startIndex)
        Some(Int32.Parse targetStr)
    with _ ->
        None

let bombAndCollectData (ty: BombType) (commandWithConnection: int -> string) =
    stage "bomb and collect" {
        run (fun ctx ->
            async {
                for connection in connections do
                    let result = List<int>()
                    for i in 1..round do
                        printfn $"Connection {connection}, Round {i}"
                        match! ctx.RunCommandCaptureOutput(commandWithConnection connection) with
                        | Ok str -> parseRPS str |> Option.iter result.Add
                        | _ -> ()
                        do! Async.Sleep 10_000
                    bombData[ty].Add((Seq.sum result) / result.Count)
            }
        )
    }

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
            whenNot {
                cmdArg "--axum"
                cmdArg "--csharp"
            }
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
                bombAndCollectData CSHARP (sprintf "dotnet run -c Release -- http://127.0.0.1:3000/hello %d")
            }
            stage "fun-bomb-rs" {
                workingDir "fun-bomb-rs"
                bombAndCollectData RUST (sprintf "cargo run -r -q -- http://127.0.0.1:3000/hello %d")
            }
            run (fun _ ->
                let dateStr = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")
                let xAxis = connections |> List.map string

                let xAxisLayout = LayoutObjects.LinearAxis()
                xAxisLayout.SetValue("title", "Concurrent connections")

                [ Chart.Column(Seq.zip xAxis bombData[CSHARP]) |> Chart.withTraceInfo "csharp"
                  Chart.Column(Seq.zip xAxis bombData[RUST]) |> Chart.withTraceInfo "rust" ]
                |> Chart.combine
                |> Chart.withXAxis xAxisLayout
                |> Chart.saveHtml $"result-{dateStr}.html"
                raise (PipelineCancelledException "All bombing is finished, will cancel the pipeline.")
                ()
            )
        }
    }
    runIfOnlySpecified false
}

tryPrintPipelineCommandHelp ()
