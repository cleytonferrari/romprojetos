using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using ROMProjetos.Models;

namespace ROMProjetos.Aplicacao
{
    public class ProjetoAplicacao : Base.Aplicacao<Projeto>
    {
        public virtual Projeto BuscarPorTarefaId(string id)
        {
            var query = Query.EQ("Tarefas._id", id);
            return repositorio.Collection.Find(query).First();
        }

        public virtual IEnumerable<Projeto> BuscarPorColaboradorId(string id)
        {
            return repositorio.Collection.AsQueryable().Where(x => x.Tarefas.Any(xx => xx.Colaborador.Id == id)).ToList();
        }

        public static void LimpaTarefasQueNaoSaoDoColaborador(IEnumerable<Projeto> projetos, string idColaborador)
        {
            foreach (var projeto in projetos)
            {
                projeto.Tarefas.RemoveAll(x => x.Colaborador == null);
                projeto.Tarefas.RemoveAll(x => x.Colaborador.Id != idColaborador);
            }
        }
    }
}