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
    
    public partial class lancamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lancamento()
        {
            this.notafiscallancamento = new HashSet<notafiscallancamento>();
            this.produtolancamento = new HashSet<produtolancamento>();
            this.caixa = new HashSet<caixa>();
        }
    
        public int id { get; set; }
        public string descricao { get; set; }
        public decimal valortotal { get; set; }
        public bool pago { get; set; }
        public Nullable<System.DateTime> datavencimento { get; set; }
        public System.DateTime datacadastro { get; set; }
        public string observacao { get; set; }
        public int contasaldo_id { get; set; }
        public int categoria_id { get; set; }
        public int usuario_id { get; set; }
        public int instituicao_id { get; set; }
    
        public virtual categoria categoria { get; set; }
        public virtual contasaldo contasaldo { get; set; }
        public virtual instituicao instituicao { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notafiscallancamento> notafiscallancamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<produtolancamento> produtolancamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caixa> caixa { get; set; }
    }
}
