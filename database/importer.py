# Simple script to import the 'data.json' into the DB
# In here, The database structure is not handled with an ORM to keep the script simple
# This of course, is not the case for the actual backend engine
# Naming conventions are based on 'PEP 8'

import json
from os import curdir
from time import sleep
import psycopg2

tryCount = 60 # 5 mins
retryDelay = 5
db_connection = None

print("JSON Importer started...", flush=True)

while tryCount > 0:
    try:
        db_connection = psycopg2.connect(
            user="postgres", 
            password="test123",
            host="database",
            port=5432)
        break
    except:
        print("retrying connection to db (propably not stareted yet!)", flush=True)
        sleep(retryDelay)
        tryCount = tryCount - 1

if not db_connection:
    print("unable to make a db connection. quitting...", flush=True)
    quit()


print('Successfully connected to DB', flush=True)
db_cursor = db_connection.cursor()

# Initializing the DB structure
db_cursor.execute("""
    CREATE TABLE IF NOT EXISTS history_data (
        id bigint PRIMARY KEY,
        city varchar(100),
        start_date date,
        end_date date,
        price float,
        status varchar(10),
        color varchar(10)
    );
    CREATE INDEX IF NOT EXISTS start_date_idx ON history_data(start_date);
    CREATE INDEX IF NOT EXISTS end_date_idx ON history_data(end_date);
""")
    
db_connection.commit()

# Opening data file
fInput = open('./data.json')
importedData = json.load(fInput)
fInput.close()

# get the existings IDs in database
db_cursor.execute("SELECT id FROM history_data")
existing_ids = [row[0] for row in db_cursor.fetchall()]

# inserting everything into a temporary table
insert_query = '\n'.join([F"""
    INSERT INTO history_data(id, city, start_date, end_date, price, status, color) VALUES (
        {row['id']},
        '{row['city'].replace("'", "''")}',
        '{row['start_date']}',
        '{row['end_date']}',
        {row['price']},
        '{row['status']}',
        '{row['color']}'
    );""" for row in importedData if row['id'] not in existing_ids])
if insert_query:
    db_cursor.execute(insert_query)
    db_connection.commit()
    print('Successfully imported "data.json" to DB', flush=True)
else:
    print('Nothing new to import from "data.json"', flush=True)