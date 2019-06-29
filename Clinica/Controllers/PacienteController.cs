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
    public class PacienteController : PessoaController
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Listar(string inputBusca)
        {
            if (inputBusca == null)
            {
                return View(db.Paciente.OrderBy(p => p.Nome).ToList());
            }
            else
            {
                return View(db.Paciente.Where(p => p.Nome.IndexOf(inputBusca) > 0).OrderBy(p => p.Nome).ToList());
            }
        }

        public ActionResult Incluir()
        {
            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Paciente model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Login.Trim()))
                {
                    Paciente pessoa = db.Paciente.Where(p => p.Login == model.Login).FirstOrDefault();
                    if (pessoa != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com este login");
                    }
                    else
                    {
                        model.Perfil = PerfilEnumerator.Paciente;
                        db.Paciente.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
            }

            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            return View(model);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Paciente paciente = db.Paciente.Find(id);

            if (paciente == null)
            {
                return HttpNotFound();
            }

            return View(paciente);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Paciente paciente = db.Paciente.Find(id);

            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Paciente pessoa)
        {
            if (ModelState.IsValid)
            {
                if ((pessoa.Login != null) && (pessoa.Login != ""))
                {
                    Pessoa objPessoa = db.Pessoa.Where(p => p.Login == pessoa.Login && p.Id != pessoa.Id).FirstOrDefault();

                    if (objPessoa != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com este Login");
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

            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExclusao(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            Consulta consulta = db.Consulta.Where(p => p.PacienteId == id).FirstOrDefault();
            if (consulta != null)
            {
                ModelState.AddModelError("", "O paciente não pode ser excluído pois possui registro de consulta.");
                return View(paciente);
            }

            Agenda agenda = db.Agenda.Where(p => p.PacienteId == id).FirstOrDefault();
            if (agenda != null)
            {
                ModelState.AddModelError("", "O paciente não pode ser excluído pois possui registro em agenda.");
                return View(paciente);
            }

            db.Paciente.Remove(paciente);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }
    }
}