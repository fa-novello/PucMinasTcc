using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Clinica.Controllers
{
    [Authorize(Roles = "Médico,Laboratório,Paciente,Secretária")]
    public class ConsultaController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Médico")]
        public ActionResult IncluirMedicamento(int id)
        {
            ConsultaMedicamento model = new ConsultaMedicamento();
            model.ConsultaId = id;
            ViewBag.MedicamentoId = new SelectList(db.Medicamento, "Id", "FabricanteNome", "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Médico")]
        public ActionResult IncluirMedicamento(ConsultaMedicamento model)
        {
            if (ModelState.IsValid)
            {
                if (model.Posologia != null)
                {
                    ConsultaMedicamento obj = db.ConsultaMedicamentos.Where(p => p.ConsultaId == model.ConsultaId && p.MedicamentoId == model.MedicamentoId).FirstOrDefault();
                    if (obj == null)
                    {
                        db.ConsultaMedicamentos.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Detalhes", new { id = model.ConsultaId });
                    }
                    ModelState.AddModelError("", "Esse medicamento já está incluído na consulta");
                }
                else
                {
                    ModelState.AddModelError("", "A posologia do medicamento é obrigatória");
                }
            }
            ViewBag.MedicamentoId = new SelectList(db.Medicamento, "Id", "FabricanteNome", "");
            return View(model);
        }

        [Authorize(Roles = "Médico")]
        public ActionResult IncluirExame(int id)
        {
            ConsultaExame model = new ConsultaExame();
            model.ConsultaId = id;
            //model.DataLiberacaoExame = DateTime.Now;
            ViewBag.ExameId = new SelectList(db.Exame, "Id", "Descricao", "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Médico")]
        public ActionResult IncluirExame(ConsultaExame model)
        {
            if (ModelState.IsValid)
            {
                ConsultaExame obj = db.ConsultaExames.Where(p => p.ConsultaId == model.ConsultaId && p.ExameId == model.ExameId).FirstOrDefault();
                if (obj == null)
                {
                    db.ConsultaExames.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Detalhes", new { id = model.ConsultaId });
                }
                ModelState.AddModelError("", "Esse exame já está incluído na consulta");
            }
            ViewBag.ExameId = new SelectList(db.Exame, "Id", "Descricao", "");
            return View(model);
        }

        [Authorize(Roles = "Médico")]
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Consulta consulta = db.Consulta.Find(id);

            if (consulta == null)
            {
                return HttpNotFound();
            }
            consulta.ConsultaExame = db.ConsultaExames.Where(p => p.ConsultaId == id).ToList();
            consulta.ConsultaMedicamento = db.ConsultaMedicamentos.Where(p => p.ConsultaId == id).ToList();

            return View(consulta);
        }

        [Authorize(Roles = "Médico")]
        public ActionResult Listar(int? tipoFiltro)
        {
            int idMedico = Convert.ToInt32(Session["id"].ToString());
            switch (tipoFiltro)
            {
                case 1:
                    return View(db.Consulta.Where(p => p.MedicoId == idMedico).OrderBy(p => p.Horario).OrderBy(p => p.Data).ToList());
                case 2:
                    return View(db.Consulta.Where(p => p.Realizada && p.MedicoId == idMedico).OrderBy(p => p.Horario).OrderBy(p => p.Data).ToList());
                default:
                    return View(db.Consulta.Where(p => !p.Realizada && p.MedicoId == idMedico).OrderBy(p => p.Horario).OrderBy(p => p.Data).ToList());
            }
        }

        [Authorize(Roles = "Médico")]
        public ActionResult Atender(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Consulta consulta = db.Consulta.Find(id);

            if (consulta == null)
            {
                return HttpNotFound();
            }

            List<Medico> medico = db.Medico.ToList();
            ViewBag.MedicoNome = (medico.Where(p => p.Id == consulta.MedicoId)).FirstOrDefault().Nome;
            List<Paciente> paciente = db.Paciente.ToList();
            ViewBag.PacienteNome = (paciente.Where(p => p.Id == consulta.PacienteId)).FirstOrDefault().Nome;

            var options = db.CID.Select(p => new { Id = p.Id, Codigo = p.Codigo + " - " + p.Descricao }).ToList();

            ViewBag.CIDId = new SelectList(options, "Id", "Codigo", "");
            return View(consulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Médico")]
        public ActionResult Atender(Consulta model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detalhes", new { model.Id });
            }

            List<Medico> medico = db.Medico.ToList();
            ViewBag.MedicoNome = (medico.Where(p => p.Id == model.MedicoId)).FirstOrDefault().Nome;
            List<Paciente> paciente = db.Paciente.ToList();
            ViewBag.PacienteNome = (paciente.Where(p => p.Id == model.PacienteId)).FirstOrDefault().Nome;

            var options = db.CID.Select(p => new { Id = p.Id, Codigo = p.Codigo + " - " + p.Descricao }).ToList();

            ViewBag.CIDId = new SelectList(options, "Id", "Codigo", "");
            return View(model);
        }

        [Authorize(Roles = "Médico")]
        public ActionResult ExcluirExame(int id)
        {
            ConsultaExame exame = db.ConsultaExames.Find(id);
            int consultaId = exame.ConsultaId;
            db.ConsultaExames.Remove(exame);
            db.SaveChanges();
            return RedirectToAction("Detalhes", new { id = consultaId });
        }

        [Authorize(Roles = "Médico")]
        public ActionResult ExcluirMedicamento(int id)
        {
            ConsultaMedicamento medicamento = db.ConsultaMedicamentos.Find(id);
            int consultaId = medicamento.ConsultaId;
            db.ConsultaMedicamentos.Remove(medicamento);
            db.SaveChanges();
            return RedirectToAction("Detalhes", new { id = consultaId });
        }

        [Authorize(Roles = "Médico")]
        public ActionResult HistoricoPaciente(int pacienteId)
        {
            int idMedico = Convert.ToInt32(Session["id"].ToString());
            List<Consulta> consulta = db.Consulta.Where(p => p.MedicoId == idMedico && p.PacienteId == pacienteId && p.Realizada).OrderByDescending(p => p.Data).ToList();

            return View(consulta);
        }

        [Authorize(Roles = "Médico,Laboratório,Secretária")]
        public ActionResult ExamePendente()
        {
            List<ConsultaExame> consultaExame = db.ConsultaExames.Where(p => !p.Realizado).ToList();

            int usuario = Convert.ToInt32(Session["id"].ToString());

            bool medico = db.Medico.Where(p => p.Id == usuario).Count() > 0;
            List<ListarExames> consultaExamePendente = new List<ListarExames>();
            foreach (var item in consultaExame)
            {
                ListarExames itemTemp = new ListarExames();
                Consulta consulta;
                if (medico)
                {
                    consulta = db.Consulta.Where(p => p.Id == item.ConsultaId && p.MedicoId == usuario).FirstOrDefault();
                }
                else
                {
                    consulta = db.Consulta.Where(p => p.Id == item.ConsultaId).FirstOrDefault();
                }
                if (consulta != null)
                {
                    itemTemp.Paciente = db.Paciente.Where(p => p.Id == consulta.PacienteId).FirstOrDefault().Nome;
                    itemTemp.Medico = db.Medico.Where(p => p.Id == consulta.MedicoId).FirstOrDefault().Nome;
                    itemTemp.ExameId = item.ExameId;
                    itemTemp.ConsultaId = item.ConsultaId;
                    itemTemp.DataConsulta = consulta.Data;
                    itemTemp.Id = item.Id;

                    consultaExamePendente.Add(itemTemp);
                }
            }

            return View(consultaExamePendente.OrderBy(p => p.DataConsulta).OrderBy(p => p.Paciente).ToList());
        }

        [Authorize(Roles = "Médico,Laboratório,Secretária")]
        public ActionResult AlterarExame(int idExameConsulta)
        {
            return View(db.ConsultaExames.Find(idExameConsulta));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Médico,Laboratório,Secretária")]
        public ActionResult AlterarExame(ConsultaExame model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ExamePendente");
            }

            return View(model);
        }

        [Authorize(Roles = "Paciente")]
        public ActionResult ConsultarResultado()
        {
            int idPaciente = Convert.ToInt32(Session["id"].ToString());
            List<Consulta> consulta = db.Consulta.Where(p => p.PacienteId == idPaciente && p.Realizada && (p.ConsultaExame.Where(d => d.Realizado).Count()>0)).ToList();

            List<ListarExames> listaExamePaciente = new List<ListarExames>();
            foreach (var item in consulta)
            {
                List<ConsultaExame> exameList = item.ConsultaExame.Where(d => d.Realizado).ToList();

                foreach (var item2 in exameList)
                {
                    ListarExames itemTemp = new ListarExames();

                    itemTemp.Paciente = db.Paciente.Where(p => p.Id == item.PacienteId).FirstOrDefault().Nome;
                    itemTemp.Medico = db.Medico.Where(p => p.Id == item.MedicoId).FirstOrDefault().Nome;

                    itemTemp.ExameId = item2.ExameId;
                    itemTemp.ConsultaId = item2.ConsultaId;
                    itemTemp.DataConsulta = item.Data;
                    itemTemp.Id = item2.Id;

                    listaExamePaciente.Add(itemTemp);
                }
            }

            return View(listaExamePaciente.OrderBy(p => p.DataConsulta).OrderBy(p => p.Paciente).ToList());
        }

        [Authorize(Roles = "Paciente,Médico,Secretária")]
        public ActionResult DetalheExame(int idExameConsulta)
        {
            return View(db.ConsultaExames.Find(idExameConsulta));
        }

    }
}