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
    public class ContaSaldoController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        // GET: ContaSaldo
        public async Task<ActionResult> Index()
        {
            return View(await db.contasaldo.ToListAsync());
        }

        // GET: ContaSaldo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contasaldo contasaldo = await db.contasaldo.FindAsync(id);
            if (contasaldo == null)
            {
                return HttpNotFound();
            }
            return View(contasaldo);
        }

        // GET: ContaSaldo/Create
        public ActionResult Create()
        {
            var list = new[]
            {
                new SelectListItem { Value = "POUPANCA", Text = "Poupança" },
                new SelectListItem { Value = "CORRENTE", Text = "Corrente" },
                new SelectListItem { Value = "MISTA", Text = "Mista" },
            };

            ViewBag.Lista = new SelectList(list, "Value", "Text");
            return View();
        }

        // POST: ContaSaldo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "nome,banco,saldo,agencia,conta,titular,tipo")] contasaldo contasaldo)
        {
            var list = new[]
           {
                new SelectListItem { Value = "POUPANCA", Text = "Poupança" },
                new SelectListItem { Value = "CORRENTE", Text = "Corrente" },
                new SelectListItem { Value = "MISTA", Text = "Mista" },
            };

            ViewBag.Lista = new SelectList(list, "Value", "Text");

            if (ModelState.IsValid)
            {
                contasaldo.status = true;
                contasaldo.nome = contasaldo.nome.ToUpper();
                contasaldo.banco = contasaldo.banco.ToUpper();
                if(contasaldo.titular != null)
                {
                    contasaldo.titular = contasaldo.titular.ToUpper();
                }
                
                db.contasaldo.Add(contasaldo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            

            return View(contasaldo);
        }

        // GET: ContaSaldo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contasaldo contasaldo = await db.contasaldo.FindAsync(id);
            if (contasaldo == null)
            {
                return HttpNotFound();
            }
            return View(contasaldo);
        }

        // POST: ContaSaldo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,banco,saldo,status,agencia,conta,titular,tipo")] contasaldo contasaldo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contasaldo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contasaldo);
        }

        // GET: ContaSaldo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contasaldo contasaldo = await db.contasaldo.FindAsync(id);
            if (contasaldo == null)
            {
                return HttpNotFound();
            }
            return View(contasaldo);
        }

        // POST: ContaSaldo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            contasaldo contasaldo = await db.contasaldo.FindAsync(id);
            db.contasaldo.Remove(contasaldo);
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
    }
}
