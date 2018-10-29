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

        // GET: Lancamento
        public async Task<ActionResult> Index()
        {
            var lancamento = db.lancamento.Include(l => l.categoria).Include(l => l.instituicao).Include(l => l.contasaldo);
            return View(await lancamento.ToListAsync());
        }

        public async Task<ActionResult> Index2()
        {
            var lancamento = db.lancamento.Include(l => l.categoria).Include(l => l.instituicao).Include(l => l.contasaldo);
            return View(await lancamento.ToListAsync());
        }

        // GET: Lancamento/Details/5
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

        // GET: Lancamento/Create
        public ActionResult CreateReceita()
        {
            ViewBag.categoria_id = new SelectList(db.categoria.Where(x => x.rd == "R"), "id", "nome");
            ViewBag.instituicao_id = new SelectList(db.instituicao.Where(x => x.fc == "C"), "id", "nome");
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");


           
            return View();
        }

        // GET: Lancamento/Create
        public ActionResult CreateDespesa()
        {
            ViewBag.categoria_id = new SelectList(db.categoria.Where(x=> x.rd == "D"), "id", "nome");
            ViewBag.instituicao_id = new SelectList(db.instituicao.Where(x=>x.fc == "F"), "id", "nome");
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome");

         

            return View();
        }

        // POST: Lancamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                if(lancamento.pago == true )
                {
                    var categoria = db.categoria.Where(x => x.id == lancamento.categoria_id).FirstOrDefault();
                    var contasaldo = db.contasaldo.Where(x => x.id == lancamento.contasaldo_id).FirstOrDefault();
                    if(categoria.rd == "D")
                    {
                        contasaldo.saldo -= lancamento.valor;
                    }
                    if(categoria.rd == "R"){
                        contasaldo.saldo += lancamento.valor;
                    }
                    
                }
                lancamento.descricao = lancamento.descricao.ToUpper();
                lancamento.datacadastro = AuxCodes.Data.HoraBrasilia();
                db.lancamento.Add(lancamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", lancamento.categoria_id);
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", lancamento.instituicao_id);
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
            return View(lancamento);
        }

        // GET: Lancamento/Edit/5
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
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", lancamento.instituicao_id);
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
            return View(lancamento);
        }

        // POST: Lancamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,descricao,quantidade,valor,pago,numerotitulo,datavencimento,dataemissao,datacadastro,observacao,categoria_id,instituicao_id,contasaldo_id")] lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lancamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", lancamento.categoria_id);
            ViewBag.instituicao_id = new SelectList(db.instituicao, "id", "nome", lancamento.instituicao_id);
            ViewBag.contasaldo_id = new SelectList(db.contasaldo, "id", "nome", lancamento.contasaldo_id);
            return View(lancamento);
        }

        // GET: Lancamento/Delete/5
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

        // POST: Lancamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            lancamento lancamento = await db.lancamento.FindAsync(id);
            db.lancamento.Remove(lancamento);
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

        [HttpPost]
        public ActionResult pagar(int? id)
        {
            FinanceiroBanco db = new FinanceiroBanco();
            lancamento lancamento;
            try
            {
                lancamento = db.lancamento.Where(i => i.id == id).FirstOrDefault();
                if (lancamento.pago == true) { 
                    lancamento.pago = false;
                    if(lancamento.categoria.rd == "D")
                    {
                        lancamento.contasaldo.saldo +=  lancamento.valor;
                    }
                    if(lancamento.categoria.rd == "R")
                    {
                        lancamento.contasaldo.saldo -=  lancamento.valor;
                    }
                    
                }
                else
                {
                    lancamento.pago = true;
                    if (lancamento.categoria.rd == "D")
                    {
                        lancamento.contasaldo.saldo -= lancamento.valor;
                    }
                    if (lancamento.categoria.rd == "R")
                    {
                        lancamento.contasaldo.saldo += lancamento.valor;
                    }
                }
                   

                db.SaveChanges();
            }catch(Exception e)
            {
                return Json("Erro:"+e, JsonRequestBehavior.AllowGet);
            }
           
            if(lancamento.pago == false)
                return Json("false", JsonRequestBehavior.AllowGet);
            else
                return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}
