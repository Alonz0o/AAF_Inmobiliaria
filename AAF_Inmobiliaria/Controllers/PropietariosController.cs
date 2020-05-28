using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AAF_Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AAF_Inmobiliaria.Controllers
{
    [Authorize]
    public class PropietariosController : Controller
    {
        private readonly IConfiguration configuracion;        private readonly RepositorioPropietario repositorioPropietario;        public PropietariosController(IConfiguration configuration)        {            this.configuracion = configuration;            repositorioPropietario = new RepositorioPropietario(configuration);        }

        // GET: Propietarios
        public ActionResult Index()
        {
            var lista = repositorioPropietario.ObtenerTodos1();
            return View(lista);
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            var i = repositorioPropietario.ObtenerPorId(id);
            return View(i);
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
                int res = repositorioPropietario.Alta(p);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex);
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
        public ActionResult Edit(int id, Propietario p)
        {
            try
            {           
                int res = repositorioPropietario.Modificacion(p);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ex);
            }

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