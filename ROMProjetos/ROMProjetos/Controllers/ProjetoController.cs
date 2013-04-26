using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ROMProjetos.Aplicacao;
using ROMProjetos.Models;

namespace ROMProjetos.Controllers
{
    public class ProjetoController : Controller
    {
        //
        // GET: /Projeto/

        public ActionResult Index()
        {
            return View(new ProjetoAplicacao().Buscar(new Projeto()));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                new ProjetoAplicacao().Salvar(projeto);
                return RedirectToAction("Index");
            }
            return View(projeto);
        }

        public ActionResult Detalhes(string id)
        {
            var projeto = new ProjetoAplicacao().BuscarPorId(id );
            if (projeto == null)
            {
                return HttpNotFound();
            }

            return View(projeto);
        }
    }
}
