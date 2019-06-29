using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Controllers
{
    [Authorize(Roles = "Médico")]
    public class RelatorioController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exame(int consultaId, int? exameId)
        {
            Relatorio relatorio = new Relatorio();

            Consulta consulta = db.Consulta.Where(p => p.Id == consultaId).FirstOrDefault();

            relatorio.Paciente = consulta.PacienteNome;
            relatorio.Medico = consulta.MedicoNome;
            relatorio.Especialidade = db.Medico.Where(p => p.Id == consulta.MedicoId).FirstOrDefault().EspecialidadeDescricao;
            relatorio.ConsultaId = consultaId;

            relatorio.ExameRelatorio = new List<ExameRelatorio>();
            if (exameId != null)
            {
                ConsultaExame consExame = consulta.ConsultaExame.Where(p =>p.Id == exameId).FirstOrDefault();
                ExameRelatorio exRelat = new ExameRelatorio();
                exRelat.Exame = consExame.Exame.Descricao;
                if (!string.IsNullOrEmpty(consExame.Exame.OrientacoesPrevias))
                {
                    exRelat.OrientacoesPrevias = consExame.Exame.OrientacoesPrevias;
                }
                relatorio.ExameRelatorio.Add(exRelat);
            }
            else
            {
                foreach (var item in consulta.ConsultaExame)
                {
                    ExameRelatorio exRelat = new ExameRelatorio();
                    exRelat.Exame = item.Exame.Descricao;
                    if (!string.IsNullOrEmpty(item.Exame.OrientacoesPrevias))
                    {
                        exRelat.OrientacoesPrevias = item.Exame.OrientacoesPrevias;
                    }
                    relatorio.ExameRelatorio.Add(exRelat);
                }
            }
            return View(relatorio);
        }

        public ActionResult Prescricao(int consultaId, int? medicamentoId)
        {
            Relatorio relatorio = new Relatorio();

            Consulta consulta = db.Consulta.Where(p => p.Id == consultaId).FirstOrDefault();

            relatorio.Paciente = consulta.PacienteNome;
            relatorio.Medico = consulta.MedicoNome;
            relatorio.Especialidade = db.Medico.Where(p => p.Id == consulta.MedicoId).FirstOrDefault().EspecialidadeDescricao;
            relatorio.ConsultaId = consultaId;

            relatorio.ReceitaRelatorio = new List<ReceitaRelatorio>();
            if (medicamentoId != null)
            {
                ConsultaMedicamento consMedicamento = consulta.ConsultaMedicamento.Where(p => p.Id == medicamentoId).FirstOrDefault();
                ReceitaRelatorio recRelat = new ReceitaRelatorio();
                recRelat.Posologia = consMedicamento.Posologia;
                recRelat.Medicamento = consMedicamento.Medicamento.FabricanteNome;
                
                relatorio.ReceitaRelatorio.Add(recRelat);
            }
            else
            {
                foreach (var item in consulta.ConsultaMedicamento)
                {
                    ReceitaRelatorio recRelat = new ReceitaRelatorio();
                    recRelat.Posologia = item.Posologia;
                    recRelat.Medicamento = item.Medicamento.FabricanteNome;

                    relatorio.ReceitaRelatorio.Add(recRelat);
                }
            }
            return View(relatorio);
        }
    }
}