#RabbitMq
docker pull rabbitmq:3-management

#MS SQL Server
docker build --no-cache "Storage" -t example-sql

#Api Endpoint
docker build --no-cache "ApiEndpoint" -t example-pyapi

#.net core service
dotnet build ".\ExampleService\Svc.Implementation\Svc.Implementation.csproj"
docker build --no-cache "ExampleService\Svc.Implementation" -t example-svc

#add network
docker network create example-ntw