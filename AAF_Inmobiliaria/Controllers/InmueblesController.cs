using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAF_Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AAF_Inmobiliaria.Controllers
{
    [Authorize]
    public class InmueblesController : Controller
    {

        private readonly IConfiguration configuracion;
        private readonly RepositorioInmueble repositorioInmueble;
        private readonly RepositorioPropietario repositorioPropietario;
        public InmueblesController(IConfiguration configuration)
        {
            this.configuracion = configuration;
            repositorioInmueble = new RepositorioInmueble(configuration);
            repositorioPropietario = new RepositorioPropietario(configuration);
        }

        // GET: Inmuebles
        public ActionResult Index(String dni)
        {
            dni = dni ?? "";
                var lista = repositorioInmueble.ObtenerInmueblePorDni(dni);
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
            ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                
                int res = repositorioInmueble.Alta(inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
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