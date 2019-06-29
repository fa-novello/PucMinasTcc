using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Models
{
    public class Medico: Colaborador
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        private int especialidadeTeste;

        [Display(Name = "CRM", GroupName = "Documento")]
        [Required(ErrorMessage = "Obrigatório informar o CRM")]
        public string CRMNumero { get; set; }

        [Display(Name = "Órgão emissor", GroupName = "Documento")]
        [Required(ErrorMessage = "Obrigatório informar o órgão emissor do registro")]
        public string CRMEmissor { get; set; }

        [Display(Name = "Especialidade", GroupName = "Documento")]
        [Required(ErrorMessage = "Obrigatório informar a especialidade")]
        public int EspecialidadeId
        {
            get { return especialidadeTeste; }
            set
            {
                especialidadeTeste = value;
                if (value > 0)
                {
                    Especialidade obj = db.Especialidade.Where(p => p.Id == especialidadeTeste).FirstOrDefault();
                    if (obj != null)
                    {
                        EspecialidadeDescricao = obj.Descricao;
                    }
                }
            }
        }
        //[ForeignKey("EspecialidadeId")]
        public virtual string EspecialidadeDescricao { get; set; }
    }

}