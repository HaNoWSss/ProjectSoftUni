using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Exhibition;

namespace WoodcarvingApp.Services.Data
{
    public class ExhibitionService : BaseService, IExhibitionService
    {
        private readonly IExhibitionRepository _exhibitionRepository;

        public ExhibitionService(IExhibitionRepository exhibitionRepository)
        {
            _exhibitionRepository = exhibitionRepository;
        }

        public async Task<IEnumerable<ExhibitionIndexViewModel>> GetAllExhibitionsAsync()
        {
            return await _exhibitionRepository
                .GetAllAttached()
                .Where(e => !e.IsDeleted)
                .Select(e => new ExhibitionIndexViewModel
                {
                    Id = e.Id,
                    ExhibitionName = e.ExhibitionName,
                    CityName = e.City.CityName,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    ImageUrl = e.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<ExhibitionCreateEditViewModel> GetCreateExhibitionDataAsync()
        {
            var cities = await _exhibitionRepository.GetAvailableCitiesAsync();
            var woodcarvings = await _exhibitionRepository.GetAvailableWoodcarvingsAsync();

            return new ExhibitionCreateEditViewModel
            {
                Cities = cities.Select(c => new CityViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName
                }),
                Woodcarvings = woodcarvings.Select(w => new WoodcarvingCheckboxViewModel
                {
                    Id = w.Id,
                    Title = w.Title,
                    WoodcarverName = $"{w.Woodcarver.FirstName} {w.Woodcarver.LastName}"
                }).ToList()
            };
        }

        public async Task<bool> CreateExhibitionAsync(ExhibitionCreateEditViewModel model)
        {
            if (model == null || !model.Woodcarvings.Any()) return false;

            var exhibition = new Exhibition
            {
                ExhibitionName = model.ExhibitionName,
                Address = model.Address,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CityId = model.CityId,
                ImageUrl = model.ImageUrl
            };

            foreach (var woodcarvingCheckBox in model.Woodcarvings.Where(w => w.IsSelected))
            {
                exhibition.ExhibitionWoodcarvings.Add(new WoodcarvingExhibition
                {
                    WoodcarvingId = woodcarvingCheckBox.Id
                });
            }

            return await _exhibitionRepository.AddAsyncBool(exhibition);
        }
        public async Task<ExhibitionCreateEditViewModel> GetEditExhibitionDataAsync(Guid id)
        {
            var exhibition = await _exhibitionRepository
                .GetAllAttached()
                .Include(e => e.ExhibitionWoodcarvings)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (exhibition == null)
            {
                return null;
            }

            var cities = await _exhibitionRepository.GetAvailableCitiesAsync();
            var woodcarvings = await _exhibitionRepository.GetAvailableWoodcarvingsAsync();

            return new ExhibitionCreateEditViewModel
            {
                Id = exhibition.Id,
                ExhibitionName = exhibition.ExhibitionName,
                Address = exhibition.Address,
                StartDate = exhibition.StartDate,
                EndDate = exhibition.EndDate,
                CityId = exhibition.CityId,
                Cities = cities.Select(c => new CityViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName
                }),
                ImageUrl = exhibition.ImageUrl,
                Woodcarvings = woodcarvings.Select(w => new WoodcarvingCheckboxViewModel
                {
                    Id = w.Id,
                    Title = w.Title,
                    WoodcarverName = $"{w.Woodcarver.FirstName} {w.Woodcarver.LastName}",
                    IsSelected = exhibition.ExhibitionWoodcarvings.Any(ew => ew.WoodcarvingId == w.Id)
                }).ToList()
            };
        }

        public async Task<bool> EditExhibitionAsync(ExhibitionCreateEditViewModel model)
        {
            if (model == null || !model.Woodcarvings.Any()) return false;

            var exhibition = await _exhibitionRepository.GetAllAttached()
                .Include(e => e.ExhibitionWoodcarvings)
                .FirstOrDefaultAsync(e => e.Id == model.Id && !e.IsDeleted);

            if (exhibition == null)
            {
                return false;
            }

            exhibition.ExhibitionName = model.ExhibitionName;
            exhibition.Address = model.Address;
            exhibition.StartDate = model.StartDate;
            exhibition.EndDate = model.EndDate;
            exhibition.CityId = model.CityId;
            exhibition.ImageUrl = model.ImageUrl;

            exhibition.ExhibitionWoodcarvings.Clear();
            foreach (var woodcarvingCheckBox in model.Woodcarvings.Where(w => w.IsSelected))
            {
                exhibition.ExhibitionWoodcarvings.Add(new WoodcarvingExhibition
                {
                    WoodcarvingId = woodcarvingCheckBox.Id
                });
            }

            return await _exhibitionRepository.UpdateAsync(exhibition);
        }
        public async Task<ExhibitionDetailsViewModel> GetExhibitionDetailsAsync(Guid id)
        {
            var exhibition = await _exhibitionRepository
                .GetAllAttached()
                .Include(e => e.City)
                .Include(e => e.ExhibitionWoodcarvings)
                .ThenInclude(ew => ew.Woodcarving)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (exhibition == null)
            {
                return null;
            }

            return new ExhibitionDetailsViewModel
            {
                Id = exhibition.Id,
                ExhibitionName = exhibition.ExhibitionName,
                Address = exhibition.Address,
                StartDate = exhibition.StartDate,
                EndDate = exhibition.EndDate,
                CityName = exhibition.City.CityName,
                ImageUrl = exhibition.ImageUrl,
                Woodcarvings = exhibition.ExhibitionWoodcarvings.Select(w => new ExhibitionWoodcarvingViewModel
                {
                    Id = w.Woodcarving.Id,
                    Title = w.Woodcarving.Title,
                    ImageUrl = w.Woodcarving.ImageUrl
                }).ToList()
            };
        }
        public async Task<ExhibitionDeleteViewModel> GetExhibitionDeleteDataAsync(Guid id)
        {
            var exhibition = await _exhibitionRepository
                .GetAllAttached()
                .Include(e => e.City)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (exhibition == null)
            {
                return null;
            }

            return new ExhibitionDeleteViewModel
            {
                Id = exhibition.Id,
                ExhibitionName = exhibition.ExhibitionName,
                CityName = exhibition.City.CityName,
                StartDate = exhibition.StartDate,
                EndDate = exhibition.EndDate
            };
        }
        public async Task<bool> DeleteExhibitionAsync(Guid id)
        {
            var exhibition = await _exhibitionRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (exhibition == null)
            {
                return false;
            }

            exhibition.IsDeleted = true;
            return await _exhibitionRepository.UpdateAsync(exhibition);
        }

    }
}
