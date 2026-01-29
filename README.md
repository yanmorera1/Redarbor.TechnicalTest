# Redarbor.TechnicalTest

Guía rápida para configurar, compilar y ejecutar el proyecto, y una breve descripción de las tecnologías utilizadas.

## Requisitos
- .NET 10 SDK
- Git
- SQL Server
- Docker y Docker Compose

## Clonar el repositorio
```
git clone https://github.com/yanmorera1/Redarbor.TechnicalTest.git cd Redarbor.TechnicalTest
```

## Proyectos relevantes
- `src/Redarbor.TechnicalTest.Api` — ASP.NET Core Web API (host y startup)
- `src/Redarbor.TechnicalTest.Application` — Lógica de aplicación
- `src/Redarbor.TechnicalTest.Infrastructure` — Persistencia, `DbContext`, mapeos Dapper
- `src/Redarbor.TechnicalTest.Domain` — Entidades y ValueObjects
- `src/Redarbor.TechnicalTest.Application.Tests` — Pruebas unitarias

La cadena de conexión principal está configurada en el archivo `appsettings.json` bajo la clave `ConnectionStrings:Database`.

### Modificación de Parámetros
Si necesitas cambiar la configuración de la base de datos, debes asegurarte de actualizar los siguientes puntos para mantener la consistencia:

1.  **appsettings.json**: Cambia la cadena de conexión en la sección `ConnectionStrings`.
2.  **docker-compose.override.yml**: Localiza el servicio `employeedb`. Si modificas variables como la contraseña (`SA_PASSWORD`) o los puertos, estos deben coincidir exactamente con lo definido en el `appsettings.json` para que la API pueda conectarse correctamente.

## Configuración de Identidad (Keycloak)

El proyecto utiliza **Keycloak** para la gestión de autenticación y autorización. La configuración del entorno (roles, clientes y usuarios) se realiza automáticamente mediante la importación de un archivo de respaldo.

### 1. Importación del Realm
Para asegurar que el entorno cuente con la configuración necesaria, asegúrese de que el archivo `realm-export.json` se encuentre en la carpeta raíz del proyecto o en el directorio especificado en el `docker-compose.yml`.

### 2. Configuración en Docker Compose
En su archivo `docker-compose.yml`, el servicio de Keycloak debe estar configurado con el siguiente comando para procesar la importación al inicio:

```yaml
keycloak:
  container_name: keycloak
  environment:
    - KC_BOOTSTRAP_ADMIN_USERNAME=admin
    - KC_BOOTSTRAP_ADMIN_PASSWORD=admin
  command: ["start-dev", "--import-realm"]
  ports:
    - "8080:8080"
  volumes:
    - keycloak-data:/opt/keycloak/data
    - ./realm-export.json:/opt/keycloak/data/import/realm.json:ro
```

## Autenticación y Pruebas con Postman

Para realizar las pruebas de los endpoints protegidos, se incluye una colección de Postman en la raíz del proyecto: `Redarbor.postman_collection.json`.

### Configuración de OAuth 2.0 en Postman
En lugar de solicitar el token manualmente, la colección está configurada para obtenerlo directamente desde Keycloak siguiendo estos pasos:

1. **Importar la colección**: Cargue el archivo `Redarbor.postman_collection.json` en Postman.
2. **Acceder a la pestaña Authorization**: Seleccione la colección o una petición específica y diríjase a la pestaña **Authorization**.
3. **Seleccionar Tipo**: Asegúrese de que el campo *Type* esté configurado como **OAuth 2.0**.
4. **Obtener Nuevo Token**:
   * Desplácese hasta el final del panel de configuración de la derecha.
   * Haga clic en el botón naranja **"Get New Access Token"**.
5. **Autenticación**:
   * Se abrirá una ventana de inicio de sesión de Keycloak.
   * Ingrese las credenciales de prueba:
     * **Usuario**: `employee`
     * **Contraseña**: `employee12345`
6. **Usar Token**: Una vez autenticado, Postman mostrará el token generado. Haga clic en **"Use Token"**.

A partir de este momento, todas las peticiones que hereden la autenticación de la colección incluirán automáticamente el encabezado `Authorization: Bearer <token>` necesario para operar la API.

## Compilar y ejecutar localmente
1. Restaurar paquetes:
   ```
   dotnet restore
   ```
2. Compilar la API:
   ```
   dotnet build src/Redarbor.TechnicalTest.Api/Redarbor.TechnicalTest.Api.csproj -c Release
   ```
3. Ejecutar con Docker Compose: Para levantar toda la infraestructura (API, Base de Datos y RabbitMQ) de forma automatizada, ejecute el siguiente comando en la raíz del proyecto:
     ```bash
     docker-compose up -d --build
     ```
4. Acceso a la API: Una vez que los contenedores estén en ejecución, la API responderá en los puertos configurados:

    - HTTP: `http://localhost:64389`

    - HTTPS: `https://localhost:64390`

Puede validar que los servicios se encuentran activos mediante el comando `docker ps`, verificando que todos los contenedores tengan el estado Up.

## Pruebas
Ejecutar las pruebas unitarias:
```
dotnet test src/Redarbor.TechnicalTest.Application.Tests/Redarbor.TechnicalTest.Application.Tests.csproj --configuration Release
```

## Integración continua
Existe un workflow en `.github/workflows/ci.yml` que:
- configura .NET 10,
- cachea paquetes NuGet,
- restaura dependencias,
- compila `Redarbor.TechnicalTest.Api`,
- ejecuta las pruebas en `Redarbor.TechnicalTest.Application.Tests`.

## Tecnologías clave
- .NET 10 / ASP.NET Core — plataforma del API.
- Entity Framework Core — ORM y migraciones (SQL Server).
- Dapper — acceso ligero y type handlers.
- Mapster — mapeo de objetos.
- Carter — rutas/handlers modulares para APIs ligeras.
- Serilog — logging estructurado.
- GitHub Actions — CI.
- RabbitMQ — mensajería de eventos.
- Keycloak — autenticación.
- Docker — contenedores.
- Docker Compose — configuración de contenedores.

## Consejos
- Abrir la solución en Visual Studio y usar __Solution Explorer__ para ejecutar el proyecto o pruebas desde el IDE.