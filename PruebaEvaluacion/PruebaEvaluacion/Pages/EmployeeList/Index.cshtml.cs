using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PruebaEvaluacion.Models;
using PruebaEvaluacion.Repository;

namespace PruebaEvaluacion.Pages.EmployeeList
{
    public class IndexModel : PageModel
    {
        #region Atributos
        /// <summary>
        /// Se definen las clases que poseen la logica de la db
        /// </summary>
        private readonly ICrudGeneral<Empleado> db;
        private readonly ICrudGeneral<Area> dbAreas;
        #endregion
        #region Propiedades
        /// <summary>
        /// Se definen las listas a usar, en este caso son:
        /// Una lista para los empleados
        /// Una lista para las areas
        /// un objeto de tipo int que almacena el id del area seleccionada en el  <select>
        /// </summary>
        [BindProperty]
        public IEnumerable<Empleado> ActualEmployees { get; set; }
        [BindProperty]
        public IEnumerable<Area> Areas { get; set; }
        [BindProperty]
        public int SelectedArea { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Se instancian las clases encargadas de la logica de negocios con la db
        /// </summary>
        public IndexModel()
        {
            db = new REmpleado();
            dbAreas = new RArea();
        }
        #endregion
        #region Handlers
        /// <summary>
        /// En el metodo OnGet se obtienen todos los empleados para ser mostrados en la tabla
        /// se obtienen todas las areas para ser mostradas en el select
        /// se setea el indice seleccionado como 0
        /// </summary>
        public async void OnGet()
        {
            ActualEmployees = await db.GetAll();
            Areas = await dbAreas.GetAll();
            SelectedArea = 0;
        }
        /// <summary>
        /// En el metodo OnPostDelete se obtiene el id por url del elemento que se desea eliminar
        /// se realiza la peticion a la db
        /// en caso de que sea exitosa nos recarga la pagina actual
        /// de lo contrario, muestra un error de not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var response = db.Delete(id);
            if (!response)
            {
                return NotFound();
            }
            return RedirectToPage("Index");
        }
        /// <summary>
        /// Metodo OnPostFilterList  es el encargado de filtrar todos los empleados por area
        /// Primero obtiene los empleados registrados
        /// Luego con Linq se filtran todos aquellos que el idArea sea igual al indice seleccionado del select
        /// Por ultimo se guardan en los empleados actuales que se muestran en la tabla
        /// </summary>
        public async void OnPostFilterList()
        {
            var Employees = await db.GetAll();
            var FilterEmployees = from employee in Employees
                                  where employee.IdArea.Equals(SelectedArea)
                                  select employee;
            ActualEmployees = FilterEmployees;
            Areas = await dbAreas.GetAll();
        }
        /// <summary>
        /// Metodo OnPostCleanFilter se encarga de limpiar el filtrado y recargar todos los emplados
        /// </summary>
        public async void OnPostCleanFilter()
        {
            ActualEmployees = await db.GetAll();
            Areas = await dbAreas.GetAll();
        } 
        #endregion
    }
}
