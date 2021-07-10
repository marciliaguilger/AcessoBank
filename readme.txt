Install and run RabbitMQ on Docker
docker run -d --hostname acesso-rabbit --name acesso-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management


Install and run SQL Server on Docker
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest

Install and run Account API on Docker 
docker run -d -p 5000:80 baldini/testacesso


============= Interface RabbitMQ

http://localhost:15672
username: guest 
pwd: guest



https://www.notion.so/df51e08cc3cf4c5d8ef5f04596651266?v=671cdff627ab4071b6be61771c36b514&p=02fc98e3b5414c78a7c96942f1c6a362

https://www.youtube.com/watch?v=w84uFSwulBI

http://localhost:5000/swagger/index.html


========== RODAR LOCAL
docker run -d -p 5000:80 0b164b9b1782

docker run -d -p 15672:15672 -p 5672:5672 485c275e2364

docker run -d  -p 1433:1433 a43c412ed225