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
    public class CaixaController : Controller
    {
        private FinanceiroBanco db = new FinanceiroBanco();

        #region [ Index ]
        public async Task<ActionResult> Index()
        {
            var caixa = db.caixa.Include(c => c.lancamento).OrderByDescending(x => x.id);
            return View(await caixa.ToListAsync());
        }
        #endregion

        #region [ Details ]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            caixa caixa = await db.caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }
            return View(caixa);
        }
        #endregion

        #region [ Abrir Caixa ]
        public ActionResult Abrir()
        {
            FinanceiroBanco db = new FinanceiroBanco();
            caixa model = new caixa();
            model.horaabertura = AuxCodes.Date.HoraBrasilia();
            var ultimoCaixa = db.caixa.OrderByDescending(x => x.id).FirstOrDefault();
            var ultimoFechamento = ultimoCaixa != null ? ultimoCaixa.valorfechamento : 0;
            model.valorabertura = ultimoFechamento == null ? 0 : Convert.ToDecimal(ultimoFechamento); // O caixa abre com o valor que havia no último fechamento.
            return View(model);
            //ViewBag.lancamento_id = new SelectList(db.lancamento, "id", "descricao");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Abrir([Bind(Include = "id,valorabertura,valorfechamento,horaabertura,horafechamento,lancamento_id")] caixa caixa)
        {
            if (ModelState.IsValid)
            {
                caixa.lancamento_id = 1; // Código temporário - quando o caixa for fechado atualizaremos para o valor correto ( foreign key não pode ser vazia).
                db.caixa.Add(caixa);
                await db.SaveChangesAsync();
                TempData["sucesso"] = "Caixa Aberto com sucesso!";
                return RedirectToAction("Index");
            }

            //ViewBag.lancamento_id = new SelectList(db.lancamento, "id", "descricao", caixa.lancamento_id);
            TempData["erro"] = "Erro ao tentar abrir o caixa.";
            return RedirectToAction("Index",caixa);
        }
        #endregion

        #region [ Fechar Caixa ]
        public ActionResult Fechar()
        {
            //ViewBag.lancamento_id = new SelectList(db.lancamento, "id", "descricao");
            caixa model = new caixa();
            model = db.caixa.OrderByDescending(x => x.id).FirstOrDefault();
            model.valorfechamento = model.valorfechamento == null ? model.valorabertura : model.valorfechamento; // Se não for marcado valor para fechamento, fica sendo o de abertura.
            model.horafechamento = AuxCodes.Date.HoraBrasilia();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fechar([Bind(Include = "id,valorabertura,valorfechamento,horaabertura,horafechamento,lancamento_id")] caixa caixa)
        {
            if (ModelState.IsValid)
            {
                caixa.valorfechamento = caixa.valorfechamento == null ? caixa.valorabertura : caixa.valorfechamento; // Se não for marcado valor para fechamento, fica sendo o de abertura.
                caixa.horafechamento = caixa.horafechamento == null ? AuxCodes.Date.HoraBrasilia() : caixa.horafechamento; // Se não tiver capturado momento de fechamento, fica sendo agora.
                if (caixa.lancamento_id == 0)
                {
                    lancamento lancamentoCaixa = new lancamento();
                    lancamentoCaixa.descricao = "FECHAMENTO CAIXA - " + User.Identity.Name;
                    lancamentoCaixa.datacadastro = AuxCodes.Date.HoraBrasilia();
                    lancamentoCaixa.datavencimento = AuxCodes.Date.HoraBrasilia();

                    var conta = db.contasaldo.FirstOrDefault(w => w.nome == "CAIXA"); // Sempre criar conta CAIXA
                    if (conta == null)
                    {
                        TempData["erro"] = "CONTA para o CAIXA não cadastrada. Solicite ao administrador que crie a conta, antes de fechar o caixa.";
                        return RedirectToAction("Index",caixa);
                    }
                    lancamentoCaixa.contasaldo_id = conta.id;


                    var categoria = db.categoria.FirstOrDefault(w => w.nome == "CAIXA"); // Sempre criar categoria pro CAIXA
                    if (categoria == null)
                    {
                        TempData["erro"] = "CATEGORIA para o CAIXA não cadastrada. Solicite ao administrador que crie a categoria, antes de fechar o caixa.";
                        return RedirectToAction("Index", caixa); 
                    }
                    lancamentoCaixa.categoria_id = categoria.id;

                    var instituicao = db.instituicao.FirstOrDefault(w => w.nome == "CAIXA"); // Sempre criar Instituição pro CAIXA
                    if (instituicao == null)
                    {
                        TempData["erro"] = "Instituição para o CAIXA não cadastrada. Solicite ao administrador que cadastre a instituição, antes de fechar o caixa.";
                        return RedirectToAction("Index", caixa);
                    }
                    lancamentoCaixa.instituicao_id = instituicao.id;

                    lancamentoCaixa.valortotal = Convert.ToDecimal(caixa.valorfechamento) - Convert.ToDecimal(caixa.valorabertura); // CONFERIR ESSA PARTE (LEMBRAR DAS SANGRIAS)
                    lancamentoCaixa.pago = true;

                    lancamentoCaixa.usuario_id = 1; //MUDAR ISSOoooooo

                    db.lancamento.Add(lancamentoCaixa);
                    db.SaveChanges();
                    caixa.lancamento_id = lancamentoCaixa.id;
                }
                var caixaaberto = db.caixa.FirstOrDefault(x => x.id == caixa.id);
                caixaaberto.valorfechamento = caixa.valorfechamento;
                caixaaberto.horafechamento = caixa.horafechamento;
                caixaaberto.lancamento_id = caixa.lancamento_id;
                db.SaveChanges();
                TempData["sucesso"] = "Caixa fechado com sucesso!";
                return RedirectToAction("Index");
            }

            //ViewBag.lancamento_id = new SelectList(db.lancamento, "id", "descricao", caixa.lancamento_id);
            TempData["erro"] = "Erro ao fechar o caixa.";
            return RedirectToAction("Index",caixa);
        }
        #endregion

        #region [ Edit ] 
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            caixa caixa = await db.caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }
            ViewBag.lancamento_id = new SelectList(db.lancamento, "id", "descricao", caixa.lancamento_id);
            return View(caixa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,valorabertura,valorfechamento,horaabertura,horafechamento,lancamento_id")] caixa caixa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caixa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.lancamento_id = new SelectList(db.lancamento, "id", "descricao", caixa.lancamento_id);
            return View(caixa);
        }
        #endregion

        #region [ Delete ]

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            caixa caixa = await db.caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }
            return View(caixa);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            caixa caixa = await db.caixa.FindAsync(id);
            db.caixa.Remove(caixa);
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
