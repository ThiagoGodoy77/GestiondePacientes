# 🏥 Gestión de Pacientes

**Autores:** Godoy Thiago, Bortoluzzi Fernando

**Materia:** Metodologías de la Programación II
**Profesor:** Botta Christian

Aplicación de escritorio desarrollada en **C# y .NET con Windows Forms**, diseñada para facilitar las tareas del personal de recepción de un centro médico. Permite gestionar consultas de turnos, reservas y retiro de resultados desde una interfaz centralizada, sencilla e intuitiva.

## 🚀 Funcionalidades principales

- **🔎 Consulta de turnos:** búsqueda por DNI para consultar la fecha, el horario y el estudio asociado al paciente, con el detalle de todos sus turnos próximos en una ventana dedicada.
- **📅 Reserva de turnos:** selección de estudios, elección de fecha desde un calendario y asignación de horarios disponibles, con registro o actualización de los datos del paciente.
- **📋 Gestión de resultados:** consulta del estado de los resultados médicos y registro de su retiro cuando se encuentran disponibles.

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET
- **Interfaz gráfica:** Windows Forms (WinForms)
- **Persistencia y ORM:** Entity Framework Core con SQLite
- **Tecnología de consulta:** LINQ
- **Pruebas automatizadas:** xUnit
- **Herramientas de compilación y gestión:** .NET CLI / MSBuild
- **Control de versiones:** Git y GitHub

## 🏗️ Arquitectura y diseño

El proyecto utiliza una separación de responsabilidades para facilitar el mantenimiento y la evolución del sistema, organizada en carpetas según el rol de cada componente:

- **`Entidades/`** — las clases del dominio: `Paciente`, `Estudio`, `Turno` y `Resultado`.
- **`Interfaces/`** — el contrato `IGestorDeAtencion`, que evita que la vista dependa directamente de una implementación concreta.
- **`Servicios/`** — `GestorDeAtencion`, la clase que centraliza las operaciones principales del sistema.
- **`Datos/`** — `CentroMedicoContext`, que administra el acceso a los datos mediante Entity Framework Core.
- **Formularios** (`Form1`, `ConsultaTurnosForm`, `SeleccionarFechaForm`) — la interfaz gráfica, que solo se comunica con la lógica de negocio a través de `IGestorDeAtencion`.

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

2. Ingresar al directorio del proyecto, donde se encuentra el archivo `WinFormsApp1.csproj`.

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

## ✅ Pruebas automatizadas

El repositorio incluye el proyecto `GestionPacientes.Tests`, con pruebas unitarias de las clases del dominio (`Paciente`, `Estudio`, `Turno`, `Resultado`) y de la lógica de negocio (`GestorDeAtencion`), cubriendo casos normales, casos borde y casos de error.

**Pasos para ejecutarlas**

Parado en la carpeta que contiene la solución (donde están las carpetas `WinFormsApp1` y `GestionPacientes.Tests`):

1. Restaurar las dependencias del proyecto de pruebas:

   ```bash
   dotnet restore GestionPacientes.Tests/GestionPacientes.Tests.csproj
   ```

2. Correr las pruebas:

   ```bash
   dotnet test GestionPacientes.Tests/GestionPacientes.Tests.csproj
   ```

3. (Opcional) Ver el detalle de cada prueba ejecutada, no solo el resumen final:

   ```bash
   dotnet test GestionPacientes.Tests/GestionPacientes.Tests.csproj --logger "console;verbosity=detailed"
   ```

Si todo sale bien, el resultado final muestra algo como:

```
Passed! - Failed: 0, Passed: 32, Skipped: 0
```

## 🎯 Objetivo del proyecto

Desarrollar una herramienta que centralice las operaciones habituales de recepción médica, simplifique el acceso a la información y mejore la organización de los turnos y la entrega de resultados.

---

**Proyecto desarrollado con C# y .NET.**