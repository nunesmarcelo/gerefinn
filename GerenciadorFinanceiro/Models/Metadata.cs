//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace GerenciadorFinanceiro.Models
//{
//    //              CATEGORIA
//    [MetadataType(typeof(CategoriaMetadata))]
//    public partial class categoria
//    {
//    }

//    public class CategoriaMetadata
//    {
//        public int id;

//        [Display(Name = "Nome da categoria:")]
//        public string nome;

//        [Display(Name = "Status:")]
//        public bool status;

//        [Display(Name = "Tipo:")]
//        public string tipo;

//        [Display(Name = "Descrição:")]
//        public string descricao;

//        public string rd { get; set; }
//    }


//    //              CONTASALDO
//    [MetadataType(typeof(ContaSaldoMetadata))]
//    public partial class contasaldo
//    {
//    }

//    public class ContaSaldoMetadata
//    {
//        [Display(Name = "Centro de Custo:")]
//        public int id;
//        [Display(Name = "Apelido:")]
//        public string nome;
//        [Display(Name = "Banco:")]
//        public string banco;
//        [Display(Name = "Saldo inicial:")]
//        public decimal saldo;
//        [Display(Name = "Status:")]
//        public bool status;
//        [Display(Name = "Agência:")]
//        public string agencia;
//        [Display(Name = "Conta:")]
//        public string conta;
//        [Display(Name = "Titular:")]
//        public string titular;
//        [Display(Name = "Tipo de conta:")]
//        public string tipo;
//    }

//    //              ESTOQUE
//    [MetadataType(typeof(EstoqueMetadata))]
//    public partial class estoque
//    {
//    }

//    public class EstoqueMetadata
//    {
//        [Display(Name = "Id:")]
//        public int id { get; set; }
//        [Display(Name = "Nome:")]
//        public string nome { get; set; }
//        [Display(Name = "Responsável:")]
//        public string responsavel { get; set; }
//        [Display(Name = "Telefone:")]
//        public string telefone { get; set; }
//        [Display(Name = "Email:")]
//        public string email { get; set; }
//        [Display(Name = "Rua:")]
//        public string rua { get; set; }
//        [Display(Name = "Número:")]
//        public Nullable<int> numero { get; set; }
//        [Display(Name = "Bairro:")]
//        public string bairro { get; set; }
//        [Display(Name = "Cidade:")]
//        public string cidade { get; set; }
//        [Display(Name = "Estado:")]
//        public string estado { get; set; }
//        [Display(Name = "CEP:")]
//        public string cep { get; set; }
//        [Display(Name = "Status:")]
//        public bool status { get; set; }
//    }



//    //            INSTITUIÇÃO
//    [MetadataType(typeof(InstituicaoMetadata))]
//    public partial class instituicao
//    {
//    }

//    public class InstituicaoMetadata
//    {
//        public int id;
//        [Display(Name = "Nome:")]
//        public string nome;
//        [Display(Name = "CNPJ:")]
//        public string cnpj;
//        [Display(Name = "Email:")]
//        public string email;
//        [Display(Name = "Telefone principal:")]
//        public string telefone1;
//        [Display(Name = "Telefone secundário:")]
//        public string telefone2;
//        [Display(Name = "Responsável:")]
//        public string responsavel;
//        [Display(Name = "Rua:")]
//        public string rua { get; set; }
//        [Display(Name = "Número:")]
//        public Nullable<int> numero;
//        [Display(Name = "Bairro:")]
//        public string bairro;
//        [Display(Name = "Cidade:")]
//        public string cidade;
//        [Display(Name = "Estado:")]
//        public string estado;
//        [Display(Name = "CEP:")]
//        public string cep;
//        public string fc;
//    }

//    //          LANÇAMENTO
//    [MetadataType(typeof(LancamentoMetadata))]
//    public partial class lancamento
//    {
//    }

//    public class LancamentoMetadata
//    {
//        public int id { get; set; }
//        [Display(Name = "Item:")]
//        public string descricao { get; set; }
//        [Display(Name = "Quantidade:")]
//        public Nullable<int> quantidade { get; set; }
//        [Display(Name = "Valor:")]
//        public decimal valor { get; set; }
//        [Display(Name = "Pago:")]
//        public bool pago { get; set; }
//        [Display(Name = "Número:")]
//        public string numerotitulo { get; set; }
//        [Display(Name = "Vencimento:")]
//        public Nullable<System.DateTime> datavencimento { get; set; }
//        [Display(Name = "Emissão:")]
//        public Nullable<System.DateTime> dataemissao { get; set; }
//        [Display(Name = "Cadastro:")]
//        public System.DateTime datacadastro { get; set; }
//        [Display(Name = "Observação:")]
//        public string observacao { get; set; }

