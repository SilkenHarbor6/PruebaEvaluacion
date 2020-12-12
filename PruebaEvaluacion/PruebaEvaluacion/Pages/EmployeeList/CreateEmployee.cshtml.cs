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
    public class CreateEmployeeModel : PageModel
    {
        #region Atributos
        /// <summary>
        /// Se definen las clases que contienen la logica de la db
        /// </summary>
        private readonly ICrudGeneral<Empleado> dbEmployee;
        private readonly ICrudGeneral<Area> dbArea;

        #endregion
        #region Propiedades
        /// <summary>
        /// Se definen las listas con la informacion de los empleados que pueden ser jefes y las areas disponibles
        /// Se definen las variables encargadas de almacenar el id del jefe y el id del area
        /// </summary>
        [BindProperty]
        public Empleado Employee { get; set; }
        public IEnumerable<Empleado> Employees { get; set; }
        public IEnumerable<Area> Areas { get; set; }
        [BindProperty]
        public int SelectedArea { get; set; }
        [BindProperty]
        public int SelectedBoss { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Se instancian las clases con la logica de db
        /// </summary>
        public CreateEmployeeModel()
        {
            dbEmployee = new REmpleado();
            dbArea = new RArea();
        }
        #endregion
        #region Metodos
        /// <summary>
        /// En el metodo OnGet se obtienen todos los empleados y todas las areas disponibles
        /// </summary>
        public async void OnGet()
        {
            Employees = await dbEmployee.GetAll();
            Areas = await dbArea.GetAll();
        }
        /// <summary>
        /// En el metodo OnPost se valida que el objeto este completo, si el selected boss es 0, significa que no hay ningun empleado registrado, por lo que se deja nulo
        /// si el selected boss es distinto de 0 se le asigna al objeto empleado
        /// Se le asigna el id del area a la que pertenece
        /// y se realiza el post
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (SelectedBoss != 0)
                {
                    Employee.IdJefe = SelectedBoss;
                }
                Employee.IdArea = SelectedArea;
                await dbEmployee.Post(Employee);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        } 
        #endregion
    }
}
