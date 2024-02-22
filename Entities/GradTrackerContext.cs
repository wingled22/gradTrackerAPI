using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gradTrackerAPI.Entities;

public partial class GradTrackerContext : DbContext
{
    public GradTrackerContext()
    {
    }

    public GradTrackerContext(DbContextOptions<GradTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumnus> Alumni { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GradTracker;User ID=SA;Password=VeryStr0ngP@ssw0rd;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumnus>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Barangay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("barangay");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("middle_name");
            entity.Property(e => e.Municipality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipality");
            entity.Property(e => e.Program)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("program");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.YearGraduated)
                .HasColumnType("date")
                .HasColumnName("year_graduated");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmploymentHistory>(entity =>
        {
            entity.ToTable("EmploymentHistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
