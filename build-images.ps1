docker pull rabbitmq:3-management

docker build --no-cache "Storage" -t example-sql

docker build --no-cache "ApiEndpoint" -t example-pyapi

dotnet build ".\ExampleService\Svc.Implementation\Svc.Implementation.csproj"
docker build --no-cache "ExampleService\Svc.Implementation" -t example-svc

docker network create example-ntw