using Microsoft.EntityFrameworkCore;

namespace Lab08_AlonsoSahuanay.Models
{
    public class Lab08DbContext : DbContext
    {
        public Lab08DbContext(DbContextOptions<Lab08DbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales del modelo
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailId);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
                
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
        }
    }
}