1. Start 'Flask' python application locally:

python -m pip install Flask

$env:FLASK_APP = "C:\Users\azarin.sa\Desktop\winter scool\example\ApiEndpoint\app.py"
$env:FLASK_ENV='development'

python -m flask run

2. Dockerfile - create (rebuild) image:
docker build --no-cache "C:\Users\azarin.sa\Desktop\winter scool\example\ApiEndpoint" -t example-pyapi

------------------стартануть контейнер (http://localhost:15000/)
docker run -p 15000:5000 --network=example-ntw --name example-pyapi example-pyapi

------------------запрос к хосту внутри контейнера:
docker exec -it example-pyapi curl localhost:5000 

------------------удалить контейнерcat 
docker container rm -f {container id}
docker rm -f  (docker ps -q -a)


----------------------------запускаем RabbitMQ (UI - localhost:15005)
docker run -d -p 15005:15672 -p 15006:5672 --network=example-ntw --name example-rabbit -e RABBITMQ_DEFAULT_VHOST=example_vhost rabbitmq:3-management

----------------------------запускаем sql server (127.0.0.1,15433)
docker build --no-cache "C:\Users\azarin.sa\Desktop\winter scool\example\Storage" -t example-sql

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=555331qQ!' -p 15433:1433 --network=example-ntw --name example-sql example-sql

-----------------------------инициализируем БД
docker container exec -it example-sql /opt/mssql-tools/bin/sqlcmd -U sa -P 555331qQ! -i initial.sql
/opt/mssql-tools/bin/sqlcmd -U sa -P 555331qQ! -i initial.sql


--------------------------запуск сервиса
docker build --no-cache "C:\Users\azarin.sa\Desktop\winter scool\example\ExampleService\Svc.Implementation" -t example-svc

docker container run -p 15008:15008 --network=example-ntw --name example-svc example-svc


---------------######################-------------

cat etc/*release





