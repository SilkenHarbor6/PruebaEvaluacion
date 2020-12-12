using Microsoft.EntityFrameworkCore;
using PruebaEvaluacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PruebaEvaluacion.Repository
{
    public class REmpleado : ICrudGeneral<Empleado>
    {
        private readonly EvaluacionContext db;
        public REmpleado()
        {
            db = new EvaluacionContext();
        }
        /// <summary>
        /// Primero se busca el objeto por su id, si existe se elimina de la db
        /// De lo contrario se retorna falso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var oEmpleado = db.Empleados.Find(id);
            if (oEmpleado == null)
            {
                return false;
            }
            db.Empleados.Remove(oEmpleado);
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Se retorna toda la tabla area como una lista
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Empleado>> GetAll()
        {
            IEnumerable<Empleado> Employees = db.Empleados.Include(p=> p.IdJefeNavigation).Include(p=>p.IdAreaNavigation).ToList();
            if (Employees==null)
            {
                Employees = default(IEnumerable<Empleado>);
            }
            return Employees;
        }
        /// <summary>
        /// Se busca el objeto por su id y se retorna
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Empleado> GetById(int id)
        {
            return await db.Empleados.FindAsync(id);
        }
        /// <summary>
        /// Se valia que el objeto no sea nulo
        /// Se inserta en la db y se guardan cambios
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public async Task<Empleado> Post(Empleado item)
        {
            if (item==null)
            {
                return default(Empleado);
            }
            await db.Empleados.AddAsync(item);
            db.SaveChanges();
            return item;
        }
        /// <summary>
        /// Se obtiene el objeto nuevo
        /// con su id se busca en la bd
        /// se actualizan todos sus atributos
        /// y se realiza el cambio
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Empleado Put(Empleado item)
        {
            var oEmpleado = db.Empleados.Find(item.IdEmpleado);
            if (oEmpleado==null)
            {
                return default(Empleado);
            }
            oEmpleado.NombreCompleto = item.NombreCompleto;
            oEmpleado.Cedula = item.Cedula;
            oEmpleado.Correo = item.Correo;
            oEmpleado.FechaNacimiento = item.FechaNacimiento;
            oEmpleado.FechaIngreso = item.FechaIngreso;
            oEmpleado.IdJefe = item.IdJefe;
            oEmpleado.IdArea = item.IdArea;
            oEmpleado.Foto = item.Foto;
            db.Entry(oEmpleado).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return oEmpleado;
        }
    }
}
