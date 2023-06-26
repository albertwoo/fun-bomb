rust 学习记 - 创建一个简单的 http 压测命令行

## rust VS csharp

环境要求：
- dotnet sdk 7
- rust

```bash
查看可用测试参数
dotnet fsi .\build.fsx -- -h

Options(collected from stages):
  --axum                          use axum as the backend server
  --csharp                        use asp.net core as the backend server
```

运行一下命令以可获取结果

```bash
dotnet fsi ./build.fsx
```

### actix-web 为服务端


> 12th Gen Intel(R) Core(TM) i7-12700H   2.30 GHz   32.0 GB Windows11 WSL ubuntu 20

|rust|csharp|
|----|------|
|Connection 10, Round 1 (rust)|Connection 10, Round 1 (csharp)|
|Actual time 10.00s, RPS 85573/s|Actual time 10.01s, RPS 88985/s.|
|Actual time 10.00s, RPS 92941/s|Actual time 10.01s, RPS 88910/s.|
|Actual time 10.00s, RPS 85349/s|Actual time 10.00s, RPS 100648/s.|
|Actual time 10.00s, RPS 95909/s|Actual time 10.01s, RPS 97259/s.|
|Actual time 10.00s, RPS 94112/s|Actual time 10.01s, RPS 97003/s.|
|||
|Connection 20, Round 1 (rust)|Connection 20, Round 1 (csharp)|
|Actual time 10.00s, RPS 182357/s|Actual time 10.00s, RPS 171261/s.|
|Actual time 10.00s, RPS 180520/s|Actual time 10.01s, RPS 184691/s.|
|Actual time 10.00s, RPS 183225/s|Actual time 10.01s, RPS 181769/s.|
|Actual time 10.00s, RPS 180303/s|Actual time 10.01s, RPS 187477/s.|
|Actual time 10.00s, RPS 183589/s|Actual time 10.00s, RPS 201372/s.|
|||
|Connection 30, Round 1 (rust)|Connection 30, Round 1 (csharp)|
|Actual time 10.00s, RPS 208267/s|Actual time 10.00s, RPS 253877/s.|
|Actual time 10.00s, RPS 212352/s|Actual time 10.01s, RPS 251552/s.|
|Actual time 10.00s, RPS 208786/s|Actual time 10.01s, RPS 205041/s.|
|Actual time 10.00s, RPS 204985/s|Actual time 10.01s, RPS 251806/s.|
|Actual time 10.00s, RPS 224859/s|Actual time 10.01s, RPS 257656/s.|
|||
|Connection 50, Round 1 (rust)|Connection 50, Round 1 (csharp)|
|Actual time 10.00s, RPS 247100/s|Actual time 10.01s, RPS 201637/s.|
|Actual time 10.00s, RPS 246518/s|Actual time 10.01s, RPS 280216/s.|
|Actual time 10.00s, RPS 246809/s|Actual time 10.01s, RPS 313129/s.|
|Actual time 10.00s, RPS 237625/s|Actual time 10.00s, RPS 276330/s.|
|Actual time 10.00s, RPS 247118/s|Actual time 10.01s, RPS 245079/s.|
|||
|Connection 70, Round 1 (rust)|Connection 70, Round 1 (csharp)|
|Actual time 10.00s, RPS 275318/s|Actual time 10.01s, RPS 279826/s.|
|Actual time 10.00s, RPS 271087/s|Actual time 10.01s, RPS 364807/s.|
|Actual time 10.00s, RPS 259446/s|Actual time 10.01s, RPS 341391/s.|
|Actual time 10.00s, RPS 282300/s|Actual time 10.01s, RPS 411538/s.|
|Actual time 10.00s, RPS 268135/s|Actual time 10.01s, RPS 418216/s.|
|||
|Connection 100, Round 1 (rust)|Connection 100, Round 1 (csharp)|
|Actual time 10.00s, RPS 294125/s|Actual time 10.00s, RPS 426371/s.|
|Actual time 10.00s, RPS 305736/s|Actual time 10.01s, RPS 428612/s.|
|Actual time 10.00s, RPS 313183/s|Actual time 10.00s, RPS 447875/s.|
|Actual time 10.00s, RPS 293870/s|Actual time 10.01s, RPS 456444/s.|
|Actual time 10.00s, RPS 307381/s|Actual time 10.01s, RPS 463084/s.|
|||
|Connection 200, Round 1 (rust)|Connection 200, Round 1 (csharp)|
|Actual time 10.00s, RPS 338148/s|Actual time 10.00s, RPS 439548/s.|
|Actual time 10.00s, RPS 328346/s|Actual time 10.01s, RPS 460706/s.|
|Actual time 10.00s, RPS 322443/s|Actual time 10.01s, RPS 437305/s.|
|Actual time 10.00s, RPS 318359/s|Actual time 10.01s, RPS 472216/s.|
|Actual time 10.00s, RPS 310657/s|Actual time 10.01s, RPS 443494/s.|
|||
|Connection 300, Round 1 (rust)|Connection 300, Round 1 (csharp)|
|Actual time 10.00s, RPS 343662/s|Actual time 10.00s, RPS 437967/s.|
|Actual time 10.00s, RPS 341992/s|Actual time 10.00s, RPS 498864/s.|
|Actual time 10.00s, RPS 330047/s|Actual time 10.01s, RPS 422319/s.|
|Actual time 10.00s, RPS 342497/s|Actual time 10.01s, RPS 482154/s.|
|Actual time 10.00s, RPS 347933/s|Actual time 10.01s, RPS 475552/s.|
|||
|Connection 500, Round 1 (rust)|Connection 500, Round 1 (csharp)|
|Actual time 10.01s, RPS 330649/s|Actual time 10.00s, RPS 475345/s.|
|Actual time 10.00s, RPS 352196/s|Actual time 10.01s, RPS 407393/s.|
|Actual time 10.00s, RPS 339571/s|Actual time 10.00s, RPS 370039/s.|
|Actual time 10.00s, RPS 304571/s|Actual time 10.00s, RPS 348164/s.|
|Actual time 10.00s, RPS 338826/s|Actual time 10.01s, RPS 451372/s.|

