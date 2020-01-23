FROM python:3

RUN pip install Flask
RUN pip install pika --upgrade
RUN pip install requests --upgrade

COPY . /app

EXPOSE 5000

CMD ["python", "app/app.py"]
