using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string submit)
        {
            var btnPressedName = submit;

            if (btnPressedName.Equals("Logar"))
            {
                LoginModel user = model.Login_();
                bool logged = true;


                if (!user.Login.Equals(model.Login))
                    logged = false;
                if (!user.Password.Equals(model.Password))
                    logged = false;

                if (logged)
                {
                    Session["UserLogin"] = user;
                    return RedirectToAction("Index", "Pesquisa");
                }
            }

            if (btnPressedName.Equals("Cadastrar"))
            {

            }

            var canedo = model;
            return View();
        }
    }
}