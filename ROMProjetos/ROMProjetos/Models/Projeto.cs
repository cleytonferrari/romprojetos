using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Models
{
    public class Projeto : Entidade
    {
        public Projeto()
        {
            Tarefas = new List<Tarefa>();
        }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Preencha o nome do projeto")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "{0} deve ter de {1} a {2} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(800, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Data Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Preencha a data inicial do projeto")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data Final")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFinal { get; set; }

        [Display(Name = "Interessado")]
        [Required(ErrorMessage = "Preencha o nome do interessado")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "{0} deve ter de {1} a {2} caracteres")]
        public string Interessado { get; set; }

        [Display(Name = "Status")]
        public StatusProjeto Status { get; set; }

        [Display(Name = "Tarefas")]
        public List<Tarefa> Tarefas { get; set; }
    }

    public class StatusProjeto
    {
        public string Nome { get; set; }
    }
}