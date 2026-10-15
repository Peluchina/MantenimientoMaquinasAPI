# API de mantenimiento de máquinas

API ASP.NET Core 8 organizada por capas de dominio, aplicación (CQRS) e infraestructura. La persistencia usa Dapper y SQL Server; las consultas y escrituras se realizan mediante procedimientos almacenados.

## Configuración

1. Crea la base de datos y ejecuta los scripts de [`Database/Tables`](Database/Tables) y [`Database/StoredProcedures`](Database/StoredProcedures) en orden. Los procedimientos esperan el esquema `dbo` y que existan también las tablas `Users` y `Roles` referenciadas.
2. Configura `ConnectionStrings:PPA` en `appsettings.json` o como variable de entorno `ConnectionStrings__PPA`. No guardes credenciales reales en el repositorio.
3. Ejecuta la API con `dotnet run` desde la carpeta del proyecto. En desarrollo, Swagger queda disponible en `/swagger`.

## Endpoints

| Método | Ruta | Acción |
|---|---|---|
| GET | `/api/machines` | Listar máquinas |
| GET | `/api/machines/{id}` | Obtener máquina |
| POST | `/api/machines` | Crear máquina |
| PUT | `/api/machines/{id}` | Actualizar máquina |
| DELETE | `/api/machines/{id}` | Eliminar máquina; responde 409 si conserva historial o mantenimientos |
| GET | `/api/users` | Listar usuarios |
| GET | `/api/users/{id}` | Obtener usuario |
| POST | `/api/users` | Crear usuario |
| PUT | `/api/users/{id}` | Actualizar usuario |
| DELETE | `/api/users/{id}` | Desactivar usuario sin borrar su historial |

Las solicitudes inválidas responden `400 Bad Request`; las búsquedas de recursos inexistentes responden `404 Not Found`; los nombres de usuario y correos duplicados responden `409 Conflict`. La creación responde `201 Created`, las actualizaciones y eliminaciones o desactivaciones exitosas responden `204 No Content`, y las consultas exitosas responden `200 OK`.

Los módulos Machine y Users están organizados en dominio, aplicación CQRS e infraestructura. Los handlers dependen de contratos de repositorio; las implementaciones Dapper invocan los procedimientos almacenados. Las contraseñas de Users se almacenan con hash PBKDF2 y no se incluyen en las respuestas.

La eliminación de un usuario usa el procedimiento `Users_Delete`, que lo marca como `INACTIVO` para conservar referencias e historial.
