using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class AgendaConfiguracao
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        private int medicoTeste;

        //Configura��o da disponibilidade do m�dico
        [Key]
        public int Id { get; set; }

        [Display(Name = "M�dico")]
        public int MedicoId
        {
            get { return medicoTeste; }
            set
            {
                medicoTeste = value;
                if (value > 0)
                {
                    Medico obj = db.Medico.Where(p => p.Id == medicoTeste).FirstOrDefault();
                    if (obj != null)
                    {
                        Medico = obj.Nome;
                    }
                }
            }
        }
        public virtual string Medico { get; set; }

        [Display(Name = "Dia da semana")]
        public DayOfWeek DiaSemana { get; set; }

        [Display(Name = "Limite de consultas no dia")]
        [RangeAttribute(1, 40)]
        public int LimiteConsultas { get; set; }

        [Display(Name = "Tempo da consulta (em minutos)")]
        [RangeAttribute(15, 120)]
        public int TempoConsulta { get; set; }

        [Display(Name = "Hor�rio de in�cio")]
        [DataType(DataType.Time)]
        public DateTime HorarioInicio { get; set; }
    }
}
