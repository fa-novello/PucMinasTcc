using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Controllers
{
    [Authorize(Roles = "Secretária")]
    public class PessoaController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        // GET: Pessoa
        public ActionResult Index()
        {
            return View(db.Pessoa.ToList());
        }
    }
}