using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoVendas.Cap03.Controllers
{
    public class HomeController : Controller 
    {
        // GET: Home
        public ActionResult Index()// metodo Index
        {
            return View();
        }

        public ActionResult MostrarErro()
        {
            ViewBag.MensagemErro = "Erro interno do servidor";
            return View("Error");
        }
    }
}