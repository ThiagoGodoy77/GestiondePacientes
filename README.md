# 🏥 Gestión de Pacientes

**Autores:** Bortoluzzi Fernando , Godoy Thiago

Metodologías de la Programación II
**Profesor:** Botta Christian

Aplicación de escritorio desarrollada en **C# y .NET con Windows Forms**, pensada para el personal de recepción de un centro médico. Permite consultar turnos, reservar nuevos, registrar el retiro de resultados y ver estadísticas de la demanda, todo desde una interfaz centralizada, sencilla e intuitiva. Los datos se guardan en una base SQLite local, que se conservan entre ejecuciones.

## 🚀 Funcionalidades principales

- **🔎 Consulta de turnos:** Búsqueda por DNI para ver la fecha, el horario y el estudio de los turnos próximos del paciente, ordenados del más cercano al más lejano, en una ventana dedicada.
- **📅 Reserva de turnos:** Elección del estudio, de la fecha desde un selector y de un horario disponible de la agenda (de 8:00 a 18:00, en bloques de 30 minutos). El sistema no permite reservar horarios ocupados ni fechas pasadas. Si el paciente es nuevo, se registra automáticamente; si el DNI ya existe con otro nombre, la reserva se rechaza para evitar errores de carga.
- **📋 Gestión de resultados:** Consulta de los resultados pendientes de retiro y registro de su entrega cuando están disponibles. Un resultado no puede entregarse dos veces ni antes de estar listo.
- **📊 Estadísticas:** Desde el encabezado de la ventana principal, muestra la cantidad de turnos reservados por estudio médico.

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET 10
- **Interfaz gráfica:** Windows Forms (WinForms)
- **Persistencia y ORM:** Entity Framework Core con SQLite
- **Tecnología de consulta:** LINQ
- **Pruebas automatizadas:** xUnit
- **Herramientas de compilación y gestión:** .NET CLI / MSBuild
- **Control de versiones:** Git y GitHub

## 🏗️ Arquitectura y diseño

El código está dividido en tres capas, cada una en su propia carpeta dentro de `WinFormsApp1`, para separar las responsabilidades y facilitar el mantenimiento:

- **`CentroMedico.Data`** — acceso a datos:
  - `Entidades/` — las clases del dominio: `Paciente`, `Estudio`, `Turno` y `Resultado`.
  - `Datos/` — `CentroMedicoContext`, que administra el acceso a la base SQLite mediante Entity Framework Core.
- **`CentroMedico.Business`** — lógica del negocio:
  - `Interfaces/` — el contrato `IGestorDeAtencion`, que evita que la interfaz dependa de una implementación concreta.
  - `Servicios/` — `GestorDeAtencion`, que centraliza las reglas del centro: agenda, reservas, consultas, resultados y estadísticas.
- **`CentroMedico.WinForms`** — presentación:
  - `Forms/` — `Form1` (ventana principal), `ConsultaTurnosForm`, `SeleccionarFechaForm`, `EstadisticasForm` y `PinForm`.
  - `Program.cs` — punto de entrada de la aplicación.

Las pantallas solo se comunican con la lógica a través de `IGestorDeAtencion`.

La documentación adicional (diagramas de secuencia y de clases) se encuentra en la carpeta `docs/`.

## 🎯 Objetivo del proyecto

Desarrollar una herramienta que centralice las operaciones habituales de recepción médica, simplifique el acceso a la información y mejore la organización de los turnos y la entrega de resultados.

## ▶️ Ejecución del proyecto

**Requisitos previos**

- Windows y un entorno compatible con Windows Forms.
- SDK de .NET compatible con la versión del proyecto.

**Pasos para ejecutar**

Desde una terminal:

```bash
git clone https://github.com/ThiagoGodoy77/GestiondePacientes
cd GestiondePacientes
dotnet run --project WinFormsApp1
```

`dotnet run` restaura las dependencias, compila y abre la aplicación. La primera vez, se crea la base de datos local (`gestionpacientes.db`) automáticamente 

**Desde Visual Studio:** abrir el archivo de la solución (`.sln`), dejar `WinFormsApp1` como proyecto de inicio y presioná **F5**.


## ✅ Pruebas automatizadas

El repositorio incluye el proyecto `GestionPacientes.Tests`, con pruebas unitarias en xUnit que cubren:

- **Dominio:** Las clases `Paciente`, `Estudio`, `Turno` y `Resultado`, incluidas sus validaciones y el ciclo de vida del retiro de resultados.
- **Lógica de negocio (`GestorDeAtencion`):** La agenda (horarios válidos, ocupados, fechas pasadas y límites de atención), la consulta de turnos por DNI, el registro de resultados y de su retiro, y las estadísticas por estudio.
- **Validación del DNI:** Normalización (puntos, guiones y espacios) y control de que tenga 7 u 8 dígitos y solo números.

Cada prueba cubre un Caso Normal, un Caso Borde o un Caso de Error. Las pruebas que usan la base de datos parten de una base vacía y la eliminan al terminar (se ejecutan una por vez para no interferir entre sí).

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

---

**Proyecto desarrollado con C# y .NET.**