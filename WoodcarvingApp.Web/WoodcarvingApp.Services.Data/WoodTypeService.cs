using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.WoodType;

namespace WoodcarvingApp.Services.Data
{
    public class WoodTypeService : BaseService, IWoodTypeService
    {
        private readonly IRepository<WoodType, Guid> woodTypeRepository;

        public WoodTypeService(IRepository<WoodType, Guid> woodTypeRepository)
        {
            this.woodTypeRepository = woodTypeRepository;
        }
        public async Task<IEnumerable<WoodTypeIndexViewModel>> GetAllWoodTypesAsync()
        {
            return await woodTypeRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted)
                .Select(w => new WoodTypeIndexViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName,
                    ImageUrl = w.ImageUrl
                })
                .ToListAsync();
        }
        public async Task<bool> CreateWoodTypeAsync(WoodTypeCreateViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var woodType = new WoodType
            {
                WoodTypeName = model.WoodTypeName,
                Description = model.Description,
                Hardness = model.Hardness,
                Color = model.Color,
                ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
                    ? "/images/woodtype-default.png"
                    : model.ImageUrl
            };

            await woodTypeRepository.AddAsync(woodType);
            await woodTypeRepository.SaveChangesAsync();

            return true;
        }
        public async Task<WoodTypeDetailsViewModel?> GetWoodTypeDetailsAsync(Guid id)
        {
            return await woodTypeRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Select(w => new WoodTypeDetailsViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName,
                    Description = w.Description,
                    Hardness = w.Hardness,
                    Color = w.Color,
                    ImageUrl = w.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<WoodTypeEditViewModel?> GetWoodTypeForEditAsync(Guid id)
        {
            return await woodTypeRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Select(w => new WoodTypeEditViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName,
                    Description = w.Description,
                    Hardness = w.Hardness,
                    Color = w.Color,
                    ImageUrl = w.ImageUrl
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateWoodTypeAsync(WoodTypeEditViewModel model)
        {
            var woodType = await woodTypeRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == model.Id)
                .FirstOrDefaultAsync();

            if (woodType == null)
            {
                return false;
            }

            woodType.WoodTypeName = model.WoodTypeName;
            woodType.Description = model.Description;
            woodType.Hardness = model.Hardness;
            woodType.Color = model.Color;
            woodType.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
                ? "/images/woodtype-default.png"
                : model.ImageUrl;

            woodTypeRepository.Update(woodType);
            await woodTypeRepository.SaveChangesAsync();

            return true;
        }
        public async Task<WoodTypeDeleteViewModel?> GetWoodTypeForDeleteAsync(Guid id)
        {
            return await woodTypeRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Select(w => new WoodTypeDeleteViewModel
                {
                    Id = w.Id,
                    WoodTypeName = w.WoodTypeName,
                    ImageUrl = w.ImageUrl
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteWoodTypeAsync(Guid id)
        {
            var woodType = await woodTypeRepository
                .GetAllAttached()
                .Where(w => !w.IsDeleted && w.Id == id)
                .FirstOrDefaultAsync();

            if (woodType == null)
            {
                return false;
            }

            woodType.IsDeleted = true;
            woodTypeRepository.Update(woodType);
            await woodTypeRepository.SaveChangesAsync();

            return true;
        }

    }
}
