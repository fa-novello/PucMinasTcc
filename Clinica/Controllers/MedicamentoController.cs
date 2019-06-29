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
    public class MedicamentoController : Controller
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
                return View(db.Medicamento.OrderBy(p => p.GenericoNome).ToList());
            }
            else
            {
                return View(db.Medicamento.Where(p => p.GenericoNome.IndexOf(inputBusca) > 0).OrderBy(p => p.GenericoNome).ToList());
            }
        }

        public ActionResult Incluir()
        {
            return View();
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Medicamento medicamento = db.Medicamento.Find(id);

            if (medicamento == null)
            {
                return HttpNotFound();
            }

            return View(medicamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Medicamento model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Fabricante.Trim()))
                {
                    Medicamento medicamento = db.Medicamento.Where(p => p.FabricanteNome == model.FabricanteNome).FirstOrDefault();
                    if (medicamento != null)
                    {
                        ModelState.AddModelError("", "Já existe esse medicamento cadastrado");
                    }
                    else
                    {
                        db.Medicamento.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Medicamento medicamento = db.Medicamento.Find(id);

            if (medicamento == null)
            {
                return HttpNotFound();
            }

            return View(medicamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                if ((medicamento.GenericoNome != null) && (medicamento.FabricanteNome != ""))
                {
                    Medicamento objMedicamento = db.Medicamento.Where(p => p.Id != medicamento.Id && p.FabricanteNome == medicamento.FabricanteNome).FirstOrDefault();

                    if (objMedicamento != null)
                    {
                        ModelState.AddModelError("", "Remédio já cadastrado");
                    }
                    else
                    {
                        db.Entry(medicamento).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Listar");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nome do fabricante é obrigatório");
                }
            }

            return View(medicamento);
        }

        // GET: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        public ActionResult Excluir(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Medicamento medicamento = db.Medicamento.Find(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            return View(medicamento);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoExclusao(int id)
        {
            Medicamento medicamento = db.Medicamento.Find(id);
            ConsultaMedicamento consultaMed = db.ConsultaMedicamentos.Where(p => p.MedicamentoId == id).FirstOrDefault();
            if (consultaMed != null)
            {
                ModelState.AddModelError("", "O medicamento não pode ser excluído pois está incluído em consulta");
                return View(medicamento);
            }

            db.Medicamento.Remove(medicamento);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }

    }
}
