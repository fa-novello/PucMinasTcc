using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class Paciente: Pessoa
    {
        [Display(Name = "Possui convênio", GroupName = "Convenio")]
        //[Required(ErrorMessage = "Obrigatório informar se possui convênio")]
        public bool PossuiConvenio { get; set; }

        [Display(Name = "Empresa conveniada", GroupName = "Convenio")]
        //[RequiredIfTrue("PossuiConvenio", ErrorMessage = "Obrigatório informar a empresa do convênio")]
        public string EmpresaConvenio { get; set; }

        [Display(Name = "Validade", GroupName = "Convenio")]
        //[RequiredIfTrue("PossuiConvenio", ErrorMessage = "Obrigatório informar a validade do convênio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public DateTime? ValidadeConvenio { get; set; }

        [Display(Name = "Número da Carteirinha", GroupName = "Documento", Description = "Número da carteirinha")]
        public string NumeroCarteiraConvenio { get; set; }

        [Display(Name = "Número da CNS", GroupName = "Documento", Description = "Certidão nacional de saúde")]
        public string NumeroCNS { get; set; }
    }
}