using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorFinanceiro.Models
{
    public class CadastrarVenda
    {
        public venda Venda { get; set; }

        public List<produtovenda> Produtos { get; set; }

        public List<pagamento> Pagamentos { get; set; }
    }
}