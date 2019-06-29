namespace Clinica.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class Especialidade
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }


}