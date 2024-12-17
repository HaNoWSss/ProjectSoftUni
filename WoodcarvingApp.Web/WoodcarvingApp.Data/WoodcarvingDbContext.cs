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
                new City { Id = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), CityName = "Woodville", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse2.mm.bing.net%2Fth%3Fid%3DOIP.JdS0gRMOKp50OdrdPtnkpwHaE8%26pid%3DApi&f=1&ipt=97ed82df0cb89ed0dbae5d680652f0513fbbf2df7d9f8f348584dece9e4b9abd&ipo=images", IsDeleted = false },
                new City { Id = Guid.Parse("c30fe708-a15c-42e8-b088-23d6ce4619fd"), CityName = "Carverton", ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.cXZECwOwvxCLTwpnK2l3TAHaE7%26pid%3DApi&f=1&ipt=a7083c6c3178beed48a1bfe3c62a86360491e732eb671367ba82cefb91ee78e8&ipo=images", IsDeleted = false },
                new City { Id = Guid.Parse("26019535-ae83-4aa8-93cd-0b03a1b2df52"), CityName = "Timbertown", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false },
                new City { Id = Guid.Parse("16cae79f-14b0-491e-ade7-13ccdd10718a"), CityName = "Forestburg", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false },
                new City { Id = Guid.Parse("6731e5de-f039-45a1-9e42-ff3d5e5508a8"), CityName = "Oakwood", ImageUrl = "/images/woodcarving-image-not-added.png", IsDeleted = false }
            );

            modelBuilder.Entity<Woodcarver>().HasData(
                new Woodcarver { Id = Guid.Parse("78632fd1-22d1-4a9d-9120-1da7383d8632"), FirstName = "John", LastName = "Smith", CityId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), Age = 40, PhoneNumber = "1234567890", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.Parse("dd483604-dcec-4d01-9b5b-07543a563c4a"), FirstName = "Jane", LastName = "Doe", CityId = Guid.Parse("c30fe708-a15c-42e8-b088-23d6ce4619fd"), Age = 35, PhoneNumber = "9574868501", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.Parse("bab54739-3aec-46c9-a56c-4038c2a409b8"), FirstName = "Mike", LastName = "Brown", CityId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), Age = 50, PhoneNumber = "1859346950", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.Parse("c77480d2-e2d3-4c1e-a4fc-ca076b34c775"), FirstName = "Anna", LastName = "White", CityId = Guid.Parse("c30fe708-a15c-42e8-b088-23d6ce4619fd"), Age = 28, PhoneNumber = "1682765248", ImageUrl = null, IsDeleted = false },
                new Woodcarver { Id = Guid.Parse("eae143bd-cd7b-4937-a9a1-660083393faf"), FirstName = "Lucy", LastName = "Green", CityId = Guid.Parse("c30fe708-a15c-42e8-b088-23d6ce4619fd"), Age = 45, PhoneNumber = "1267236895", ImageUrl = null, IsDeleted = false }
            );

            modelBuilder.Entity<WoodType>().HasData(
                new WoodType { Id = Guid.Parse("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"), WoodTypeName = "Oak", Description = "Strong and durable wood.", Hardness = "Hard", Color = "Brown", ImageUrl = "/images/woodtypes/oak.png", IsDeleted = false },
                new WoodType { Id = Guid.Parse("8b644971-56b0-4caf-8055-3c5178512cbc"), WoodTypeName = "Pine", Description = "Soft and easy to carve.", Hardness = "Soft", Color = "Light Brown", ImageUrl = "/images/woodtypes/pine.png", IsDeleted = false },
                new WoodType { Id = Guid.Parse("ed86f019-20de-4478-be30-ef28801fe50d"), WoodTypeName = "Maple", Description = "Smooth and fine grain.", Hardness = "Hard", Color = "Light Yellow", ImageUrl = "/images/woodtypes/maple.png", IsDeleted = false },
                new WoodType { Id = Guid.Parse("98762166-0a17-44bd-a403-b9437789c31a"), WoodTypeName = "Walnut", Description = "Rich dark tones.", Hardness = "Hard", Color = "Dark Brown", ImageUrl = "/images/woodtypes/walnut.png", IsDeleted = false },
                new WoodType { Id = Guid.Parse("35b17586-b912-4310-9894-643178901a9b"), WoodTypeName = "Cherry", Description = "Beautiful reddish tint.", Hardness = "Medium", Color = "Red", ImageUrl = "/images/woodtypes/cherry.png", IsDeleted = false }
            );

            modelBuilder.Entity<Exhibition>().HasData(
                new Exhibition { Id = Guid.Parse("bda37e99-eb32-4b8a-b28a-4cace7b19f24"), ExhibitionName = "Carving Masterpieces", Address = "123 Wood St, Woodville", StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(-20), CityId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/exhibitions/masterpieces.png", IsDeleted = false },
                new Exhibition { Id = Guid.Parse("d28bb261-5ff3-432c-b1fd-162f0280c936"), ExhibitionName = "Timber Treasures", Address = "456 Forest Rd, Carverton", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(10), CityId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/exhibitions/treasures.png", IsDeleted = false },
                new Exhibition { Id = Guid.Parse("0cf6d5a3-1025-4fe1-ad94-a431f25bacd7"), ExhibitionName = "Wood Wonders", Address = "789 Timber Ln, Timbertown", StartDate = DateTime.Now.AddDays(15), EndDate = DateTime.Now.AddDays(25), CityId = Guid.Parse("26019535-ae83-4aa8-93cd-0b03a1b2df52"), ImageUrl = "/images/exhibitions/wonders.png", IsDeleted = false },
                new Exhibition { Id = Guid.Parse("5c10ff4f-57f6-472b-9c08-89c777208719"), ExhibitionName = "Forest Art", Address = "321 Grove Ave, Forestburg", StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), CityId = Guid.Parse("c30fe708-a15c-42e8-b088-23d6ce4619fd"), ImageUrl = "/images/exhibitions/art.png", IsDeleted = false },
                new Exhibition { Id = Guid.Parse("05cfa271-7171-4fc6-909d-7e98edd52eea"), ExhibitionName = "Oak Creations", Address = "654 Branch Blvd, Oakwood", StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), CityId = Guid.Parse("26019535-ae83-4aa8-93cd-0b03a1b2df52"), ImageUrl = "/images/exhibitions/creations.png", IsDeleted = false }
            );

            modelBuilder.Entity<Woodcarving>().HasData(
                new Woodcarving { Id = Guid.Parse("96224008-7927-4af8-a7f1-6260ff1f7b09"), Title = "Forest Spirit", Description = "A carving of a mystical forest spirit.", WoodcarverId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), WoodTypeId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/woodcarvings/spirit.png", IsAvailable = true, IsDeleted = false },
                new Woodcarving { Id = Guid.Parse("2a99a6a1-ab30-45f3-8d70-6d10fc679800"), Title = "Eagle Soar", Description = "An eagle in flight.", WoodcarverId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), WoodTypeId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/woodcarvings/eagle.png", IsAvailable = true, IsDeleted = false },
                new Woodcarving { Id = Guid.Parse("e3877536-f9b7-40a8-9157-8656a1c0f79b"), Title = "Bear Strength", Description = "A mighty bear.", WoodcarverId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), WoodTypeId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/woodcarvings/bear.png", IsAvailable = false, IsDeleted = false },
                new Woodcarving { Id = Guid.Parse("02420c28-4001-472d-b491-f674c60443b3"), Title = "Tree of Life", Description = "An intricate tree design.", WoodcarverId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), WoodTypeId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/woodcarvings/tree.png", IsAvailable = true, IsDeleted = false },
                new Woodcarving { Id = Guid.Parse("daef10a0-76da-46c1-8e2d-c0f4b328688d"), Title = "River Flow", Description = "Abstract carving of a flowing river.", WoodcarverId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), WoodTypeId = Guid.Parse("b2c8c39d-571a-4e0b-83d8-7f062083537e"), ImageUrl = "/images/woodcarvings/river.png", IsAvailable = true, IsDeleted = false }
            );
        }
    }
}
