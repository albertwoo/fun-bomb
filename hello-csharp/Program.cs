var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

var app = builder.Build();

app.MapGet("/hello", () => "world");

app.Run("http://0.0.0.0:3000");
