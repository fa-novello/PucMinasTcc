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
    public class ColaboradorController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Listar(string inputBusca)
        {
            if (inputBusca == null)
            {
                return View(db.Colaborador.Where(p => p.Perfil != PerfilEnumerator.Médico).OrderBy(p => p.Nome).ToList());
            }
            else
            {
                return View(db.Colaborador.Where(p => (p.Nome.IndexOf(inputBusca) > 0) && 
                            (p.Perfil != PerfilEnumerator.Médico)).OrderBy(p => p.Nome).ToList());
            }
        }

        public ActionResult Incluir()
        {
            ViewBag.HidePerfil = (Request.FilePath.IndexOf("Medico") > 0 || Request.FilePath.IndexOf("Paciente") > 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Incluir(Colaborador model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Login.Trim()))
                {
                    Colaborador pessoa = db.Colaborador.Where(p => p.Login == model.Login).FirstOrDefault();
                    if (pessoa != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com este login");
                    }
                    else
                    {
                        db.Colaborador.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
            }
            
            return View(model);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Colaborador colaborador = db.Colaborador.Find(id);

            if (colaborador == null)
            {
                return HttpNotFound();
            }

            return View(colaborador);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Colaborador colaborador = db.Colaborador.Find(id);

            if (colaborador == null)
            {
                return HttpNotFound();
            }

            return View(colaborador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Colaborador pessoa)
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

            Colaborador colab = db.Colaborador.Find(id);
            if (colab == null)
            {
                return HttpNotFound();
            }
            return View(colab);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExclusao(int id)
        {
            Colaborador colab = db.Colaborador.Find(id);
            
            db.Colaborador.Remove(colab);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }
    }
}