# CRUD-WebApi #
## Используемые технологии ##
+ ASP.Net Core 6
+ EF CORE
+ MSSQL Server
+ AutoMapper
+ MediatoR
+ FluentValidation
+ Jwt-Bearer
+ Swagger
+ CQRS
+ Serilog

## Инструкция по запуску ##
1. Сменить в файле CRUD_WebApi.WebApi/appsettings.json название сервера БД.
2. Перейти в директорию CRUD_WebApi.WebApi.
3. Выполнить dotnet run через терминал.


## Использование запросов ##
Методы контроллера CRUD_WebApi требует наличие JWT-токена,по этому перед использованием:
1. Пройти регистрацию через запрос   api/auth/register.
2. Получить JWT-токен через запрос api/auth/login.
3. Пройти авторизацию в Swagger и получить доступ к работе с API.
4. Либо добавить заголовок Authorization:"Bearer "+ JWT-токен при работе через Postman.

## Примечание ##
- Проект выполнен в архитектурном стиле CQRS.
- Предназначен для работы со списком пользователей.
- Класс Account создан исключительно для демонстрационной цели. При регистрации аккуанта нет валидации, хеширования пароля и проверки на уникальность имени.
Он создан исключительно для имитации авторизации и создания JWT-токена, а так   же получения доступа к работе с основным списком пользователей.

