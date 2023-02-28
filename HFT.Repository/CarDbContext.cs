using HFT.Models;
using Microsoft.EntityFrameworkCore;

namespace HFT.Repository
{
    public partial class CarDbContext : DbContext
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }

        public CarDbContext()
        {
            this.Database.EnsureCreated();
        }

        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("CarShopDatabase").UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity => {
                entity.HasOne(car => car.Brand).WithMany(brand => brand.Cars).HasForeignKey(car => car.BrandId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Car>(entity => {
                entity.HasOne(car => car.Owner).WithMany(owner => owner.Cars).HasForeignKey(car => car.OwnerId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            Brand bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand citroen = new Brand() { Id = 2, Name = "Citroen" };
            Brand audi = new Brand() { Id = 3, Name = "Audi" };

            Owner gabi = new Owner() { Id = 1, Name = "Gábor" };
            Owner jani = new Owner() { Id = 2, Name = "János" };

            Car bmw1 = new Car() { Id = 1, BrandId = bmw.Id, OwnerId = gabi.Id, BasePrice = 20000, Model = "BMW 116d" };
            Car bmw2 = new Car() { Id = 2, BrandId = bmw.Id, BasePrice = 30000, Model = "BMW 510" };
            Car citroen1 = new Car() { Id = 3, BrandId = citroen.Id, OwnerId = gabi.Id, BasePrice = 10000, Model = "Citroen C1" };
            Car citroen2 = new Car() { Id = 4, BrandId = citroen.Id, BasePrice = 15000, Model = "Citroen C3" };
            Car audi1 = new Car() { Id = 5, BrandId = audi.Id, OwnerId = jani.Id, BasePrice = 20000, Model = "Audi A3" };
            Car audi2 = new Car() { Id = 6, BrandId = audi.Id, BasePrice = 25000, Model = "Audi A4" };

            modelBuilder.Entity<Brand>().HasData(bmw, citroen, audi);
            modelBuilder.Entity<Car>().HasData(bmw1, bmw2, citroen1, citroen2, audi1, audi2);
            modelBuilder.Entity<Owner>().HasData(gabi, jani);
        }
    }
}
