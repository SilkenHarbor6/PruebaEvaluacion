using PruebaEvaluacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaEvaluacion.Repository
{
    public class RArea : ICrudGeneral<Area>
    {
        private readonly EvaluacionContext db;
        public RArea()
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
            var oArea = db.Areas.Find(id);
            if (oArea == null)
            {
                return false;
            }
            db.Areas.Remove(oArea);
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Se retorna toda la tabla area como una lista
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Area>> GetAll()
        {
            return db.Areas.ToList();
        }
        /// <summary>
        /// Se busca el objeto por su id y se retorna
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Area> GetById(int id)
        {
            return await db.Areas.FindAsync(id);
        }
        /// <summary>
        /// Se valia que el objeto no sea nulo
        /// Se inserta en la db y se guardan cambios
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Area> Post(Area item)
        {
            if (item == null)
            {
                return default(Area);
            }
            await db.Areas.AddAsync(item);
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
        public Area Put(Area item)
        {
            var oArea = db.Areas.Find(item.IdArea);
            if (oArea == null)
            {
                return default(Area);
            }
            oArea.Nombre = item.Nombre;
            oArea.Descripcion = item.Descripcion;
            db.Entry(oArea).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return oArea;
        }
    }
}
