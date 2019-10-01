using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactServiceSolution.Data.Entity
{
    public partial class ContactDatabaseContext : DbContext
    {
        public ContactDatabaseContext()
        {
        }

        public ContactDatabaseContext(DbContextOptions<ContactDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactEntity> Contact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEntity>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50).IsRequired();

                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });
        }
    }
}
