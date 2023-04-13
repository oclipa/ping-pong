# PingPong

This is a simple example of enabling two services running on the same docker instance to communicate via REST.

## Instructions

- Install Docker
- Add `127.0.0.1       host.docker.internal` to the hosts file
   - Windows: C:\Windows\System32\drivers\etc\hosts
- In the root folder, run: 
   - `docker compose up --force-recreate --no-deps --build sender responder`
- Open the following URL: 
   - http://host.docker.internal:5082/swagger/index.html
- Execute the `/api/Send` endpoint
   - Any message string is acceptable

The services will then send simple "ping" and "pong" messages back and forth, once every second, until the process is stopped (CTRL-C).