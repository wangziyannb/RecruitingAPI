using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecInfrastructure.Data
{
    public class RecDbContext : DbContext
    {

        public RecDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmployeeRequirementType>()
            //    .ToTable("EmployeeRequirementTypes")
            //    .HasOne(a => a.EmployeeType)
            //    .WithMany(b => b.EmployeeRequirementTypes)
            //    .HasForeignKey(b => b.EmployeeTypeId);
            //modelBuilder.Entity<EmployeeRequirementType>()
            //    .ToTable("EmployeeRequirementTypes")
            //    .HasOne(a => a.JobRequirement)
            //    .WithMany(b => b.EmployeeRequirementTypes)
            //    .HasForeignKey(b => b.JobRequirementId);

            //Fluent API
            modelBuilder.Entity<EmployeeRequirementType>(ConfigureEmployeeRequirementType);
            modelBuilder.Entity<EmployeeType>(ConfigureEmployeeType);
        }
        private void ConfigureEmployeeRequirementType(EntityTypeBuilder<EmployeeRequirementType> builder)
        {
            builder.ToTable("EmployeeRequirementTypes");
            builder.HasKey(e => new { e.EmployeeTypeId, e.JobRequirementId });
            builder.HasOne(a => a.EmployeeType).WithMany(b => b.EmployeeRequirementTypes).HasForeignKey(b => b.EmployeeTypeId);
            builder.HasOne(a => a.JobRequirement).WithMany(b => b.EmployeeRequirementTypes).HasForeignKey(b => b.JobRequirementId);
        }
        private void ConfigureEmployeeType(EntityTypeBuilder<EmployeeType> builder)
        {
            builder.ToTable("EmployeeTypes");
            builder.HasKey(et => et.Id);
            builder.Property(n => n.TypeName).HasMaxLength(128);
            //builder.Property(r => r.ReviewText).HasMaxLength(4000);
            //builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
            //builder.Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<JobRequirement> JobRequirements { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeRequirementType> EmployeeRequirementTypes { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Status> Statuses { get; set; }

    }
}
