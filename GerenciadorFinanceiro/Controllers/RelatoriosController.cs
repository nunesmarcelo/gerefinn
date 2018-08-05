using GerenciadorFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorFinanceiro.Controllers
{
    public class RelatoriosController : Controller
    {
        public ActionResult Index()
        {
            PesquisarPeriodo model = new PesquisarPeriodo();

            return View(model);

        }

        [HttpGet]
        public ActionResult PesquisarPeriodo(PesquisarPeriodo model)
        {
            // *********** Verificações
            if (model.inicioVencimento > model.fimVencimento)
            {
                TempData["erro"] = "Data de início da busca por VENCIMENTO deve ser menor ou igual à data de fim.";
                return RedirectToAction("Index");
            }

            if (model.inicioCadastro > model.fimCadastro)
            {
                TempData["erro"] = "Data de início da busca por CADASTRO deve ser menor ou igual à data de fim.";
                return RedirectToAction("Index");
            }

            if (model.inicioEmissao > model.fimEmissao)
            {
                TempData["erro"] = "Data de início da busca por EMISSÃO deve ser menor ou igual à data de fim.";
                return RedirectToAction("Index");
            }

            // *********** fim - Verificações
            return View(model);
        }

        //[HttpPost]
        //public  ActionResult ConsultaCategoria(string inicioVencimento)
        //{

        //    return Json(new { value = "39" , name = "20" }, JsonRequestBehavior.AllowGet);
        //}

        #region [Consulta Categoria]
        [HttpPost]
        public ActionResult ConsultaCategoria(string inicioVencimento, string fimVencimento, string inicioCadastro, string fimCadastro, string inicioEmissao, string fimEmissao)
        {
            FinanceiroBanco db = new FinanceiroBanco();

            // CONVERTER AS STRINGS VINDAS POR PARÂMETROS EM DATETIME
            DateTime inicioVencimentoo = Convert.ToDateTime(inicioVencimento);
            DateTime fimVencimentoo = Convert.ToDateTime(fimVencimento);
            DateTime inicioCadastroo = Convert.ToDateTime(inicioCadastro);
            DateTime fimCadastroo = Convert.ToDateTime(fimCadastro);
            DateTime inicioEmissaoo = Convert.ToDateTime(inicioEmissao);
            DateTime fimEmissaoo = Convert.ToDateTime(fimEmissao);

            // VARIAVEIS PARA SABER QUAIS FILTROS FORAM PREENCHIDOS
            bool verificaVencimento = false;
            bool verificaCadastro = false;
            bool verificaEmissao = false;

            if ((inicioVencimentoo.Year != 2001 && inicioVencimentoo.Month != 01 && inicioVencimentoo.Day != 01) || (fimVencimentoo.Month != 01 && fimVencimentoo.Day != 01 && fimVencimentoo.Year != 2001))
                verificaVencimento = true;

            if ((inicioCadastroo.Year != 2001 && inicioCadastroo.Month != 01 && inicioCadastroo.Day != 01) || (fimCadastroo.Month != 01 && fimCadastroo.Day != 01 && fimCadastroo.Year != 2001))
                verificaCadastro = true;

            if ((inicioEmissaoo.Year != 2001 && inicioEmissaoo.Month != 01 && inicioEmissaoo.Day != 01) || (fimEmissaoo.Month != 01 && fimEmissaoo.Day != 01 && fimEmissaoo.Year != 2001))
                verificaEmissao = true;


            //Esse queryable vai receber um conjunto de categorias (group by), que está dentro da faixa dos filtros escolhidos.
            IQueryable<IGrouping<categoria,lancamento>> query;

            if (verificaVencimento && verificaCadastro && verificaEmissao)
                query = db.lancamento.Where(x => x.datavencimento >= inicioVencimentoo && x.datavencimento <= fimVencimentoo &&
                            x.datavencimento >= inicioCadastroo && x.datavencimento <= fimCadastroo &&
                            x.datavencimento >= inicioEmissaoo && x.datavencimento <= fimEmissaoo).GroupBy(x => x.categoria);
            else if (verificaVencimento && verificaCadastro)
                query = db.lancamento.Where(x => x.datavencimento >= inicioVencimentoo && x.datavencimento <= fimVencimentoo &&
                        x.datavencimento >= inicioCadastroo && x.datavencimento <= fimCadastroo).GroupBy(x => x.categoria);
            else if (verificaVencimento && verificaEmissao)
                query = db.lancamento.Where(x => x.datavencimento >= inicioVencimentoo && x.datavencimento <= fimVencimentoo &&
                        x.datavencimento >= inicioEmissaoo && x.datavencimento <= fimEmissaoo).GroupBy(x => x.categoria);
            else if (verificaCadastro && verificaEmissao)
                query = db.lancamento.Where(x => x.datavencimento >= inicioCadastroo && x.datavencimento <= fimCadastroo &&
                        x.datavencimento >= inicioEmissaoo && x.datavencimento <= fimEmissaoo).GroupBy(x => x.categoria);
            else if (verificaVencimento)
                query = db.lancamento.Where(x => x.datavencimento >= inicioVencimentoo && x.datavencimento <= fimVencimentoo).GroupBy(x => x.categoria);
            else if (verificaCadastro)
                query = db.lancamento.Where(x => x.datavencimento >= inicioCadastroo && x.datavencimento <= fimCadastroo).GroupBy(x => x.categoria);
            else if (verificaEmissao)
                query = db.lancamento.Where(x => x.datavencimento >= inicioEmissaoo && x.datavencimento <= fimEmissaoo).GroupBy(x => x.categoria);
            else
                query = db.lancamento.GroupBy(x => x.categoria);


            List<string> names = new List<string>();
            List<string> values = new List<string>();
            List<string> rd = new List<string>();
            //LISTA DE CATEGORIAS DEFINIDA, SÓ PERCORRER IMPRIMINDO A SOMA DE VALORES.
            foreach (var lancamentoPorCategoria in query)
            {
                //Com a lista de categorias já definida, cria-se o array de nomes de tais categorias
                names.Add(lancamentoPorCategoria.Key.nome);

                rd.Add(lancamentoPorCategoria.Key.rd);

                //Adiciona os valores relativos às somas , depois de já se definir a categoria e os filtros.
                values.Add(lancamentoPorCategoria.Sum(x => x.valor).ToString());
                
            }

           
            return Json(new { nomes = names, valores = values , rd = rd }, JsonRequestBehavior.AllowGet);
        }
    }
    #endregion

}