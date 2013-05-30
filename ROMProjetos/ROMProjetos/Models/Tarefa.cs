using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Models
{
    public class Tarefa : Entidade
    {
        public Tarefa()
        {
            Thread = new List<Thread>();
            Prioridade = new PrioridadeTarefa();
            TipoTarefa = new TipoTarefa();
            Status = new StatusTarefa();
            Colaborador = new Colaborador();
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CriadaEm { get; set; }

        [Display(Name = "Entregar até")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EntregarAte { get; set; }

        [Display(Name = "Prioridade")]
        public PrioridadeTarefa Prioridade { get; set; }

        [Display(Name = "Tipo de tarefa")]
        public TipoTarefa TipoTarefa { get; set; }

        [Display(Name = "Colaborador")]
        public Colaborador Colaborador { get; set; }

        [Display(Name = "Autor")]
        public Colaborador Autor { get; set; }

        [Display(Name = "Status")]
        public StatusTarefa Status { get; private set; }

        public void SetStatus(Colaborador colaborador, StatusTarefa statusTarefa, String mensagem)
        {
            if (Status.Chave == statusTarefa.Chave) return;

            Status = statusTarefa;
            Thread.Add(new LogTarefa { Data = DateTime.Now, Status = statusTarefa, Colaborador = colaborador, Mensagem = mensagem });
        }



        public List<Thread> Thread { get; private set; }
    }

    public class StatusTarefa
    {
        public string Nome { get; set; }
        public string Chave { get; set; }
    }

    public class TipoTarefa
    {
        public string Nome { get; set; }
    }

    public class PrioridadeTarefa
    {
        public string Nome { get; set; }
    }

    #region Comentarios e Logs

    [BsonKnownTypes(typeof(LogTarefa), typeof(Comentario))]
    public abstract class Thread
    {
        public DateTime Data { get; set; }
        public Colaborador Colaborador { get; set; }
        public string Mensagem { get; set; }
    }

    public class LogTarefa : Thread
    {
        public LogTarefa()
        {
            Status = new StatusTarefa();
        }
        public StatusTarefa Status { get; set; }
    }

    public class Comentario : Thread
    {

    }

    #endregion
}