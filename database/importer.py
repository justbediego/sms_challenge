from time import sleep
import psycopg2

tryCount = 60 # 5 mins
retryDelay = 5
db_connection = None

print("CSV Importer started...", flush=True)

while tryCount > 0:
    try:
        db_connection = psycopg2.connect(
            user="postgres", 
            password="test123",
            host="database",
            port=5432)
        break
    except Exception as whatever:
        print("retrying connection to db (propably not stareted yet!)", flush=True)
        print(whatever)
        sleep(retryDelay)
        tryCount = tryCount - 1

if not db_connection:
    print("unable to make a db connection. quitting...", flush=True)
    quit()


print('Successfully connected to DB', flush=True)