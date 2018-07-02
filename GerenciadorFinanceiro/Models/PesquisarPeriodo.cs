﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciadorFinanceiro.Models
{
    public class PesquisarPeriodo
    {
        [Display(Name = "Data de Ínicio")]
        public DateTime inicio { get; set; }


        [Display(Name = "Data de Finalização")]
        public DateTime fim { get; set; }

        [Display(Name = "Pagamento já efetuado")]
        public string pago { get; set; }

        public string usuarioCpf { get; set; }

        public List<lancamento> lancamentos { get; set; }

        public string permissao { get; set; }

        [Display(Name = "Cooperativa")]
        public List<string> cooperativas { get; set; }

        public string cooperativa { get; set; }

        public decimal totalFixas { get; set; }

        public decimal totalVariadas { get; set; }

        public decimal totalGeral { get; set; }
    }
}