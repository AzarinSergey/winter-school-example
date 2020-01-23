import serviceMessages as messageProvider
import serviceApi as httpApiProvider

from flask import Flask
app = Flask(__name__)

@app.route('/')
def hello_world():
    return 'Hello, World!\n'

@app.route('/service/ping')
def ping_service():
    return httpApiProvider.ping()

@app.route('/service/get')
def service_get_tasks():
    return httpApiProvider.getTasks()

@app.route('/service/create/<taskName>/<taskTime>')
def service_new_task(taskName, taskTime=60):
    return messageProvider.sendMessage({'newTaskName':taskName, 'newTaskTime':taskTime })



  


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=int("5000"), debug=True)