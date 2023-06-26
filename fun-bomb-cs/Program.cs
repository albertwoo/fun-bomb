using System.Diagnostics;

var url = args[0];
var connections = int.Parse(args[1]);

var http_client = new HttpClient();

var isDoneLocker = new object();
var isDone = false;
var tasks = new List<Task<long>>();

var timer = Stopwatch.StartNew();
for (int i = 0; i < connections; i++) {
    tasks.Add(Task.Run(async () => {
        var count = 0L;
        while (true) {
            lock (isDoneLocker) {
                if (isDone) return count;
            }
            var response = await http_client.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                count++;
            }
        }
    }));
}

await Task.Delay(10_000);
lock (isDoneLocker) {
    isDone = true;
}
await Task.WhenAll(tasks);

var timeCost = timer.ElapsedMilliseconds;
var successCount = tasks.Select(t => t.Result).Sum();
var rps = successCount * 1000 / timeCost;

Console.WriteLine($"Actual time {timeCost / 1000.0,0:N2}s, RPS {rps}/s.");
