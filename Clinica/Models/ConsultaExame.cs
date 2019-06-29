using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class ConsultaExame
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        private int exameTeste;

        [Key]
        public int Id { get; set; }

        [Required]
        public int ConsultaId { get; set; }
        
        [Required]
        [Display(Name = "Exame")]
        public int ExameId
        {
            get { return exameTeste; }
            set
            {
                exameTeste = value;
                if (value > 0)
                {
                    Exame = db.Exame.Where(p => p.Id == value).FirstOrDefault();
                }
            }
        }
        [ForeignKey("ExameId")]
        public virtual Exame Exame { get; set; }

        public bool Realizado { get; set; }

        [Display(Name = "Data liberação")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public DateTime? DataLiberacaoExame { get; set; }

        public string Laudo { get; set; }
    }

    public class ListarExames : ConsultaExame
    {
        public virtual string Paciente { get; set; }

        [Display(Name = "Médico solicitante")]
        public virtual string Medico { get; set; }

        [Display(Name = "Data da consulta")]
        [DataType(DataType.Date)]
        public virtual DateTime DataConsulta { get; set; }
    }

}