using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using ROMProjetos.Aplicacao;
using ROMProjetos.Models;

namespace ROMProjetos.Controllers
{
    public class TarefaController : Controller
    {
        //
        // GET: /Tarefa/

        public ActionResult Index(string idProjeto, string status = "")
        {
            var projeto = new ProjetoAplicacao().BuscarPorId(idProjeto);
            if (projeto == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(status))
                projeto.Tarefas.RemoveAll(x => x.Status.Chave != status);

            ViewBag.projeto = projeto;
            return View(projeto.Tarefas.OrderBy(x => x.EntregarAte));
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
            ViewBag.prioridades = new SelectList(DadosEstaticos.PrioridadeTarefa, "Nome", "Nome");
            ViewBag.tipotarefas = new SelectList(DadosEstaticos.TipoTarefa, "Nome", "Nome");
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CriarTarefa(Tarefa tarefa, string idProjeto, string idColaborador)
        {
            var colaboradorLogado = new Colaborador
            {
                Email = "usuario@logado.com",
                Nome = "Usuario Logado"
            };

            var projetoAplicacao = new ProjetoAplicacao();
            var projeto = projetoAplicacao.BuscarPorId(idProjeto);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                tarefa.Colaborador = new ColaboradorAplicacao().BuscarPorId(idColaborador);
                tarefa.SetStatus(colaboradorLogado, DadosEstaticos.StatusTarefa.FirstOrDefault(x => x.Chave == "aberta"), "Criado a tarefa");
                tarefa.Id = ObjectId.GenerateNewId().ToString(); //TODO: criar uma convesao no mongo para gerar isso automaticamente

                tarefa.Autor = colaboradorLogado;

                projeto.Tarefas.Add(tarefa);
                projetoAplicacao.Salvar(projeto);
                TempData["MensagemSucesso"] = "Registro salvo com sucesso";
                return RedirectToAction("Index", new { idProjeto = idProjeto });
            }


            ViewBag.projeto = projeto;
            ViewBag.colaboradores = new SelectList(new ColaboradorAplicacao().Buscar(new Colaborador()), "Id", "Nome");
            ViewBag.prioridades = new SelectList(DadosEstaticos.PrioridadeTarefa, "Nome", "Nome");
            ViewBag.tipotarefas = new SelectList(DadosEstaticos.TipoTarefa, "Nome", "Nome");
            return View(tarefa);
        }

        public ActionResult Detalhes(string id)
        {
            var projeto = new ProjetoAplicacao().BuscarPorTarefaId(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }

            var tarefa = projeto.Tarefas.FirstOrDefault(x => x.Id == id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }

            ViewBag.projeto = projeto;
            return View(tarefa);
        }

        /*[System.Web.Mvc.HttpPost]
        public JsonResult AlterarStatus(string id, string status)
        {
            var projetoAplicacao = new ProjetoAplicacao();

            var projeto = projetoAplicacao.BuscarPorTarefaId(id);

            var tarefa = projeto.Tarefas.FirstOrDefault(x => x.Id == id);

            if (tarefa == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var statusBanco = DadosEstaticos.StatusTarefa.FirstOrDefault(x => x.Chave == status.ToLower());
            if (statusBanco == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            tarefa.SetStatus(statusBanco);

            projetoAplicacao.Salvar(projeto);

            return Json(tarefa, JsonRequestBehavior.AllowGet);
        }*/

        [System.Web.Mvc.HttpPost]
        public JsonResult AlterarStatusForm(string statusId, string statusValue, string statusComentario)
        {
            var colaboradorLogado = new Colaborador
            {
                Email = "usuario@logado.com",
                Nome = "Usuario Logado"
            };

            var projetoAplicacao = new ProjetoAplicacao();

            var projeto = projetoAplicacao.BuscarPorTarefaId(statusId);

            var tarefa = projeto.Tarefas.FirstOrDefault(x => x.Id == statusId);

            if (tarefa == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var statusBanco = DadosEstaticos.StatusTarefa.FirstOrDefault(x => x.Chave == statusValue.ToLower());
            if (statusBanco == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            tarefa.SetStatus(colaboradorLogado, statusBanco, statusComentario);

            projetoAplicacao.Salvar(projeto);

            return Json(tarefa, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult PostarMensagem(string id, string mensagem)
        {
            var colaboradorLogado = new Colaborador
            {
                Email = "usuario@logado.com",
                Nome = "Usuario Logado"
            };
            var projetoAplicacao = new ProjetoAplicacao();

            var projeto = projetoAplicacao.BuscarPorTarefaId(id);

            var tarefa = projeto.Tarefas.FirstOrDefault(x => x.Id == id);

            if (tarefa == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var comentario = new Comentario()
                             {
                                 Data = DateTime.Now,
                                 Mensagem = mensagem,
                                 Colaborador = colaboradorLogado
                             };

            tarefa.Thread.Add(comentario);

            projetoAplicacao.Salvar(projeto);

            return RedirectToAction("Detalhes", new { id = id });
        }
    }
}
