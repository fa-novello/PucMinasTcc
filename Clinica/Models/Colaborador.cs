using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class Colaborador : Pessoa
    {
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }

}