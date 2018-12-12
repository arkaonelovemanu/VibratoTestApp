# VibratoTestApp
To demonstrate the automation of the spin-up and installation of a single-machine n-tier architecture using Docker.The goal is to be able to spin up the environment using a single command (eg. `vagrant up` or `docker-compose up` or `kubectl apply -f ...`. The user only has to then connect to the instances IP address with a browser to see the resulting data returned. 

## The tiering
Having worked primarily in Microsoft .NET and Azure PaaS platform,I built a .NET Core API which will communicate with a SQL Sever 2016 instance to retreieve,post and update data. Then, there is a simple web application for doing CRUD operations which is hosted in an Apache Server.The web application will communicate to the API over port 5000 and the API will communicate with the Database Server on port 1433.Along with these, I am also spinning up a Flyway Instance(https://flywaydb.org/getstarted/firststeps/commandline) which helps in migrating my DB scripts to the Database Server for the initial data setup.
So, summing up, there are 4 docker images running on linux containers,all of them orchestrated from a docker-compose yaml.

## Running the App with Docker Compose

1. Install `Docker for Mac` or `Docker for Windows` (or Docker Toolbox: http://docker.com/toolbox if you have to)

2. Open a separate command prompt window and clone the repo- 
    #### git clone https://github.com/arkaonelovemanu/VibratoTestApp.git

3. Navigate to the root of the repo
    #### $ cd VibratoTestApp
4. Run docker compose command to build the images and run the containers.
    #### `docker-compose up`

5. Navigate to http://localhost:8080 (http://192.168.99.100:8080 if using Docker Toolbox) in your browser to view the site.

  #### Additionally, you can navigate to http://localhost:5000 to open the swagger UI of API

## Thoughts
This was a great assignment. I had never got the oppurtunity to work hands on with Docker before this was a great learning curve.Glueing the things and orchestrating them together from a single yaml file is very cool. Also, I spent some time refining the API to enable logging, CORS, mappers, DI,swagger  and a middleware for exception handling. I also thought of including a JWT middleware so that the API authorizes users based on tokens and claims but felt it would consume some more time. The web application uses Angular 1.5 because I it served the purpose of simple data binding and making rest http calls to the api.
