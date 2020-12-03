//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlasticShop
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.CUSTOMERORDERS = new HashSet<CUSTOMERORDER>();
            this.STOREORDERS = new HashSet<STOREORDER>();
        }
    
        public decimal PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal PRODUCTS_IN_STOCK { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public string PRODUCER { get; set; }
        public decimal PRICE { get; set; }
    
        public virtual BRUSH BRUSH { get; set; }
        public virtual CANVAS CANVAS { get; set; }
        public virtual CRAYON CRAYON { get; set; }
        public virtual INK INK { get; set; }
        public virtual MARKER MARKER { get; set; }
        public virtual NOTEBOOK NOTEBOOK { get; set; }
        public virtual PAINT PAINT { get; set; }
        public virtual PAPERPAD PAPERPAD { get; set; }
        public virtual PENCIL PENCIL { get; set; }
        public virtual PEN PEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMERORDER> CUSTOMERORDERS { get; set; }
        public virtual SCULPTINGMATERIAL SCULPTINGMATERIAL { get; set; }
        public virtual SET SET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STOREORDER> STOREORDERS { get; set; }
    }
}
