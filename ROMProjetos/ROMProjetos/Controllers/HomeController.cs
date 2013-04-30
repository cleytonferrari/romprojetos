using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ROMProjetos.Aplicacao;
using ROMProjetos.Models;

namespace ROMProjetos.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var projetoDoColaborador = new List<ProjetosDoColaboradorViewModel>();
            var listaDeColaboradores = new ColaboradorAplicacao().Buscar(new Colaborador());
            foreach (var colaborador in listaDeColaboradores)
            {
                var tempColaborador = new ProjetosDoColaboradorViewModel
                                          {
                                              Colaborador = colaborador
                                          };
                var listaProjeto = new ProjetoAplicacao().BuscarPorColaboradorId(colaborador.Id);
                ProjetoAplicacao.LimpaTarefasQueNaoSaoDoColaborador(listaProjeto, colaborador.Id);
                tempColaborador.Projetos.AddRange(listaProjeto);
                projetoDoColaborador.Add(tempColaborador);
            }

            return View(projetoDoColaborador);
        }

        
    }

    public class ProjetosDoColaboradorViewModel
    {
        public ProjetosDoColaboradorViewModel()
        {
            Colaborador = new Colaborador();
            Projetos = new List<Projeto>();
        }
        public Colaborador Colaborador { get; set; }
        public List<Projeto> Projetos { get; set; }
    }
}
