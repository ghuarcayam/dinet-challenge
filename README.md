# README

Este proyecto es una implementación de un servicio API en .NET Core (última versión) que ofrece funcionalidades para la gestión de Orderos. La solución sigue una arquitectura hexagonal utilizando CQRS con DDD, incorporando un pipeline de ejecución para validar requests y logear los comandos ejecutados. Además, se implementa el patrón UnitOfWork para gestionar las transacciones de manera efectiva.

## Requisitos

### 1. Crear un rest API en .NET Core
La aplicación está desarrollada en .NET Core, utilizando las últimas versiones disponibles al momento de la creación.

### 1.2. Documentar la API usando Swagger
La documentación de la API se encuentra generada mediante Swagger, facilitando la comprensión y prueba de las distintas operaciones disponibles. Puedes acceder a la documentación navegando a `/swagger/index.html` una vez que el proyecto esté en ejecución.

### 1.3. Usar patrones
La implementación sigue patrones como CQRS, DDD, y el patrón UnitOfWork para asegurar una estructura robusta y mantenible.

### 1.4. Aplicar principios SOLID y Clean Code
Se han aplicado los principios SOLID y se ha buscado mantener un código limpio y legible para facilitar el mantenimiento y comprensión del proyecto.

### 1.5. Implementar la solución haciendo uso de TDD
El desarrollo ha seguido la metodología de Desarrollo Dirigido por Pruebas (TDD), garantizando la fiabilidad y robustez de la aplicación.

### 1.6. Usar buenos patrones para las validaciones del Request y considerar HTTP Status Codes
Se han implementado patrones efectivos para la validación de las solicitudes (requests) y se han manejado adecuadamente los códigos de estado HTTP en cada respuesta.

### 1.7. Estructurar el proyecto en N-capas
La aplicación sigue una estructura de N-capas para mejorar la organización y separación de responsabilidades.

### 1.8. Agregar un archivo README (README.md)
Este archivo proporciona información detallada sobre la estructura del proyecto, los patrones utilizados y los pasos para levantar la aplicación localmente.

### 1.9. Subir el proyecto a GitHub de manera pública
El código fuente se encuentra disponible en el repositorio público de GitHub [aquí](https://github.com/ghuarcayam/challenge-Dinet.git).

## Funcionalidades Implementadas

### 2.1. Operaciones CRUD de maestro de Orderos
- **Insert (POST):** Agrega un nuevo Ordero.
- **Update (PUT):** Modifica la información de un Ordero existente.
- **GetById (GET):** Obtiene la información detallada de un Ordero por su identificador.

### 2.2. Registro de tiempo de respuesta
El tiempo de respuesta de cada solicitud se registra en un archivo de texto plano para su análisis y seguimiento.

### 2.3. Caché de estados del Ordero
Se mantiene en caché un diccionario de estados del Ordero con una duración de 5 minutos. Esto mejora el rendimiento al obtener los nombres de estado directamente desde la caché.

### 2.4. Persistencia local de la información del Ordero
La información del Ordero se guarda localmente utilizando Entity Framework con una persistencia en memoria para facilitar la ejecución.

### 2.5. Detalles del Ordero en el Método GetById
El método GetById retorna información detallada del Ordero, incluyendo campos adicionales como `Discount` y `FinalPrice`.

## Pasos para Levantar el Proyecto Localmente

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/ghuarcayam/challenge-Dinet.git
1. **Abra la solucion con visual studio 2022:**
    Asegurece tener instalado netcore 8, ejecute la aplicacion (F5f)