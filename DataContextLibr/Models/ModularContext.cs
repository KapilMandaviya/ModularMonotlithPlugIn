using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataContextLibr.Models;

public partial class ModularContext : DbContext
{
    public ModularContext()
    {
    }

    public ModularContext(DbContextOptions<ModularContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CourseMaster> CourseMasters { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<FormField> FormFields { get; set; }

    public virtual DbSet<FormUserControl> FormUserControls { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<SqlDatatypeList> SqlDatatypeLists { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SubMenu> SubMenus { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VT26F9U\\SQLEXPRESS;Database=ModularMonolithPlugin; Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

       

        modelBuilder.Entity<CourseMaster>(entity =>
        {
            entity.ToTable("CourseMaster");

            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FormName).HasMaxLength(255);
            entity.Property(e => e.TableName).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<FormField>(entity =>
        {
            entity.Property(e => e.CssClass).HasMaxLength(255);
            entity.Property(e => e.DataType).HasMaxLength(255);
            entity.Property(e => e.FieldName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FieldType).HasMaxLength(255);
            entity.Property(e => e.FormId).HasColumnName("Form_Id");
            entity.Property(e => e.Label).HasMaxLength(255);
            entity.Property(e => e.LengthValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OptionTableName).HasMaxLength(100);
            entity.Property(e => e.OptionTextField).HasMaxLength(100);
            entity.Property(e => e.OptionValueField).HasMaxLength(100);
            entity.Property(e => e.Tooltip).HasMaxLength(255);
        });

        modelBuilder.Entity<FormUserControl>(entity =>
        {
            entity.ToTable("FormUserControl");

            entity.Property(e => e.UserControl)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menus__3214EC07CC5A4492");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Modules__3214EC07C6B366E6");

            entity.Property(e => e.DllPath).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SqlDatatypeList>(entity =>
        {
            entity.ToTable("SqlDatatypeList");

            entity.Property(e => e.Datatype)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubMenus__3214EC07DD786A32");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.ToTable("UserMaster");

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
