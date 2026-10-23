# 🏥 Gestión de Pacientes

Aplicación de escritorio desarrollada en **C# y .NET con Windows Forms**, diseñada para facilitar las tareas del personal de recepción de un centro médico. Permite gestionar consultas de turnos, reservas y retiro de resultados desde una interfaz centralizada, sencilla e intuitiva.

## 🚀 Funcionalidades principales

- **🔎 Consulta de turnos:** búsqueda por DNI para consultar la fecha, el horario y el estudio asociado al paciente.
- **📅 Reserva de turnos:** selección de estudios y asignación de horarios disponibles, con registro o actualización de los datos del paciente.
- **📋 Gestión de resultados:** consulta del estado de los resultados médicos y registro de su retiro cuando se encuentran disponibles.

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET
- **Interfaz gráfica:** Windows Forms
- **Persistencia:** base de datos local SQLite
- **ORM:** Entity Framework Core
- **Control de versiones:** Git y GitHub

## 🏗️ Arquitectura y diseño

El proyecto utiliza una separación de responsabilidades para facilitar el mantenimiento y la evolución del sistema.

La interfaz gráfica se comunica con la lógica de negocio mediante la abstracción `IGestorDeAtencion`, evitando que la vista dependa directamente de una implementación concreta.

La clase `GestorDeAtencion` centraliza las operaciones principales, mientras que `CentroMedicoContext` administra el acceso a los datos mediante Entity Framework Core.

Esta organización permite:

- Reducir el acoplamiento entre la interfaz y la lógica de negocio.
- Centralizar las operaciones de gestión de pacientes, turnos y resultados.
- Facilitar las modificaciones y la incorporación de nuevas funcionalidades.
- Mantener los datos almacenados localmente entre ejecuciones.

## ▶️ Ejecución del proyecto

**Requisitos previos**

- Windows y un entorno compatible con Windows Forms.
- SDK de .NET compatible con la versión del proyecto.
- Dependencias de Entity Framework Core configuradas.

**Pasos para ejecutar**

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/ThiagoGodoy77/GestiondePacientes
   ```

2. Ingresar al directorio del proyecto, donde se encuentra el archivo `.csproj`.

3. Restaurar las dependencias:

   ```bash
   dotnet restore
   ```

4. Compilar la aplicación:

   ```bash
   dotnet build
   ```

5. Ejecutar el proyecto:

   ```bash
   dotnet run
   ```

La base de datos local se inicializa según la configuración del proyecto.

## 🎯 Objetivo del proyecto

Desarrollar una herramienta que centralice las operaciones habituales de recepción médica, simplifique el acceso a la información y mejore la organización de los turnos y la entrega de resultados.

## 🔮 Posibles mejoras futuras

- Incorporación de pruebas automatizadas.
- Ampliación de las validaciones de datos y reglas de negocio.
- Mejora de la gestión de disponibilidad horaria.
- Evolución hacia una arquitectura que permita integrar clientes web o móviles.

---

**Proyecto desarrollado con C# y .NET.**