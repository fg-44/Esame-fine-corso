namespace TheAncientInn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Orders
    {
        [Key]
        public int Id_Order { get; set; }

        public int? Id_User { get; set; }

        public int? Id_Product { get; set; }

        public int? Id_Ingredients { get; set; }

        [Required]
        [StringLength(50)]
        public string Adress_Client { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsFulfilled { get; set; }

        public DateTime Order_Time { get; set; }

        public DateTime Delivering_Time { get; set; }

        [Required]
        [StringLength(250)]
        public string Note_Client { get; set; }

        public virtual Tbl_Ingredients Tbl_Ingredients { get; set; }

        public virtual Tbl_Products Tbl_Products { get; set; }

        public virtual Tbl_Users Tbl_Users { get; set; }
    }
}
