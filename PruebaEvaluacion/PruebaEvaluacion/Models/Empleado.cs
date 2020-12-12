using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaEvaluacion.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            EmpleadoHabilidads = new HashSet<EmpleadoHabilidad>();
            InverseIdJefeNavigation = new HashSet<Empleado>();
        }

        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? IdJefe { get; set; }
        public int? IdArea { get; set; }
        public string Foto { get; set; }

        public virtual Area IdAreaNavigation { get; set; }
        public virtual Empleado IdJefeNavigation { get; set; }
        public virtual ICollection<EmpleadoHabilidad> EmpleadoHabilidads { get; set; }
        public virtual ICollection<Empleado> InverseIdJefeNavigation { get; set; }

        public int CalcularRango(DateTime FechaEvaluada)
        {
            return DateTime.Today.Year - FechaEvaluada.Year;
        }
    }
}
