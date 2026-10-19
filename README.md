Gestión de Pacientes
Aplicación de escritorio utilizada por el personal de recepción, que podrá acceder rápidamente a cada una de estas opciones y consultar la información del paciente.

🚀 Funcionalidades Principales
Permite consultar un turno, ingresando el DNI del paciente para verificar la fecha, horario y estudio correspondiente.

Permite sacar un turno, seleccionando el estudio y uno de los horarios disponibles para el paciente.

Permite consultar y registrar el retiro de un resultado, verificando previamente si el estudio ya se encuentra disponible.

🛠️ Tecnologías y Arquitectura
Lenguaje y Entorno: C# .NET (Windows Forms).

Base de Datos: Local

ORM: Entity Framework Core (Npgsql).

Patrones: Arquitectura basada en interfaces (IGestorDeAtencion) para garantizar el desacoplamiento entre la vista, la lógica de negocio y la capa de persistencia.
