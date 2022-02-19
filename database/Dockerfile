FROM python:3.7-alpine
WORKDIR /
RUN apk add --no-cache libpq-dev gcc musl-dev linux-headers
COPY requirements.txt requirements.txt
COPY data.json data.json
COPY importer.py main.py
RUN pip install -r requirements.txt
CMD ["python", "main.py"]