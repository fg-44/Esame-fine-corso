using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TheAncientInn.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Tbl_Ingredients> Tbl_Ingredients { get; set; }
        public virtual DbSet<Tbl_Orders> Tbl_Orders { get; set; }
        public virtual DbSet<Tbl_Products> Tbl_Products { get; set; }
        public virtual DbSet<Tbl_Users> Tbl_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbl_Ingredients>()
                .Property(e => e.Name_Ingredients)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Ingredients>()
                .HasOptional(e => e.Tbl_Ingredients1)
                .WithRequired(e => e.Tbl_Ingredients2);

            modelBuilder.Entity<Tbl_Orders>()
                .Property(e => e.Adress_Client)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Orders>()
                .Property(e => e.Note_Client)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Products>()
                .Property(e => e.Nome_Product)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Products>()
                .Property(e => e.Description_Product)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Products>()
                .Property(e => e.Photo1)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Products>()
                .Property(e => e.Photo2)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Products>()
                .Property(e => e.Photo3)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Products>()
                .Property(e => e.Price_Product)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Tbl_Users>()
                .Property(e => e.Name_User)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Users>()
                .Property(e => e.Surname_User)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Users>()
                .Property(e => e.Username_User)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Users>()
                .Property(e => e.Email_User)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Users>()
                .Property(e => e.Password_User)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Users>()
                .Property(e => e.Role_User)
                .IsUnicode(false);

        }
    }
}
