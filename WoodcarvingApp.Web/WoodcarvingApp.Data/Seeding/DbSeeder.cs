//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;
//using System.Text;
//using WoodcarvingApp.Data.Models;
//using WoodcarvingApp.Data.Seeding.DataTransferObjects;
//using WoodcarvingApp.Web.Data;

//namespace WoodcarvingApp.Data.Seeding
//{
//    public static class DbSeeder
//    {
//        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
//        {
//            using var dbContext = serviceProvider.GetRequiredService<WoodcarvingDbContext>();

//            // Call individual seed methods here
//            await SeedCitiesAsync(dbContext, "Data/cities.json");
//            await SeedWoodcarversAsync(dbContext, "Data/woodcarvers.json");
//            await SeedWoodTypesAsync(dbContext, "Data/woodtypes.json");
//            await SeedWoodcarvingsAsync(dbContext, "Data/woodcarvings.json");
//            await SeedExhibitionsAsync(dbContext, "Data/exhibitions.json");
//            await SeedWoodcarvingExhibitionsAsync(dbContext, "Data/woodcarvingsexhibitions.json");
//        }

//        private static async Task SeedCitiesAsync(WoodcarvingDbContext dbContext, string filePath)
//        {
//            if (await dbContext.Cities.AnyAsync()) return;

//            var cities = await ReadAndValidateDataAsync<CitySeedDto>(filePath);
//            foreach (var dto in cities)
//            {
//                var city = new City
//                {
//                    Id = dto.Id,
//                    CityName = dto.CityName,
//                    ImageUrl = dto.ImageUrl,
//                    IsDeleted = dto.IsDeleted
//                };

//                await dbContext.Cities.AddAsync(city);
//            }

//            await dbContext.SaveChangesAsync();
//            Console.WriteLine("Cities seeded successfully!");
//        }

//        private static async Task SeedWoodcarversAsync(WoodcarvingDbContext dbContext, string filePath)
//        {
//            if (await dbContext.Woodcarvers.AnyAsync()) return;

//            var woodcarvers = await ReadAndValidateDataAsync<WoodcarverSeedDto>(filePath);
//            foreach (var dto in woodcarvers)
//            {
//                var woodcarver = new Woodcarver
//                {
//                    Id = dto.Id,
//                    FirstName = dto.FirstName,
//                    LastName = dto.LastName,
//                    Age = dto.Age,
//                    PhoneNumber = dto.PhoneNumber,
//                    CityId = dto.CityId,
//                    ImageUrl = dto.ImageUrl,
//                    IsDeleted = dto.IsDeleted
//                };

//                await dbContext.Woodcarvers.AddAsync(woodcarver);
//            }

//            await dbContext.SaveChangesAsync();
//            Console.WriteLine("Woodcarvers seeded successfully!");
//        }

//        private static async Task SeedWoodTypesAsync(WoodcarvingDbContext dbContext, string filePath)
//        {
//            if (await dbContext.WoodTypes.AnyAsync()) return;

//            var woodTypes = await ReadAndValidateDataAsync<WoodTypeSeedDto>(filePath);
//            foreach (var dto in woodTypes)
//            {
//                var woodType = new WoodType
//                {
//                    Id = dto.Id,
//                    WoodTypeName = dto.WoodTypeName,
//                    Description = dto.Description,
//                    Hardness = dto.Hardness,
//                    Color = dto.Color,
//                    ImageUrl = dto.ImageUrl,
//                    IsDeleted = dto.IsDeleted
//                };

//                await dbContext.WoodTypes.AddAsync(woodType);
//            }

//            await dbContext.SaveChangesAsync();
//            Console.WriteLine("Wood types seeded successfully!");
//        }

//        private static async Task SeedWoodcarvingsAsync(WoodcarvingDbContext dbContext, string filePath)
//        {
//            if (await dbContext.Woodcarvings.AnyAsync()) return;

//            var woodcarvings = await ReadAndValidateDataAsync<WoodcarvingSeedDto>(filePath);
//            foreach (var dto in woodcarvings)
//            {
//                var woodcarving = new Woodcarving
//                {
//                    Id = dto.Id,
//                    Title = dto.Title,
//                    Description = dto.Description,
//                    WoodcarverId = dto.WoodcarverId,
//                    WoodTypeId = dto.WoodTypeId,
//                    ImageUrl = dto.ImageUrl,
//                    IsAvailable = dto.IsAvailable,
//                    IsDeleted = dto.IsDeleted
//                };

//                await dbContext.Woodcarvings.AddAsync(woodcarving);
//            }

//            await dbContext.SaveChangesAsync();
//            Console.WriteLine("Woodcarvings seeded successfully!");
//        }

//        private static async Task SeedExhibitionsAsync(WoodcarvingDbContext dbContext, string filePath)
//        {
//            if (await dbContext.Exhibitions.AnyAsync()) return;

//            var exhibitions = await ReadAndValidateDataAsync<ExhibitionSeedDto>(filePath);
//            foreach (var dto in exhibitions)
//            {
//                var exhibition = new Exhibition
//                {
//                    Id = dto.Id,
//                    ExhibitionName = dto.ExhibitionName,
//                    Address = dto.Address,
//                    StartDate = dto.StartDate,
//                    EndDate = dto.EndDate,
//                    CityId = dto.CityId,
//                    ImageUrl = dto.ImageUrl,
//                    IsDeleted = dto.IsDeleted
//                };

//                await dbContext.Exhibitions.AddAsync(exhibition);
//            }

//            await dbContext.SaveChangesAsync();
//            Console.WriteLine("Exhibitions seeded successfully!");
//        }

//        private static async Task SeedWoodcarvingExhibitionsAsync(WoodcarvingDbContext dbContext, string filePath)
//        {
//            if (await dbContext.WoodcarvingExhibitions.AnyAsync()) return;

//            var woodcarvingExhibitions = await ReadAndValidateDataAsync<WoodcarvingExhibitionSeedDto>(filePath);
//            foreach (var dto in woodcarvingExhibitions)
//            {
//                var woodcarvingExhibition = new WoodcarvingExhibition
//                {
//                    WoodcarvingId = dto.WoodcarvingId,
//                    ExhibitionId = dto.ExhibitionId,
//                    IsDeleted = dto.IsDeleted
//                };

//                await dbContext.WoodcarvingExhibitions.AddAsync(woodcarvingExhibition);
//            }

//            await dbContext.SaveChangesAsync();
//            Console.WriteLine("WoodcarvingExhibitions seeded successfully!");
//        }

//        private static async Task<List<T>> ReadAndValidateDataAsync<T>(string filePath)
//        {
//            var validData = new List<T>();

//            if (!File.Exists(filePath))
//            {
//                Console.WriteLine($"File not found: {filePath}");
//                return validData;
//            }

//            try
//            {
//                var jsonData = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
//                var dtos = JsonConvert.DeserializeObject<List<T>>(jsonData);

//                foreach (var dto in dtos)
//                {
//                    if (IsValid(dto))
//                    {
//                        validData.Add(dto);
//                    }
//                    else
//                    {
//                        Console.WriteLine($"Invalid data skipped: {JsonConvert.SerializeObject(dto)}");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
//            }

//            return validData;
//        }

//        private static bool IsValid(object obj)
//        {
//            var validationResults = new List<ValidationResult>();
//            var context = new ValidationContext(obj);
//            return Validator.TryValidateObject(obj, context, validationResults, true);
//        }
//    }
//}
