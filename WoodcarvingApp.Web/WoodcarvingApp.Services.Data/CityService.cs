using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.City;

namespace WoodcarvingApp.Services.Data
{
    public class CityService : BaseService, ICityService
    {
        private readonly IRepository<City, Guid> cityRepository;

        public CityService(IRepository<City, Guid> cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        public async Task<IEnumerable<CityIndexViewModel>> GetAllCitiesAsync()
        {
            return await cityRepository
                .GetAllAttached()
                .Where(c => !c.IsDeleted)
                .Select(c => new CityIndexViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();
        }
        public async Task CreateCityAsync(CityCreateViewModel inputModel)
        {
            var city = new City
            {
                CityName = inputModel.CityName,
                ImageUrl = inputModel.ImageUrl
            };

            await cityRepository.AddAsync(city);
            await cityRepository.SaveChangesAsync();
        }
        public async Task<CityDetailsViewModel?> GetCityDetailsAsync(Guid id)
        {
            var city = await cityRepository
                .GetAllAttached()
                .Include(c => c.Woodcarvers)
                .Include(c => c.Exhibitions)
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return null;
            }

            return new CityDetailsViewModel
            {
                Id = city.Id,
                CityName = city.CityName,
                ImageUrl = city.ImageUrl,
                Woodcarvers = city.Woodcarvers
                    .Where(w => !w.IsDeleted)
                    .Select(w => new CityWoodcarverViewModel
                    {
                        Id = w.Id,
                        FullName = $"{w.FirstName} {w.LastName}",
                        ImageUrl = w.ImageUrl
                    })
                    .ToList(),
                Exhibitions = city.Exhibitions
                    .Where(e => !e.IsDeleted)
                    .Select(e => new CityExhibitionViewModel
                    {
                        Id = e.Id,
                        Name = e.ExhibitionName,
                        ImageUrl = e.ImageUrl
                    })
                    .ToList()
            };
        }
        public async Task<CityEditViewModel?> GetCityEditModelAsync(Guid id)
        {
            var city = await cityRepository
                .GetAllAttached()
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return null;
            }

            return new CityEditViewModel
            {
                Id = city.Id,
                CityName = city.CityName,
                ImageUrl = city.ImageUrl
            };
        }
        public async Task<bool> EditCityAsync(CityEditViewModel inputModel)
        {
            var city = await cityRepository
                .GetAllAttached()
                .Where(c => c.Id == inputModel.Id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return false;
            }

            city.CityName = inputModel.CityName;
            city.ImageUrl = inputModel.ImageUrl;

            cityRepository.Update(city);
            await cityRepository.SaveChangesAsync();

            return true;
        }
        public async Task<CityDeleteViewModel?> GetCityDeleteModelAsync(Guid id)
        {
            return await cityRepository
                .GetAllAttached()
                .Where(c => c.Id == id && !c.IsDeleted)
                .Select(c => new CityDeleteViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    ImageUrl = c.ImageUrl
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteCityAsync(Guid id)
        {
            var city = await cityRepository
                .GetAllAttached()
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                return false;
            }

            city.IsDeleted = true;

            cityRepository.Update(city);
            await cityRepository.SaveChangesAsync();

            return true;
        }

    }
}
