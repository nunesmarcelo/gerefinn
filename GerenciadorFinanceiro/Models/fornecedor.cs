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
    
    public partial class fornecedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fornecedor()
        {
            this.lancamento = new HashSet<lancamento>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string email { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string responsavel { get; set; }
        public string rua { get; set; }
        public Nullable<int> numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lancamento> lancamento { get; set; }
    }
}
