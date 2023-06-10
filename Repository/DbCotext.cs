using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Repository
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admins.Admin> Admins { get; set; }
        public DbSet<SuperAdmin> superAdmins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // Set the table name

                entity.HasKey(e => e.Id); // Set the primary key

                // Configure other properties
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                // Add additional properties configuration as needed
            });
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=DESKTOP-93L68OP\\SQLEXPRESS;Initial Catalog=ORRBP;Integrated Security=True; TrustServerCertificate=True;");
        //    }
        //}
    }
}
