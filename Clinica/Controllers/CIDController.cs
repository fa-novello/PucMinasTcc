using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Controllers
{
    [Authorize(Roles = "Secretária,Médico")]
    public class CIDController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        // GET: CID
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(string inputBusca)
        {
            if (inputBusca == null)
            {
                return View(db.CID.OrderBy(p => p.Codigo).ToList());
            }
            else
            {
                return View(db.CID.Where(p => p.Descricao.IndexOf(inputBusca) > 0).OrderBy(p => p.Codigo).ToList());
            }
        }
    }
}