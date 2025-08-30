Proyecto Examen Caja de Ahorro


Crear un repositorio público (GitHub, GitLab, etc.) con una solución básica en .NET 6+ que contenga:
✅ Requerimientos funcionales
1. Endpoint de login
o Input: usuario y contraseña.
o Output: token JWT si el login es exitoso.
2. Endpoint de consulta de cuentas (GET /accounts)
o Debe requerir token JWT válido.
o Responder con una lista de cuentas bancarias (número, tipo, saldo).

✅ Requerimientos no funcionales
 Arquitectura limpia o hexagonal (mínimo separación de capas: controller, service,
repository).
 Uso de inyección de dependencias.
 Manejo adecuado de errores.
 Autenticación con JWT.
 Buen uso de DTOs.
 Documentación Swagger (OpenAPI).
 README que explique cómo correr el proyecto localmente (dotnet run o docker, si aplica).
 Para la capa de datos se puede utilizar alguna librería de FakeDatabase.
Entrega esperada:
URL del repositorio con el código fuente, incluyendo instrucciones para ejecutar y probar la API.

---
Estructura del proyecto Clean Architecture

<img width="419" height="257" alt="image" src="https://github.com/user-attachments/assets/503aaaa5-678e-488e-b620-9a56d09181a2" />

1. El proyecto Exam_CA.Domain, implementa los modelos de datos asi como las interfaces que definen las acciones de los repositorios.
<img width="445" height="380" alt="image" src="https://github.com/user-attachments/assets/e1cfc634-c772-4c7a-b200-1a350b7e6846" />

2. Exam_Ca.Infraestructure, implementa las inferfaces definidas en el proyecto Domain, pero estas implementaciones estan asociadas a Sql Server, sin embargo al ser una arquitectura limpia, el desacoplamiento permitiria migrar hacia otro manejador sin afectar el compoortamiento. Injecta el Contexto de Base de datos de EntityFramework
<img width="494" height="424" alt="image" src="https://github.com/user-attachments/assets/6171da62-d75a-4f60-a515-9a0489fac81a" />

3. Exam_CA.Application, implementa las interfaces de los services para la presentacion de los datos entragados por los repositorios (estos fueron injectados), y establecer el comportamiento y reglas de negocio. Incluye los DTOs (Objetos de Transferencia de Datos)
   <img width="493" height="392" alt="image" src="https://github.com/user-attachments/assets/ce07e349-2938-4452-a92d-5c6db77d6698" />

5. Exam_CA.WebAPI, es el "front" de la arquitectura, siendo el mecanismo de entrega final de los datos, de acuerdo a las reglas de negocio establecidas por Exam_CA.Application. Se incluyen validadores de los modelos utilizando el nuget FluentValidation que al implementar sus interfaces se puede realizar validaciones de la entrada de los datos en los modelos. Siendo una buena practica para evitar Ataques de inyección y fallos de autenticacion y autorizacion.
   
<img width="490" height="479" alt="image" src="https://github.com/user-attachments/assets/0f2c8fbf-3484-48dc-a90d-58be1a405e01" />

  
-----
Instalación de base de datos
Base de datos: SQL Server
Se sugiere utilizar el script de bd para evitar complicaciones con el manejo de las versiones del motor de bd
1. Desde el SQL Server Management conectese a su instancia de base de datos.
2. Abra el archivo ScribDb.sql
3. Ejecute el archivo, y espere a que se reflejen los cambios
4. Aplique la configuración de autenticacion mixta, aunque se usa la de Sql server
5. Abra la solución del proyecto completo
6. Modifique el archivo appsettings.jxon del proyecto: Exam_CA.WebApi en el atributo ConnectionStrings.DefaultConnection
           <img width="870" height="96" alt="image" src="https://github.com/user-attachments/assets/d6b98d97-33ef-42eb-88e6-991915d6a1f8" />
        Establezca el nombre del host, o nombre de la instancia de sql server donde se encuentra sla db, cambie el nombre de la base de datos en caso de ser necesasrio, y establezca sus credenciales de acceso en los atributos User_ID y paassword
        <img width="632" height="54" alt="image" src="https://github.com/user-attachments/assets/2f23131a-15c9-4d7a-b0c2-05079903fbb3" />
  
7. Una vez realizado ello, podra realizar la ejecucion del proyecto, asegurese que el proyecto Exam_CA.WebApi este seleccionado como el proyecto printipal

----
Se adjunta: 
1. backup de base de datos, seria otra forma, aunque no recomenda por posible conflicto en las versiones de base de datos.
2. Coleccion de postman con pruebas y ejemplos de consumo
3. Enviroment con variables de entorno de postman para pruebas.
