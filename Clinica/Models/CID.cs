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

        [Required(ErrorMessage = "O código é obrigatório")]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 3)]
        public string Descricao { get; set; }

    }
}
