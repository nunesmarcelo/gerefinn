using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciadorFinanceiro.Models
{
    public class CadastrarLancamento
    {
        public List<lancamento> lancamentos { get; set; }

        public lancamento lancamento { get; set; }

        [Display(Name = "Cooperativa")]
        public List<string> cooperativasNomes { get; set; }

        public string cooperativaSelecionada { get; set; }

    }
}