using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Controllers
{
    [Authorize(Roles = "Secretária")]
    public class AgendaController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Listar(int tipoFiltro, int? medicoId)
        {
            List<AgendaViewModel> model = new List<AgendaViewModel>();

            if (medicoId == null)
            {
                medicoId = db.Medico.FirstOrDefault().Id;
            }
            DateTime date = DateTime.Now.Date;

            List<Medico> medico = db.Medico.Where(p => p.Id == medicoId).ToList();
            List<Agenda> agenda;
            switch (tipoFiltro)
            {
                //somente agenda livre
                case 1:
                    agenda = db.Agenda.Where(p => p.Data.CompareTo(date) >= 0 && (p.PacienteId == 0 || p.PacienteId == null)).ToList();
                    break;
                //somente agenda não confirmada
                case 2:
                    agenda = db.Agenda.Where(p => p.Data.CompareTo(date) >= 0 && p.PacienteId > 0 && !p.Confirmada && !p.ConsultaCriada).ToList();
                    break;
                //consultar agendamentos anteriores
                case 3:
                    agenda = db.Agenda.Where(p => p.Data.CompareTo(date) < 0).ToList();
                    break;
                //consultar proximos agendamentos
                case 4:
                    agenda = db.Agenda.Where(p => p.Data.CompareTo(date) >= 0).ToList();
                    break;
                //0 - somente data atual
                default:
                    agenda = db.Agenda.Where(p => p.Data.CompareTo(date) == 0).ToList();
                    break;
            }
        
            foreach (var item in medico)
            {
                if (agenda.Where(p => p.MedicoId == item.Id).FirstOrDefault() != null)
                {
                    model.Add(new AgendaViewModel
                    {
                        Id = item.Id,
                        Medico = item.Nome + " - " + item.EspecialidadeDescricao,
                        Agendas = agenda.Where(p => p.MedicoId == item.Id).
                            OrderBy(p => p.Horario).OrderBy(p => p.Data).ToList()
                    });
                }
            }

            ViewBag.MedicoId = new SelectList(db.Medico.Where(p => p.Ativo), "Id", "Nome", "");
            return View(model);
        }

        public ActionResult ConfListar()
        {
            return View(db.AgendaConfiguracao.OrderBy(p => p.DiaSemana).OrderBy(p => p.MedicoId).ToList());
        }

        public ActionResult CancelarAgenda(int id)
        {
            Agenda model = db.Agenda.Where(p => p.Id == id).FirstOrDefault();
            if (!model.ConsultaCriada)
            {
                model.PacienteId = null;
                model.PacienteNome = null;
                model.Confirmada = false;
                model.Convenio = null;
                model.Telefone = null;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Listar", new { tipoFiltro = 0});
        }

        public ActionResult ConfIncluir()
        {
            ViewBag.MedicoId = new SelectList(db.Medico.Where(p => p.Ativo), "Id", "Nome", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfIncluir(AgendaConfiguracao model)
        {
            if (ModelState.IsValid)
            {
                if (model.MedicoId != 0)
                {
                    AgendaConfiguracao obj = db.AgendaConfiguracao.Where(p => p.MedicoId == model.MedicoId &&
                            p.DiaSemana == model.DiaSemana).FirstOrDefault();
                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Já existe uma configuração de agenda neste dia da semana para esse médico");
                    }
                    else
                    {
                        db.AgendaConfiguracao.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("ConfListar");
                    }
                }
            }

            ViewBag.MedicoId = new SelectList(db.Medico.Where(p => p.Ativo), "Id", "Nome", "");
            return View(model);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Agenda agenda = db.Agenda.Find(id);

            if (agenda == null)
            {
                return HttpNotFound();
            }
            List<Medico> medico = db.Medico.ToList();
            //if ((agenda.Medico == null) && agenda.MedicoId > 0)
            //{
            //    agenda.Medico = medico.Where(p => p.Id == agenda.MedicoId).FirstOrDefault();
            //}
            ViewBag.MedicoNome = (medico.Where(p => p.Id == agenda.MedicoId)).FirstOrDefault().Nome;
            ViewBag.PacienteId = new SelectList(db.Paciente, "Id", "Nome", "");

            return View(agenda);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Agenda model)
        {
            if (ModelState.IsValid)
            {
                if (model.MedicoId > 0)
                {
                    //if ((model.Paciente == null) && model.PacienteId > 0)
                    //{
                    //    List<Paciente> paciente = db.Paciente.ToList();
                    //    model.Paciente = paciente.Where(p => p.Id == model.PacienteId).FirstOrDefault();
                    //}

                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Listar", new { tipoFiltro = 0 });
                }
            }
            List<Medico> medico = db.Medico.ToList();

            ViewBag.MedicoNome = (medico.Where(p => p.Id == model.MedicoId)).FirstOrDefault().Nome;
            ViewBag.PacienteId = new SelectList(db.Paciente, "Id", "Nome", "TEST");
            return View(model);
        }

        public ActionResult ConfEditar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AgendaConfiguracao agendaConf = db.AgendaConfiguracao.Find(id);

            if (agendaConf == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicoId = new SelectList(db.Medico.Where(p => p.Ativo), "Id", "Nome", "");
            return View(agendaConf);
        }

        public ActionResult ConverterConsulta(int? id)
        {
            Agenda agenda = db.Agenda.Find(id);
            bool criarConsulta = true;
            if (agenda.PacienteId == null)
            {
                ModelState.AddModelError("", "O paciente é obrigatório para converter a consulta");
                criarConsulta = false;
            }

            if (criarConsulta)
            {
                //List<Medico> medico = db.Medico.ToList();
                //List<Paciente> paciente = db.Paciente.ToList();

                Consulta model = new Consulta
                {
                    Data = agenda.Data,
                    Horario = agenda.Horario,
                    MedicoId = agenda.MedicoId,
                    PacienteId = ((int)agenda.PacienteId)

                    //Medico = medico.Where(p => p.Id == agenda.MedicoId).FirstOrDefault(),
                    //Paciente = paciente.Where(p => p.Id == agenda.PacienteId).FirstOrDefault()
                };

                db.Consulta.Add(model);
                db.SaveChanges();

            }
            return DefinirConsultaCriada(agenda);
        }

        public ActionResult DefinirConsultaCriada(Agenda agenda)
        {
            agenda.ConsultaCriada = true;
            //agenda.Paciente = null;
            db.Entry(agenda).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Listar", new { tipoFiltro = 0 });
        }

        public ActionResult Gerar()
        {
            ViewBag.MedicoId = new SelectList(db.Medico.Where(p => p.Ativo), "Id", "Nome", "");
            ViewBag.PacienteId = new SelectList(db.Paciente, "Id", "Nome", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Gerar(AgendaGerar model)
        {
            if (ModelState.IsValid)
            {
                bool configuracaoInvalida = false;
                if (model.MedicoId == 0)
                {
                    configuracaoInvalida = true;
                    ModelState.AddModelError("", "O médico é obrigatório");
                }
                if (model.DataFinal < model.DataInicial)
                {
                    configuracaoInvalida = true;
                    ModelState.AddModelError("", "A data final não pode ser menor que a inicial");
                }

                if (!configuracaoInvalida)
                {
                    Agenda obj = db.Agenda.Where(p => p.MedicoId == model.MedicoId && p.Data >= model.DataInicial && p.Data <= model.DataFinal).FirstOrDefault();

                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Há um conflito de agendas. Já existe agenda para o dia " + obj.Data);
                    }
                    else
                    {
                        DateTime firstSunday = new DateTime(1753, 1, 7);
                        for (int i = 0; i < (model.DataFinal - model.DataInicial).TotalDays; i++)
                        {
                            //confere se o dia da semana/hora terá agenda criada
                            AgendaConfiguracao agendaConfiguracao = db.AgendaConfiguracao.Where(p => p.MedicoId == model.MedicoId && (DbFunctions.DiffDays(firstSunday, DbFunctions.AddDays(model.DataInicial,i)) % 7) == (int)(p.DiaSemana)).FirstOrDefault();
                            if (agendaConfiguracao != null)
                            {
                                DateTime horaInicio = agendaConfiguracao.HorarioInicio;
                                for (int j = 0; j < agendaConfiguracao.LimiteConsultas; j++)
                                {
                                    Agenda agenda = new Agenda();
                                    agenda.Data = model.DataInicial.AddDays(i);
                                    agenda.MedicoId = model.MedicoId;
                                    agenda.Horario = horaInicio;
                                    horaInicio = horaInicio.AddMinutes(agendaConfiguracao.TempoConsulta);
                                    //db.Agenda.Add(agenda);
                                    db.Entry(agenda).State = EntityState.Added;
                                    db.SaveChanges();
                                }
                            }
                        }

                        return RedirectToAction("Listar", "Agenda", new { tipoFiltro = 0 });
                    }
                }
            }

            ViewBag.MedicoId = new SelectList(db.Medico.Where(p => p.Ativo), "Id", "Nome", "");
            return View();
        }

        // GET: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        public ActionResult ConfExcluir(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AgendaConfiguracao confAgenda = db.AgendaConfiguracao.Find(id);
            if (confAgenda == null)
            {
                return HttpNotFound();
            }
            return View(confAgenda);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("ConfExcluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusaoConfiguracao(int id)
        {
            AgendaConfiguracao confAgenda = db.AgendaConfiguracao.Find(id);
            db.AgendaConfiguracao.Remove(confAgenda);
            db.SaveChanges();
            return RedirectToAction("ConfListar");
        }
    }
}