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
    
    public partial class INFOORDERCUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INFOORDERCUSTOMER()
        {
            this.CUSTOMERORDERS = new HashSet<CUSTOMERORDER>();
        }
    
        public decimal ORDER_ID { get; set; }
        public System.DateTime ORDER_DATE { get; set; }
        public Nullable<System.DateTime> SHIPPING_DATE { get; set; }
        public Nullable<decimal> SUMMARY_DISCOUNT { get; set; }
        public Nullable<decimal> CUSTOMER_ID { get; set; }
    
        public virtual CUSTOMER CUSTOMER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMERORDER> CUSTOMERORDERS { get; set; }
    }
}