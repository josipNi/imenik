using Imenik_JN.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Imenik_JN.Server.Data
{
    public class Hrcloud_DB_Context : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }

        public Hrcloud_DB_Context(DbContextOptions<Hrcloud_DB_Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {                
                entity.Property(p => p.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
                entity.Property(p => p.Modified).HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Tag>(entity =>
            {                
                entity.Property(p => p.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
                entity.Property(p => p.Modified).HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            });           
            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(p => p.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
                entity.Property(p => p.Modified).HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(p => p.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
                entity.Property(p => p.Modified).HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            });
      
            base.OnModelCreating(modelBuilder);

        }
    }
}
