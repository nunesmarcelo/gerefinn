using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciadorFinanceiro.AuxCodes;
using GerenciadorFinanceiro.Models;

namespace GerenciadorFinanceiro.Controllers
{
    public class CooperativaController : Controller
    {
    //    private FinanceiroBanco db = new FinanceiroBanco();

    //    public ActionResult CadastrarCooperativa()
    //    {
    //        cooperativa cooperativa = new cooperativa();
    //        return View(cooperativa);
    //    }

    //    [HttpPost]
    //    public ActionResult CadastrarCooperativa(cooperativa modelCooperativa)
    //    {

    //        if (ModelState.IsValid)
    //        {
    //            using (var db = new FinanceiroBanco())
    //            {

    //                try
    //                {
    //                    cooperativa cooperativa = new cooperativa();

    //                    cooperativa.id = 0;

    //                    if (Sec.ExisteCNPJ(modelCooperativa.cnpj))
    //                    {
    //                        TempData["erro"] = "Cooperativa já cadastrada";
    //                        return View(modelCooperativa);
    //                    }
    //                    else
    //                    {
    //                        cooperativa.cnpj = modelCooperativa.cnpj;
    //                        if (modelCooperativa.nome != null)
    //                        {
    //                            cooperativa.nome = Sec.SemAcento(modelCooperativa.nome);
    //                        }
    //                        else
    //                        {
    //                            cooperativa.nome = null;
    //                        }
    //                        if (modelCooperativa.email != null)
    //                        {
    //                            cooperativa.email = Sec.SemAcento(modelCooperativa.email);
    //                        }
    //                        else
    //                        {
    //                            cooperativa.email = null;
    //                        }

    //                        cooperativa.telefone1 = modelCooperativa.telefone1;
    //                        cooperativa.telefone2 = modelCooperativa.telefone2;


    //                        cooperativa.cep = modelCooperativa.cep;
    //                        cooperativa.rua = modelCooperativa.rua;
    //                        cooperativa.numero = modelCooperativa.numero;
    //                        cooperativa.bairro = modelCooperativa.bairro;
    //                        cooperativa.cidade = modelCooperativa.cidade;
    //                        cooperativa.estado = modelCooperativa.estado;

    //                        db.cooperativa.Add(cooperativa);
    //                        db.SaveChanges();
    //                    }
    //                }
    //                catch (DbEntityValidationException e)
    //                {
    //                    foreach (var eve in e.EntityValidationErrors)
    //                    {
    //                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
    //                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
    //                        foreach (var ve in eve.ValidationErrors)
    //                        {
    //                            Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
    //                               ve.PropertyName,
    //                               eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
    //                               ve.ErrorMessage);
    //                        }
    //                    }
    //                }

    //                TempData["sucesso"] = "Cooperativa cadastrada com sucesso!";

    //                return RedirectToAction("Index", "Home");

    //            }
    //        }

    //        return View();
    //    }
    }
}
