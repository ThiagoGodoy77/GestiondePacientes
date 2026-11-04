#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.CentroMedico.Business.Servicios;
using WinFormsApp1.CentroMedico.Data.Datos;
using WinFormsApp1.CentroMedico.Data.Entidades;
using Xunit;

// Evita la ejecución paralela para que las pruebas de SQLite no interfieran entre sí
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace GestionPacientes.Tests
{
    // =========================================================================
    // DATOS DE PRUEBA COMPARTIDOS
    // Cada test crea sus propias instancias: nada estático, nada compartido.
    // =========================================================================
    internal static class TestData
    {
        public const string DniPaciente = "12345678";
        public const string NombreEstudio = "Radiografia de Torax";

        public static Paciente NuevoPaciente(string dni = DniPaciente, string nombre = "Juan", string apellido = "Perez")
            => new Paciente(dni, nombre, apellido);

        public static Estudio NuevoEstudio(string nombre = NombreEstudio, string descripcion = "RX",
                                           string profesional = "Dr. Lopez", string asistente = "Asist. Marta")
            => new Estudio(nombre, descripcion, profesional, asistente);

        public static Turno NuevoTurno(DateTime? fechaHora = null)
            => new Turno(fechaHora ?? DateTime.Today.AddHours(10), NuevoPaciente(), NuevoEstudio());

        public static Resultado NuevoResultado(bool disponible)
            => new Resultado(NuevoTurno(), disponible);
    }

    // =========================================================================
    // 1. PACIENTE
    // =========================================================================
    public class PacienteTests
    {
        [Fact]
        public void CasoNormal_Crear_GuardaDniNombreYApellido()
        {
            var paciente = TestData.NuevoPaciente();

            Assert.Equal("12345678", paciente.Dni);
            Assert.Equal("Juan Perez", paciente.NombreCompleto);
        }

        [Fact]
        public void CasoNormal_ActualizarDatosPersonales_CambiaNombreYNoTocaElDni()
        {
            var paciente = TestData.NuevoPaciente();

            paciente.ActualizarDatosPersonales("Carlos", "Gomez");

            Assert.Equal("12345678", paciente.Dni);
            Assert.Equal("Carlos Gomez", paciente.NombreCompleto);
        }

        [Fact]
        public void CasoBorde_ApellidoVacio_SePermiteCrearElPaciente()
        {
            var paciente = TestData.NuevoPaciente(apellido: "");

            Assert.Equal("", paciente.Apellido);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CasoError_DniVacioOSoloEspacios_LanzaArgumentException(string dni)
        {
            Assert.Throws<ArgumentException>(() => TestData.NuevoPaciente(dni: dni));
        }
    }

    // =========================================================================
    // 2. ESTUDIO
    // =========================================================================
    public class EstudioTests
    {
        [Fact]
        public void CasoNormal_Crear_GuardaTodosLosDatos()
        {
            var estudio = TestData.NuevoEstudio("Radiografia", "RX de torax", "Dr. Lopez", "Asist. Marta");

            Assert.Equal("Radiografia", estudio.Nombre);
            Assert.Equal("RX de torax", estudio.Descripcion);
            Assert.Equal("Dr. Lopez", estudio.Profesional);
            Assert.Equal("Asist. Marta", estudio.Asistente);
        }

        [Fact]
        public void CasoBorde_DescripcionVacia_SePermiteCrearElEstudio()
        {
            var estudio = TestData.NuevoEstudio(descripcion: "");

            Assert.Equal("", estudio.Descripcion);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CasoError_NombreVacioOSoloEspacios_LanzaArgumentException(string nombre)
        {
            Assert.Throws<ArgumentException>(() => TestData.NuevoEstudio(nombre: nombre));
        }
    }

    // =========================================================================
    // 3. TURNO
    // =========================================================================
    public class TurnoTests
    {
        [Fact]
        public void CasoNormal_Crear_AsignaFechaPacienteYEstudio()
        {
            var paciente = TestData.NuevoPaciente();
            var estudio = TestData.NuevoEstudio();
            var fecha = DateTime.Today.AddHours(10);

            var turno = new Turno(fecha, paciente, estudio);

            Assert.Equal(fecha, turno.FechaHora);
            Assert.Same(paciente, turno.Paciente);
            Assert.Same(estudio, turno.Estudio);
        }

        [Fact]
        public void CasoBorde_FechaPasada_SePermiteCrearElTurnoDirectamente()
        {
            // La validación de fechas pasadas es responsabilidad del gestor, no de la entidad.
            var ayer = DateTime.Today.AddDays(-1);

            var turno = TestData.NuevoTurno(ayer);

            Assert.Equal(ayer, turno.FechaHora);
        }

        [Fact]
        public void CasoError_PacienteNulo_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Turno(DateTime.Now, null, TestData.NuevoEstudio()));
        }
    }

    // =========================================================================
    // 4. RESULTADO
    // =========================================================================
    public class ResultadoTests
    {
        [Fact]
        public void CasoNormal_MarcarComoRetirado_CambiaElEstado()
        {
            var resultado = TestData.NuevoResultado(disponible: true);

            resultado.MarcarComoRetirado();

            Assert.True(resultado.Retirado);
        }

        [Fact]
        public void CasoBorde_ResultadoRecienCreado_NoEstaRetirado()
        {
            var resultado = TestData.NuevoResultado(disponible: false);

            Assert.False(resultado.Disponible);
            Assert.False(resultado.Retirado);
        }

        [Fact]
        public void CasoError_MarcarComoRetiradoDosVeces_LanzaInvalidOperationException()
        {
            var resultado = TestData.NuevoResultado(disponible: true);
            resultado.MarcarComoRetirado();

            Assert.Throws<InvalidOperationException>(() => resultado.MarcarComoRetirado());
        }

        [Fact]
        public void CasoError_MarcarComoRetiradoSinEstarDisponible_LanzaInvalidOperationException()
        {
            var resultado = TestData.NuevoResultado(disponible: false);

            Assert.Throws<InvalidOperationException>(() => resultado.MarcarComoRetirado());
        }
    }

    // =========================================================================
    // 5. GESTOR DE ATENCION (integración con base de datos SQLite)
    // =========================================================================
    public class GestorDeAtencionTests : IDisposable
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

                _estudio = TestData.NuevoEstudio();
                context.Estudios.Add(_estudio);
                context.SaveChanges();
            }

            _gestor = new GestorDeAtencion();
            _paciente = TestData.NuevoPaciente();
            _gestor.RegistrarPaciente(_paciente);
        }

        // xUnit crea una instancia por test y llama a Dispose al terminar:
        // la base de prueba no queda en disco entre ejecuciones.
        public void Dispose()
        {
            using var context = new CentroMedicoContext();
            context.Database.EnsureDeleted();
        }

        // ---------------------------------------------------------------------
        // Helpers
        // ---------------------------------------------------------------------

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
        public void CasoNormal_RegistrarPacienteExistente_ActualizaSusDatos()
        {
            _gestor.RegistrarPaciente(TestData.NuevoPaciente(nombre: "Carlos", apellido: "Gomez"));

            Assert.Equal("Carlos Gomez", _gestor.ConsultarPaciente(TestData.DniPaciente).NombreCompleto);
        }

        [Fact]
        public void CasoNormal_ConsultarPaciente_AceptaDniConPuntos()
        {
            Paciente encontrado = _gestor.ConsultarPaciente("12.345.678");

            Assert.NotNull(encontrado);
            Assert.Equal(TestData.DniPaciente, encontrado.Dni);
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
        public void CasoNormal_SacarTurno_ConPacienteExistente_AsignaLaFechaPedida()
        {
            Turno turno = _gestor.SacarTurno(_paciente, _estudio, Manana(10));

            Assert.Equal(Manana(10), turno.FechaHora);
        }

        [Fact]
        public void CasoNormal_SacarTurno_ConPacienteNuevo_LoRegistraAutomaticamente()
        {
            _gestor.SacarTurno(TestData.NuevoPaciente("87654321", "Maria", "Gomez"), _estudio, Manana(11));

            Assert.Equal("Maria Gomez", _gestor.ConsultarPaciente("87654321").NombreCompleto);
        }

        [Fact]
        public void CasoNormal_SacarTurnoConEstudioNuevo_LoRegistra()
        {
            var nuevo = TestData.NuevoEstudio("Electrocardiograma", "ECG", "Dr. Ruiz", "Asist. Ana");

            _gestor.SacarTurno(_paciente, nuevo, Manana(10));

            using var context = new CentroMedicoContext();
            Assert.Equal(2, context.Estudios.Count());
        }

        [Fact]
        public void CasoNormal_SacarTurnoConEstudioDeMismoNombre_NoLoDuplica()
        {
            var mismoNombre = TestData.NuevoEstudio();

            _gestor.SacarTurno(_paciente, mismoNombre, Manana(11));

            using var context = new CentroMedicoContext();
            Assert.Single(context.Estudios);
        }

        [Theory]
        [InlineData(1, 8, 0)]    // Primer bloque del día
        [InlineData(1, 12, 30)]  // Bloque intermedio
        [InlineData(1, 17, 30)]  // Último bloque del día
        public void CasoBorde_SacarTurnoEnBloqueValido_NoLanzaExcepcion(int dias, int hora, int minutos)
        {
            var fechaHora = DateTime.Today.AddDays(dias).AddHours(hora).AddMinutes(minutos);

            Turno turno = _gestor.SacarTurno(_paciente, _estudio, fechaHora);

            Assert.Equal(fechaHora, turno.FechaHora);
        }

        [Theory]
        [InlineData(-1, 10, 0)]   // Fecha pasada
        [InlineData(1, 7, 30)]    // Antes de abrir (08:00)
        [InlineData(1, 18, 0)]    // Hora de cierre
        [InlineData(1, 10, 15)]   // Fuera de bloque de 30 min
        public void CasoBorde_SacarTurnoEnHorarioInvalido_LanzaInvalidOperationException(int dias, int hora, int minutos)
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
        public void CasoError_SacarTurnoSinPaciente_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _gestor.SacarTurno(null, _estudio, Manana(10)));
        }

        [Fact]
        public void CasoError_SacarTurnoSinEstudio_LanzaArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _gestor.SacarTurno(_paciente, null, Manana(10)));
        }

        [Fact]
        public void CasoNormal_ObtenerHorariosDisponibles_DiaLibreTieneVeinteBloquesDe0800A1730()
        {
            List<DateTime> libres = _gestor.ObtenerHorariosDisponibles(Manana(0));

            Assert.Equal(20, libres.Count);
            Assert.Equal(Manana(8), libres.First());
            Assert.Equal(Manana(17, 30), libres.Last());
        }

        [Fact]
        public void CasoNormal_ObtenerHorariosDisponibles_ExcluyeSoloElBloqueOcupado()
        {
            _gestor.SacarTurno(_paciente, _estudio, Manana(8));

            List<DateTime> conOcupado = _gestor.ObtenerHorariosDisponibles(Manana(0));

            Assert.Equal(19, conOcupado.Count);
            Assert.DoesNotContain(Manana(8), conOcupado);
            Assert.Contains(Manana(8, 30), conOcupado);
        }

        [Fact]
        public void CasoBorde_ObtenerHorariosDisponibles_DiaCompletoOcupado_DevuelveListaVacia()
        {
            for (int i = 0; i < 20; i++)
                _gestor.SacarTurno(_paciente, _estudio, Manana(8, 30 * i));

            Assert.Empty(_gestor.ObtenerHorariosDisponibles(Manana(0)));
        }

        [Fact]
        public void CasoNormal_ConsultarTurnos_DevuelveSoloLosDelPacienteYOrdenados()
        {
            _gestor.SacarTurno(_paciente, _estudio, Manana(14));
            _gestor.SacarTurno(_paciente, _estudio, Manana(9));
            _gestor.SacarTurno(TestData.NuevoPaciente("87654321", "Maria", "Gomez"), _estudio, Manana(10));

            List<Turno> turnos = _gestor.ConsultarTurnos("12.345.678");

            Assert.Equal(new[] { Manana(9), Manana(14) }, turnos.Select(t => t.FechaHora));
        }

        [Fact]
        public void CasoBorde_ConsultarTurnos_NoIncluyeLosTurnosPasados()
        {
            GuardarResultadoDirecto(dias: -1, disponible: true);
            _gestor.SacarTurno(_paciente, _estudio, Manana(9));

            List<Turno> turnos = _gestor.ConsultarTurnos(TestData.DniPaciente);

            Assert.Equal(new[] { Manana(9) }, turnos.Select(t => t.FechaHora));
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

        [Fact]
        public void CasoBorde_ObtenerTurnosPendientesDelDia_IgnoraLosTurnosDeOtrosDias()
        {
            _gestor.SacarTurno(_paciente, _estudio, Manana(9));
            _gestor.SacarTurno(_paciente, _estudio, Manana(9).AddDays(1));

            List<Turno> pendientes = _gestor.ObtenerTurnosPendientesDelDia(DateTime.Today.AddDays(1));

            Assert.Single(pendientes);
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
            Assert.Single(context.Resultados);
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

            Assert.NotNull(pendiente);
            Assert.Equal(id, pendiente.Id);
            Assert.Equal(TestData.DniPaciente, pendiente.TurnoAsociado.Paciente.Dni);
        }

        [Theory]
        [InlineData(1, true, false)]   // El estudio es mañana: todavía no se hizo
        [InlineData(-1, true, true)]   // Ya fue retirado
        [InlineData(-1, false, false)] // El resultado no está disponible
        public void CasoBorde_ConsultarResultadoPendiente_CasosQueNoEstanPendientes(int dias, bool disponible, bool retirado)
        {
            GuardarResultadoDirecto(dias, disponible, retirado);

            Assert.Null(_gestor.ConsultarResultadoPendientePorDni(TestData.DniPaciente));
        }

        [Fact]
        public void CasoNormal_RegistrarRetiroPorDni_DejaDeEstarPendiente()
        {
            GuardarResultadoDirecto(dias: -1, disponible: true);

            bool registrado = _gestor.RegistrarRetiroResultado(TestData.DniPaciente);

            Assert.True(registrado);
            Assert.Null(_gestor.ConsultarResultadoPendientePorDni(TestData.DniPaciente));
        }

        [Fact]
        public void CasoNormal_RegistrarRetiroPorId_DejaDeEstarPendiente()
        {
            int id = GuardarResultadoDirecto(dias: -2, disponible: true);

            bool registrado = _gestor.RegistrarRetiroResultado(id);

            Assert.True(registrado);
            Assert.Null(_gestor.ConsultarResultadoPendientePorDni(TestData.DniPaciente));
        }

        [Fact]
        public void CasoError_RegistrarRetiroPorDniSinResultadoPendiente_LanzaKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _gestor.RegistrarRetiroResultado(TestData.DniPaciente));
        }

        [Fact]
        public void CasoError_RegistrarRetiroPorIdInexistente_LanzaKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _gestor.RegistrarRetiroResultado(9999));
        }

        [Fact]
        public void CasoError_RegistrarRetiroConDniVacio_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _gestor.RegistrarRetiroResultado("   "));
        }

        // ---------------------------------------------------------------------
        // ESTADÍSTICAS (S5)
        // ---------------------------------------------------------------------

        [Fact]
        public void CasoNormal_ObtenerEstadisticasTurnosPorEstudio_DevuelveConteoCorrecto()
        {
            var ecografia = TestData.NuevoEstudio("Ecografia", "ECO", "Dra. Paz", "Luis");
            _gestor.SacarTurno(_paciente, _estudio, Manana(9));
            _gestor.SacarTurno(_paciente, _estudio, Manana(10));
            _gestor.SacarTurno(_paciente, ecografia, Manana(11));

            Dictionary<string, int> estadisticas = _gestor.ObtenerEstadisticasTurnosPorEstudio();

            Assert.Equal(2, estadisticas.Count);
            Assert.Equal(2, estadisticas[TestData.NombreEstudio]);
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
    // UTILIDADES DE DNI
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
        [InlineData(null)]
        [InlineData("abc")]
        public void CasoBorde_NormalizarValorNoValido_DevuelveCadenaVacia(string ingresado)
        {
            Assert.Equal("", GestorDeAtencion.NormalizarDni(ingresado));
        }

        [Theory]
        [InlineData("1234567", true)]    // 7 dígitos: límite inferior
        [InlineData("12345678", true)]   // 8 dígitos: límite superior
        [InlineData("123456", false)]    // uno menos que el mínimo
        [InlineData("123456789", false)] // uno más que el máximo
        [InlineData("", false)]          // supuesto: vacío no es válido (verificar contra EsDniValido)
        [InlineData("abcdefgh", false)]  // supuesto: letras no son válidas (verificar contra EsDniValido)
        public void EsDniValido_AceptaSoloSieteUOchoDigitos(string dni, bool esperado)
        {
            Assert.Equal(esperado, GestorDeAtencion.EsDniValido(dni));
        }
    }
}