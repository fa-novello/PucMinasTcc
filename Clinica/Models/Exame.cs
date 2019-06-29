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

        [Required(ErrorMessage = "O c�digo � obrigat�rio")]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "C�digo")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A descri��o � obrigat�ria")]
        [StringLength(250, MinimumLength = 1)]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; }

        [Display(Name = "Orienta��es pr�vias")]
        public string OrientacoesPrevias { get; set; }

    }
}