> Intel(R) Core(TM) i7-1065G7 CPU @ 1.30GHz   1.50 GHz  32GB Windows11

|rust|csharp|
|----|------|
|Connection 5, Round 1 (rust)|Connection 5, Round 1 (csharp)|
|Actual time 10.00s, RPS 29564/s|Actual time 10.00s, RPS 28132/s.|
|Actual time 10.00s, RPS 30361/s|Actual time 10.01s, RPS 37819/s.|
|Actual time 10.00s, RPS 30092/s|Actual time 10.01s, RPS 36971/s.|
|Actual time 10.00s, RPS 29030/s|Actual time 10.01s, RPS 37832/s.|
|Actual time 10.00s, RPS 30595/s|Actual time 10.01s, RPS 37615/s.|
|||
|Connection 10, Round 1 (rust)|Connection 10, Round 1 (csharp)|
|Actual time 10.00s, RPS 40264/s|Actual time 10.01s, RPS 55290/s.|
|Actual time 10.00s, RPS 41621/s|Actual time 10.01s, RPS 56793/s.|
|Actual time 10.00s, RPS 42158/s|Actual time 10.01s, RPS 56506/s.|
|Actual time 10.00s, RPS 43071/s|Actual time 10.01s, RPS 56307/s.|
|Actual time 10.00s, RPS 42018/s|Actual time 10.02s, RPS 56338/s.|
|||
|Connection 20, Round 1 (rust)|Connection 20, Round 1 (csharp)|
|Actual time 10.00s, RPS 45038/s|Actual time 10.00s, RPS 60172/s.|
|Actual time 10.00s, RPS 47110/s|Actual time 10.01s, RPS 59223/s.|
|Actual time 10.00s, RPS 43045/s|Actual time 10.00s, RPS 59957/s.|
|Actual time 10.00s, RPS 38728/s|Actual time 10.00s, RPS 60470/s.|
|Actual time 10.00s, RPS 46073/s|Actual time 10.01s, RPS 60347/s.|

### asp.net core 为服务端

|rust|csharp|
|----|------|
|Connection 5, Round 1 (rust)|Connection 5, Round 1 (csharp)|
|Actual time 10.00s, RPS 30407/s|Actual time 10.00s, RPS 25049/s.|
|Actual time 10.00s, RPS 29453/s|Actual time 10.01s, RPS 33032/s.|
|Actual time 10.00s, RPS 30117/s|Actual time 10.01s, RPS 33045/s.|
|Actual time 10.00s, RPS 30630/s|Actual time 10.00s, RPS 31518/s.|
|Actual time 10.00s, RPS 29377/s|Actual time 10.00s, RPS 32132/s.|
|||
|Connection 10, Round 1 (rust)|Connection 10, Round 1 (csharp)|
|Actual time 10.00s, RPS 38573/s|Actual time 10.01s, RPS 51875/s.|
|Actual time 10.00s, RPS 37235/s|Actual time 10.01s, RPS 50317/s.|
|Actual time 10.00s, RPS 38108/s|Actual time 10.00s, RPS 49122/s.|
|Actual time 10.00s, RPS 37624/s|Actual time 10.01s, RPS 51314/s.|
|Actual time 10.00s, RPS 38422/s|Actual time 10.02s, RPS 51016/s.|
|||
|Connection 20, Round 1 (rust)|Connection 20, Round 1 (csharp)|
|Actual time 10.00s, RPS 44713/s|Actual time 10.00s, RPS 56790/s.|
|Actual time 10.00s, RPS 42052/s|Actual time 10.01s, RPS 59041/s.|
|Actual time 10.00s, RPS 44516/s|Actual time 10.00s, RPS 55582/s.|
|Actual time 10.00s, RPS 44711/s|Actual time 10.00s, RPS 56045/s.|
|Actual time 10.00s, RPS 42935/s|Actual time 10.02s, RPS 56778/s.|
