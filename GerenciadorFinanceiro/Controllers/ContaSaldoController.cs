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

        #region [ Index ] 
        public async Task<ActionResult> Index()
        {
            return View(await db.contasaldo.ToListAsync());
        }
        #endregion

        #region [ Details ] 
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
        #endregion

        #region [ Create ] 
        public ActionResult Create()
        {
            var list = new[]
            {
                new SelectListItem { Value = "POUPANCA" , Text = "Poupança"},
                new SelectListItem { Value = "CORRENTE" , Text = "Corrente"},
                new SelectListItem { Value = "MISTA"    , Text = "Mista"},
                new SelectListItem { Value = "OUTRO"    , Text = "Nenhum/Outro"},
            };

            ViewBag.Lista = new SelectList(list, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "nome,banco,saldo,status,agencia,conta,titular,tipo")] contasaldo contasaldo)
        {

            var list = new[]
            {
                    new SelectListItem { Value = "POUPANCA", Text = "Poupança" },
                    new SelectListItem { Value = "CORRENTE", Text = "Corrente" },
                    new SelectListItem { Value = "MISTA", Text = "Mista" },
                    new SelectListItem { Value = "OUTRO", Text = "Nenhum/Outro" },
                };

            ViewBag.Lista = new SelectList(list, "Value", "Text");

            if (ModelState.IsValid)
            {
                contasaldo.status = true;
                contasaldo.nome = contasaldo.nome.ToUpper();
                if (contasaldo.banco != null)
                {
                    contasaldo.banco = contasaldo.banco.ToUpper();
                }

                if (contasaldo.titular != null)
                {
                    contasaldo.titular = contasaldo.titular.ToUpper();
                }

                db.contasaldo.Add(contasaldo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return View(contasaldo);
        }
        #endregion

        #region [ Edit ]
        public async Task<ActionResult> Edit(int? id)
        {
            var list = new[]
           {
                    new SelectListItem { Value = "POUPANCA", Text = "Poupança" },
                    new SelectListItem { Value = "CORRENTE", Text = "Corrente" },
                    new SelectListItem { Value = "MISTA", Text = "Mista" },
                    new SelectListItem { Value = "OUTRO", Text = "Nenhum/Outro" },
                };

            ViewBag.Lista = new SelectList(list, "Value", "Text");


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,banco,saldo,status,agencia,conta,titular,tipo")] contasaldo contasaldo)
        {
            var list = new[]
           {
                    new SelectListItem { Value = "POUPANCA", Text = "Poupança" },
                    new SelectListItem { Value = "CORRENTE", Text = "Corrente" },
                    new SelectListItem { Value = "MISTA", Text = "Mista" },
                    new SelectListItem { Value = "OUTRO", Text = "Nenhum/Outro" },
                };

            ViewBag.Lista = new SelectList(list, "Value", "Text");

            if (ModelState.IsValid)
            {
                db.Entry(contasaldo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contasaldo);
        }
        #endregion

        #region [ Delete ] 
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            contasaldo contasaldo = await db.contasaldo.FindAsync(id);
            db.contasaldo.Remove(contasaldo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
