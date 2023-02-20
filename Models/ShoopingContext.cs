using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace LineBuyCart.Models
{
    public partial class ShoopingContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ShoopingContext()
        {
        }

        public ShoopingContext(DbContextOptions<ShoopingContext> options,IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<OrderList> OrderLists { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfoies { get; set; } = null!;
        public virtual DbSet<OrderFlow> OrderFlows { get; set; } = null!;
        public virtual DbSet<OrderConfirm> OrderConfirms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer();
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.ToTable("orderlist");

                entity.Property(e => e.OrderListId).HasColumnName("OrderListID");

                entity.Property(e => e.Name).HasMaxLength(25);
            });
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("userinfo");

                entity.Property(e => e.UserInfoId).HasColumnName("UserInfoId");

            });
            modelBuilder.Entity<OrderFlow>(entity =>
            {
                entity.ToTable("orderflow");

                entity.Property(e => e.OrderFlowId).HasColumnName("OrderFlowId");

            });
            modelBuilder.Entity<OrderConfirm>(entity =>
            {
                entity.ToTable("orderconfirm");

                entity.Property(e => e.OrderConfirmId).HasColumnName("OrderConfirmId");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
