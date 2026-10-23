using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public class CentroMedicoContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Estudio> Estudios { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Resultado> Resultados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gestionpacientes.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>().HasKey(p => p.Dni);
            modelBuilder.Entity<Estudio>().HasKey(e => e.Id);
            modelBuilder.Entity<Turno>().HasKey(t => t.Id);
            modelBuilder.Entity<Resultado>().HasKey(r => r.Id);
        }
    }
}