using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Exame
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório")]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(250, MinimumLength = 1)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Orientações prévias")]
        public string OrientacoesPrevias { get; set; }

    }
}
