using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PruebaEvaluacion.Models;
using PruebaEvaluacion.Repository;

namespace PruebaEvaluacion.Pages.AreaList
{
    public class EditAreaModel : PageModel
    {
        #region Atributos
        /// <summary>
        /// Definicion de la clase encargada de la logica de la db
        /// </summary>
        private readonly ICrudGeneral<Area> db;
        #endregion
        #region Propiedades
        /// <summary>
        /// Objeto area que contiene la informacion del elemento a modificar
        /// Objeto idArea que contiene el id del objeto que se actualizara, se obtiene el id por medio de la url
        /// </summary>
        [BindProperty]
        public Area area { get; set; }
        [BindProperty]
        public int idArea { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Se instancia la clase encargada de la logica de la db
        /// </summary>
        public EditAreaModel()
        {
            db = new RArea();
        }
        #endregion
        #region Metodos
        /// <summary>
        /// En el metodo OnGet se obtiene el id por la url, se almacena en la variable y se obtiene el objeto por medio del metodo GetById()
        /// </summary>
        /// <param name="id"></param>
        public async void OnGet(int id)
        {
            idArea = id;
            area = await db.GetById(idArea);
        }
        /// <summary>
        /// En el metodo OnPost se obtienen los datos almacenados en los input y se arma el objeto area para aplicar el metodo Put en la db
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            area.IdArea = idArea;
            if (ModelState.IsValid)
            {
                db.Put(area);
                return RedirectToPage("Index");
            }
            return Page();
        } 
        #endregion
    }
}
