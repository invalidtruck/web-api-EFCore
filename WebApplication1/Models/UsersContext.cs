using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace WebApplication1.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UsersContext()
        {
        }

        protected UsersContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"server=DESKTOP-MAGT0LC\SQLEXPRESS;Database=UsersDB;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(p => p.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(p => p.Username).HasMaxLength(50).IsUnicode(false); 
            });
        }
    }
}
