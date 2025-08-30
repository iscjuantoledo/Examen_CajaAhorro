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
