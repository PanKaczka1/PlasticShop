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
    
    public partial class INFOSTOREORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INFOSTOREORDER()
        {
            this.STOREORDERS = new HashSet<STOREORDER>();
        }
    
        public decimal ORDER_ID { get; set; }
        public System.DateTime ORDER_DATE { get; set; }
        public Nullable<System.DateTime> DELIVERY_DATE { get; set; }
        public Nullable<decimal> DELIVERER_ID { get; set; }
    
        public virtual DELIVERER DELIVERER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STOREORDER> STOREORDERS { get; set; }
    }
}
