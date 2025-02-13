# Services Order Add

## Description
This microservice allows adding orders to the system using a REST API. It is built with .NET, utilizing MySQL as the database.

## Prerequisites
Ensure you have the following installed:
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Git](https://git-scm.com/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Clone the Repository
To clone this project, run:
```sh
git clone https://github.com/Anggeloo/services-order-add.git
cd services-order-add
```


## Installation
1. Restore dependencies:
   ```sh
   dotnet restore
   ```

2. Build the service:
   ```sh
   dotnet build
   ```

3. Run the service:
   ```sh
   dotnet run
   ```

## Running with Docker
1. Build the Docker image:
   ```sh
   docker build -t services-order-add .
   ```

2. Run the container:
   ```sh
   docker run -p 5001:5000 --env-file .env services-order-add
   ```

3. Alternatively, use Docker Compose:
   ```sh
   docker-compose up --build
   ```

## API Documentation
This service provides API documentation via Swagger. Once the service is running, access it at:
```
http://localhost:5050/swagger/index.html
```

## Database Setup
Ensure you have a MySQL database running. If using Docker, you can spin up a MySQL container with:
```sh
docker run --name mysql-db -e MYSQL_ROOT_PASSWORD=AWSMServices001 -e MYSQL_DATABASE=db_order -e MYSQL_USER=admin -e MYSQL_PASSWORD=AWSMServices001 -p 3306:3306 -d mysql:latest
```

## Testing
Currently, no test scripts are defined. You may add tests and run them using:
```sh
dotnet test
```
## Authors
Cadena Anggelo and Caiza Katherine
