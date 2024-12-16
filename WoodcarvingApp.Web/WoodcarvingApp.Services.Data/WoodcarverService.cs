using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Woodcarver;

namespace WoodcarvingApp.Services.Data
{
    public class WoodcarverService : BaseService, IWoodcarverService
    {
        private readonly IWoodcarverRepository woodcarverRepository;

        public WoodcarverService(IWoodcarverRepository woodcarverRepository)
        {
            this.woodcarverRepository = woodcarverRepository;
        }
        public async Task<IEnumerable<WoodcarverIndexViewModel>> GetAllWoodcarversAsync()
        {
            return await woodcarverRepository
                .GetAllAttached() // Assuming this provides IQueryable<Woodcarver>
                .Where(w => !w.IsDeleted)
                .Select(w => new WoodcarverIndexViewModel
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    ImageUrl = w.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<WoodcarverCreateViewModel> GetWoodcarverCreateModelAsync()
        {
            var cities = await woodcarverRepository.GetCityListAsync();

            return new WoodcarverCreateViewModel
            {
                Cities = cities.Select(c => new CityViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName
                })
                .ToList()
            };
        }

        public async Task<bool> CreateWoodcarverAsync(WoodcarverCreateViewModel inputModel)
        {
            if (inputModel == null || !ValidateWoodcarverCreateInput(inputModel))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(inputModel.ImageUrl))
            {
                inputModel.ImageUrl = "/images/woodcarver-image-not-added.png";
            }

            var woodcarver = new Woodcarver
            {
                Id = Guid.NewGuid(),
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Age = inputModel.Age ?? throw new InvalidOperationException("Age is required."),
                PhoneNumber = inputModel.PhoneNumber ?? throw new InvalidOperationException("Phone Number is required."),
                CityId = inputModel.CityId ?? throw new InvalidOperationException("CityId is required."),
                ImageUrl = inputModel.ImageUrl
            };

            await woodcarverRepository.AddAsync(woodcarver);
            await woodcarverRepository.SaveChangesAsync();

            return true;
        }

        public async Task<WoodcarverDetailsViewModel> GetWoodcarverDetailsAsync(Guid id)
        {
            var woodcarver = await woodcarverRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Include(w => w.City)
                .FirstOrDefaultAsync();

            if (woodcarver == null) return null;

            return new WoodcarverDetailsViewModel
            {
                Id = woodcarver.Id,
                FullName = $"{woodcarver.FirstName} {woodcarver.LastName}",
                ImageUrl = woodcarver.ImageUrl,
                CityId = woodcarver.CityId,
                CityName = woodcarver.City.CityName,
                Age = woodcarver.Age,
                PhoneNumber = woodcarver.PhoneNumber
            };
        }
        public async Task<WoodcarverEditViewModel> GetWoodcarverForEditAsync(Guid id)
        {
            var woodcarver = await woodcarverRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Select(w => new WoodcarverEditViewModel
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    Age = w.Age,
                    PhoneNumber = w.PhoneNumber,
                    CityId = w.CityId,
                    ImageUrl = w.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (woodcarver == null) return null;

            var cities = await woodcarverRepository.GetCityListAsync();

            woodcarver.Cities = cities
                .Select(c => new CityViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName
                })
                .ToList();

            return woodcarver;
        }
        public async Task<bool> UpdateWoodcarverAsync(WoodcarverEditViewModel inputModel)
        {
            if (inputModel == null || !ValidateWoodcarverEditInput(inputModel))
            {
                return false;
            }

            var woodcarver = await woodcarverRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == inputModel.Id)
                .FirstOrDefaultAsync();

            if (woodcarver == null)
            {
                return false;
            }

            woodcarver.FirstName = inputModel.FirstName;
            woodcarver.LastName = inputModel.LastName;
            woodcarver.Age = inputModel.Age ?? throw new InvalidOperationException("Age is required.");
            woodcarver.PhoneNumber = inputModel.PhoneNumber ?? throw new InvalidOperationException("Phone Number is required.");
            woodcarver.CityId = inputModel.CityId ?? throw new InvalidOperationException("CityId is required.");
            woodcarver.ImageUrl = string.IsNullOrWhiteSpace(inputModel.ImageUrl)
                ? "/images/woodcarver-image-not-added.png"
                : inputModel.ImageUrl;

            woodcarverRepository.Update(woodcarver);
            await woodcarverRepository.SaveChangesAsync();

            return true;
        }
        public async Task<WoodcarverDeleteViewModel?> GetWoodcarverForDeleteAsync(Guid id)
        {
            var woodcarver = await woodcarverRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Select(w => new WoodcarverDeleteViewModel
                {
                    Id = w.Id,
                    FullName = $"{w.FirstName} {w.LastName}",
                    ImageUrl = w.ImageUrl
                })
                .FirstOrDefaultAsync();

            return woodcarver;
        }
        public async Task<bool> DeleteWoodcarverAsync(Guid id)
        {
            var woodcarver = await woodcarverRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .FirstOrDefaultAsync();

            if (woodcarver == null)
            {
                return false;
            }

            woodcarver.IsDeleted = true;

            woodcarverRepository.Update(woodcarver);
            await woodcarverRepository.SaveChangesAsync();

            return true;
        }



        private bool ValidateWoodcarverEditInput(WoodcarverEditViewModel inputModel)
        {
            return !string.IsNullOrWhiteSpace(inputModel.FirstName) &&
                   !string.IsNullOrWhiteSpace(inputModel.LastName) &&
                   inputModel.Age.HasValue &&
                   !string.IsNullOrWhiteSpace(inputModel.PhoneNumber) &&
                   inputModel.CityId.HasValue;
        }
        private bool ValidateWoodcarverCreateInput(WoodcarverCreateViewModel inputModel)
        {
            return !string.IsNullOrWhiteSpace(inputModel.FirstName) &&
                   !string.IsNullOrWhiteSpace(inputModel.LastName) &&
                   inputModel.Age.HasValue &&
                   !string.IsNullOrWhiteSpace(inputModel.PhoneNumber) &&
                   inputModel.CityId.HasValue;
        }

    }
}
