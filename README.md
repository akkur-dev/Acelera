## Acelera Project



Условно бесплатная витрина для поиска автоинструкторов



#### Поддержка

Открыть терминал в корневой папке решения, и выполнить указанные команды (в зависимости от текущей задачи):

- Сборка проекта для Docker: `docker-compose build --no-cache`
- Запуск проекта в Docker: `docker-compose up -d`
- Завершение проекта в Docker: `docker-compose down -v`
- Создание миграции БД: `dotnet ef migrations add {MigrationName} --project sources/Acelera.Infrastructure`
- Применение миграции БД: `dotnet ef database update --project sources/Acelera.Infrastructure`