## Proposito del proyecto
El objetivo de este proyecto es desarrollar una API RESTful para la gestión de una biblioteca de libros. Esta API permite:

-	📖 Consultar datos importantes acerca de los libros.

-	✅ Verificar la disponibilidad y/o existencia de un libro en el inventario.

-	➕ Agregar nuevos libros al sistema.

-	✏️ Editar información de libros existentes.

-	❌ Eliminar registros de los libros ya creados.

Este proyecto facilita la administración eficiente de un catalogo de libros, brindando información suficiente para su gestión y dando bases solidas para
la integración del FRONTEND dentro de un mismo sistema.

## Tecnologias Utilizadas:
	- .NET Core 8.0
	- ASP.NET Core Web API
	- Entity Framework Core
	- Microsoft SQL Server
	- Swagger

## Librerias:
	- EntityFrameworkCore 9.0.4
	- EntityFrameworkCore.SqlServer 9.0.4
	- EntityFrameworkCore.Tools 9.0.4
	- Swashbuckle.AspNetCore 6.8.1


## Instrucciones de instalación y configuración
1. Clona el repositorio de github.
2. Utiliza `dotnet restore` para obtener las dependencias del proyecto.
2. Configura la cadena de conexión en `appsettings.Development.json` que esta dentro de `WebAPI`.
	**Template for Windows Auth:**
	```json
	"ConnectionStrings": {
	  "SqlServerConnection": "Server=localhost;Database=Library;TrustServerCertificate=true;Trusted_Connection=True;"
	}
	```
	**Other templates:** https://www.connectionstrings.com/sql-server/
3. Ejecutar migración utilizando
	-`dotnet ef database update`
	- o `database-update` dentro del *Package Manager Console*.
4. Corre el proyecto
	-`dotnet run`
	- o F5 desde Visual Studio.

__**TIP: la carpeta `Scripts-SQL/` guarda un script `onlydata_librarydb.sql` con registros predefinidos para tests.**__


## Estructura del proyecto
**ApplicationLayer:** (Estructura)
- `DTOs/` Carpeta para los Data Transfer Objects.
- `Interfaces/` Carpeta para las interfaces de los servicios/repositorios.

**BusinessLayer:** (Logica)
- `Repositories/` Carpeta con las clases que gestionan los datos.

**DataAccessLayer** (Acceso a Datos)
- `Context/` Guarda la clase del App Database Context.
- `Entities/` Guarda las entidades/modelos de la base de datos.
- `Migrations/` Guarda las migraciones hechas para la base de datos.

**WebAPI**
- `Controllers` Contiene los controladores para la manipulación de endpoints de cada modelo.

## Endpoints Principales

**Metodo:** `GET`  

**URL:** `/api/books`  

**Funcion:** Obtiene una lista paginada con un maximo de 3 libros de la biblioteca.  

**Ejemplo:** `/api/Books?page=1`  
___

**Metodo:** `POST`  

**URL:** `/api/books`  

**Funcion:** Crea un nuevo libro y lo agrega al inventario.  

**Ejemplo:** `/api/Books`  

**Request Body:**
```json
{
	"title": "DUNE",
	"author": "Frank Herbert",
	"yearPublication": 1965,
	"isbn": "9788467963403",
	"genre": "Ciencia Ficcion",
	"available": true
}
```

___


**Metodo:** `GET`  

**URL:** `/api/Books/{id}`  

**Funcion:** Obtiene los datos del libro con un identificador especifico si existe.  

**Ejemplo:** `/api/Books/1`  
___


**Metodo:** `DELETE`  

**URL:** `/api/Books/{id}`  

**Funcion:** Elimina los datos del libro con un identificador especifico si existe.  

**Ejemplo:** `/api/Books/1`  
___


**Metodo:** `PUT`  

**URL:** `/api/Books/{id}`  

**Funcion:** Actualiza los datos de un libro con un identificador especifico si existe.  

**Request Body:**
**ID:** `1`
```json
{
      "title": "Le Petit Prince",
      "author": "Antoine de Saint-Exupery",
      "yearPublication": 1943,
      "isbn": "9788478887194",
      "genre": "Novela",
      "available": true
}
```


## Decisiones de Diseño

- La aplicación de la división del proyecto en capas es para mantener la logica de negocio, el acceso a la base de datos y la definición de metodos, separada y ordenada.
- Utilice un repositorio generico `/Repository.cs` para la creación de operaciones basicas CRUD, haciendo el codigo reutilizable y el sistema de facil escalabilidad.
- El `WithoutIdBookRequest` es un DTO formado para la creación de `Books` que evita exponer el ID, siendo este un dato sensible y asignado automaticamente por la base de datos.
- `PagedRepositoryDTO` es utilizado para el endpoint `GetAll()`, el cual recibe como parametro un numero de pagina, siendo este DTO quien la devuelve junto con una lista de entidades
