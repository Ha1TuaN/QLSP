﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebQLSP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class InvoiceIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceIn()
        {
            this.DetailInvoiceIns = new List<DetailInvoiceIn>();
        }
        [Required(ErrorMessage = "Mã hóa đơn là bắt buộc.")]
        public string Inv_ID { get; set; }
        [Required(ErrorMessage = "Ngày nhập hóa đơn là bắt buộc.")]
        public System.DateTime Inv_DateIn { get; set; }
        public string Sup_ID { get; set; }
        public bool isDelete { get; set; }
        public Nullable<int> Emp_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<DetailInvoiceIn> DetailInvoiceIns { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}