## Acelera Project

A conditionally free showcase for finding driving instructors.

#### Support

Open a terminal in the root folder of the solution and execute the specified commands (depending on the current task):

- Build the project for Docker: `docker-compose build --no-cache`
- Start the project in Docker: `docker-compose up -d`
- Stop the project in Docker: `docker-compose down -v`
- Create a DB migration: `dotnet ef migrations add {MigrationName} --project sources/Acelera.Infrastructure`
- Apply the DB migration: `dotnet ef database update --project sources/Acelera.Infrastructure`
