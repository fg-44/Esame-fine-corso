namespace TheAncientInn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Net;
    using System.Web;

    public partial class Tbl_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Products()
        {
            Tbl_Orders = new HashSet<Tbl_Orders>();
        }

        [Key]
        public int Id_Product { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(250)]
        public string Nome_Product { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(250)]
        public string Description_Product { get; set; }

        // ------ PRODUCT PHOTO ------ //

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Upload Img")]
        public string Photo1 { get; set; }

        public string Photo2 { get; set; }

        public string Photo3 { get; set; }

        // --------------------------- //

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Price")]
        public decimal Price_Product { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Quantity")]
        public int Quantity_Product { get; set; }

        public DateTime TimeOfDelivering { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Orders> Tbl_Orders { get; set; }

    }
}
