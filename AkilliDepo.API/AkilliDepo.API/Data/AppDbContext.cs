using AkilliDepo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AkilliDepo.API.Data
{
    // AppDbContext sınıfımız EF Core'un "DbContext" sınıfından miras alır.
    // Bu sayede veritabanı özelliklerini kazanır.
    public class AppDbContext : DbContext
    {
        // Veritabanı bağlantı ayarlarını içeri almak için gereken yapıcı metot (Constructor)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aşağıdaki kodlar, Entity sınıflarımızın SQL'de hangi isimle tablo olacağını belirler
        public DbSet<Product> Products { get; set; }
        public DbSet<WarehouseZone> WarehouseZones { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    }
}