//------------------------------------------------------------------------------
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
    
    public partial class DetailInvoiceIn
    {
        public int ID { get; set; }
        public string Inv_ID { get; set; }
        public string Prod_ID { get; set; }
        public int Quantity { get; set; }
        public decimal Inv_Total { get
            {
                if(Product != null)
                {
                    return (decimal)Quantity * Product.Prod_Price_In;
                }
                return 0;
            } set { } }
    
        public virtual InvoiceIn InvoiceIn { get; set; }
        public virtual Product Product { get; set; }
    }
}
