using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;

namespace WoodcarvingApp.Web.Data
{
    public class WoodcarvingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WoodcarvingDbContext(DbContextOptions<WoodcarvingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Woodcarving> Woodcarvings { get; set; } = null!;
        public virtual DbSet<Woodcarver> Woodcarvers { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<WoodType> WoodTypes { get; set; } = null!;
        public virtual DbSet<Exhibition> Exhibitions { get; set; } = null!;
        public virtual DbSet<WoodcarvingExhibition> WoodcarvingExhibitions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(
                new City { Id = Guid.NewGuid(), CityName = "Woodville", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false },
                new City { Id = Guid.NewGuid(), CityName = "Carverton", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false },
                new City { Id = Guid.NewGuid(), CityName = "Timbertown", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false },
                new City { Id = Guid.NewGuid(), CityName = "Forestburg", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false },
                new City { Id = Guid.NewGuid(), CityName = "Oakwood", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false }
            );

            modelBuilder.Entity<Woodcarver>().HasData(
                new Woodcarver { Id = Guid.NewGuid(), FirstName = "John", LastName = "Smith", CityId = Guid.NewGuid(), Age = 40, PhoneNumber = "+359392952960", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", CityId = Guid.NewGuid(), Age = 35, PhoneNumber = "++353392955910", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.NewGuid(), FirstName = "Mike", LastName = "Brown", CityId = Guid.NewGuid(), Age = 50, PhoneNumber = "+356392932965", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.NewGuid(), FirstName = "Anna", LastName = "White", CityId = Guid.NewGuid(), Age = 28, PhoneNumber = "+159392952960", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.NewGuid(), FirstName = "Lucy", LastName = "Green", CityId = Guid.NewGuid(), Age = 45, PhoneNumber = "+379397952930", ImageUrl = null, IsDeleted = false }
            );

            modelBuilder.Entity<WoodType>().HasData(
                new WoodType { Id = Guid.NewGuid(), WoodTypeName = "Oak", Description = "Strong and durable wood.", Hardness = "Hard", Color = "Brown", ImageUrl = "/images/woodtypes/oak.png", IsDeleted = false },
                new WoodType { Id = Guid.NewGuid(), WoodTypeName = "Pine", Description = "Soft and easy to carve.", Hardness = "Soft", Color = "Light Brown", ImageUrl = "/images/woodtypes/pine.png", IsDeleted = false },
                new WoodType { Id = Guid.NewGuid(), WoodTypeName = "Maple", Description = "Smooth and fine grain.", Hardness = "Hard", Color = "Light Yellow", ImageUrl = "/images/woodtypes/maple.png", IsDeleted = false },
                new WoodType { Id = Guid.NewGuid(), WoodTypeName = "Walnut", Description = "Rich dark tones.", Hardness = "Hard", Color = "Dark Brown", ImageUrl = "/images/woodtypes/walnut.png", IsDeleted = false },
                new WoodType { Id = Guid.NewGuid(), WoodTypeName = "Cherry", Description = "Beautiful reddish tint.", Hardness = "Medium", Color = "Red", ImageUrl = "/images/woodtypes/cherry.png", IsDeleted = false }
            );

            modelBuilder.Entity<Exhibition>().HasData(
                new Exhibition { Id = Guid.NewGuid(), ExhibitionName = "Carving Masterpieces", Address = "123 Wood St, Woodville", StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(-20), CityId = Guid.NewGuid(), ImageUrl = "/images/exhibitions/masterpieces.png", IsDeleted = false },
                new Exhibition { Id = Guid.NewGuid(), ExhibitionName = "Timber Treasures", Address = "456 Forest Rd, Carverton", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(10), CityId = Guid.NewGuid(), ImageUrl = "/images/exhibitions/treasures.png", IsDeleted = false },
                new Exhibition { Id = Guid.NewGuid(), ExhibitionName = "Wood Wonders", Address = "789 Timber Ln, Timbertown", StartDate = DateTime.Now.AddDays(15), EndDate = DateTime.Now.AddDays(25), CityId = Guid.NewGuid(), ImageUrl = "/images/exhibitions/wonders.png", IsDeleted = false },
                new Exhibition { Id = Guid.NewGuid(), ExhibitionName = "Forest Art", Address = "321 Grove Ave, Forestburg", StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), CityId = Guid.NewGuid(), ImageUrl = "/images/exhibitions/art.png", IsDeleted = false },
                new Exhibition { Id = Guid.NewGuid(), ExhibitionName = "Oak Creations", Address = "654 Branch Blvd, Oakwood", StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), CityId = Guid.NewGuid(), ImageUrl = "/images/exhibitions/creations.png", IsDeleted = false }
            );

            modelBuilder.Entity<Woodcarving>().HasData(
                new Woodcarving { Id = Guid.NewGuid(), Title = "Forest Spirit", Description = "A carving of a mystical forest spirit.", WoodcarverId = Guid.NewGuid(), WoodTypeId = Guid.NewGuid(), ImageUrl = "/images/woodcarvings/spirit.png", IsAvailable = true, IsDeleted = false },
                new Woodcarving { Id = Guid.NewGuid(), Title = "Eagle Soar", Description = "An eagle in flight.", WoodcarverId = Guid.NewGuid(), WoodTypeId = Guid.NewGuid(), ImageUrl = "/images/woodcarvings/eagle.png", IsAvailable = true, IsDeleted = false },
                new Woodcarving { Id = Guid.NewGuid(), Title = "Bear Strength", Description = "A mighty bear.", WoodcarverId = Guid.NewGuid(), WoodTypeId = Guid.NewGuid(), ImageUrl = "/images/woodcarvings/bear.png", IsAvailable = false, IsDeleted = false },
                new Woodcarving { Id = Guid.NewGuid(), Title = "Tree of Life", Description = "An intricate tree design.", WoodcarverId = Guid.NewGuid(), WoodTypeId = Guid.NewGuid(), ImageUrl = "/images/woodcarvings/tree.png", IsAvailable = true, IsDeleted = false },
                new Woodcarving { Id = Guid.NewGuid(), Title = "River Flow", Description = "Abstract carving of a flowing river.", WoodcarverId = Guid.NewGuid(), WoodTypeId = Guid.NewGuid(), ImageUrl = "/images/woodcarvings/river.png", IsAvailable = true, IsDeleted = false }
            );
        }
    }
}
