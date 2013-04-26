using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Models
{
    public class Tarefa : Entidade
    {
        public Tarefa()
        {
            Prioridade = new PrioridadeTarefa();
            TipoTarefa = new TipoTarefa();
            Status = new StatusTarefa();
            Colaborador = new Colaborador();
            LogTarefas = new List<LogTarefa>();
        }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Preencha o título da tarefa")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "{0} deve ter de {1} a {2} caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "Preencha o peso da tarefa")]
        //TODO: Validar o peso
        public decimal Peso { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Preencha a descrição da tarefa")]
        public string Descricao { get; set; }

        [Display(Name = "Criada em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CriadaEm { get; set; }

        [Display(Name = "Entregar até")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EntregarAte { get; set; }

        [Display(Name = "Prioridade")]
        public PrioridadeTarefa Prioridade { get; set; }

        [Display(Name = "Tipo de tarefa")]
        public TipoTarefa TipoTarefa { get; set; }

        [Display(Name = "Colaborador")]
        public Colaborador Colaborador { get; set; }

        [Display(Name = "Status")]
        public StatusTarefa Status { get; set; }

        public List<LogTarefa> LogTarefas { get; set; }
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

    //Dados
}