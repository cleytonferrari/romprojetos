using System.Linq;
using MongoDB.Driver.Builders;
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
    }
}