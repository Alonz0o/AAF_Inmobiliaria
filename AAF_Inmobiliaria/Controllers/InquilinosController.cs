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
    public class InquilinosController : Controller
    {
        private readonly IConfiguration configuracion;        private readonly RepositorioInquilino repositorioInquilino;        public InquilinosController(IConfiguration configuration)        {            this.configuracion = configuration;            repositorioInquilino = new RepositorioInquilino(configuration);        }
        // GET: Inquilinos
        public ActionResult Index()
        {
            var lista = repositorioInquilino.ObtenerTodos();
            return View(lista);
        }

        // GET: Inquilinos/Details/5
        public ActionResult Details(int id)
        {
            var i = repositorioInquilino.ObtenerPorId(id);
            return View(i);
        }

        // GET: Inquilinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino i)
        {
            try
            {
                // TODO: Add insert logic here
                repositorioInquilino.Alta(i);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            var i = repositorioInquilino.ObtenerPorId(id);
            return View(i);
        }
        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Inquilino i = null;
            try
            {
                // TODO: Add update logic here
                i = repositorioInquilino.ObtenerPorId(id);
                i.Nombre = collection["Nombre"];                i.Apellido = collection["Apellido"];                i.Dni = collection["Dni"];                i.Email = collection["Email"];                i.Telefono = collection["Telefono"];                repositorioInquilino.Modificacion(i);                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;                ViewBag.StackTrate = ex.StackTrace;
                return View(i);
            }
        }

        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
            var i = repositorioInquilino.ObtenerPorId(id);
            return View(i);
        }

        // POST: Inquilinos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino i)
        {
            try
            {
                // TODO: Add delete logic here
                repositorioInquilino.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                return View(i);
            }
        }
    }
}