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
    
    public partial class COLOUR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COLOUR()
        {
            this.CRAYONS = new HashSet<CRAYON>();
            this.INKS = new HashSet<INK>();
            this.MARKERS = new HashSet<MARKER>();
            this.PAINTS = new HashSet<PAINT>();
            this.PENS = new HashSet<PEN>();
            this.SCULPTINGMATERIALS = new HashSet<SCULPTINGMATERIAL>();
        }
    
        public decimal COLOUR_ID { get; set; }
        public string COLOUR_NAME { get; set; }
        public decimal RED_VALUE { get; set; }
        public decimal GREEN_VALUE { get; set; }
        public decimal BLUE_VALUE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRAYON> CRAYONS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INK> INKS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MARKER> MARKERS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAINT> PAINTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEN> PENS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCULPTINGMATERIAL> SCULPTINGMATERIALS { get; set; }
    }
}
