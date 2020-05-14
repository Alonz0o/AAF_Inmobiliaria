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
    public class AlquileresController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioAlquiler repositorioAlquiler;
        private readonly RepositorioInquilino repositorioInquilino;
        private readonly RepositorioInmueble repositorioInmueble;
        public AlquileresController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioAlquiler = new RepositorioAlquiler(configuration);
            repositorioInquilino = new RepositorioInquilino(configuration);
            repositorioInmueble = new RepositorioInmueble(configuration);
        }
        // GET: Alquileres
        public ActionResult Index()
        {
            var lista = repositorioAlquiler.ObtenerTodos();
            return View(lista);
        }
        // GET: Alquileres/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Alquileres/Create
        public ActionResult Create()
        {
            ViewBag.Inmuebless = repositorioInmueble.ObtenerTodos();
            ViewBag.Inquilinoss = repositorioInquilino.ObtenerTodos();
            return View();
        }
        // POST: Alquileres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alquiler alquiler)
        {
            try
            {
                repositorioAlquiler.Alta(alquiler);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Alquileres/Edit/5
        public ActionResult Edit(int id)
        {
            var al = repositorioAlquiler.ObtenerPorId(id);
            ViewBag.Inmuebless = repositorioInmueble.ObtenerTodos();
            ViewBag.Inquilinoss = repositorioInquilino.ObtenerTodos();
            return View(al);
        }
        // POST: Alquileres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Alquiler alquiler)
        {
            try
            {
                repositorioAlquiler.Modificacion(alquiler);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Alquileres/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: Alquileres/Delete/5
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