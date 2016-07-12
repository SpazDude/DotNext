﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public partial class DotNextContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=DotNext;Integrated Security=True");
        //}

        public DotNextContext(DbContextOptions<DotNextContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasComputedColumnSql("CONVERT([nvarchar](max),[node])")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<People>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("char(2)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("char(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Race)
                    .IsRequired()
                    .HasColumnName("race")
                    .HasMaxLength(50);

                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasColumnName("ssn")
                    .HasColumnType("nchar(11)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(50);

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<UserAuthorization>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AuthorizationId })
                    .HasName("PK_UserAuthorization");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AuthorizationId).HasColumnName("authorization_id");

                entity.Property(e => e.Permissions)
                    .HasColumnName("permissions")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Authorization)
                    .WithMany(p => p.UserAuthorization)
                    .HasForeignKey(d => d.AuthorizationId)
                    .HasConstraintName("FK_UserAuthorization_Authorization");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAuthorization)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserAuthorization_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasColumnName("identifier")
                    .HasMaxLength(100);
            });
        }

        public virtual DbSet<Authorization> Authorization { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<UserAuthorization> UserAuthorization { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}