# AcessoBank

## API developed to perform bank transfers between two accounts

<p align="center">
 <a href="#features">Features</a> • 
 <a href="#tecnologias">Tecnologys</a> • 
 <a href="#autor">Autor</a>
</p>

<a href="#features">### Features</a>

- [x] Transference
- [x] Query status transference

### Pré-requisitos

Before starting, you will need to have the following tools installed on your machine:

[Git](https://git-scm.com), 
[.Net Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet/3.1),
[Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads/),
[Docker](https://www.docker.com/)

Run Rabbit MQ in Docker:
1. docker run -d --hostname acesso-rabbit --name acesso-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management

Run Elasticsearch and Kibana in Docker:
1. Go to file docker-compose.yaml 
2. Docker-compose up -d

Update database:
1. Open the project solution in VS 2019
2. Configure connectionString in appsetting.json
3. Run in package manager console: Update-Database

### 🛠 Tecnologys

The following tools were used in the construction of the project:

- [.Net Core 3.1]
- [SQL Server]
- [Elastisearch]
- [RabbitMQ]

### Autora

Feito com ❤️ por Marcilia 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Marcilia-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/marcilia-guilger-62661933/)]
