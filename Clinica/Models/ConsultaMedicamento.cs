using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class ConsultaMedicamento
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        private int medicamentoTeste;

        [Key]
        public int Id { get; set; }

        [Required]
        public int ConsultaId { get; set; }

        [Required]
        public int MedicamentoId
        {
            get { return medicamentoTeste; }
            set
            {
                medicamentoTeste = value;
                if (value > 0)
                {
                    Medicamento = db.Medicamento.Where(p => p.Id == value).FirstOrDefault();
                }
            }
        }
        [ForeignKey("MedicamentoId")]
        public virtual Medicamento Medicamento { get; set; }

        [Required(ErrorMessage = "A posologia é obrigatória")]
        public string Posologia { get; set; }
    }
}