using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class Consulta
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        private int pacienteTeste;
        private int medicoTeste;
        private int idTeste;
        private int? cidTeste;

        [Key]
        public int Id
        {
            get { return idTeste; }
            set
            {
                idTeste = value;
                if(value > 0)
                {
                    ConsultaExame = db.ConsultaExames.Where(p => p.ConsultaId == value).ToList();
                    ConsultaMedicamento = db.ConsultaMedicamentos.Where(p => p.ConsultaId == value).ToList();
                }
            }
        }

        public bool Realizada { get; set; }

        [Display(Name = "Médico")]
        [Required(ErrorMessage = "Obrigatório informar o médico responsável pela consulta")]
        public int MedicoId
        {
            get
            {
                if (medicoTeste > 0)
                {
                    Medico obj = db.Medico.Where(p => p.Id == medicoTeste).FirstOrDefault();
                    if (obj != null)
                    {
                        MedicoNome = obj.Nome;
                    }
                }
                return medicoTeste;
            }
            set { medicoTeste = value; }
        }
        //[ForeignKey("MedicoId")]
        public virtual string MedicoNome { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "Obrigatório informar o paciente da consulta")]
        public int PacienteId
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
        //[ForeignKey("PacienteId")]
        public virtual string PacienteNome { get; set; }

        [Required(ErrorMessage = "A hora da consulta é obrigatória")]
        [Display(Name = "Hora")]
        [DataType(DataType.Time)]
        public DateTime Horario { get; set; }

        [Required(ErrorMessage = "A data da consulta é obrigatória")]
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public string Peso { get; set; }

        public string Altura { get; set; }

        [Display(Name = "Anotações")]
        public string Anotacoes { get; set; }

        [Display(Name = "CID")]
        public int? CIDId
        {
            get { return cidTeste; }
            set
            {
                cidTeste = value;
                if (value > 0)
                {
                    CID = db.CID.Where(p => p.Id == value).FirstOrDefault();
                }
            }
        }
        [ForeignKey("CIDId")]
        public virtual CID CID { get; set; }

        public virtual ICollection<ConsultaMedicamento> ConsultaMedicamento { get; set; }        

        public virtual ICollection<ConsultaExame> ConsultaExame { get; set; }
    }
}