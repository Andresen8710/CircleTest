using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaCycle.Domain.Entities;

namespace PruebaTecnicaCycle.Infraestructure.Persistance
{
    public class PruebaTecnicaCycleDBContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public PruebaTecnicaCycleDBContext(DbContextOptions<PruebaTecnicaCycleDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product", "Catalog");

            modelBuilder.Entity<Product>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e=>e.Name)
                .IsRequired()
                .HasMaxLength(150);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e=>e.Category)
                .IsRequired()
                .HasMaxLength(150);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e=>e.Description)
                .HasMaxLength(500);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Image)
                .HasColumnType("varchar(max)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("numeric(10,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}