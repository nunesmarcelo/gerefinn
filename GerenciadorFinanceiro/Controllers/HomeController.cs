using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorFinanceiro.Controllers
{
    public class HomeController : Controller
    {
        #region [ Index ] 
        public ActionResult Index()
        {
            return View();
        }
        #endregion

    }
}