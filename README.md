# 🏥 Gestión de Pacientes

**Autores:** Bortoluzzi Fernando, Godoy Thiago  
**Materia:** Metodologías de la Programación II  
**Profesor:** Botta Christian

Aplicación de escritorio desarrollada en **C# y .NET 10 con Windows Forms**, diseñada para optimizar la atención diaria en la recepción de un centro médico. Centraliza en una única pantalla principal la consulta de turnos, la asignación de nuevas citas, el registro de retiro de resultados y la visualización de estadísticas de demanda. Los datos persisten de forma local mediante **SQLite y Entity Framework Core**, conservándose entre ejecuciones.

## 🚀 Funcionalidades principales

- **🔎 Consulta de turnos (`ConsultaTurnosForm`):** Búsqueda por DNI (con normalización automática de puntos, guiones y espacios) para visualizar en una ventana dedicada los próximos turnos del paciente ordenados cronológicamente, detallando fecha, horario, estudio médico, profesional responsable y asistente a cargo.
- **📅 Reserva de turnos (`Form1` y `SeleccionarFechaForm`):** Selección del estudio médico en la pantalla principal, elección de la fecha mediante el calendario (`SeleccionarFechaForm`) y asignación de una franja horaria disponible (de `08:00` a `18:00`, en intervalos de 30 minutos). El sistema filtra automáticamente los horarios ocupados y bloquea fechas pasadas o fuera del rango de atención. Si el paciente es nuevo, se registra automáticamente dentro de la misma transacción; si el DNI ya existe con otro nombre o apellido, la reserva se rechaza para evitar errores de carga.
- **📋 Gestión y retiro de resultados:** Consulta de informes pendientes a partir del DNI del paciente y registro formal de su entrega. Antes de concretar el retiro, el sistema verifica tres condiciones: que el turno ya haya transcurrido, que el informe esté disponible y que no haya sido entregado con anterioridad. Además, despliega un cuadro de diálogo de confirmación con los datos completos del paciente, el estudio y la fecha de atención antes de actualizar su estado en la base de datos.
- **📊 Estadísticas de demanda (`PinForm` y `EstadisticasForm`):** Accesible desde el encabezado de la ventana principal y protegido mediante un **PIN de administración** (con caracteres enmascarados) para restringir su uso al personal autorizado. Una vez validado el acceso, muestra el conteo total de turnos reservados agrupados por estudio médico, manejando de forma segura los casos sin registros.

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET 10 
- **Interfaz gráfica:** Windows Forms (WinForms)
- **Persistencia y ORM:** Entity Framework Core con SQLite 
- **Tecnología de consulta:** LINQ
- **Pruebas automatizadas:** xUnit
- **Herramientas de compilación y gestión:** .NET CLI / MSBuild

## 🏗️ Arquitectura y diseño

El código está dividido en tres capas dentro de `WinFormsApp1` para separar responsabilidades y facilitar el mantenimiento, acompañadas por un proyecto independiente de pruebas:

- **`CentroMedico.Data`** — acceso a datos y dominio:
  - `Entidades/` — clases del dominio (`Paciente`, `Estudio`, `Turno` y `Resultado`) implementadas bajo programación defensiva (propiedades con *setters* privados, validaciones en constructores y control de estados).
  - `Datos/` — `CentroMedicoContext`, que administra el mapeo relacional y el acceso a la base de datos SQLite mediante Entity Framework Core.
- **`CentroMedico.Business`** — lógica de negocio:
  - `Interfaces/` — contrato `IGestorDeAtencion`, que desacopla la interfaz gráfica de la implementación concreta.
  - `Servicios/` — `GestorDeAtencion`, que centraliza las reglas del centro: cálculo de agenda disponible, reservas transaccionales, consultas por DNI, entrega segura de resultados y estadísticas.
- **`CentroMedico.WinForms`** — presentación:
  - `Forms/` — `Form1` (ventana principal), `ConsultaTurnosForm`, `SeleccionarFechaForm`, `PinForm` y `EstadisticasForm`.
  - `Program.cs` — punto de entrada de la aplicación.
- **`GestionPacientes.Tests`** — proyecto de pruebas unitarias e integración automatizadas en xUnit.

Las pantallas se comunican exclusivamente con la lógica de negocio a través de `IGestorDeAtencion`. La documentación adicional (diagramas de clases y de secuencia) se encuentra en la carpeta `docs/`.

## 🎯 Objetivo del proyecto

Desarrollar una herramienta que centralice las operaciones habituales de recepción médica, simplifique el acceso a la información y garantice la consistencia en la gestión de turnos y la entrega de resultados.

## ▶️ Ejecución del proyecto

### Requisitos previos

- Windows y un entorno compatible con Windows Forms.
- SDK de .NET 10 instalado.

### Pasos para ejecutar

Desde una terminal:

```bash
git clone [https://github.com/ThiagoGodoy77/GestiondePacientes.git](https://github.com/ThiagoGodoy77/GestiondePacientes.git)
cd GestiondePacientes
dotnet run --project WinFormsApp1