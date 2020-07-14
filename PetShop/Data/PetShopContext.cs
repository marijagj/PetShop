using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Models
{
    public class PetShopContext : IdentityDbContext
    {
        public PetShopContext (DbContextOptions<PetShopContext> options)
            : base(options)
        {
        }

        public DbSet<Produkt> Produkt { get; set; }
        public DbSet<Sopstvenik> Sopstvenik { get; set; }
        public DbSet<Junction> Junction { get; set; }
        public object EntityEntry { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Junction>()
            .HasOne<Sopstvenik>(p => p.Sopstvenik)
            .WithMany(p => p.Produkti)
            .HasForeignKey(p => p.SopstvenikID);
            //.HasPrincipalKey(p => p.Id);

            builder.Entity<Junction>()
            .HasOne<Produkt>(p => p.Produkt)
            .WithMany(p => p.Sopstvenici)
            .HasForeignKey(p => p.ProduktID);
            //.HasPrincipalKey(p => p.Id);
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        internal IEnumerable<object> Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
