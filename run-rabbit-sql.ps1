docker run -d -p 15005:15672 -p 15006:5672 --network=example-ntw --name example-rabbit -e RABBITMQ_DEFAULT_VHOST=example_vhost rabbitmq:3-management

docker run -d -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=555331qQ!' -p 15433:1433 --network=example-ntw --name example-sql example-sql



