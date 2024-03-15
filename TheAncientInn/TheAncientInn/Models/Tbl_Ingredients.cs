namespace TheAncientInn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Ingredients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Ingredients()
        {
            Tbl_Orders = new HashSet<Tbl_Orders>();
        }

        [Key]
        public int Id_Ingredients { get; set; }

        [Required]
        [StringLength(50)]
        public string Name_Ingredients { get; set; }

        public virtual Tbl_Ingredients Tbl_Ingredients1 { get; set; }

        public virtual Tbl_Ingredients Tbl_Ingredients2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Orders> Tbl_Orders { get; set; }
    }
}
