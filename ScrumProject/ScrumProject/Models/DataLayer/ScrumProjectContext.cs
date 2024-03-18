using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class ScrumProjectContext : DbContext
{
    public ScrumProjectContext()
    {
    }

    public ScrumProjectContext(DbContextOptions<ScrumProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=SPContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.ApptId).HasName("PK__Appointm__E43EE9965759C9F2");

            entity.Property(e => e.ApptStat).IsFixedLength();
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImgId).HasName("PK__Images__6F16A71CD161E410");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Jobs__6E32B6A56167BAC9");

            entity.HasOne(d => d.Img).WithMany(p => p.Jobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jobs__img_id__3D5E1FD2");

            entity.HasOne(d => d.UIdNavigation).WithMany(p => p.Jobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jobs__u_id__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UId).HasName("PK__Users__B51D3DEA7BC1645C");

            entity.Property(e => e.UserType).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
