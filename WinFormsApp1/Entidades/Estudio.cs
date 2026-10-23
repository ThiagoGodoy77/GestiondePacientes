using System;

namespace WinFormsApp1
{
    public class Estudio
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Profesional { get; private set; }
        public string Asistente { get; private set; }

        private Estudio() { }

        public Estudio(string nombre, string descripcion, string profesional, string asistente)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre del estudio no puede estar vacío.");
            Nombre = nombre.Trim();
            Descripcion = descripcion?.Trim();
            Profesional = profesional?.Trim();
            Asistente = asistente?.Trim();
        }
    }
}