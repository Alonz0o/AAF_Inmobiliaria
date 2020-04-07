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
    public class PropietariosController : Controller
    {
        private readonly IConfiguration configuracion;        private readonly RepositorioPropietario repositorioPropietario;        public PropietariosController(IConfiguration configuration)        {            this.configuracion = configuration;            repositorioPropietario = new RepositorioPropietario(configuration);        }

        // GET: Propietarios
        public ActionResult Index()
        {
            var lista = repositorioPropietario.ObtenerTodos();
            return View(lista);
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        {
            try
            {
                // TODO: Add insert logic here
                repositorioPropietario.Alta(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            var p = repositorioPropietario.ObtenerPorId(id);
            return View(p);
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Propietario p = null;

            try
            {
                // TODO: Add update logic here
                p = repositorioPropietario.ObtenerPorId(id);                p.Nombre = collection["Nombre"];                p.Apellido = collection["Apellido"];                p.Dni = collection["Dni"];                p.Email = collection["Email"];                p.Telefono = collection["Telefono"];                repositorioPropietario.Modificacion(p);                TempData["Mensaje"] = "Datos guardados correctamente";                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)            {                ViewBag.Error = ex.Message;                ViewBag.StackTrate = ex.StackTrace;                return View(p);            }

        } 

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            var p = repositorioPropietario.ObtenerPorId(id);
            return View(p);
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario p)
        {
            try
            {
                // TODO: Add delete logic here
                repositorioPropietario.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                return View(p);
            }
        }
    }
}