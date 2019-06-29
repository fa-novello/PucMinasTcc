using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Clinica.Controllers
{
    public class AcessoController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();
        // GET: Acesso
        public ActionResult Index()
        {
            return View();
        }

        //----------------------------------------------

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TrocarSenha()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocarSenha(LoginModel model)
        {
            int usuario = Convert.ToInt32(Session["id"].ToString());
            var objPessoa = db.Pessoa.Where(f => f.Id.Equals(usuario) && f.Senha.Equals(model.Senha)).FirstOrDefault();

            if (objPessoa != null)
            {
                if (model.NovaSenha != model.ConfirmarNovaSenha)
                {
                    ModelState.AddModelError("", "Senha nova não confere com a confirmação da senha");
                }
                else
                {
                    objPessoa.Senha = model.NovaSenha;
                    db.Entry(objPessoa).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Senha atual é inválida");
            }
            return View(model);
        }

        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logar(LoginModel model)
        {
            if ((!ModelState.IsValid) || (model == null))
            {
                ModelState.AddModelError("", "Usuário e/ou senha inválidos");
                return View();
            }

            using (db)
            {
                var objPessoa = db.Pessoa.Where(f => f.Login.Equals(model.Login) && f.Senha.Equals(model.Senha)).FirstOrDefault();

                if (objPessoa != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);

                    var authTicket = new FormsAuthenticationTicket(1, model.Login, DateTime.Now, DateTime.Now.AddMinutes(2000), false, objPessoa.Perfil.ToString());
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    Session["id"] = objPessoa.Id.ToString();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário e/ou senha inválidos");
                    return View("Logar");
                }
            }
        }

    }
}