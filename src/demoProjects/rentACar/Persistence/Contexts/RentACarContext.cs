using Core.Persistence.Repositories;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Asn1.Ocsp;
using Persistence.Extensions;

namespace Persistence.Contexts
{
    public class RentACarContext : DbContext
    {
 

        public RentACarContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>(m =>
            {
                m.ToTable("Models").HasKey(m => m.Id);
                m.ConfigureBaseEntityProperties();
                m.Property(m => m.BrandId).HasColumnName("BrandId");
                m.Property(m => m.Name).HasColumnName("Name").HasMaxLength(100);
                m.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
                m.HasOne(m => m.Brand);
            });
            modelBuilder.Entity<Brand>(b =>
            {
                b.ToTable("Brands").HasKey(b => b.Id);
                b.ConfigureBaseEntityProperties();
                b.Property(b => b.Id).HasColumnName("Id");
                b.Property(b => b.Name).HasColumnName("Name");
                b.HasMany(b => b.Models);
            });

            modelBuilder.Entity<OperationClaim>(m =>
            {
                m.ToTable("OperationClaims").HasKey(m => m.Id);
            });

          
            modelBuilder.Entity<User>(m =>
            {
                m.ToTable("Users").HasKey(m => m.Id);
            });

            modelBuilder.Entity<RefreshToken>(m => {
                m.ToTable("RefreshTokens").HasKey(m => m.Id);
            });

            modelBuilder.Entity<UserOperationClaim>(m =>
            {
                m.ToTable("UserOperationClaims").HasKey(m => m.Id);
            });

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123", out passwordHash, out passwordSalt);

            User[] brandUsers = new User[]
            {
                new(1,"Halit","Kalaycı","user@user.com",passwordHash,passwordSalt,true,Core.Security.Enums.AuthenticatorType.None),
                new(2,"Admin","Admin","admin@admin.com",passwordHash,passwordSalt,true,Core.Security.Enums.AuthenticatorType.None)
            };
            modelBuilder.Entity<User>().HasData(brandUsers);

            OperationClaim[] operationClaims = new OperationClaim[]
            {
                new(1,"user"),
                new(2,"admin")
            };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaims);

            UserOperationClaim[] userOperationClaims = new UserOperationClaim[]
            {
                new(1,1,1),
                new(2,2,2)
            };
            modelBuilder.Entity<UserOperationClaim>().HasData(userOperationClaims);

            Brand[] brandSeeds = new Brand[]
            {
                new(1,"Bmw"),
                new(2,"Mercedes"),
                new(3,"Tofaş")
            };
            modelBuilder.Entity<Brand>().HasData(brandSeeds);

            Model[] modelSeeds = new Model[]
            {
                new(1,"M5",1),
                new(2,"CLA",2),
                new(3,"Kartal",3)
            };


            modelBuilder.Entity<Model>().HasData(modelSeeds);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(i => i is not null && i.Entity is Entity && (i.State == EntityState.Modified || i.State == EntityState.Added));
            foreach (var item in entries)
            {
                Entity entity = (Entity)item.Entity;
                entity.UpdatedDate = DateTime.Now;
                if (item.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(i => i is not null && i.Entity is Entity && (i.State == EntityState.Modified || i.State == EntityState.Added));
            foreach (var item in entries)
            {
                Entity entity = (Entity)item.Entity;
                entity.UpdatedDate = DateTime.Now;
                if(item.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

    }
}
