using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ROMProjetos.Aplicacao;
using ROMProjetos.Models;

namespace ROMProjetos.Controllers
{
    public class TarefaController : Controller
    {
        //
        // GET: /Tarefa/

        public ActionResult Index(string idProjeto)
        {
            var projeto = new ProjetoAplicacao().BuscarPorId(idProjeto);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            ViewBag.projeto = projeto;
            return View(projeto.Tarefas);
        }

        public ActionResult CriarTarefa(string idProjeto)
        {
            var projeto = new ProjetoAplicacao().BuscarPorId(idProjeto);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            ViewBag.projeto = projeto;
            ViewBag.colaboradores = new SelectList(new ColaboradorAplicacao().Buscar(new Colaborador()), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult CriarTarefa(Tarefa tarefa, string idProjeto, string idColaborador)
        {
            var projetoAplicacao = new ProjetoAplicacao();
            var projeto = projetoAplicacao.BuscarPorId(idProjeto);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                tarefa.Colaborador = new ColaboradorAplicacao().BuscarPorId(idColaborador);
                projeto.Tarefas.Add(tarefa);
                projetoAplicacao.Salvar(projeto);
                return RedirectToAction("Index", new { idProjeto = idProjeto });
            }


            ViewBag.projeto = projeto;
            ViewBag.colaboradores = new SelectList(new ColaboradorAplicacao().Buscar(new Colaborador()), "Id", "Nome");
            return View(tarefa);
        }

    }
}
