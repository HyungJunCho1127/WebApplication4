using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication4.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .Property(e => e.Image1)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<WebApplication4.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Pet> Pets { get; set; }
    }
}