//        [Display(Name = "Categoria:")]
//        public int categoria_id { get; set; }

//        [Display(Name = "Para:")]
//        public int instituicao_id { get; set; }

//        [Display(Name = "Conta envolvida:")]
//        public int contasaldo_id { get; set; }
//    }



//    //LANCAMENTO ESTOQUE
//    [MetadataType(typeof(LancamentoEstoqueMetadata))]
//    public partial class lancamentoestoque
//    {
//    }

//    public class LancamentoEstoqueMetadata
//    {
//        public long id { get; set; }
//        [Display(Name = "Data de entrada:")]
//        public Nullable<System.DateTime> dataentrada { get; set; }
//        [Display(Name = "Data de saída:")]
//        public Nullable<System.DateTime> datasaida { get; set; }
//        [Display(Name = "Validade:")]
//        public Nullable<System.DateTime> validade { get; set; }
//        [Display(Name = "Lote:")]
//        public string lote { get; set; }
//        [Display(Name = "Produto:")]
//        public int produto_id { get; set; }
//        [Display(Name = "Centro de Custo:")]
//        public int contasaldo_id { get; set; }
//        [Display(Name = "Depósito:")]
//        public int estoque_id { get; set; }
//        [Display(Name = "Usuário:")]
//        public Nullable<int> usuario_id { get; set; }
//        public decimal valor { get; set; }

//        [Display(Name = "Centro de Custo:")]
//        public virtual contasaldo contasaldo { get; set; }
//        [Display(Name = "Depósito:")]
//        public virtual estoque estoque { get; set; }
//        [Display(Name = "Produto:")]
//        public virtual produto produto { get; set; }
//        [Display(Name = "Usuário:")]
//        public virtual usuario usuario { get; set; }
//    }

//    //PRODUTO
//    [MetadataType(typeof(ProdutoMetadata))]
//    public partial class produto
//    {
//    }

//    public class ProdutoMetadata
//    {

//        [Display(Name = "Id:")]
//        public int id { get; set; }
//        [Display(Name = "Nome:")]
//        public string nome { get; set; }
//        [Display(Name = "Valor Unitário:")]
//        public Nullable<decimal> valorunitario { get; set; }
//        [Display(Name = "Custo Unitário:")]
//        public Nullable<decimal> custounitario { get; set; }
//        [Display(Name = "Status:")]
//        public bool status { get; set; }
//    }

//    //PRODUTO EM ESTOQUE
//    [MetadataType(typeof(ProdutoEmEstoqueMetadata))]
//    public partial class produtoemestoque
//    {
//    }

//    public class ProdutoEmEstoqueMetadata
//    {

//        [Display(Name = "Produto:")]
//        public int produto_id { get; set; }
//        [Display(Name = "Depósito:")]
//        public int estoque_id { get; set; }
//        [Display(Name = "Quantidade:")]
//        public int quantidade { get; set; }
//    }

//    //             Usuário
//    [MetadataType(typeof(UsuarioMetadata))]
//    public partial class usuario
//    {
//    }

//    public class UsuarioMetadata
//    {
//        public int id { get; set; }
//        [Display(Name = "Nome:")]
//        public string nome { get; set; }
//        [Display(Name = "Login:")]
//        public string login { get; set; }
//        [Display(Name = "Senha:")]
//        public string senha { get; set; }
//        [Display(Name = "CPF:")]
//        public string cpf { get; set; }
//        [Display(Name = "Nível de permissão:")]
//        public string permissao { get; set; }
//        [Display(Name = "Email:")]
//        public string email { get; set; }
//        [Display(Name = "Telefone:")]
//        public string telefone { get; set; }
//        [Display(Name = "Número:")]
//        public string rua { get; set; }
//        public Nullable<int> numero { get; set; }
//        [Display(Name = "Bairro:")]
//        public string bairro { get; set; }
//        [Display(Name = "Cidade:")]
//        public string cidade { get; set; }
//        [Display(Name = "Estado:")]
//        public string estado { get; set; }
//        [Display(Name = "CEP:")]
//        public string cep { get; set; }

//        public string confirmar_senha { get; set; }
//    }
//}