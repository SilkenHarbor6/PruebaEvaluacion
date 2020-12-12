using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PruebaEvaluacion.Models;
using PruebaEvaluacion.Repository;

namespace PruebaEvaluacion.Pages.EmployeeList
{
    public class EditEmployeeModel : PageModel
    {
        #region Atributos
        /// <summary>
        /// Se definen las clases que contienen la logica de negocios con la db
        /// </summary>
        private readonly ICrudGeneral<Empleado> db;
        private readonly ICrudGeneral<Area> dbArea;
        #endregion
        #region Propiedades
        /// <summary>
        /// se definen los elementos a utilizar, en este caso
        /// Un objeto empleado que es el que se actualiza
        /// el id del jefe y el id del area
        /// </summary>
        [BindProperty]
        public Empleado Employee { get; set; }
        [BindProperty]
        public int SelectedBoss { get; set; }
        [BindProperty]
        public int SelectedArea { get; set; }
        public IEnumerable<Empleado> Employees { get; set; }
        public IEnumerable<Area> Areas { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Se instancian las clases que contienen la logica de negocios y se llama el metodo populateList
        /// </summary>
        public EditEmployeeModel()
        {
            db = new REmpleado();
            dbArea = new RArea();
            PopulateLists();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// En el metodo populate list se cargan los select de los empleados y de areas
        /// </summary>
        private async void PopulateLists()
        {
            Employees = await db.GetAll();
            Areas = await dbArea.GetAll();
        }
        /// <summary>
        /// En el metodo OnGet se obtiene el empleado por medio del id incrustado en la url
        /// se setea el objeto seleccionado del select (id del jefe)
        /// se setea el objeto seleccionado del select (id del area)
        /// </summary>
        /// <param name="id"></param>
        public async void OnGet(int id)
        {
            Employee = await db.GetById(id);
            SelectedArea = (int)Employee.IdArea;
            if (Employee.IdJefe != null)
            {
                SelectedBoss = (int)Employee.IdJefe;
            }
            else
            {
                SelectedBoss = 0;
            }

        }
        /// <summary>
        /// En el metodo OnPost se arma el objeto de nuevo
        /// se asignan los id del jefe y del area
        /// se realiza el metodo Put en la db
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Employee.IdArea = SelectedArea;
                Employee.IdJefe = SelectedBoss;
                db.Put(Employee);
                return RedirectToPage("Index");
            }
            return Page();
        } 
        #endregion
    }
}
