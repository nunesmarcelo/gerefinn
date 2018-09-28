using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorFinanceiro.Controllers
{
    public class CaixaController : Controller
    {
        // GET: Caixa
        public ActionResult AbrirCaixa()
        {
            return View();
        }
    }
}