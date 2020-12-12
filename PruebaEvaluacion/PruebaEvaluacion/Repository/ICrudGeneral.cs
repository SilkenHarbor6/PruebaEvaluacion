using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaEvaluacion.Repository
{
    interface ICrudGeneral<T>
    {
        /// <summary>
        /// Esta interfaz es generica ya que se reutiliza en todos los repositorios para obtener los datos de la db
        /// algunos metodos son task porque se realizan de manera asincronica
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Post(T item);
        T Put(T item);
        bool Delete(int id);
    }
}
