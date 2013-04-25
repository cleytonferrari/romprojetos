using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Aplicacao.Base
{
    public abstract class Aplicacao<T> where T : Entidade
    {
        protected readonly Repositorio.Repositorio<T> repositorio;

        protected Aplicacao()
        {
            repositorio = new Repositorio.Repositorio<T>();
        }

        public virtual IEnumerable<T> Buscar(T entidade)
        {
            var retorno = repositorio.Collection.AsQueryable();
            MontaBusca(entidade, ref retorno, "");
            return retorno;
        }

        public virtual T BuscarPorId(string Id)
        {
            return repositorio.Collection.AsQueryable().FirstOrDefault(x => x.Id == Id);
        }

        public virtual void Excluir(string id)
        {
            repositorio.Collection.Remove(Query.EQ("_id", new ObjectId(id)));
        }

        public virtual ObjetoRetorno<T> Salvar(T entidade)
        {
            var retorno = entidade.Verifica<T>();
            if (retorno.TemErro)
            {
                return retorno;
            }
            repositorio.Collection.Save(entidade);
            return retorno.SetRetorno(entidade);
        }

        private void MontaBusca(object entidade, ref IQueryable<T> retorno, string pai = "")
        {
            var propriedades = entidade.GetType().GetProperties();
            foreach (var propriedade in propriedades)
            {
                var valorDaPropriedade = propriedade.GetValue(entidade, null);

                if (valorDaPropriedade == null) continue;
                var tipo = valorDaPropriedade.GetType().ToString();
                switch (tipo)
                {
                    case "System.String":
                        if (propriedade.Name != "Id" && valorDaPropriedade != "000000000000000000000000")
                        {
                            retorno = retorno.Where(pai + propriedade.Name + ".ToLower().Contains(@0)", new[] { valorDaPropriedade.ToString().ToLower() });
                        }
                        break;

                    case "System.Int":
                    case "System.Double":
                    case "System.Decimal":
                    case "System.bool":
                        retorno = retorno.Where(pai + propriedade.Name + "==(@0)", new[] { valorDaPropriedade });
                        break;

                    default:
                        var baseType = valorDaPropriedade.GetType().BaseType;
                        if (baseType != null && baseType.Name == "Entidade")
                        {
                            MontaBusca(valorDaPropriedade, ref retorno, propriedade.Name + ".");
                        }
                        break;
                }
            }
        }
    }
}