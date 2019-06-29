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
    public class ExameController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(string inputBusca)
        {
            if (inputBusca == null)
            {
                return View(db.Exame.OrderBy(p => p.Descricao).ToList());
            }
            else
            {
                return View(db.Exame.Where(p => p.Descricao.IndexOf(inputBusca) > 0).OrderBy(p => p.Descricao).ToList());
            }
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Exame model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Descricao.Trim()))
                {
                    Exame obj = db.Exame.Where(p => p.Codigo == model.Codigo).FirstOrDefault();
                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com esse código");
                    }
                    else
                    {
                        db.Exame.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Editar(int id)
        {
            Exame exame = db.Exame.Find(id);

            if (exame == null)
            {
                return HttpNotFound();
            }

            return View(exame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Exame model)
        {
            if (ModelState.IsValid)
            {
                if ((model.Descricao != null) && (model.Descricao != ""))
                {
                    Exame obj = db.Exame.Where(p => p.Descricao == model.Descricao && p.Id != model.Id).FirstOrDefault();

                    if (obj != null)
                    {
                        ModelState.AddModelError("", "Já existe um cadastro com essa descrição");
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
                ModelState.AddModelError("", "A descrição é obrigatória");
            }

            return View(model);
        }

        // GET: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Exame exame = db.Exame.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Posts/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExclusao(int id)
        {
            Exame exame = db.Exame.Find(id);
            ConsultaExame consultaEx= db.ConsultaExames.Where(p => p.ExameId == id).FirstOrDefault();
            if (consultaEx != null)
            {
                ModelState.AddModelError("", "O exame não pode ser excluído pois está incluído em consulta");
                return View(exame);
            }

            db.Exame.Remove(exame);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }
    }
}