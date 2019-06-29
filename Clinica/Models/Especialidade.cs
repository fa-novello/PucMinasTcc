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

        [Required(ErrorMessage = "A descri��o � obrigat�ria")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; }
    }


}