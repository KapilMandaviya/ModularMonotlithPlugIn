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

    public virtual DbSet<Shyam> Shyams { get; set; }

    public virtual DbSet<SqlDatatypeList> SqlDatatypeLists { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SubMenu> SubMenus { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__Menus__3214EC07B5BC3BAB");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Modules__3214EC07C6B366E6");

            entity.Property(e => e.DllPath).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Shyam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shyam__3214EC07AD6085D7");

            entity.ToTable("Shyam");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false);
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
            entity.HasKey(e => e.Id).HasName("PK__SubMenus__3214EC073B538768");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3214EC0717F8FFC4");

            entity.ToTable("Test");

            entity.Property(e => e.Address).HasMaxLength(400);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Hobby)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Photo)
                .HasMaxLength(150)
                .IsUnicode(false);
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
