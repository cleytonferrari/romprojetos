using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ROMProjetos.Models.Base;

namespace ROMProjetos.Models
{
    public class Colaborador: Entidade
    {
        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "Preencha o nome do colaborador")]
        [StringLength(75, MinimumLength = 4, ErrorMessage = "O nome do colaborador deve ter de 4 a 200 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Preencha o email do colaborador")]
        //TODO: Validar email
        public string Email { get; set; }
    }
}