//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GerenciadorFinanceiro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class notafiscallancamento
    {
        public int id { get; set; }
        public string numero { get; set; }
        public System.DateTime data { get; set; }
        public int lancamento_id { get; set; }
    
        public virtual lancamento lancamento { get; set; }
    }
}
