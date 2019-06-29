using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class Relatorio
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        public int ConsultaId { get; set; }

        public string Paciente { get; set; }

        public string Medico { get; set; }

        public string Especialidade { get; set; }

        public virtual ICollection<ReceitaRelatorio> ReceitaRelatorio { get; set; }

        public virtual ICollection<ExameRelatorio> ExameRelatorio { get; set; }
    }

    public class ReceitaRelatorio
    {
        [Key]
        public int Id { get; set; }

        public string Medicamento { get; set; }

        public string Posologia { get; set; }
    }

    public class ExameRelatorio
    {
        [Key]
        public int Id { get; set; }

        public string Exame { get; set; }

        [Display(Name = "Orientações prévias")]
        public string OrientacoesPrevias { get; set; }
    }
}