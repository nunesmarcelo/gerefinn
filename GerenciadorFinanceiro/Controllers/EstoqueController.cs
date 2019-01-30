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
    public class EstoqueController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        #region [ Index ]
        public async Task<ActionResult> Index()
        {
            return View(await db.estoque.ToListAsync());
        }
        #endregion

        #region [ Details ] 
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque estoque = await db.estoque.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }
        #endregion

        #region [ Create ]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nome,responsavel,telefone,email,rua,numero,bairro,cidade,estado,cep,status")] estoque estoque)
        {
            if (ModelState.IsValid)
            {
                db.estoque.Add(estoque);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(estoque);
        }
        #endregion

        #region [ Edit ]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque estoque = await db.estoque.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,responsavel,telefone,email,rua,numero,bairro,cidade,estado,cep,status")] estoque estoque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoque).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estoque);
        }
        #endregion

        #region [ Delete ] 
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque estoque = await db.estoque.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            estoque estoque = await db.estoque.FindAsync(id);
            db.estoque.Remove(estoque);
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
