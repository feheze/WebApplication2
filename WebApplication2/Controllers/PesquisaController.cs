using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class PesquisaController : Controller
    {
        // GET: Pesquisa
        public ActionResult Index(SearchViewModel model)
        {
            

            if(Session["UserLogin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (model.Id == 0)
            {
                model = new SearchViewModel();

                LoginModel user = (LoginModel)Session["UserLogin"];

                model.Id = user.IdLogin;
                model.Login = user.Login;
                model.Password = user.Password;
                model.Deleted = user.Deleted;
            }
                

            

            return View(model);
        }

        

        [HttpPost]
        public ActionResult Search(SearchViewModel model, string submit)
        {
            var btnPressedName = submit;

            if (btnPressedName.Equals("Salvar"))
            {
                model.Save();

                return View("Index", new SearchViewModel());
            }

            if (btnPressedName.Equals("Procurar"))
            {
                if (model.SearchBy.Length > 0)
                {
                    model = model.Search();
                    return RedirectToAction("Index", "Pesquisa", model);
                }
                    
                else
                    return View("Index", new SearchViewModel());

            }

            return View();
        }
    }
}