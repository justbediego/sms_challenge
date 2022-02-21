# Solution for SMS fullstack challenge

### This projcet is implemented as a solution for the challenge provided in [here](./mission/README.md).

## Solution

1. A <strong>postgres</strong> database server is created within a docker container and is then filled with the data provided as a <strong>json</strong> file using a <strong>python</strong> script.
2. A <strong>.net Core</strong> application is running as the backend engine to provide a RESTful interface together with their swaggerUI documentations.
3. A <strong>react-admin</strong> frontend is implemented to present the data in a sortable table.
4. Two Date pickers and a Search box is added to filter the data. (the filter is applied server-side)

## Screenshots

| Start     |
|-----------|
|![Start](./screen1.PNG)|

| Filtered Result |
|-----------|
|![Filtered Result](./screen2.PNG)|

| Creating a new Data |
|-----------|
|![Filtered Result](./screen4.PNG)|

| Swagger Documentation |
|-----------|
|![SwaggerUI](./screen3.PNG)|

## Structure

This solution is divided into four parts:
- <strong>database</strong>: A docker container of PostgreSQL instance with an empty database
- <strong>json_importer</strong>: A python script that waits for the database to initialize and then imports its neighboring file named <strong>data.json</strong> into the database. The script can run multiple time without causing any issue as it only inserts the non-existing data into the DB.
- <strong>backend</strong>: A .net core webapi application that communicates with the database and serves apis to be accessed by the frontend. The backend also hosts swagger documentations.
- <strong>frontend</strong>: A react/react-admin web application that communicates with the backend CRUD apis to retrieve the neccessary data.

## How to run

There is a <strong>docker-compose</strong> configuration available to run the project.

### Quick Start
You can simply run all four components together at once with the following command:

<code>docker-compose up</code>

After the command is successfully ran you can access the application through these two URLs (granting that docker containers are running locally):

- Application: `http://localhost:4020/`
- SwaggerUI: `http://localhost:5020/swagger`

### Running locally
In case you need to run any of these components locally on your machine you need to consider modifying their network configuration. Since they will no longer be running within a bridged netweek inside docker, they need to pinpoint to the correct address.
For example, the backend component is currently pointing to internal docker address, <strong>http://database:5432</strong>, and this will need to change to an external address, something similar to <strong>http://localhost:1432</strong>.


### Running Tests


## Arguments
- As the business is small, I personally did not find the need to apply <strong>repository pattern</strong>
- Considering the size of the project, validations are done at DTO level 
- GetHistoryData url params are considered optional so url gets shorter when not necessary
- "From" and "To" filters are applied to both start_date and end_date. This is because I concluded the mission wanted me to extract the overlap of the two ranges (and not the containment).
- In the importer script, The database structure is not handled with an ORM to keep the script simple
- serial ID generation is disabled while importing the data.json to assure the same id for the row as given in the json file. 