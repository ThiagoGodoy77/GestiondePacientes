#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1;
using WinFormsApp1.Datos;
using Xunit;

// Evita la ejecución paralela para que las pruebas de SQLite no interfieran entre sí
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace GestionPacientes.Tests
{
    // =========================================================================
    // 1. PACIENTE
    // =========================================================================
    public class PacienteTests
    {
        [Fact]
        public void CasoNormal_CrearYActualizarDatos_GuardaLosCambiosCorrectamente()
        {
            var paciente = new Paciente("12345678", "Juan", "Perez");
            paciente.ActualizarDatosPersonales("Carlos", "Gomez");

            Assert.Equal("12345678", paciente.Dni);
            Assert.Equal("Carlos Gomez", paciente.NombreCompleto);
        }

        [Fact]
        public void CasoBorde_ApellidoVacio_SePermiteCrearElPaciente()
        {
            // NOTA: la única validación obligatoria del paciente es el DNI (ver caso error).
            // El nombre y el apellido no son obligatorios, así que un apellido vacío
            // es un valor límite válido, no un error. Si al correrlo falla, es señal
            // de que Paciente sí exige apellido y hay que ajustar este caso borde.
            var paciente = new Paciente("12345678", "Juan", "");

            Assert.Equal("", paciente.Apellido);
        }

        [Fact]
        public void CasoError_DniVacio_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Paciente("   ", "Juan", "Perez"));
        }
    }

    // =========================================================================
    // 2. ESTUDIO
    // =========================================================================
    public class EstudioTests
    {
        [Fact]
        public void CasoNormal_CrearEstudio_GuardaTodosLosDatosCorrectamente()
        {
            var estudio = new Estudio("Radiografia", "RX de torax", "Dr. Lopez", "Asist. Marta");

            Assert.Equal("Radiografia", estudio.Nombre);
            Assert.Equal("RX de torax", estudio.Descripcion);
            Assert.Equal("Dr. Lopez", estudio.Profesional);
            Assert.Equal("Asist. Marta", estudio.Asistente);
        }

        [Fact]
        public void CasoBorde_DescripcionVacia_SePermiteCrearElEstudio()
        {
            var estudio = new Estudio("Ecografia", "", "Dra. Gomez", "Asist. Luis");

            Assert.Equal("", estudio.Descripcion);
        }

        [Fact]
        public void CasoError_NombreVacio_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Estudio("", "RX", "Dr. Lopez", "Asist. Marta"));
        }
    }

    // =========================================================================
    // 3. TURNO
    // =========================================================================
    public class TurnoTests
    {
        private static readonly Paciente Pac = new Paciente("12345678", "Juan", "Perez");
        private static readonly Estudio Est = new Estudio("Radiografia", "RX", "Dr. Lopez", "Asist. Marta");

        [Fact]
        public void CasoNormal_CrearTurno_AsignaFechaPacienteYEstudioCorrectamente()
        {
            var turno = new Turno(DateTime.Today.AddHours(10), Pac, Est);

            Assert.Equal(DateTime.Today.AddHours(10), turno.FechaHora);
            Assert.Equal(Pac, turno.Paciente);
            Assert.Equal(Est, turno.Estudio);
        }

        [Fact]
        public void CasoBorde_FechaPasada_SePermiteCrearElTurnoDirectamente()
        {
            // NOTA: quien rechaza fechas pasadas es GestorDeAtencion.SacarTurno(), no el
            // propio Turno (así lo usa el gestor para guardar resultados de turnos ya
            // ocurridos). Por eso crear un Turno "a mano" con una fecha de ayer es un
            // caso límite válido para esta clase.
            var turno = new Turno(DateTime.Today.AddDays(-1), Pac, Est);

            Assert.Equal(DateTime.Today.AddDays(-1), turno.FechaHora);
        }

        [Fact]
        public void CasoError_PacienteNulo_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Turno(DateTime.Now, null, Est));
        }
    }

    // =========================================================================
    // 4. RESULTADO
    // =========================================================================
    public class ResultadoTests
    {
        private static readonly Paciente Pac = new Paciente("12345678", "Juan", "Perez");
        private static readonly Estudio Est = new Estudio("Radiografia", "RX", "Dr. Lopez", "Asist. Marta");

        private static Resultado NuevoResultado(bool disponible) =>
            new Resultado(new Turno(DateTime.Today.AddHours(10), Pac, Est), disponible);

        [Fact]
        public void CasoNormal_MarcarComoRetirado_CambiaElEstadoCorrectamente()
        {
            var resultado = NuevoResultado(disponible: true);

            resultado.MarcarComoRetirado();

            Assert.True(resultado.Retirado);
        }

        [Fact]
        public void CasoBorde_ResultadoReciénCreado_TodaviaNoEstaRetirado()
        {
            var resultado = NuevoResultado(disponible: false);

            Assert.False(resultado.Disponible);
            Assert.False(resultado.Retirado);
        }

        [Fact]
        public void CasoError_MarcarComoRetiradoDosVeces_LanzaInvalidOperationException()
        {
            var resultado = NuevoResultado(disponible: true);
            resultado.MarcarComoRetirado();

            Assert.Throws<InvalidOperationException>(() => resultado.MarcarComoRetirado());
        }

        [Fact]
        public void CasoError_MarcarComoRetiradoSinEstarDisponible_LanzaInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => NuevoResultado(disponible: false).MarcarComoRetirado());
        }
    }

    // =========================================================================
    // 5. GESTOR DE ATENCION (integración con base de datos SQLite)
    // =========================================================================
    public class GestorDeAtencionTests
    {
        private readonly GestorDeAtencion _gestor;
        private readonly Paciente _paciente;
        private readonly Estudio _estudio;

        public GestorDeAtencionTests()
        {
            using (var context = new CentroMedicoContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                _estudio = new Estudio("Radiografia de Torax", "RX", "Dr. Lopez", "Asist. Marta");
                context.Estudios.Add(_estudio);
                context.SaveChanges();
            }

            _gestor = new GestorDeAtencion();
            _paciente = new Paciente("12345678", "Juan", "Perez");
            _gestor.RegistrarPaciente(_paciente);
        }

        private static DateTime Manana(int hora, int minutos = 0) =>
            DateTime.Today.AddDays(1).AddHours(hora).AddMinutes(minutos);

        private int GuardarResultadoDirecto(int dias, bool disponible, bool retirado = false)
        {
            using var context = new CentroMedicoContext();
            var paciente = context.Pacientes.First(p => p.Dni == _paciente.Dni);
            var estudio = context.Estudios.First(e => e.Id == _estudio.Id);
            var turno = new Turno(DateTime.Today.AddDays(dias).AddHours(9), paciente, estudio);

            var resultado = new Resultado(turno, disponible);
            if (retirado)
                resultado.MarcarComoRetirado();

            context.Resultados.Add(resultado);
            context.SaveChanges();
            return resultado.Id;
        }

        // ---------------------------------------------------------------------
        // PACIENTES
        // ---------------------------------------------------------------------

        [Fact]
        public void CasoNormal_RegistrarPaciente_ActualizaDatosYAceptaDniConPuntos()
        {
            _gestor.RegistrarPaciente(new Paciente("12345678", "Carlos", "Gomez"));

            Assert.Equal("Carlos Gomez", _gestor.ConsultarPaciente("12.345.678").NombreCompleto);
        }

        [Fact]
        public void CasoBorde_ConsultarPacienteInexistente_DevuelveNull()
        {
            Assert.Null(_gestor.ConsultarPaciente("99999999"));
        }

        // ---------------------------------------------------------------------
        // TURNOS Y AGENDA
        // ---------------------------------------------------------------------

        [Fact]
        public void CasoNormal_SacarTurno_ConPacienteExistenteYNuevo()
        {
            Turno turno = _gestor.SacarTurno(_paciente, _estudio, Manana(10));
            _gestor.SacarTurno(new Paciente("87654321", "Maria", "Gomez"), _estudio, Manana(11));

            Assert.Equal(Manana(10), turno.FechaHora);
            Assert.Equal("Maria Gomez", _gestor.ConsultarPaciente("87654321").NombreCompleto);
        }

        [Fact]
        public void CasoNormal_SacarTurnoConEstudio_RegistraElNuevoYNoDuplicaElDeMismoNombre()
        {
            var nuevo = new Estudio("Electrocardiograma", "ECG", "Dr. Ruiz", "Asist. Ana");
            var mismoNombre = new Estudio("Radiografia de Torax", "RX", "Dr. Lopez", "Asist. Marta");

            _gestor.SacarTurno(_paciente, nuevo, Manana(10));
            _gestor.SacarTurno(_paciente, mismoNombre, Manana(11));

            using var context = new CentroMedicoContext();
            Assert.Equal(2, context.Estudios.Count());
        }

        [Theory]
        [InlineData(-1, 10, 0)]   // Fecha pasada
        [InlineData(1, 7, 30)]    // Antes de abrir (08:00)
        [InlineData(1, 18, 0)]    // Hora de cierre
        [InlineData(1, 10, 15)]   // Fuera de bloque de 30 min
        public void CasoBorde_SacarTurnoEnHorarioLimite_LanzaInvalidOperationException(int dias, int hora, int minutos)
        {
            var fechaHora = DateTime.Today.AddDays(dias).AddHours(hora).AddMinutes(minutos);

            Assert.Throws<InvalidOperationException>(() => _gestor.SacarTurno(_paciente, _estudio, fechaHora));
        }

        [Fact]
        public void CasoError_SacarTurnoEnHorarioOcupado_LanzaInvalidOperationException()
        {
            _gestor.SacarTurno(_paciente, _estudio, Manana(10));

            Assert.Throws<InvalidOperationException>(() => _gestor.SacarTurno(_paciente, _estudio, Manana(10)));
        }

        [Fact]
        public void CasoError_SacarTurnoSinPacienteOSinEstudio_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _gestor.SacarTurno(null, _estudio, Manana(10)));
            Assert.Throws<ArgumentNullException>(() => _gestor.SacarTurno(_paciente, null, Manana(10)));
        }

        [Fact]
        public void CasoNormal_ObtenerHorariosDisponibles_DiaLibreTieneVeinteTurnosYExcluyeLosOcupados()
        {
            List<DateTime> libres = _gestor.ObtenerHorariosDisponibles(Manana(0));
            _gestor.SacarTurno(_paciente, _estudio, Manana(8));
            List<DateTime> conOcupado = _gestor.ObtenerHorariosDisponibles(Manana(0));

            Assert.Equal(20, libres.Count);
            Assert.Equal(Manana(17, 30), libres.Last());
            Assert.DoesNotContain(Manana(8), conOcupado);
            Assert.Contains(Manana(8, 30), conOcupado);
        }

        [Fact]
        public void CasoNormal_ConsultarTurnos_DevuelveSoloLosDelPacienteOrdenadosYSinLosPasados()
        {
            _gestor.SacarTurno(_paciente, _estudio, Manana(14));
            _gestor.SacarTurno(_paciente, _estudio, Manana(9));
            _gestor.SacarTurno(new Paciente("87654321", "Maria", "Gomez"), _estudio, Manana(10));
            GuardarResultadoDirecto(dias: -1, disponible: true);

            List<Turno> turnos = _gestor.ConsultarTurnos("12.345.678");

            Assert.Equal(new[] { Manana(9), Manana(14) }, turnos.Select(t => t.FechaHora));
        }

        [Fact]
        public void CasoBorde_ConsultarTurnosConDniInexistente_DevuelveListaVacia()
        {
            Assert.Empty(_gestor.ConsultarTurnos("abc"));
        }

        [Fact]
        public void CasoNormal_ObtenerTurnosPendientesDelDia_FiltraYOrdenaCronologicamente()
        {
            var fechaManana = DateTime.Today.AddDays(1);
            _gestor.SacarTurno(_paciente, _estudio, fechaManana.AddHours(15));
            _gestor.SacarTurno(_paciente, _estudio, fechaManana.AddHours(9));

            List<Turno> pendientes = _gestor.ObtenerTurnosPendientesDelDia(fechaManana);

            Assert.Equal(2, pendientes.Count);
            Assert.Equal(fechaManana.AddHours(9), pendientes[0].FechaHora);
            Assert.Equal(fechaManana.AddHours(15), pendientes[1].FechaHora);
        }

        // ---------------------------------------------------------------------
        // RESULTADOS
        // ---------------------------------------------------------------------

        [Fact]
        public void CasoNormal_RegistrarResultado_QuedaGuardadoEnLaBaseDeDatos()
        {
            Turno turno = _gestor.SacarTurno(_paciente, _estudio, Manana(10));

            _gestor.RegistrarResultado(new Resultado(turno, disponible: true));

            using var context = new CentroMedicoContext();
            Assert.Equal(1, context.Resultados.Count());
        }

        [Fact]
        public void CasoError_RegistrarResultadoDuplicado_LanzaInvalidOperationException()
        {
            Turno turno = _gestor.SacarTurno(_paciente, _estudio, Manana(10));
            _gestor.RegistrarResultado(new Resultado(turno, disponible: true));

            Assert.Throws<InvalidOperationException>(() => _gestor.RegistrarResultado(new Resultado(turno, true)));
        }

        [Fact]
        public void CasoNormal_ConsultarResultadoPendiente_EstudioYaRealizado()
        {
            int id = GuardarResultadoDirecto(dias: -1, disponible: true);

            Resultado pendiente = _gestor.ConsultarResultadoPendientePorDni("12.345.678");

            Assert.Equal(id, pendiente.Id);
            Assert.Equal("12345678", pendiente.TurnoAsociado.Paciente.Dni);
        }

        [Theory]
        [InlineData(1, true, false)]   // el estudio es mañana: todavía no se hizo
        [InlineData(-1, true, true)]   // ya fue retirado
        [InlineData(-1, false, false)] // el resultado no está disponible
        public void CasoBorde_ConsultarResultadoPendiente_CasosQueNoEstanPendientes(int dias, bool disponible, bool retirado)
        {
            GuardarResultadoDirecto(dias, disponible, retirado);

            Assert.Null(_gestor.ConsultarResultadoPendientePorDni("12345678"));
        }

        [Fact]
        public void CasoNormal_RegistrarRetiro_PorDniYPorId_DejaDePendienteElResultado()
        {
            GuardarResultadoDirecto(dias: -1, disponible: true);
            Assert.True(_gestor.RegistrarRetiroResultado("12345678"));
            Assert.Null(_gestor.ConsultarResultadoPendientePorDni("12345678"));

            int otroId = GuardarResultadoDirecto(dias: -2, disponible: true);
            Assert.True(_gestor.RegistrarRetiroResultado(otroId));
            Assert.Null(_gestor.ConsultarResultadoPendientePorDni("12345678"));
        }

        [Fact]
        public void CasoError_RegistrarRetiroSinResultadoPendiente_LanzaKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _gestor.RegistrarRetiroResultado("12345678"));
            Assert.Throws<KeyNotFoundException>(() => _gestor.RegistrarRetiroResultado(9999));
        }

        [Fact]
        public void CasoError_RegistrarRetiroConDniVacio_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _gestor.RegistrarRetiroResultado("   "));
        }

        // ---------------------------------------------------------------------
        // ESTADÍSTICAS
        // ---------------------------------------------------------------------

        [Fact]
        public void CasoNormal_ObtenerEstadisticasTurnosPorEstudio_DevuelveConteoCorrecto()
        {
            var ecografia = new Estudio("Ecografia", "ECO", "Dra. Paz", "Luis");
            _gestor.SacarTurno(_paciente, _estudio, Manana(9));
            _gestor.SacarTurno(_paciente, _estudio, Manana(10));
            _gestor.SacarTurno(_paciente, ecografia, Manana(11));

            Dictionary<string, int> estadisticas = _gestor.ObtenerEstadisticasTurnosPorEstudio();

            Assert.Equal(2, estadisticas["Radiografia de Torax"]);
            Assert.Equal(1, estadisticas["Ecografia"]);
        }

        [Fact]
        public void CasoBorde_ObtenerEstadisticasSinTurnos_DevuelveDiccionarioVacio()
        {
            Dictionary<string, int> estadisticas = _gestor.ObtenerEstadisticasTurnosPorEstudio();

            Assert.NotNull(estadisticas);
            Assert.Empty(estadisticas);
        }
    }

    // =========================================================================
    // UTILIDADES DE DNI (función auxiliar usada por GestorDeAtencion)
    // =========================================================================
    public class DniTests
    {
        [Theory]
        [InlineData("12345678", "12345678")]
        [InlineData("12.345.678", "12345678")]
        [InlineData("  12-345-678  ", "12345678")]
        public void CasoNormal_Normalizar_QuitaPuntosGuionesYEspacios(string ingresado, string esperado)
        {
            Assert.Equal(esperado, GestorDeAtencion.NormalizarDni(ingresado));
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("abc", "")]
        public void CasoBorde_NormalizarValorNoValido_DevuelveCadenaVacia(string ingresado, string esperado)
        {
            Assert.Equal(esperado, GestorDeAtencion.NormalizarDni(ingresado));
        }

        [Theory]
        [InlineData("1234567", true)]
        [InlineData("12345678", true)]
        public void CasoNormal_EsValido_AceptaSieteUOchoDigitos(string dni, bool esperado)
        {
            Assert.Equal(esperado, GestorDeAtencion.EsDniValido(dni));
        }

        [Theory]
        [InlineData("123456", false)]
        [InlineData("123456789", false)]
        public void CasoError_EsValido_RechazaCantidadDeDigitosIncorrecta(string dni, bool esperado)
        {
            Assert.Equal(esperado, GestorDeAtencion.EsDniValido(dni));
        }
    }
}