use actix_web::web::{get};
use actix_web::{App, HttpResponse, HttpServer};

#[tokio::main]
async fn main() -> std::io::Result<()> {
    let host = "0.0.0.0:3000";

    println!("Start listening on: {}", host);

    HttpServer::new(|| {
        App::new()
            .route(
                "hello",
                get().to(|| async { HttpResponse::Ok().body("world") }),
            )
    })
    .bind(("0.0.0.0", 3000))?
    .run()
    .await
}
