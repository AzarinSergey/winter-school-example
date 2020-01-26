import pika
import json



def sendMessage(message):
    
    connection = pika.BlockingConnection(pika.ConnectionParameters('example-rabbit', 5672, 'example_vhost'))
    channel = connection.channel()
    
    channel.queue_declare(queue='example')

    jsonMessage = json.dumps(message)
    channel.basic_publish(exchange='', routing_key='example', body=jsonMessage)

    channel.close()
    connection.close()

    return "Message sent! </br> Payload: </br> \n " + jsonMessage

