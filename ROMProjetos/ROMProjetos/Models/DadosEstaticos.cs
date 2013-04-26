using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ROMProjetos.Models
{
    public static class DadosEstaticos
    {
        public static ICollection<StatusProjeto> StatusProjeto
        {
            get
            {
                return new Collection<StatusProjeto>
                           {
                               new StatusProjeto { Nome = "Em Execução"},
                               new StatusProjeto { Nome = "Concluído"},
                               new StatusProjeto { Nome = "Cancelado"}
                           };
            }
        }

        public static ICollection<StatusTarefa> StatusTarefa
        {
            get
            {
                return new Collection<StatusTarefa>
                           {
                               new StatusTarefa { Nome = "Pendente"},
                               new StatusTarefa { Nome = "Sendo Feita"},
                               new StatusTarefa { Nome = "Concluída"},
                               new StatusTarefa { Nome = "Cancelada"}
                           };
            }
        }

        public static ICollection<TipoTarefa> TipoTarefa
        {
            get
            {
                return new Collection<TipoTarefa>
                           {
                               new TipoTarefa { Nome = "Bug"},
                               new TipoTarefa { Nome = "Tarefa"},
                               new TipoTarefa { Nome = "Melhoramento"},
                               new TipoTarefa { Nome = "Idéia"}
                           };
            }
        }

        public static ICollection<PrioridadeTarefa> PrioridadeTarefa
        {
            get
            {
                return new Collection<PrioridadeTarefa>
                           {
                               new PrioridadeTarefa { Nome = "Baixa"},
                               new PrioridadeTarefa { Nome = "Normal"},
                               new PrioridadeTarefa { Nome = "Alta"},
                               new PrioridadeTarefa { Nome = "Critica"},
                               new PrioridadeTarefa { Nome = "Bloqueante"}
                           };
            }
        }
    }
}