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
    public class CategoriaController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        // GET: Categoria Receita
        public async Task<ActionResult> IndexReceita()
        {
            return View(await db.categoria.Where(x => x.rd.Equals("R")).ToListAsync());
        }

        // GET: Categoria Despesa
        public async Task<ActionResult> IndexDespesa()
        {
            return View(await db.categoria.Where(x => x.rd.Equals("D")).ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = await db.categoria.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categoria/CreateDespesa
        public ActionResult CreateDespesa()
        {
            var tipo = new[]
           {
                new SelectListItem { Value = "PC", Text = "PASSIVO CIRCULANTE" },
                new SelectListItem { Value = "PNC", Text = "PASSIVO NÃO-CIRCULANTE" },
                new SelectListItem { Value = "PL", Text = "PATRIMÔNIO LÍQUIDO" },
            };

            ViewBag.Lista = new SelectList(tipo, "Value", "Text");

            return View();
        }

        // GET: Categoria/CreateReceita
        public ActionResult CreateReceita()
        {
            var tipo = new[]
           {
                new SelectListItem { Value = "AC", Text = "ATIVO CIRCULANTE" },
                new SelectListItem { Value = "ANC", Text = "ATIVO NÃO-CIRCULANTE" },
                new SelectListItem { Value = "PL", Text = "PATRIMÔNIO LÍQUIDO" },
            };

            ViewBag.Lista = new SelectList(tipo, "Value", "Text");
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nome,tipo,descricao,rd")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.status = true;
                categoria.nome = categoria.nome.ToUpper();
                db.categoria.Add(categoria);
                await db.SaveChangesAsync();
                if (categoria.rd == "D")
                {
                    return RedirectToAction("IndexDespesa", "Categoria");
                }
                else
                {
                    return RedirectToAction("IndexReceita", "Categoria");
                }

            }

            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = await db.categoria.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            if (categoria.rd == "D")
            {
                var tipo = new[]
                {
                new SelectListItem { Value = "PC", Text = "PASSIVO CIRCULANTE" },
                new SelectListItem { Value = "PNC", Text = "PASSIVO NÃO-CIRCULANTE" },
                new SelectListItem { Value = "PL", Text = "PATRIMÔNIO LÍQUIDO" },
                };
                ViewBag.Lista = new SelectList(tipo, "Value", "Text");
            }
            else
            {
                var tipo = new[]
                {
                new SelectListItem { Value = "AC", Text = "ATIVO CIRCULANTE" },
                new SelectListItem { Value = "ANC", Text = "ATIVO NÃO-CIRCULANTE" },
                new SelectListItem { Value = "PL", Text = "PATRIMÔNIO LÍQUIDO" },
                };
                ViewBag.Lista = new SelectList(tipo, "Value", "Text");
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,status,tipo,descricao,rd")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.nome = categoria.nome.ToUpper();
                db.Entry(categoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (categoria.rd == "D")
                {
                    return RedirectToAction("IndexDespesa");
                }
                else
                {
                    return RedirectToAction("IndexReceita");
                }

            }
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoria categoria = await db.categoria.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(categoria categoria)
        {
            categoria categoriaTipo = await db.categoria.FindAsync(categoria.id);
            var retorno = categoriaTipo.rd;
            db.categoria.Remove(categoriaTipo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                TempData["erro"] = "Erro ao excluir, você possui lançamentos ligados à essa categoria. Para a proteção dos seus dados, é necessário apagar eles antes de excluí-la.";
                if (retorno.Equals("D"))
                {
                    return RedirectToAction("IndexDespesa");
                }
                else
                {
                    return RedirectToAction("IndexReceita");
                }
            }

            if (retorno.Equals("D"))
            {
                return RedirectToAction("IndexDespesa");
            }
            else
            {
                return RedirectToAction("IndexReceita");
            }

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
