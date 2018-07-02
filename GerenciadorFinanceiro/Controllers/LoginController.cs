using GerenciadorFinanceiro.AuxCodes;
using GerenciadorFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GerenciadorFinanceiro.Controllers
{
    public class LoginController : Controller
    {
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            /*Recebe a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View(new usuario());
        }

        /// <param name="login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(usuario usuarioDigitado, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (FinanceiroBanco db = new FinanceiroBanco())
                {
                    var vLogin = db.usuario.Where(p => p.login.Equals(usuarioDigitado.login)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
                    Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                        ativo. Caso não esteja cai direto no else*/
                        //if (Equals(vLogin.ATIVO, "S"))
                        //{
                        /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                          do banco. Caso não cai direto no else*/
                        if (Equals(Sec.Decrypt(vLogin.senha), usuarioDigitado.senha))
                        {
                            FormsAuthentication.SetAuthCookie(vLogin.cpf, false);
                            if (Url.IsLocalUrl(returnUrl)
                            && returnUrl.Length > 1
                            && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//")
                            && returnUrl.StartsWith("/\\"))
                            {
                                return Redirect(returnUrl);
                            }
                            /*código abaixo cria uma session para armazenar o nome do usuário*/
                            //Session["Nome"] = vLogin.nome;
                            //Session["CPF"] = vLogin.cpf;
                            //Session["id"] = vLogin.id;
                            /*retorna para a tela inicial do Home*/
                            return RedirectToAction("Index", "Home");
                        }
                        /*Else responsável da validação da senha*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            //ModelState.AddModelError("", "Senha informada Inválida!!!");
                            TempData["erro"] = "Senha inválida!!!";
                            /*Retorna a tela de login*/
                            return View(new usuario());
                        }
                        //}
                        /*Else responsável por verificar se o usuário está ativo*/
                        //else
                        //{
                        //    /*Escreve na tela a mensagem de erro informada*/
                        //    ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");
                        //    /*Retorna a tela de login*/
                        //    return View(new ACESSO());
                        //}
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        //ModelState.AddModelError("", "Login e/ou senha inválidos!!!");
                        TempData["erro"] = "Erro: Login e/ou senha inválidos!";
                        /*Retorna a tela de login*/
                        return View(new usuario());
                    }
                }
            }
            /*Caso os campos não esteja de acordo com a solicitação retorna a tela de login com as mensagem dos campos*/
            return View(usuarioDigitado);
        }

        #region [ Deslogar ]
        public ActionResult Deslogar()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        #endregion


        public ActionResult CadastrarUsuario()
        {
            CadastrarUsuario cadastrarUsuario = new CadastrarUsuario();
            FinanceiroBanco db = new FinanceiroBanco();

            cadastrarUsuario.cooperativasNomes = db.cooperativa.Select(x => x.nome).ToList();
            return View(cadastrarUsuario);
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuario modelUsuario)
        {

            if (ModelState.IsValid)
            {
                string tipoUsuario = "";

                using (var db = new FinanceiroBanco())
                {
                    if (modelUsuario.usuario.senha != modelUsuario.usuario.confirmar_senha)
                    {
                        TempData["erro"] = "Senhas não conferem!";
                        return View(modelUsuario);
                    }
                    else
                    {
                        try
                        {
                            usuario usuario = new usuario();

                            usuario.id = 0;

                            if (Sec.ExisteCPF(modelUsuario.usuario.cpf))
                            {
                                TempData["erro"] = "Usuário já existe";
                                return View(modelUsuario);
                            }
                            else
                            {
                                usuario.cpf = modelUsuario.usuario.cpf;
                                if (modelUsuario.usuario.nome != null)
                                {
                                    usuario.nome = Sec.SemAcento(modelUsuario.usuario.nome);
                                }
                                else
                                {
                                    usuario.nome = null;
                                }
                                if (modelUsuario.usuario.email != null)
                                {
                                    usuario.email = Sec.SemAcento(modelUsuario.usuario.email);
                                }
                                else
                                {
                                    usuario.email = null;
                                }
                                usuario.login = modelUsuario.usuario.login;
                                usuario.telefone = modelUsuario.usuario.telefone;
                                string permissao = (modelUsuario.usuario.permissao == "Usuário") ? "U" : "A";
                                usuario.permissao = permissao;
                                tipoUsuario = permissao;
                                usuario.senha = Sec.Encrypt(modelUsuario.usuario.senha);

                                if (permissao.Equals("U"))
                                {
                                    usuario.cooperativa.Add(db.cooperativa.Where(x => x.nome.Equals(modelUsuario.cooperativaSelecionada)).FirstOrDefault());
                                }
                                usuario.cep = modelUsuario.usuario.cep;
                                usuario.rua = modelUsuario.usuario.rua;
                                usuario.numero = modelUsuario.usuario.numero;
                                usuario.bairro = modelUsuario.usuario.bairro;
                                usuario.cidade = modelUsuario.usuario.cidade;
                                usuario.estado = modelUsuario.usuario.estado;

                                db.usuario.Add(usuario);
                                db.SaveChanges();


                            }
                            TempData["sucesso"] = "Usuário cadastrado com sucesso!";

                            return RedirectToAction("Index", "Home");
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                       ve.PropertyName,
                                       eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                       ve.ErrorMessage);
                                    TempData["erro"] += "Erro :" + ve.ErrorMessage;
                                }
                                return RedirectToAction("CadastrarUsuario", "Login");
                            }
                        }


                    }
                }
            }

            return View();
        }

        public ActionResult Negado()
        {
            return View();
        }
    }
}