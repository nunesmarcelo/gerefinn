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

        #region [ Index Fornecedor ]
        public async Task<ActionResult> IndexFornecedor()
        {
            return View(await db.instituicao.Where(x => x.fc.Equals("F")).ToListAsync());
        }
        #endregion

        #region [ Index Cliente ]
        public async Task<ActionResult> IndexCliente()
        {
            return View(await db.instituicao.Where(x => x.fc.Equals("C")).ToListAsync());
        }
        #endregion

        #region [ Details ]
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
        #endregion

        #region [ Create Fornecedor ]
        public ActionResult CreateFornecedor()
        {
            return View();
        }
        #endregion

        #region [ Create Cliente ] 
        public ActionResult CreateCliente()
        {
            return View();
        }
        #endregion

        #region [ Create - POST ]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nome,cnpj,email,telefone1,telefone2,responsavel,rua,numero,bairro,cidade,estado,cep,fc")] instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
                db.instituicao.Add(instituicao);
                instituicao.nome = instituicao.nome.ToUpper();
                if (instituicao.responsavel != null)
                {
                    instituicao.responsavel = instituicao.responsavel.ToUpper();
                }
                db.instituicao.Add(instituicao);
                await db.SaveChangesAsync();
                if (instituicao.fc == "F")
                {
                    return RedirectToAction("IndexFornecedor", "Instituicao");
                }
                else
                {
                    return RedirectToAction("IndexCliente", "Instituicao");
                }
            }

            return View(instituicao);
        }
        #endregion

        #region [ Edit ]
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,cnpj,email,telefone1,telefone2,responsavel,rua,numero,bairro,cidade,estado,cep,fc")] instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(instituicao).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    TempData["erro"] = "Erro ao modificar os dados. Por favor, tente novamente.";
                }


                if (instituicao.fc == "F")
                {
                    return RedirectToAction("IndexFornecedor");
                }
                else
                {
                    return RedirectToAction("IndexCliente");

                }
            }
            return View(instituicao);
        }
        #endregion

        #region [ Delete ]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            instituicao instituicao = await db.instituicao.FindAsync(id);
            db.instituicao.Remove(instituicao);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                TempData["erro"] = "Erro ao excluir, você possui lançamentos ligados à essa instituição. Para a proteção dos seus dados, é necessário excluir essa instituição primeiro.";
                if (instituicao.fc == "F")
                {
                    return RedirectToAction("IndexFornecedor");
                }
                else
                {
                    return RedirectToAction("IndexCliente");
                }
            }
            if (instituicao.fc == "F")
            {
                return RedirectToAction("IndexFornecedor");
            }
            else
            {
                return RedirectToAction("IndexCliente");
            }

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
