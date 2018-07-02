using GerenciadorFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorFinanceiro.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        [Authorize]
        public ActionResult RelatorioPorData()
        {

            FinanceiroBanco db = new FinanceiroBanco();
            PesquisarPeriodo pesquisarPeriodo = new PesquisarPeriodo();
            //pesquisarPeriodo.usuarioId = Convert.ToInt16(Session["id"]);
            pesquisarPeriodo.usuarioCpf = HttpContext.User.Identity.Name;

            //Busca lista de cooperativas se o usuário pertencer a alguma (se ele for usuário)
            pesquisarPeriodo.cooperativas = db.cooperativa.Where(x => x.usuario.FirstOrDefault().cpf == pesquisarPeriodo.usuarioCpf).Select(z => z.nome).ToList();

            //Se não tiver nenhum na lista (usuário administrado) , mostre todas as cooperativas para ele escolher uma.
            if (pesquisarPeriodo.cooperativas.Count() == 0)
            {
                pesquisarPeriodo.cooperativas = db.cooperativa.Select(z => z.nome).ToList();
            }

            return View(pesquisarPeriodo);
        }

        [HttpPost]
        public ActionResult RelatorioPorData(PesquisarPeriodo pesquisarPeriodo)
        {
            FinanceiroBanco db = new FinanceiroBanco();

            //Preenchendo novamente a lista de cooperativas
            //Busca lista de cooperativas se o usuário pertencer a alguma (se ele for usuário)
            pesquisarPeriodo.cooperativas = db.cooperativa.Where(x => x.usuario.FirstOrDefault().cpf == pesquisarPeriodo.usuarioCpf).Select(z => z.nome).ToList();

            //Se não tiver nenhum na lista (usuário administrado) , mostre todas as cooperativas para ele escolher uma.
            if (pesquisarPeriodo.cooperativas.Count() == 0)
            {
                pesquisarPeriodo.cooperativas = db.cooperativa.Select(z => z.nome).ToList();
            }

            //Verificação da validade das datas.
            if (pesquisarPeriodo.inicio > pesquisarPeriodo.fim)
            {
                TempData["erro"] = "Data de início deve ser menor ou igual a data final";
                return View(pesquisarPeriodo);
            }

            var queryCooperativaId = db.cooperativa.Where(x => x.nome.Equals(pesquisarPeriodo.cooperativa)).FirstOrDefault();
            if (queryCooperativaId == null)
            {
                if (pesquisarPeriodo.inicio.Year == 1000 && pesquisarPeriodo.fim.Year == 1000)
                    pesquisarPeriodo.lancamentos = db.lancamento.OrderByDescending(x => x.data).ToList();
                else
                    pesquisarPeriodo.lancamentos = db.lancamento.Where(x => x.data >= pesquisarPeriodo.inicio && x.data <= pesquisarPeriodo.fim).OrderByDescending(x => x.data).ToList();

            }
            else
            {
                var cooperativaId = queryCooperativaId.id;
                //Se o usuário não tiver preenchido as datas, traga todos os registros.
                if (pesquisarPeriodo.inicio.Year == 1 && pesquisarPeriodo.fim.Year == 1)
                    pesquisarPeriodo.lancamentos = db.lancamento.Where(x => x.cooperativa_id == cooperativaId).OrderBy(x => x.tipo).ToList();
                //Caso o usuário preencha as datas, faça uma busca do intervalo.
                else
                    pesquisarPeriodo.lancamentos = db.lancamento.Where(x => x.cooperativa_id == cooperativaId && x.data >= pesquisarPeriodo.inicio && x.data <= pesquisarPeriodo.fim).OrderBy(x => x.tipo).ToList();

            }
            if (!pesquisarPeriodo.pago.Equals("ambos"))
            {
                if (pesquisarPeriodo.pago.Equals("true"))
                {
                    pesquisarPeriodo.lancamentos = pesquisarPeriodo.lancamentos.Where(x => x.pago == true).ToList();
                }
                else
                {
                    pesquisarPeriodo.lancamentos = pesquisarPeriodo.lancamentos.Where(x => x.pago == false).ToList();
                }

            }
            pesquisarPeriodo.totalFixas = pesquisarPeriodo.lancamentos.Where(x => x.tipo.Equals("Fixo")).Sum(x => x.valor);
            pesquisarPeriodo.totalVariadas = pesquisarPeriodo.lancamentos.Where(x => x.tipo.Equals("Variável")).Sum(x => x.valor);
            pesquisarPeriodo.totalGeral = pesquisarPeriodo.totalFixas + pesquisarPeriodo.totalVariadas;

            if (pesquisarPeriodo.lancamentos.Count == 0)
            {
                TempData["erro"] = "Não existem registros para o período buscado.";
                return View(pesquisarPeriodo);
            }


            return View(pesquisarPeriodo);
        }
    }
}