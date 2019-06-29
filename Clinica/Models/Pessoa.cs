using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public abstract class Pessoa
    {
        private string cpfTeste;

        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Obrigatório informar o Nome")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Data de nascimento", GroupName = "Documento")]
        [Required(ErrorMessage = "Obrigatório informar a data de nascimento")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "CPF", GroupName = "Documento")]
        [Required(ErrorMessage = "Obrigatório informar o CPF")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF deve ter no mínimo 11 caracteres")]
        public string CPF
        {
            get
            {
                string documento = cpfTeste;

                if ((cpfTeste != null) && (cpfTeste != "") && (documento.Length < 14))
                {
                    //formatar cpf
                    documento = Convert.ToUInt64(cpfTeste).ToString(@"000\.000\.000\-00");
                }

                return documento;
            }
            set { cpfTeste = value; }
        }

        [Display(Name = "Telefone", GroupName = "Contato")]
        [Required(ErrorMessage = "Obrigatório informar um telefone de contato")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "E-mail", GroupName = "Contato")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Perfil é obrigatório")]
        public PerfilEnumerator Perfil { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 3)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O Login é obrigatório")]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }

    }

    public enum PerfilEnumerator
    {
        Médico = 1,
        Paciente = 2,
        Secretária = 3,
        Laboratório = 4
    }
}