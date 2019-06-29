namespace Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class Agenda 
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        private int? pacienteTeste;

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O médico da agenda é obrigatório")]
        [Display(Name = "Médico")]
        public int MedicoId { get; set; }
        //[ForeignKey("MedicoId")]
        //public virtual Medico Medico { get; set; }

        [Display(Name = "Paciente")]
        public int? PacienteId
        {
            get
            {
                if (pacienteTeste > 0)
                {
                    Paciente obj = db.Paciente.Where(p => p.Id == pacienteTeste).FirstOrDefault();
                    if (obj != null)
                    {
                        PacienteNome = obj.Nome;
                    }
                }
                return pacienteTeste;
            }
            set { pacienteTeste = value; }
        }
        public virtual string PacienteNome { get; set; }

        [StringLength(250, MinimumLength = 1)]
        [Display(Name = "Convênio")]
        public string Convenio { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "A hora da consulta é obrigatória")]
        [Display(Name = "Hora")]
        [DataType(DataType.Time)]
        public DateTime Horario { get; set; }

        [Required(ErrorMessage = "A data da consulta é obrigatória")]
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public bool Confirmada { get; set; }

        [Display(Name = "Consulta criada")]
        public bool ConsultaCriada { get; set; }

    }

    public class AgendaGerar
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Médico")]
        public int MedicoId { get; set; }
        [ForeignKey("MedicoId")]
        public virtual Medico Medico { get; set; }

        [Required(ErrorMessage = "A data inicial é obrigatória")]
        [Display(Name = "Data inicial")]
        [DataType(DataType.Date)]
        public DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "A data final é obrigatória")]
        [Display(Name = "Data final")]
        [DataType(DataType.Date)]
        public DateTime DataFinal { get; set; }

    }
    public class AgendaViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Medico { get; set; }
        public IEnumerable<Agenda> Agendas { get; set; }
    }

}