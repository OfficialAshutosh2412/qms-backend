using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualityWebSystem.Models;

namespace QualityWebSystem.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewReply> ReviewReplies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Review -> Customer (AppUser)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review -> Department
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Department)
                .WithMany()
                .HasForeignKey(r => r.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

            // ReviewReply -> Admin (AppUser)
            modelBuilder.Entity<ReviewReply>()
                .HasOne(rr => rr.Admin)
                .WithMany()
                .HasForeignKey(rr => rr.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // ReviewReply -> Review
            modelBuilder.Entity<ReviewReply>()
                .HasOne(rr => rr.Review)
                .WithMany()
                .HasForeignKey(rr => rr.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            //seeding departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DeptId=1,DeptName="Sales"},
                new Department { DeptId=2,DeptName="Service"},
                new Department { DeptId=3,DeptName="Marketing"}
                );
        }
    }
}
