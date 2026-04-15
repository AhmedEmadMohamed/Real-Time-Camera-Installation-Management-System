using Microsoft.EntityFrameworkCore;
using Real_Time_Camera_Installation_Management_System.Models.Domain;
using System.Security.Cryptography;

namespace Real_Time_Camera_Installation_Management_System.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Customer)
                .WithMany(c => c.Job)
                .HasForeignKey(j => j.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.JobStatus)
                .HasDefaultValue("Pending");
        }
    }
}
