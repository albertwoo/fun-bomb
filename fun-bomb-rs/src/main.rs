use std::{env, sync::{Arc, atomic::AtomicBool}, time::Duration};

#[tokio::main]
async fn main() {
    let args: Vec<String> = env::args().collect();
    let url = &args[1];
    let connections = args[2].parse::<u16>().unwrap();

    let http_client = reqwest::Client::new();
    let is_done = Arc::new(AtomicBool::new(false));

    let mut tasks = Vec::new();

    let timer = tokio::time::Instant::now();
    for _ in 0..connections {
        let http_client = http_client.clone();
        let url = url.clone();
        let is_done = is_done.clone();
        tasks.push(tokio::spawn(async move {
            let mut count = 0;
            loop {
                if is_done.load(std::sync::atomic::Ordering::Relaxed) {
                    return count;
                }
                match http_client.get(&url).send().await {
                    Ok(resp) if resp.status().is_success() => {
                        count += 1;
                    }
                    _ => {}
                }
            }
        }));
    }

    tokio::time::sleep(Duration::from_secs(10)).await;
    is_done.store(true, std::sync::atomic::Ordering::SeqCst);
    let task_results = futures::future::join_all(tasks).await;

    let time_cost = timer.elapsed().as_secs_f64();

    let mut success_count = 0;
    for count in task_results {
        success_count += count.unwrap();
    }

    let rps = (success_count as f64 / time_cost) as u32;

    println!("Actual time {:.2}s, RPS {}/s", time_cost, rps)
}
