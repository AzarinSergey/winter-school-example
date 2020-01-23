import pika
import json

connection = pika.BlockingConnection(pika.ConnectionParameters('example-rabbit', 5672, 'example_vhost'))
channel = connection.channel()
channel.queue_declare(queue='example')

def sendMessage(message):
    jsonMessage = json.dumps(message)
    channel.basic_publish(exchange='', routing_key='example', body=jsonMessage)
    return "Message sent! </br> Payload: </br> \n " + jsonMessage

