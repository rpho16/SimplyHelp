using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimplyHelp.Models
{
    public partial class SimplyHelpContext : DbContext
    {
        public SimplyHelpContext()
        {
        }
        public SimplyHelpContext(DbContextOptions<SimplyHelpContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AlertMessage> AlertMessage { get; set; }
        public virtual DbSet<AlertType> AlertType { get; set; }
        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<Disaster> Disaster { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PlacesGeo> PlacesGeo { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<TblSubscription> TblSubscription { get; set; }
        public virtual DbSet<UserGeo> UserGeo { get; set; }
        public virtual DbSet<UserMembers> UserMembers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AlertMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlertMessageName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdAlertType).HasColumnName("id_AlertType");

                entity.HasOne(d => d.IdAlertTypeNavigation)
                    .WithMany(p => p.AlertMessage)
                    .HasForeignKey(d => d.IdAlertType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlertMessage_AlertType");
            });

            modelBuilder.Entity<AlertType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlertTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdDisaster).HasColumnName("id_Disaster");

                entity.HasOne(d => d.IdDisasterNavigation)
                    .WithMany(p => p.AlertType)
                    .HasForeignKey(d => d.IdDisaster)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlertType_Disaster");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarrierGateway)
                    .IsRequired()
                    .HasColumnName("carrier_Gateway")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CarrierName)
                    .IsRequired()
                    .HasColumnName("carrier_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Disaster>(entity =>
            {
                entity.Property(e => e.DisasterName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasColumnName("module")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlacesGeo>(entity =>
            {
                entity.ToTable("placesGeo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PlaceLat)
                    .IsRequired()
                    .HasColumnName("placeLat")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceLon)
                    .IsRequired()
                    .HasColumnName("placeLon")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceName)
                    .IsRequired()
                    .HasColumnName("placeName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceType)
                    .IsRequired()
                    .HasColumnName("placeType")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceVicinity)
                    .IsRequired()
                    .HasColumnName("placeVicinity")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("Role_Permission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPermission).HasColumnName("idPermission");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.HasOne(d => d.IdPermissionNavigation)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.IdPermission)
                    .HasConstraintName("FK_Role_Permission_Permissions");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_Role_Permission_Role");
            });

            modelBuilder.Entity<TblSubscription>(entity =>
            {
                entity.ToTable("tblSubscription");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserGeo>(entity =>
            {
                entity.ToTable("userGeo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("dateAdded")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<UserMembers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("fullName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zipCode")
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("fullName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_Role");
            });
        }
    }
}
