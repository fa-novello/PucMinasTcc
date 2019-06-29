using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [StringLength(100, MinimumLength = 1)]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength = 1)]
        public virtual string Senha { get; set; }

        [Display(Name = "Nova senha")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 1)]
        public virtual string NovaSenha { get; set; }

        [Display(Name = "Confirmar nova senha")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 1)]
        [Compare(nameof(NovaSenha), ErrorMessage = "Senha inválida")]
        public virtual string ConfirmarNovaSenha { get; set; }

    }
}