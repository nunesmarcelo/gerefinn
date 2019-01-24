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
    public class LancamentoController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        #region [ Index ] 
        public async Task<ActionResult> Index()
        {
            var lancamento = db.lancamento.Include(l => l.categoria).Include(l => l.contasaldo).Include(l => l.instituicao).Include(l => l.usuario);
            return View(await lancamento.ToListAsync());
        }
        #endregion

        #region [ Details ] 
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lancamento lancamento = await db.lancamento.FindAsync(id);
            if (lancamento == null)
            {
                return HttpNotFound();
            }
            return View(lancamento);
        }
        #endregion

        #region [ Create Despesa] 
        public ActionResult CreateDespesa()
        {
            // D = Despesa. F = Fornecedor.
            ViewBag.categoria_id = new SelectList(db.categoria.Where(x => x.rd == "D"), "id", "nome");
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");
            ViewBag.instituicao_id = new SelectList(db.instituicao.Where(x => x.fc == "F"), "id", "nome");
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome");
            return View();
        }
        #endregion

        #region [ Create Receita ] 
        public ActionResult CreateReceita()
        {
            //R = Receita. C = Cliente.
            ViewBag.categoria_id = new SelectList(db.categoria.Where(x => x.rd == "R"), "id", "nome");
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");
            ViewBag.instituicao_id = new SelectList(db.instituicao.Where(x => x.fc == "C"), "id", "nome");
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome");
            return View();
        }
        #endregion

        #region [ Post - Create ] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                if (lancamento.pago == true)
                {
                    var categoria = db.categoria.Where(x => x.id == lancamento.categoria_id).FirstOrDefault();
                    var contasaldo = db.contasaldo.Where(x => x.id == lancamento.contasaldo_id).FirstOrDefault();
                    if (categoria.rd == "D")
                    {
                        contasaldo.saldo -= lancamento.valortotal;

                        ViewBag.categoria_id = new SelectList(db.categoria.Where(x => x.rd == "D"), "id", "nome", lancamento.categoria_id);
                        ViewBag.instituicao_id = new SelectList(db.instituicao.Where(x => x.fc == "F"), "id", "nome", lancamento.instituicao_id);
                        ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
                        //ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamento.usuario_id);
                    }
                    if (categoria.rd == "R")
                    {
                        contasaldo.saldo += lancamento.valortotal;

                        ViewBag.categoria_id = new SelectList(db.categoria.Where(x => x.rd == "R"), "id", "nome", lancamento.categoria_id);
                        ViewBag.instituicao_id = new SelectList(db.instituicao.Where(x => x.fc == "C"), "id", "nome", lancamento.instituicao_id);
                        ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
                        //ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamento.usuario_id);
                    }

                }
                lancamento.observacao = lancamento.observacao != null ? lancamento.observacao.ToUpper() : null;
                lancamento.descricao = lancamento.descricao != null ? lancamento.descricao.ToUpper() : null;
                lancamento.datacadastro = AuxCodes.Date.HoraBrasilia();
                db.lancamento.Add(lancamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lancamento);
        }
        #endregion

        #region [ Edit ]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lancamento lancamento = await db.lancamento.FindAsync(id);
            if (lancamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", lancamento.categoria_id);
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", lancamento.instituicao_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamento.usuario_id);
            return View(lancamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,descricao,valortotal,pago,datavencimento,datacadastro,observacao,contasaldo_id,categoria_id,usuario_id,instituicao_id")] lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lancamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", lancamento.categoria_id);
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", lancamento.instituicao_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamento.usuario_id);
            return View(lancamento);
        }
        #endregion

        #region [ Delete ]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lancamento lancamento = await db.lancamento.FindAsync(id);
            if (lancamento == null)
            {
                return HttpNotFound();
            }
            return View(lancamento);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            lancamento lancamento = await db.lancamento.FindAsync(id);
            db.lancamento.Remove(lancamento);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region [Dispose]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region [ Pagar lançamento ]
        [HttpPost]
        public ActionResult pagar(int? id)
        {
            FinanceiroBanco db = new FinanceiroBanco();
            lancamento lancamento;
            try
            {
                lancamento = db.lancamento.Where(i => i.id == id).FirstOrDefault();
                if (lancamento.pago == true)
                {
                    lancamento.pago = false;
                    if (lancamento.categoria.rd == "D")
                    {
                        lancamento.contasaldo.saldo += lancamento.valortotal;
                    }
                    if (lancamento.categoria.rd == "R")
                    {
                        lancamento.contasaldo.saldo -= lancamento.valortotal;
                    }

                }
                else
                {
                    lancamento.pago = true;
                    if (lancamento.categoria.rd == "D")
                    {
                        lancamento.contasaldo.saldo -= lancamento.valortotal;
                    }
                    if (lancamento.categoria.rd == "R")
                    {
                        lancamento.contasaldo.saldo += lancamento.valortotal;
                    }
                }


                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json("Erro:" + e, JsonRequestBehavior.AllowGet);
            }

            if (lancamento.pago == false)
                return Json("false", JsonRequestBehavior.AllowGet);
            else
                return Json("true", JsonRequestBehavior.AllowGet);
        }
        #endregion

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "id,descricao,valortotal,pago,datavencimento,datacadastro,observacao,contasaldo_id,categoria_id,usuario_id,instituicao_id")] lancamento lancamento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.lancamento.Add(lancamento);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", lancamento.categoria_id);
        //    ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
        //    ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", lancamento.instituicao_id);
        //    ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome", lancamento.usuario_id);
        //    return View(lancamento);
        //}
    }
}
