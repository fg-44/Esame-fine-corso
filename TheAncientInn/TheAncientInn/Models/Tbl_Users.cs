namespace TheAncientInn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Users()
        {
            Tbl_Orders = new HashSet<Tbl_Orders>();
        }

        [Key]
        public int Id_User { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Surname")]
        [StringLength(50)]
        public string Name_User { get; set; }


        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Surname")]
        [StringLength(50)]
        public string Surname_User { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Username")]
        [StringLength(50)]
        public string Username_User { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50)]
        public string Email_User { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Password")]
        [StringLength(50)]
        public string Password_User { get; set; }

        [StringLength(50)]
        public string Role_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Orders> Tbl_Orders { get; set; }

    }
}
