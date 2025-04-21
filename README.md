# README.md

## Descripción del Proyecto

Este proyecto implementa una aplicación web con arquitectura limpia que consta de una API RESTful, un servicio de identidad y un frontend Angular. 

## Estructura del Proyecto

### 1. API Principal (`/src/API/Challenge.API`)

- **Arquitectura**: Clean Architecture con patrón CQRS
- **Persistencia**: Entity Framework en memoria (los datos se pierden al reiniciar el servicio)
- **Seguridad**: Todos los endpoints están protegidos con JWT y control basado en roles
- **Documentación**: Swagger integrado
- **Tecnologías utilizadas**:
 - .NET Core 8
 - Autofac para inyección de dependencias
 - MediatR para implementación de CQRS
 - Pruebas unitarias, de dominio e integrales

### 2. Servicio de Identidad (`/src/API/Identity`)

- **Framework**: Identity Server Duende en .NET Core
- **Autenticación**: Por usuario y contraseña
- **Credenciales predeterminadas**:
 - Usuario: `admin`
 - Contraseña: `admin123`
- **Funcionalidad**: Genera tokens JWT para autenticación con la API principal

### 3. Frontend (`/src/front`)

- **Framework**: Angular 19
- **Bibliotecas UI**:
 - NgPrime para componentes
 - PrimeFlex para estilos
- **Funcionalidades**:
 - Autenticación y cierre de sesión
 - Bandeja de órdenes
 - Creación, actualización y eliminación de órdenes

## Ejecución del Proyecto

Se proporciona un script PowerShell `run-all.ps1` para ejecutar todo el proyecto de manera conjunta.

### Requisitos previos

- .NET Core 8 SDK
- Node.js v22.14.0

### Instrucciones

1. Asegúrese de tener instalados todos los requisitos previos
2. Ejecute el script `run-all.ps1` desde PowerShell
3. Acceda a la aplicación a través del navegador
4. Inicie sesión con las credenciales predeterminadas

## Notas Adicionales

- La base de datos se ejecuta en memoria, por lo que todos los datos se perderán al reiniciar la aplicación
- La API está completamente documentada con Swagger para facilitar su uso
- La seguridad está implementada mediante JWT y roles de usuario