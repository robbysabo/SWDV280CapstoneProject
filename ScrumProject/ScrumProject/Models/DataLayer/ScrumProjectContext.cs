using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class ScrumProjectContext : IdentityDbContext<AuthUser>
{
    public ScrumProjectContext()
    {
    }

    public ScrumProjectContext(DbContextOptions<ScrumProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<ImageTag> ImageTags { get; set; }
    public virtual DbSet<Job> Jobs { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); //for identity
        /*
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA229F28A39");

            entity.Property(e => e.AppointmentId).ValueGeneratedOnAdd();
            entity.Property(e => e.ApptStat).IsFixedLength();

            entity.HasOne(d => d.AppointmentType).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Appoi__3D5E1FD2");
        });

        modelBuilder.Entity<AppointmentType>(entity =>
        {
            entity.HasKey(e => e.AppointmentTypeId).HasName("PK__Appointm__E258530B0ABAC468");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Images__7516F4EC9A160B74");

            entity.HasOne(d => d.ImageTag).WithMany(p => p.Images)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Images__ImageTag__440B1D61");
        });

        modelBuilder.Entity<ImageTag>(entity =>
        {
            entity.HasKey(e => e.ImageTagId).HasName("PK__ImageTag__AE6DABC46D4C5993");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Jobs__056690E2B17F3FA7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACDE25724C");

           entity.Property(e => e.UserId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__UserTypeI__44FF419A");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PK__UserType__40D2D8F69DB50F57");
        });

        */

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
