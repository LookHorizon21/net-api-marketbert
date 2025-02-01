# Proyecto de API RESTful con ASP.NET Core Web API usando JWT y .NET Identity

Es una API desarrollada en **ASP.NET Core Web API** que implementa autenticación con **JWT** y **.NET Identity**. Utiliza el **patrón de repositorio** para la gestión de datos y proporciona endpoints para la administración de categorías, productos y usuarios.

## 🚀 Características
- **ASP.NET Core Web API**
- **Autenticación JWT**
- **.NET Identity** para gestión de usuarios
- **Patrón de repositorio** para acceso a datos
- **Swagger UI** para documentación de la API

## 🔗 Acceso a la API
La API está desplegada en:
👉 [http://api-marketbert.tryasp.net/swagger/index.html](http://api-marketbert.tryasp.net/swagger/index.html)

## 🛠 Tecnologías utilizadas
- **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server 2022**
- **Swagger (Swashbuckle)**

## 📂 Estructura del Proyecto
```
ApiMarketBert
│── Controllers          # Controladores de la API
│── Data                # Configuración del contexto de la base de datos
│── MarketMappers       # Mapeo de entidades
│── Migrations          # Migraciones de la base de datos
│── Models              # Modelos y DTOs
│── Repository          # Implementación del patrón repositorio
│── appsettings.json    # Configuración de la API
│── Program.cs          # Punto de entrada de la aplicación
```

## 🔑 Autenticación
Para acceder a los endpoints protegidos, se debe utilizar autenticación **JWT**. Puedes logearte con las siguientes credenciales de prueba:
- **Usuario:** Admin
- **Contraseña:** Admin123

Una vez autenticado, el token generado debe incluirse en cada petición en el header:
```http
Authorization: Bearer {token}
```

## 📌 Endpoints Principales
### Autenticación
- `POST /api/usuarios/login` - Iniciar sesión y obtener un token JWT
- `POST /api/usuarios/registro` - Registrar un nuevo usuario

### Categorías
- `GET /api/categorias` - Obtener todas las categorías
- `POST /api/categorias` - Crear una nueva categoría

### Productos
- `GET /api/productos` - Obtener todos los productos
- `POST /api/productos` - Agregar un nuevo producto

## 🛠 Instalación y Ejecución Local
### 1️⃣ Clonar el repositorio
```sh
git clone https://github.com/LookHorizon21/net-api-marketbert.git
cd ApiMarketBert
```

### 2️⃣ Configurar la Base de Datos
Modificar `appsettings.json` con la cadena de conexión adecuada.

### 3️⃣ Aplicar Migraciones
```sh
dotnet ef database update
```

### 4️⃣ Ejecutar la API
```sh
dotnet run
```

## 📝 Contribuciones
Si deseas contribuir al proyecto, ¡serás bienvenido! Puedes abrir un **Issue** o enviar un **Pull Request**.

## 📄 Licencia
Este proyecto está bajo la licencia **MIT**.

---
📌 **Desarrollado con ASP.NET Core Web API por [Wedman Aguero]**

