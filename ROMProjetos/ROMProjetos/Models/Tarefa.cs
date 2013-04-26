using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Models
{
    public class Tarefa : Entidade
    {
        public Tarefa()
        {
            Colaborador = new Colaborador();
        }

        public string Titulo { get; set; }
        public decimal Peso { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime EntregarAte { get; set; }
        public PrioridadeTarefa Prioridade { get; set; }
        public TipoTarefa TipoTarefa { get; set; }
        public Colaborador Colaborador { get; set; }
        public StatusTarefa Status { get; set; }
        public IEnumerable<LogTarefa> LogTarefas { get; set; }
    }

    public class StatusTarefa
    {
        public string Nome { get; set; }
    }

    public class LogTarefa
    {
        public DateTime Data { get; set; }
        public string Log { get; set; }
    }

    public class TipoTarefa
    {
        public string Nome { get; set; }
    }

    public class PrioridadeTarefa
    {
        public string Nome { get; set; }
    }
}