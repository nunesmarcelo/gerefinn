using GerenciadorFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorFinanceiro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize]
        //public ActionResult Index()
        //{
        //    FinanceiroBanco db = new FinanceiroBanco();
        //    FinanceiroBanco db2 = new FinanceiroBanco();
        //    List<lancamento> model = new List<lancamento>();
        //    List<lancamento> listaLancamentos;

        //    var usuarioCpf = HttpContext.User.Identity.Name;
        //    //Busca lista de cooperativas se o usuário pertencer a alguma (se ele for usuário)
        //    var cooperativas = db.cooperativa.Where(x => x.usuario.FirstOrDefault().cpf == usuarioCpf).Select(w => new { w.nome, w.id }).OrderBy(x => x.nome).ToList();

        //    //Se não tiver nenhum na lista (usuário administrado) , mostre todas as cooperativas para ele escolher uma.
        //    if (cooperativas.Count() == 0)
        //    {
        //        cooperativas = db.cooperativa.Select(z => new { z.nome, z.id }).OrderBy(x => x.nome).ToList();
        //    }

        //    //Para cada cooperativa selecionada (Tanto todas (para administrador) quanto as filtradas (para usuários))
        //    foreach (var cooperativaId in cooperativas)
        //    {

        //        //Pegue a Lista de lançamentos (para tabela)
        //        listaLancamentos = db2.lancamento.Where(x => x.data.Value.Day >= DateTime.Now.Day - 7 && x.cooperativa_id == cooperativaId.id).OrderByDescending(x => x.data).ToList();
        //        //Para cada lançamento
        //        foreach (var lancamento in listaLancamentos)
        //        {
        //            //Adicione ele à lista de lançamentos a serem mostrados.
        //            model.Add(lancamento);
        //        }

        //    }

        //    return View(model);
        //}

        //[Authorize(Roles = "A")]
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}


    }
}