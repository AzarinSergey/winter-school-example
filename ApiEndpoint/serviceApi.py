import requests

def ping():
    response = requests.get('http://example-svc:15008/ping')
    return response.content

def getTasks():
    response = requests.get('http://example-svc:15008/get')
    return response.text.replace('{','{</br>').replace(',','</br>').replace('}','}</br>')