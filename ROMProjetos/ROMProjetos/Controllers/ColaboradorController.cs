using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ROMProjetos.Aplicacao;
using ROMProjetos.Models;

namespace ROMProjetos.Controllers
{
    public class ColaboradorController : Controller
    {
        //
        // GET: /Colaborador/

        public ActionResult Index()
        {
            return View(new ColaboradorAplicacao().Buscar(new Colaborador()));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                new ColaboradorAplicacao().Salvar(colaborador);
                return RedirectToAction("Index");
            }
            return View(colaborador);
        }
    }
}
