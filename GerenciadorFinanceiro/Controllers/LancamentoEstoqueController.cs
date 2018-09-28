using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciadorFinanceiro.Models;

namespace GerenciadorFinanceiro.Controllers
{
    public class LancamentoEstoqueController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        // GET: LancamentoEstoque
        public async Task<ActionResult> Listagem()
        {
            //var lancamentoestoque = db.produtoemestoque.Include(l => l.contasaldo).Include(l => l.estoque).Include(l => l.produto).Include(l => l.usuario);
            var produtos = await db.produtoemestoque.ToListAsync();
            var valorTotal = produtos.Sum(x => x.produto.valorunitario * x.quantidade);
            var custoTotal = produtos.Sum(x => x.produto.custounitario * x.quantidade);
            TempData["valorTotal"] = valorTotal;
            TempData["custoTotal"] = custoTotal;
            return View(produtos);
        }

        // GET: LancamentoEstoque
        public async Task<ActionResult> Index()
        {
            return View(await db.lancamentoestoque.Include(l => l.contasaldo).Include(l => l.estoque).Include(l => l.produto).Include(l => l.usuario).ToListAsync());
        }


        // GET: LancamentoEstoque/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lancamentoestoque lancamentoestoque = await db.lancamentoestoque.FindAsync(id);
            if (lancamentoestoque == null)
            {
                return HttpNotFound();
            }
            return View(lancamentoestoque);
        }

        // GET: LancamentoEstoque/Create
        public ActionResult Entrada()
        {
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");
            ViewBag.estoque_id = new SelectList(db.estoque, "id", "nome");
            ViewBag.produto_id = new SelectList(db.produto, "id", "nome");
            //ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome");
            return View();
        }

        // POST: LancamentoEstoque/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrada(lancamentoestoque model)
        {
            if (model.quantidade == 0)
            {
                TempData["erro"] = "Erro - a quantidade deve ser diferente de 0.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.lancamentoestoque.Add(model);

                    //procura o produto na tabela de cruzamento para alterar quantidade, se ele não existir cria o cruzamento e cadastra quantidade.
                    var prodestoque = db.produtoemestoque.Where(x => x.produto_id == model.produto_id && x.estoque_id == model.estoque_id).FirstOrDefault();
                    if (prodestoque == null)
                    {
                        prodestoque = new produtoemestoque();
                        prodestoque.estoque_id = model.estoque_id;
                        prodestoque.produto_id = model.produto_id;
                        if(model.quantidade < 0)
                        {
                            prodestoque.quantidade = model.quantidade * -1;
                        }
                        else
                        {
                            prodestoque.quantidade = model.quantidade;
                        }
                       
                        db.produtoemestoque.Add(prodestoque);
                    }
                    else
                    {
                        prodestoque.quantidade += model.quantidade;
                    }
                    //Retira o valor do centro de custo, para pagar a entrada de estoque.
                    db.contasaldo.Where(x => x.id == model.contasaldo_id).FirstOrDefault().saldo -= model.valor;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["erro"] = "Erro ao inserir: " + e;
                    return View(model);
                }

            }

            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", model.contasaldo_id);
            ViewBag.estoque_id = new SelectList(db.estoque, "id", "nome", model.estoque_id);
            ViewBag.produto_id = new SelectList(db.produto, "id", "nome", model.produto_id);
            //ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", model.usuario_id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Saida()
        {
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");
            ViewBag.estoque_id = new SelectList(db.estoque, "id", "nome");
            ViewBag.produto_id = new SelectList(db.produto, "id", "nome");
            //ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome");
            return View();
        }

        [HttpPost]
        public ActionResult Saida(lancamentoestoque model)
        {
            if(model.quantidade == 0)
            {
                TempData["erro"] = "Erro - a quantidade deve ser diferente de 0.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");
                    ViewBag.estoque_id = new SelectList(db.estoque, "id", "nome");
                    ViewBag.produto_id = new SelectList(db.produto, "id", "nome");
                    //ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome");

                    //Adiciona o lançamento aos logs
                    db.lancamentoestoque.Add(model);

                    //Elimina quantidades negativas.
                    if(model.quantidade < 0)
                    {
                        model.quantidade *= -1;
                    }

                    //retira o produto do estoque
                    db.produtoemestoque.Where(x => x.produto_id == model.produto_id && x.estoque_id == model.estoque_id).FirstOrDefault().quantidade -= model.quantidade;

                    //Adiciona o valor ao centro de custo.
                    db.contasaldo.Where(x => x.id == model.contasaldo_id).FirstOrDefault().saldo += model.valor;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["erro"] = "Erro ao inserir no banco de dados: " + e;
                    return View(model);
                }
            }
            TempData["erro"] = "Erro ao empacotar o objeto (modelo). Por favor, tente novamente.";
            return View(model);
        }

            //Primeiro filtro (ajax)
            public ActionResult FiltraDeposito(string estoque)
            {
                int idInt = Convert.ToInt32(estoque);
                FinanceiroBanco db = new FinanceiroBanco();

                var produtos = db.lancamentoestoque.Where(x => x.estoque_id == idInt).Select(x => new { idEstoque = x.produto_id, product = x.produto.nome }).Distinct().ToList();
                //var produtos = new SelectList(db.produtoemestoque.Where(x => x.estoque_id == idInt), "id", "nome");
                return Json(produtos, JsonRequestBehavior.AllowGet);
            }

            //Segundo filtro (ajax)
            public ActionResult FiltraProduto(string produto, string estoque)
            {
                FinanceiroBanco db = new FinanceiroBanco();
                int produtoId = Convert.ToInt32(produto);
                int estoqueId = Convert.ToInt32(estoque);
                var quantidades = db.produtoemestoque.Where(x => x.estoque_id == estoqueId && x.produto_id == produtoId).FirstOrDefault().quantidade;
                var valorunitario = db.produto.Where(x => x.id == produtoId).FirstOrDefault().valorunitario;
                return Json(new { quantidade = quantidades, valor = valorunitario }, JsonRequestBehavior.AllowGet);
            }




            // GET: LancamentoEstoque/Edit/5
            public async Task<ActionResult> Edit(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                lancamentoestoque lancamentoestoque = await db.lancamentoestoque.FindAsync(id);
                if (lancamentoestoque == null)
                {
                    return HttpNotFound();
                }
                ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamentoestoque.contasaldo_id);
                ViewBag.estoque_id = new SelectList(db.estoque, "id", "nome", lancamentoestoque.estoque_id);
                ViewBag.produto_id = new SelectList(db.produto, "id", "nome", lancamentoestoque.produto_id);
                ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamentoestoque.usuario_id);
                return View(lancamentoestoque);
            }

        // POST: LancamentoEstoque/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,quantidade,dataentrada,datasaida,validade,lote,produto_id,contasaldo_id,estoque_id,usuario_id")] lancamentoestoque lancamentoestoque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lancamentoestoque).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamentoestoque.contasaldo_id);
            ViewBag.estoque_id = new SelectList(db.estoque, "id", "nome", lancamentoestoque.estoque_id);
            ViewBag.produto_id = new SelectList(db.produto, "id", "nome", lancamentoestoque.produto_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamentoestoque.usuario_id);
            return View(lancamentoestoque);
        }

        // GET: LancamentoEstoque/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lancamentoestoque lancamentoestoque = await db.lancamentoestoque.FindAsync(id);
            if (lancamentoestoque == null)
            {
                return HttpNotFound();
            }
            return View(lancamentoestoque);
        }

        // POST: LancamentoEstoque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            lancamentoestoque lancamentoestoque = await db.lancamentoestoque.FindAsync(id);
            db.lancamentoestoque.Remove(lancamentoestoque);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CalculaValor(string quantidade, string produto)
        {
            FinanceiroBanco db = new FinanceiroBanco();

            int produtoo = Convert.ToInt16(produto);
            int quantidadee = Convert.ToInt16(quantidade);

            var valor = db.produto.Where(x => x.id == produtoo).FirstOrDefault().custounitario;

            if (valor != null)
            {
                return Json(valor * quantidadee, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}
