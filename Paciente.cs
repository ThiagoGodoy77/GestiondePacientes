using System;
namespace GestionPacientes
{
    public class Paciente
    {
    //PRIVADOS PARA EL ENCAPSULAMIENTO:
    private string dni;
    private string nombre;
    private string apellido;

    //El gestor asigna valores al crear nuevo paciente

    public Paciente(string dni,string nombre,string apellido)
    {
        if (string.IsNullOrWhiteSpace(dni))
        throw new ArgumentException("El DNI no puede estar vacio");

        this.dni = dni;
        this.nombre=nombre;
        this.apellido=apellido;
    }

    //para consultar

    public string GetDni()
    {
        return dni;
    }
    public string GetNombreCompleto()
    {
        return $"{nombre} {apellido}";
    }

    }
}
