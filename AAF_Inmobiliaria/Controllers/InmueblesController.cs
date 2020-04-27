using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAF_Inmobiliaria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AAF_Inmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {

        private readonly IConfiguration configuracion;
        private readonly RepositorioInmueble repositorioInmueble;
        public InmueblesController(IConfiguration configuration)
        {
            this.configuracion = configuration;
            repositorioInmueble = new RepositorioInmueble(configuration);
        }

        // GET: Inmuebles
        public ActionResult Index()
        {
            int id = int.Parse(@User.FindFirst("PropietarioId").Value);
            var lista = repositorioInmueble.ObtenerTodosDelPropietario(id);
            return View(lista);
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                int id = int.Parse(@User.FindFirst("PropietarioId").Value);
                inmueble.PropietarioId = id;
                int res = repositorioInmueble.Alta(inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}