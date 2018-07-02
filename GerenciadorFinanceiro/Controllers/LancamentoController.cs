using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GerenciadorFinanceiro.Models;

namespace GerenciadorFinanceiro.Controllers
{
    public class LancamentoController : Controller
    {
        // GET: Lancamento
        [Authorize]
        public ActionResult Index()
        {
            FinanceiroBanco db = new FinanceiroBanco();
            FinanceiroBanco db2 = new FinanceiroBanco();
            CadastrarLancamento cadastrarLancamento = new CadastrarLancamento();
            List<lancamento> listaLancamentos;
            //Inicializar listas -> vamos usar o .add , ele necessita a inicialização.
            cadastrarLancamento.lancamentos = new List<lancamento>();
            cadastrarLancamento.cooperativasNomes = new List<string>();

            //MONTANDO A TABELA DE LANÇAMENTOS  E O DROPDOWN LIST DE COOPERATIVAS.
            try
            {
                var usuarioCpf = HttpContext.User.Identity.Name;
                //Busca lista de cooperativas se o usuário pertencer a alguma (se ele for usuário)
                var cooperativas = db.cooperativa.Where(x => x.usuario.FirstOrDefault().cpf == usuarioCpf).Select(w => new { w.nome, w.id }).OrderBy(x => x.nome).ToList();

                //Se não tiver nenhum na lista (usuário administrado) , mostre todas as cooperativas para ele escolher uma.
                if (cooperativas.Count() == 0)
                {
                    cooperativas = db.cooperativa.Select(z => new { z.nome, z.id }).OrderBy(x => x.nome).ToList();
                }



                //Para cada cooperativa selecionada (Tanto todas (para administrador) quanto as filtradas (para usuários))
                foreach (var cooperativaId in cooperativas)
                {
                    // Preencha o dropdown list (para inserção)
                    cadastrarLancamento.cooperativasNomes.Add(cooperativaId.nome);

                    //Pegue a Lista de lançamentos (para tabela)
                    listaLancamentos = db2.lancamento.Where(x => x.cooperativa_id == cooperativaId.id).OrderByDescending(x => x.data).ToList();
                    //Para cada lançamento
                    foreach (var lancamento in listaLancamentos)
                    {
                        //Adicione ele à lista de lançamentos a serem mostrados.
                        cadastrarLancamento.lancamentos.Add(lancamento);
                    }

                }

                return View(cadastrarLancamento);
            }
            catch (Exception e)
            {
                TempData["erro"] = "Erro ao rastrear a permissão do usuário e selecionar os dados das cooperativas. <br> Erro:" + e;
                return View(cadastrarLancamento);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(CadastrarLancamento cadastrarLancamento)
        {
            FinanceiroBanco db = new FinanceiroBanco();
            lancamento lancamento = new lancamento();
            if (ModelState.IsValid)
            {
                try
                {
                    lancamento.descricao = cadastrarLancamento.lancamento.descricao;
                    lancamento.quantidade = cadastrarLancamento.lancamento.quantidade;
                    lancamento.tipo = cadastrarLancamento.lancamento.tipo;
                    lancamento.pago = cadastrarLancamento.lancamento.pago;
                    lancamento.valor = cadastrarLancamento.lancamento.valor;
                    lancamento.dataemissao = cadastrarLancamento.lancamento.dataemissao;
                    lancamento.datacadastro = cadastrarLancamento.lancamento.datacadastro;
                    lancamento.datavencimento = cadastrarLancamento.lancamento.datavencimento;
                    if (cadastrarLancamento.cooperativasNomes != null)
                    {
                        lancamento.cooperativa = db.cooperativa.Where(x => x.nome == cadastrarLancamento.cooperativaSelecionada.ToString()).FirstOrDefault();
                        lancamento.cooperativa_id = lancamento.cooperativa.id;
                    }
                    db.lancamento.Add(lancamento);
                    db.SaveChanges();
                    TempData["sucesso"] = "Lançamento cadastrado com sucesso!";
                    return RedirectToAction("Index", "Lancamento");
                }
                catch (Exception e)
                {
                    TempData["erro"] = "Erro na inserção ao banco de dados !! Por favor tente novamente. Erro:" + e;
                    return View(cadastrarLancamento);
                }
            }
            else
            {
                TempData["erro"] = "O preenchimento dos dados não foi válido.";
                return View(cadastrarLancamento);
            }

        }

        [HttpGet]
        public ActionResult Pagar(int idLancamento, string retorno, string pasta)
        {
            FinanceiroBanco db = new FinanceiroBanco();

            bool? pago = db.lancamento.Where(x => x.id == idLancamento).Select(x => x.pago).FirstOrDefault();

            if (pago == null)
            {
                TempData["erro"] = "Erro ao pagar lançamento - algum problema com o banco de dados. Se o problema persistir, contate o administrador.";

            }
            else
            {
                try
                {
                    if (pago == false)
                        db.lancamento.Where(x => x.id == idLancamento).FirstOrDefault().pago = true;

                    else if (pago == true)
                        db.lancamento.Where(x => x.id == idLancamento).FirstOrDefault().pago = false;

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["erro"] = "Erro ao completar a operação. Saída: " + e;
                }
            }
            return RedirectToAction(retorno, pasta);
        }
    }
}