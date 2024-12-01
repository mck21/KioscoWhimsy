# TPV Kiosco Whimsy

<div align="center">
  <img src="https://github.com/mck21/KioscoWhimsy/blob/master/Recursos/Imagenes/whimsyHeader.png"/>
</div>
<br>
# [English](https://github.com/mck21/KioscoWhimsy/blob/master/README_en.md)

<br><br>

Este proyecto es una solución para la gestión de ventas, inventarios, usuarios y clientes de un kiosco. Desarrollado como proyecto final del Ciclo Superior de Desarrollo de Aplicaciones Multiplataforma, implementa el patrón de diseño MVVM y tecnologías como WPF y .NET.
Para una descripción más detallada y con imágenes visita la [Documentación completa](https://github.com/mck21/KioscoWhimsy/blob/master/WhimsyDoc.pdf)

## Índice

1. [Descripción del Proyecto](#descripción-del-proyecto)
2. [Características](#características)
3. [Stack Tecnológico](#stack-tecnológico)
4. [Estructura del Proyecto](#estructura-del-proyecto)
5. [Guía de Instalación](#guía-de-instalación)
6. [Contribuciones](#contribuciones)
7. [Licencia](#licencia)

## Descripción del Proyecto

El proyecto TPV Kiosco Whimsy cubre las necesidades de un kiosco de ventas, permitiendo la gestión de ventas, inventarios, y usuarios. La aplicación optimiza las tareas del día a día en un entorno de venta al por menor.

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
```.
├── Backend
│   ├── Modelos
│   └── Servicios
├── Frontend
│   ├── Charts
│   ├── ControlUsuario
│   ├── Dialogos
│   └── Login
├── Recursos
│   ├── GIF
│   ├── Iconos
│   └── Imagenes
└── ViewModels
```

## Guía de Instalación

1. **Requisitos previos**:
   - .NET Framework y Visual Studio 2022.
   - MySQL y MySQL Workbench para la gestión de base de datos.
  
2. **Instalación**:
   - Clona el repositorio: 
     ```bash
     git clone https://github.com/mck21/KioscoWhimsy.git
     ```
   - Crea la base de datos en MySQL Workbench con este script: [kiosco.sql](https://github.com/mck21/KioscoWhimsy/blob/master/kiosco.sql).
   - Configura la base de datos en `KioscoContext.cs` en el proyecto.
   - Inicia la aplicación desde Visual Studio.

## Contribuciones

Las contribuciones son bienvenidas. Para mejorar este proyecto:

1. Realiza un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-feature`).
3. Haz commit de tus cambios (`git commit -am 'Agrego nueva feature'`).
4. Envía un pull request.

## Licencia

Este proyecto está bajo la licencia MIT.

