rust 学习记 - 创建一个简单的 http 压测命令行

## rust VS csharp

环境要求：
- dotnet sdk 7
- rust

运行一下命令以获取结果

```bash
dotnet fsi ./build.fsx
```

### 结果

|rust|csharp|
|----|------|
|Connection 5, Round 1 (rust)|Connection 5, Round 1 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 5|dotnet run -c Release -- http://127.0.0.1:3000/hello 5|
|Actual time 10.00s, RPS 29564/s|Actual time 10.00s, RPS 28132/s.|
|||
|Connection 5, Round 2 (rust)|Connection 5, Round 2 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 5|dotnet run -c Release -- http://127.0.0.1:3000/hello 5|
|Actual time 10.00s, RPS 30361/s|Actual time 10.01s, RPS 37819/s.|
|||
|Connection 5, Round 3 (rust)|Connection 5, Round 3 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 5|dotnet run -c Release -- http://127.0.0.1:3000/hello 5|
|Actual time 10.00s, RPS 30092/s|Actual time 10.01s, RPS 36971/s.|
|||
|Connection 5, Round 4 (rust)|Connection 5, Round 4 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 5|dotnet run -c Release -- http://127.0.0.1:3000/hello 5|
|Actual time 10.00s, RPS 29030/s|Actual time 10.01s, RPS 37832/s.|
|||
|Connection 5, Round 5 (rust)|Connection 5, Round 5 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 5|dotnet run -c Release -- http://127.0.0.1:3000/hello 5|
|Actual time 10.00s, RPS 30595/s|Actual time 10.01s, RPS 37615/s.|
|||
|Connection 10, Round 1 (rust)|Connection 10, Round 1 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 10|dotnet run -c Release -- http://127.0.0.1:3000/hello 10|
|Actual time 10.00s, RPS 40264/s|Actual time 10.01s, RPS 55290/s.|
|||
|Connection 10, Round 2 (rust)|Connection 10, Round 2 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 10|dotnet run -c Release -- http://127.0.0.1:3000/hello 10|
|Actual time 10.00s, RPS 41621/s|Actual time 10.01s, RPS 56793/s.|
|||
|Connection 10, Round 3 (rust)|Connection 10, Round 3 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 10|dotnet run -c Release -- http://127.0.0.1:3000/hello 10|
|Actual time 10.00s, RPS 42158/s|Actual time 10.01s, RPS 56506/s.|
|||
|Connection 10, Round 4 (rust)|Connection 10, Round 4 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 10|dotnet run -c Release -- http://127.0.0.1:3000/hello 10|
|Actual time 10.00s, RPS 43071/s|Actual time 10.01s, RPS 56307/s.|
|||
|Connection 10, Round 5 (rust)|Connection 10, Round 5 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 10|dotnet run -c Release -- http://127.0.0.1:3000/hello 10|
|Actual time 10.00s, RPS 42018/s|Actual time 10.02s, RPS 56338/s.|
|||
|Connection 20, Round 1 (rust)|Connection 20, Round 1 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 20|dotnet run -c Release -- http://127.0.0.1:3000/hello 20|
|Actual time 10.00s, RPS 45038/s|Actual time 10.00s, RPS 60172/s.|
|||
|Connection 20, Round 2 (rust)|Connection 20, Round 2 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 20|dotnet run -c Release -- http://127.0.0.1:3000/hello 20|
|Actual time 10.00s, RPS 47110/s|Actual time 10.01s, RPS 59223/s.|
|||
|Connection 20, Round 3 (rust)|Connection 20, Round 3 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 20|dotnet run -c Release -- http://127.0.0.1:3000/hello 20|
|Actual time 10.00s, RPS 43045/s|Actual time 10.00s, RPS 59957/s.|
|||
|Connection 20, Round 4 (rust)|Connection 20, Round 4 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 20|dotnet run -c Release -- http://127.0.0.1:3000/hello 20|
|Actual time 10.00s, RPS 38728/s|Actual time 10.00s, RPS 60470/s.|
|||
|Connection 20, Round 5 (rust)|Connection 20, Round 5 (csharp)|
|cargo run -r -q -- http://127.0.0.1:3000/hello 20|dotnet run -c Release -- http://127.0.0.1:3000/hello 20|
|Actual time 10.00s, RPS 46073/s|Actual time 10.01s, RPS 60347/s.|
