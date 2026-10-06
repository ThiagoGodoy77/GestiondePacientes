using system;
namespace GestionPacientes
{
    public class Estudio
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string profesional;
        private string asistente;

        public Estudio(int id, string nombre, string descripcion, string profesional, string asistente)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del estudio no puede estar vacio");
            if (string.IsNullOrWhiteSpace(profesional))
                throw new ArgumentException("El nombre del profesional no puede estar vacio");
            if (string.IsNullOrWhiteSpace(asistente))
                throw new ArgumentException("El nombre del asistente no puede estar vacio");

            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.profesional = profesional;
            this.asistente = asistente;
        }

        public string GetNombre()
        {
            return nombre;
        }
    
        public string GetProfesional()
        {
            return profesional;
        }
        
    }
}