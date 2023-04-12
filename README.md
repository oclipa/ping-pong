# ping-pong

- Add `127.0.0.1       host.docker.internal` to C:\Windows\System32\drivers\etc\hosts
- Run: `sudo docker compose up --build sender responder`
- Open: 
   - http://host.docker.internal:5083/swagger/index.html
   - http://host.docker.internal:5082/swagger/index.html
