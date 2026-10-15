using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace GestionPacientesUI
{
    public class Estudio
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Profesional { get; private set; }
        public string Asistente { get; private set; }

        public Estudio(int id, string nombre, string descripcion, string profesional, string asistente)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del estudio no puede estar vacio.");
            if (string.IsNullOrWhiteSpace(profesional))
                throw new ArgumentException("El nombre del profesional no puede estar vacio.");
            if (string.IsNullOrWhiteSpace(asistente))
                throw new ArgumentException("El nombre del asistente no puede estar vacio.");

            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Profesional = profesional;
            Asistente = asistente;
        }
    }
}