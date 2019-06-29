using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class CID
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O c�digo � obrigat�rio")]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "C�digo")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A descri��o � obrigat�ria")]
        [Display(Name = "Descri��o")]
        [StringLength(300, MinimumLength = 3)]
        public string Descricao { get; set; }

    }
}
