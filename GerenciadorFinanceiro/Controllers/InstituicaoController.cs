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
    public class InstituicaoController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        // GET: Instituicao
        public async Task<ActionResult> IndexFornecedor()
        {
            return View(await db.instituicao.Where(x=>x.fc.Equals("F")).ToListAsync());
        }

        public async Task<ActionResult> IndexCliente()
        {
            return View(await db.instituicao.Where(x => x.fc.Equals("C")).ToListAsync());
        }

        // GET: Instituicao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            instituicao instituicao = await db.instituicao.FindAsync(id);
            if (instituicao == null)
            {
                return HttpNotFound();
            }
            return View(instituicao);
        }

        // GET: Instituicao/Create
        public ActionResult CreateFornecedor()
        {
            return View();
        }

        public ActionResult CreateCliente()
        {
            return View();
        }

        // POST: Instituicao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nome,cnpj,email,telefone1,telefone2,responsavel,rua,numero,bairro,cidade,estado,cep,fc")] instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
                db.instituicao.Add(instituicao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Lancamento");
            }

            return View(instituicao);
           
            
        }

        // GET: Instituicao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            instituicao instituicao = await db.instituicao.FindAsync(id);
            if (instituicao == null)
            {
                return HttpNotFound();
            }
            return View(instituicao);
        }

        // POST: Instituicao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,cnpj,email,telefone1,telefone2,responsavel,rua,numero,bairro,cidade,estado,cep,fc")] instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instituicao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(instituicao);
        }

        // GET: Instituicao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            instituicao instituicao = await db.instituicao.FindAsync(id);
            if (instituicao == null)
            {
                return HttpNotFound();
            }
            return View(instituicao);
        }

        // POST: Instituicao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            instituicao instituicao = await db.instituicao.FindAsync(id);
            db.instituicao.Remove(instituicao);
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
