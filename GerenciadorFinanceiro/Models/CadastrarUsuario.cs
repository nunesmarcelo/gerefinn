using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciadorFinanceiro.Models
{
    public class CadastrarUsuario
    {
        //public List<cooperativa> cooperativas { get; set; }


        public usuario usuario { get; set; }

        [Display(Name = "Cooperativa")]
        public List<string> cooperativasNomes { get; set; }

        public string cooperativaSelecionada { get; set; }
    }
}