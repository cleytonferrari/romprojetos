﻿using System;
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
                projeto.Status = DadosEstaticos.StatusProjeto.FirstOrDefault(x => x.Nome == "Em Execução");
                new ProjetoAplicacao().Salvar(projeto);
                TempData["MensagemSucesso"] = "Registro salvo com sucesso";
                return RedirectToAction("Index");
            }
            return View(projeto);
        }

        public ActionResult Detalhes(string id)
        {
            var projeto = new ProjetoAplicacao().BuscarPorId(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }

            return View(projeto);
        }
    }
}
