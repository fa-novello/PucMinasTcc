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
    [Authorize(Roles = "Secretária, Médico")]
    public class EspecialidadeController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Editar(int id)
        {
            Especialidade especialidade = db.Especialidade.Find(id);

            if (especialidade == null)
            {
                return HttpNotFound();
            }

            return View(especialidade);
        }

        public ActionResult Incluir()
        {
            return View();
        }

        public ActionResult Listar(string inputBusca)
        {
            if (inputBusca == null)
            {
                return View(db.Especialidade.OrderBy(p => p.Descricao).ToList());
            }
            else
            {
                return View(db.Especialidade.Where(p => p.Descricao.IndexOf(inputBusca) > 0).OrderBy(p => p.Descricao).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Especialidade model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Descricao.Trim()))
                {
                    Especialidade obj = db.Especialidade.Where(p => p.Descricao == model.Descricao).FirstOrDefault();
                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com essa descrição");
                    }
                    else
                    {
                        db.Especialidade.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                if ((especialidade.Descricao != null) && (especialidade.Descricao != ""))
                {
                    Especialidade obj = db.Especialidade.Where(p => p.Descricao == especialidade.Descricao && p.Id != especialidade.Id).FirstOrDefault();

                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com essa descrição");
                    }
                    else
                    {
                        db.Entry(especialidade).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
                ModelState.AddModelError("", "A descrição é obrigatória");
            }

            return View(especialidade);
        }

        // GET: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Especialidade especialidade = db.Especialidade.Find(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExclusao(int id)
        {
            Especialidade especialidade = db.Especialidade.Find(id);
            Medico medico = db.Medico.Where(p => p.EspecialidadeId == id).FirstOrDefault();
            if (medico != null)
            {
                ModelState.AddModelError("", "A especialidade não pode ser excluída pois está sendo usada");
                return View(especialidade);
            }

            db.Especialidade.Remove(especialidade);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }
    }
}