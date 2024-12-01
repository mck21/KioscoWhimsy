# POS Whimsy Kiosk 

<div align="center">
  <img src="https://github.com/mck21/KioscoWhimsy/blob/master/Recursos/Imagenes/whimsyHeader.png"/>
</div>
<br>

This project is a solution for managing sales, inventory, users, and customers for a kiosk. Developed as the final project for the Cross-Platform Application Development program, it implements the MVVM design pattern and technologies such as WPF and .NET.  
For a more detailed description with screenshots, visit the [Complete Documentation](https://github.com/mck21/KioscoWhimsy/blob/master/WhimsyDoc.pdf) (only available in spanish)

## Table of Contents

1. [Project Description](#project-description)
2. [Features](#features)
3. [Tech Stack](#tech-stack)
4. [Project Structure](#project-structure)
5. [Installation Guide](#installation-guide)
6. [Contributions](#contributions)
7. [License](#license)

## Project Description

The Whimsy Kiosk POS project meets the needs of a sales kiosk, enabling the management of sales, inventory, and users. The application streamlines daily tasks in a retail environment.

## Features

- **Sales and ticket management**: Records sales and generates tickets in PDF format.
- **Inventory**: Allows adding, editing, and deleting products in stock.
- **Users and permissions**: Role-based system to control access to features.
- **Visual reports**: Sales charts by month.
- **Customization**: Modern, sleek design with dark themes.

## Tech Stack

![MySQL](https://img.shields.io/badge/mysql-%23007ACC.svg?style=for-the-badge&logo=mysql&logoColor=white)
![CSharp](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![XAML](https://img.shields.io/badge/xaml-%230C54C2.svg?style=for-the-badge&logo=xaml&logoColor=white)

- **Libraries and tools**:
  - **EntityFrameworkCore**: ORM for mapping MySQL to project entities.
  - **NLog**: Console log management.
  - **ITextSharp**: PDF generation.
  - **LiveCharts**: Data visualization.
  - **MaterialDesignThemes and Mahapps**: Window customization and styling.

## Project Structure
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

## Installation Guide

1. **Prerequisites**:
   - .NET Framework and Visual Studio 2022.
   - MySQL and MySQL Workbench for database management.
  
2. **Installation**:
   - Clone the repository: 
     ```bash
     git clone https://github.com/mck21/KioscoWhimsy.git
     ```
   - Create the database in MySQL Workbench using this script: [kiosco.sql](https://github.com/mck21/KioscoWhimsy/blob/master/kiosco.sql).
   - Configure the database in `KioscoContext.cs` in the project.
   - Run the application from Visual Studio.

## Contributions

Contributions are welcome. To improve this project:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/new-feature`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Submit a pull request.

## License

This project is licensed under the MIT License.
