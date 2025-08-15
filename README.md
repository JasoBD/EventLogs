
# EventLogs Application

Aplicación de gestión de eventos “Logs” desarrollada con **C# .NET 6**, **Angular** y **SQL Server**, desplegable en **AWS**.  
Permite registrar eventos mediante un **API** o **formulario manual**, y consultar los registros con filtros por tipo y rango de fechas.  

---

## Características principales

- Registrar eventos mediante un **endpoint API**.
- Registrar eventos mediante un **formulario manual**.
- Consultar eventos registrados en la base de datos.
- Filtros por:
  - Tipo de evento: `"API"` o `"Manual"`.
  - Rango de fechas.
- Manejo de excepciones y transacciones.
- Arquitectura desacoplada: **Controller → Service → Repository → DB**.

---

## Tecnologías usadas

- **Backend:** C# .NET 6, Entity Framework Core  
- **Frontend:** Angular 16, Angular Material  
- **Base de datos:** SQL Server  
- **Despliegue:** AWS EC2 / Elastic Beanstalk / RDS SQL Server  
- **Otras herramientas:** Postman para pruebas, Git para control de versiones

---

## Estructura de la base de datos

### Tabla `EventLogs`

| Columna      | Tipo          | Descripción                          |
|--------------|---------------|--------------------------------------|
| Id           | INT           | Identificador único (PK)             |
| EventDate    | DATETIME      | Fecha del evento                     |
| Description  | NVARCHAR(500) | Descripción del evento               |
| EventType    | NVARCHAR(50)  | Tipo de evento: "API" o "Manual"    |
| CreatedAt    | DATETIME      | Fecha de registro automático         |

**SQL de creación:**

```sql
CREATE TABLE EventLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EventDate DATETIME NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(500) NOT NULL,
    EventType NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
```

---

## Instalación y ejecución

### Backend (.NET 6)

1. Clonar el repositorio:  
   ```bash
   git clone  https://github.com/tu-usuario/EventLogs.git
  
   ```

2. Restaurar paquetes NuGet:  
   ```bash
   dotnet restore
   ```

3. Configurar conexión a SQL Server en `appsettings.json`:  
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=TU_SERVIDOR;Database=Registration;User Id=USUARIO;Password=CONTRASEÑA;"
   }
   ```

4. Ejecutar migraciones y crear base de datos:  
   ```bash
   dotnet ef database update
   ```

5. Iniciar la API:  
   ```bash
   dotnet run
   ```

   La API quedará disponible en `https://localhost:7135/api/EventLogs`

---

### Frontend (Angular)

1. Navegar al proyecto Angular:  
   ```bash

2. Instalar dependencias:  
   ```bash
   npm install
   ```

3. Configurar URL del backend en `Services.ts`:  
   ```ts
  
     apiUrl: 'https://localhost:7135/api/EventLogs'
   
   ```

4. Ejecutar la aplicación Angular:  
   ```bash
   ng serve
   ```

   La aplicación estará disponible en `http://localhost:4200`

---

## Uso

### Registrar evento vía API

- Endpoint: `POST https://localhost:7135/api/EventLogs`
- Body ejemplo (JSON):  
```json
{
  "eventDate": "2025-08-15T12:00:00",
  "description": "Evento de prueba vía API",
  "eventType": "API"
}
```

### Registrar evento manual

- Ingresar desde el formulario web.
- Completar:
  - Fecha del evento
  - Descripción
- Enviar para almacenar en base de datos.

### Consultar eventos

- Ingresar desde el formulario web.
- Filtros opcionales:
  - Tipo: `"API"` o `"Manual"`
  - Rango de fechas
- Resultado: tabla con registros filtrados.

---

## Consideraciones de diseño

- **Transaccionalidad:** EF Core asegura consistencia en la base de datos.
- **Manejo desacoplado:** Controladores → Servicios → Repositorios → DB.
- **Manejo de excepciones:** Captura de errores en backend con respuesta informativa.
- **Escalabilidad:** La arquitectura permite agregar nuevos tipos de eventos o endpoints sin afectar módulos existentes.
- **Seguridad:** Opcionalmente se puede agregar autenticación JWT o API Key.

---

## Pruebas

- Usar **Postman** para probar endpoints de la API.
- Formulario Angular incluye validaciones básicas:
  - Campos obligatorios
  - Selección de tipo de evento
  - Rango de fechas válido

---

## Despliegue sugerido (AWS)

- **Backend:** Elastic Beanstalk o EC2 + HTTPS
- **Frontend:** S3 + CloudFront
- **Base de datos:** RDS SQL Server
- Configurar variables de entorno para `ConnectionStrings` y `API_URL`.

---

## Autor

- Nombre: Jason Andres Murillo Mena
- Correo: Jamurillo049@gmail.com
- Rol: Desarrolladora FullStack
