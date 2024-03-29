﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace adminFlowerShop_Gr1.Models
{
    public partial class FlowerShop_Group1Context : DbContext
    {
        public FlowerShop_Group1Context()
        {
        }

        public FlowerShop_Group1Context(DbContextOptions<FlowerShop_Group1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<TblAccount> TblAccounts { get; set; } = null!;
        public virtual DbSet<TblAttribute> TblAttributes { get; set; } = null!;
        public virtual DbSet<TblAttributesPrice> TblAttributesPrices { get; set; } = null!;
        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblLocation> TblLocations { get; set; } = null!;
        public virtual DbSet<TblOrder> TblOrders { get; set; } = null!;
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; } = null!;
        public virtual DbSet<TblPage> TblPages { get; set; } = null!;
        public virtual DbSet<TblPost> TblPosts { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblShipper> TblShippers { get; set; } = null!;
        public virtual DbSet<TblTransactStatus> TblTransactStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-URI2OAJ\\SQLEXPRESS;Database=FlowerShop_Group1;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.ActionName).HasMaxLength(40);

                entity.Property(e => e.ControllerName).HasMaxLength(40);

                entity.Property(e => e.Link).HasMaxLength(50);

                entity.Property(e => e.MenuName).HasMaxLength(35);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<TblAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__tblAccou__349DA5868D2A4F1B");

                entity.ToTable("tblAccount");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_tblAccount_tblRoles1");
            });

            modelBuilder.Entity<TblAttribute>(entity =>
            {
                entity.HasKey(e => e.AttributeId)
                    .HasName("PK__tblAttri__C189298A134EB68A");

                entity.ToTable("tblAttributes");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<TblAttributesPrice>(entity =>
            {
                entity.HasKey(e => e.AttributesPriceId)
                    .HasName("PK__tblAttri__699F045CF9B67DBB");

                entity.ToTable("tblAttributesPrices");

                entity.Property(e => e.AttributesPriceId).HasColumnName("AttributesPriceID");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.TblAttributesPrices)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_tblAttributesPrices_tblAttributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblAttributesPrices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tblAttributesPrices_tblProducts");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__tblCateg__6A1C8ADA2E3A8878");

                entity.ToTable("tblCategories");

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CatName).HasMaxLength(250);

                entity.Property(e => e.Cover).HasMaxLength(255);

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__tblCusto__A4AE64B8FC906026");

                entity.ToTable("tblCustomers");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.Salt)
                    .HasMaxLength(8)
                    .IsFixedLength();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblCustomers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_tblCustomers_tblLocations");
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__tblLocat__E7FEA47765A3952C");

                entity.ToTable("tblLocations");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NameWithType).HasMaxLength(255);

                entity.Property(e => e.PathWithType).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(20);
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__tblOrder__C3905BAF1DD956E7");

                entity.ToTable("tblOrders");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_tblOrders_tblCustomers");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .HasConstraintName("FK_tblOrders_tblTransactStatus");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK__tblOrder__D3B9D30CEBEE0EFC");

                entity.ToTable("tblOrderDetails");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_tblOrderDetails_tblOrders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tblOrderDetails_tblProducts");
            });

            modelBuilder.Entity<TblPage>(entity =>
            {
                entity.HasKey(e => e.PageId)
                    .HasName("PK__tblPages__C565B124C9FB2C52");

                entity.ToTable("tblPages");

                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.PageName).HasMaxLength(250);

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<TblPost>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK__tblPost__C3905BAFF3BD2E10");

                entity.ToTable("tblPost");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsNewFeed).HasColumnName("isNewFeed");

                entity.Property(e => e.MetaDesc).HasMaxLength(255);

                entity.Property(e => e.MetaKey).HasMaxLength(255);

                entity.Property(e => e.Scontents).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TblPosts)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_tblPost_tblAccount2");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.TblPosts)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_tblPost_tblCategories");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__tblProdu__B40CC6ED4070F89F");

                entity.ToTable("tblProducts");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("dateModified");

                entity.Property(e => e.MetaDesc).HasMaxLength(255);

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(255)
                    .HasColumnName("metaKey");

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.ShortDesc).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Video).HasMaxLength(255);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_tblProducts_tblCategories1");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tblRoles__8AFACE3AF37849BD");

                entity.ToTable("tblRoles");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblShipper>(entity =>
            {
                entity.HasKey(e => e.ShipperId)
                    .HasName("PK__tblShipp__1F8AFFB99183DCA2");

                entity.ToTable("tblShippers");

                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.Company).HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipperName).HasMaxLength(150);
            });

            modelBuilder.Entity<TblTransactStatus>(entity =>
            {
                entity.HasKey(e => e.TransactStatusId)
                    .HasName("PK__tblTrans__C8BCD276D83B5F69");

                entity.ToTable("tblTransactStatus");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
