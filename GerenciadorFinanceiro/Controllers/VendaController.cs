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
    public class VendaController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        #region [ Index ] 
        public async Task<ActionResult> Index()
        {
            var venda = db.venda.Include(v => v.instituicao).Include(v => v.usuario).Include(v => v.caixa);
            return View(await venda.ToListAsync());
        }
        #endregion

        #region [ Details ]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = await db.venda.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }
        #endregion

        #region [ Adicionar Produto ]
        //Código explicativo : https://stackoverflow.com/questions/39129555/add-item-into-list-from-view-and-pass-to-controller-in-mvc5/39193087 , usado para adicionar dinamicamente.
        public ActionResult AdicionarProduto()
        {
            return PartialView("ProdutoPartialView");
        }
        #endregion

        #region [ Adicionar Pagamento ]
        //Código explicativo : https://stackoverflow.com/questions/39129555/add-item-into-list-from-view-and-pass-to-controller-in-mvc5/39193087 , usado para adicionar dinamicamente.
        public ActionResult AdicionarPagamento()
        {
            ViewBag.tipo_pagamento_id = new SelectList(db.tipopagamento, "id", "nome");
            return PartialView("PagamentoPartialView");
        }
        #endregion

        #region [ Create - Ver adicionar produto e pagamento]
        public ActionResult Create()
        {
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome");
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome");
            ViewBag.caixa_id = new SelectList(db.caixa, "id", "id");

            CadastrarVenda model = new CadastrarVenda();
            model.Produtos = new List<produtovenda>();
            model.Pagamentos = new List<pagamento>();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "id,concluida,valortotal,caixa_id,usuario_id,instituicao_id")] CadastrarVenda model)
        public async Task<ActionResult> Create(CadastrarVenda model)
        {
            if (ModelState.IsValid)
            {
                db.venda.Add(model.Venda);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", model.Venda.instituicao_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", model.Venda.usuario_id);
            ViewBag.caixa_id = new SelectList(db.caixa, "id", "id", model.Venda.caixa_id);
            return View(model);
        }
        #endregion

        #region [ Edit ] 
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = await db.venda.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", venda.instituicao_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", venda.usuario_id);
            ViewBag.caixa_id = new SelectList(db.caixa, "id", "id", venda.caixa_id);
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,concluida,valortotal,caixa_id,usuario_id,instituicao_id")] venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", venda.instituicao_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", venda.usuario_id);
            ViewBag.caixa_id = new SelectList(db.caixa, "id", "id", venda.caixa_id);
            return View(venda);
        }
        #endregion

        #region [ Delete ] 
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = await db.venda.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            venda venda = await db.venda.FindAsync(id);
            db.venda.Remove(venda);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        
        #region [ AutoCompleteProduto + ProdutoEscolhido]
        [HttpPost]
        public ActionResult AutoCompleteProduto(string textoPesquisado)
        {
            var produtos = db.produto.Where(x => x.nome.Contains(textoPesquisado)).Select(x=> x.nome).ToList();
            return Json(new { nomes = produtos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProdutoEscolhido(string produtoEscolhido)
        {
            var valorProduto = db.produto.Where(x => x.nome == produtoEscolhido).FirstOrDefault();
            var valor = valorProduto != null ? valorProduto.valorunitario : 0;
            return Json(new { valor = valor }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region [ Dispose ]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

    }
}
