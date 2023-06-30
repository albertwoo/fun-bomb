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

![rust http client vs csharp](./rust%20http%20client%20vs%20csharp.png)
