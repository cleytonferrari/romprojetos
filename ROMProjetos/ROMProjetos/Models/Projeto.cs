using System;
using System.ComponentModel.DataAnnotations;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Models
{
    public class Projeto : Entidade
    {
        
        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "Preencha o nome do projeto")]
        [StringLength(200,MinimumLength = 4,ErrorMessage = "O nome do projeto deve ter de 4 a 200 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Descrição:")]
        [StringLength(800, ErrorMessage = "A descrição do projeto deve ter no máximo 800 caracteres")]
        public string Descricao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Preencha a data inicial do projeto")]
        public DateTime DataInicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DataFinal { get; set; }

        [Display(Name = "Interessado:")]
        [Required(ErrorMessage = "Preencha o nome do interessado")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "O nome do interessado deve ter de 4 a 200 caracteres")]
        public string Interessado { get; set; }

        public StatusProjeto Status { get; set; }
    }

    public class StatusProjeto
    {
        public string Nome { get; set; }
    }
}