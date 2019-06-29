using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome genérico")]
        [Required(ErrorMessage = "O nome genérico é obrigatório")]
        public string GenericoNome { get; set; }

        [Display(Name = "Nome de fábrica")]
        [Required(ErrorMessage = "O nome de fábrica é obrigatório")]
        public string FabricanteNome { get; set; }

        [Display(Name = "Fabricante")]
        [Required(ErrorMessage = "O fabricante é obrigatório")]
        public string Fabricante { get; set; }

        [Display(Name = "Indicação")]
        public string Indicacao { get; set; }

        [Display(Name = "Contra indicação")]
        public string ContraIndicacao { get; set; }
    }
}