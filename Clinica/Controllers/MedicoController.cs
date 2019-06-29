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
    public class MedicoController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Index()
        {
            return View(db.Pessoa.ToList());
        }

        public ActionResult Listar(string inputBusca)
        {
            if (inputBusca == null)
            {
                return View(db.Medico.OrderBy(p => p.Nome).ToList());
            }
            else
            {
                return View(db.Medico.Where(p => p.Nome.IndexOf(inputBusca) > 0).OrderBy(p => p.Nome).ToList());
            }
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Medico medico = db.Medico.Find(id);

            if (medico == null)
            {
                return HttpNotFound();
            }

            ViewBag.EspecialidadeId = new SelectList(db.Especialidade, "Id", "Descricao", "");
            return View(medico);
        }

        public ActionResult Incluir()
        {
            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidade, "Id", "Descricao", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Incluir(Medico model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Login.Trim()))
                {
                    Pessoa pessoa = db.Pessoa.Where(p => p.Login == model.Login).FirstOrDefault();
                    if (pessoa != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com este login");
                    }
                    else
                    {
                        pessoa = db.Medico.Where(p => p.CRMNumero == model.CRMNumero).FirstOrDefault();
                        if (pessoa != null)
                        {
                            ModelState.AddModelError("", "Já existe um cadastro com este CRM");
                        }
                        else
                        {
                            model.Perfil = PerfilEnumerator.Médico;
                            db.Medico.Add(model);
                            db.SaveChanges();
                            return RedirectToAction("Listar");
                        }
                    }
                }
            }

            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidade, "ID", "Descricao", "");
            return View(model);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Medico medico = db.Medico.Find(id);

            if (medico == null)
            {
                return HttpNotFound();
            }

            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidade, "Id", "Descricao", "");
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Medico pessoa)
        {
            if (ModelState.IsValid)
            {
                Pessoa obj = db.Pessoa.Where(p => p.Login == pessoa.Login).FirstOrDefault();
                if (obj != null)
                {
                    ModelState.AddModelError("", "Já existe um cadastro com este login");
                }
                else
                {
                    pessoa = db.Medico.Where(p => p.CRMNumero == pessoa.CRMNumero).FirstOrDefault();
                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com este CRM");
                    }
                    else
                    {
                        db.Entry(pessoa).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }

                ModelState.AddModelError("", "Login é obrigatório");
            }

            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidade, "ID", "Descricao", "");
            return View(pessoa);
        }

        // GET: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExclusao(int id)
        {
            Medico medico = db.Medico.Find(id);
            Consulta consulta = db.Consulta.Where(p => p.MedicoId == id).FirstOrDefault();
            if (consulta != null)
            {
                ModelState.AddModelError("", "O médico não pode ser excluído pois possui registro de consulta");
                return View(medico);
            }

            Agenda agenda = db.Agenda.Where(p => p.MedicoId == id).FirstOrDefault();
            if (agenda != null)
            {
                ModelState.AddModelError("", "O médico não pode ser excluído pois possui registro em agenda");
                return View(medico);
            }

            AgendaConfiguracao agendaConf = db.AgendaConfiguracao.Where(p => p.MedicoId == id).FirstOrDefault();
            if (agendaConf != null)
            {
                ModelState.AddModelError("", "O médico não pode ser excluído pois possui registro em configuração de agenda");
                return View(medico);
            }

            db.Medico.Remove(medico);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }
    }
}