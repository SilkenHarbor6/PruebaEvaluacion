using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PruebaEvaluacion.Models;
using PruebaEvaluacion.Repository;

namespace PruebaEvaluacion.Pages.AreaList
{
    public class CreateAreaModel : PageModel
    {
        #region Atributos
        /// <summary>
        /// Definicion de la clase encargada de la logica de db
        /// </summary>
        private readonly ICrudGeneral<Area> db;
        #endregion
        #region Propiedades
        /// <summary>
        /// Se define el objeto area que sera registrado
        /// </summary>
        [BindProperty]
        public Area oArea { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Se instancia la clase que contiene la logica de la db
        /// </summary>

        public CreateAreaModel()
        {
            db = new RArea();
        }
        #endregion
        #region Metodos
        public void OnGet()
        {
        }
        /// <summary>
        /// Metodo OnPost se encarga de validar el modelo del area a registrar
        /// si todo esta bien, registra el area en la db
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var resp = await db.Post(oArea);
                if (resp == default(Area))
                {
                    Debug.Print("Hubo un error al registrar el area");
                }
                return RedirectToPage("Index");
            }
            return Page();
        } 
        #endregion
    }
}
