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

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DetailInvoiceIns = new HashSet<DetailInvoiceIn>();
            this.DetailInvoiceOuts = new HashSet<DetailInvoiceOut>();
        }
        [Required(ErrorMessage = "Yêu cầu nhập Mã sản phẩm.")]
        public string Prod_ID { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên sản phẩm.")]
        public string Prod_Name { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Giá nhập của sản phẩm.")]
        public decimal Prod_Price_Out { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Giá xuất sản phẩm.")]
        public decimal Prod_Price_In { get; set; }
        public string UrlImg { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập số lượng trong kho của sản phẩm.")]
        public int Quantity { get; set; }
        public int Catalog_ID { get; set; }
        public int Brand_ID { get; set; }
        public bool isDelete { get; set; }
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailInvoiceIn> DetailInvoiceIns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailInvoiceOut> DetailInvoiceOuts { get; set; }
        public virtual ProductCatalog ProductCatalog { get; set; }
    }
}
