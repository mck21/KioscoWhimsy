# TPV Kiosco Whimsy

**Proyecto de software para la gestión de un terminal de punto de venta (TPV)**

Este proyecto es una solución para la gestión de ventas, inventarios, usuarios y clientes de un kiosco. Desarrollado como proyecto final del Ciclo Superior de Desarrollo de Aplicaciones Multiplataforma, implementa el patrón de diseño MVVM y tecnologías como WPF y .NET.
Para una descripción más detallada y con imágenes visita la [Documentación completa](https://github.com/mck21/KioscoWhimsy/blob/master/WhimsyDoc.pdf)

## Índice

1. [Descripción del Proyecto](#descripción-del-proyecto)
2. [Características](#características)
3. [Stack Tecnológico](#stack-tecnológico)
4. [Estructura del Proyecto](#estructura-del-proyecto)
5. [Base de Datos](#base-de-datos)
6. [Guía de Instalación](#guía-de-instalación)
7. [Manual de Usuario](#manual-de-usuario)
8. [Contribuciones](#contribuciones)
9. [Licencia](#licencia)

## Descripción del Proyecto

El proyecto TPV Kiosco Whimsy está diseñado para satisfacer las necesidades de un kiosco de ventas, permitiendo la gestión de ventas, inventarios, y usuarios. La aplicación, de fácil manejo e interfaz amigable, optimiza las tareas del día a día en un entorno de venta al por menor.

## Características

- **Gestión de ventas y tickets**: Registro de ventas, generación de tickets en PDF.
- **Inventario**: Permite añadir, editar y eliminar productos en stock.
- **Usuarios y permisos**: Sistema de roles para controlar el acceso a las funcionalidades.
- **Reportes visuales**: Gráficos de ventas por mes.
- **Personalización**: Temas oscuros con diseño moderno y estilizado.

## Stack Tecnológico

![MySQL](https://img.shields.io/badge/mysql-%23007ACC.svg?style=for-the-badge&logo=mysql&logoColor=white)
![CSharp](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![XAML](https://img.shields.io/badge/xaml-%230C54C2.svg?style=for-the-badge&logo=xaml&logoColor=white)


- **Librerías y herramientas**:
  - **EntityFrameworkCore**: ORM para el mapeo de MySQL a las entidades del proyecto.
  - **NLog**: Registro de logs en consola.
  - **ITextSharp**: Generación de PDF.
  - **LiveCharts**: Visualización gráfica de datos.
  - **MaterialDesignThemes y Mahapps**: Personalización y estilo de ventanas.

## Estructura del Proyecto

El proyecto se divide en las siguientes carpetas:

- **Backend**: Lógica de negocio y servicios, modelos de la base de datos.
- **Frontend**: Interfaz de usuario, gráficos, controles y diálogos.
- **Recursos**: Iconos e imágenes.
- **ViewModels**: Enlaza los modelos y las vistas, siguiendo el patrón MVVM.

## Guía de Instalación

1. **Requisitos previos**:
   - .NET Framework y Visual Studio 2022.
   - MySQL y MySQL Workbench para la gestión de base de datos.
  
2. **Instalación**:
   - Clona el repositorio: 
     ```bash
     git clone https://github.com/mck21/KioscoWhimsy.git
     ```
   - Configura la base de datos en `KioscoContext.cs` en el proyecto.
   - Ejecuta las migraciones de EntityFramework para configurar las tablas en MySQL.
   - Inicia la aplicación desde Visual Studio.

## Manual de Usuario

- **Inicio de sesión**: Inicia sesión con un usuario registrado (user: admin, password: admin).
- **Pantalla de ventas**: Selecciona productos, ajusta cantidades y confirma la venta.
- **Inventario**: Administra productos en stock.
- **Gestión de usuarios**: Asigna roles y gestiona permisos (solo para administradores).

## Contribuciones

Las contribuciones son bienvenidas. Para mejorar este proyecto:

1. Realiza un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-feature`).
3. Haz commit de tus cambios (`git commit -am 'Agrego nueva feature'`).
4. Envía un pull request.

## Licencia

Este proyecto está bajo la licencia MIT.

