using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public class Paciente
    {
        public string Dni { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";

        private Paciente() { }

        public Paciente(string dni, string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(dni)) throw new ArgumentException("El DNI no puede estar vacio.");
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
        }

        public void ActualizarDatosPersonales(string nuevoNombre, string nuevoApellido)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre) || string.IsNullOrWhiteSpace(nuevoApellido))
                throw new ArgumentException("El nombre y apellido no pueden estar vacios.");

            Nombre = nuevoNombre;
            Apellido = nuevoApellido;
        }
    }
}