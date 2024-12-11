//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using WoodcarvingApp.Data.Models;
//using WoodcarvingApp.Data.Repository.Interfaces;
//using WoodcarvingApp.Services.Data.Interfaces;
//using WoodcarvingApp.Services.Mapping;
//using WoodcarvingApp.Web.ViewModels.Woodcarving;

//namespace WoodcarvingApp.Services.Data
//{
//    public class WoodcarvingService(IWoodcarvingRepository woodcarvingRepository) : BaseService, IWoodcarvingService
//    {
//        public async Task AddWoodcarvingAsync(WoodcarvingCreateViewModel model)
//        {
//            if (model == null)
//            {
//                throw new ArgumentNullException(nameof(model), "Model cannot be null");
//            }

//            var woodcarving = new Woodcarving();
//            AutoMapperConfig.MapperInstance.Map(model, woodcarving);

//            await woodcarvingRepository.AddAsync(woodcarving);
//        }
//        public async Task<(bool isValid, SelectList woodcarvers, SelectList woodTypes)> PrepareCreateViewModelAsync(WoodcarvingCreateViewModel inputModel)
//        {
//            var woodcarverList = await woodcarvingRepository.GetWoodcarverListAsync();
//            var woodTypeList = await woodcarvingRepository.GetWoodTypeListAsync();

//            //inputModel.WoodcarverList = woodcarverList;
//            //inputModel.WoodTypeList = woodTypeList;

//            if (!await woodcarvingRepository.WoodcarverExistsAsync(inputModel.WoodcarverId))
//            {
//                return (false, woodcarverList, woodTypeList);
//            }

//            if (!await woodcarvingRepository.WoodTypeExistsAsync(inputModel.WoodTypeId))
//            {
//                return (false, woodcarverList, woodTypeList);
//            }

//            if (inputModel == null)
//            {
//                throw new ArgumentNullException(nameof(inputModel), "Model cannot be null");
//            }

//            var woodcarving = new Woodcarving();
//            AutoMapperConfig.MapperInstance.Map(inputModel, woodcarving);

//            await woodcarvingRepository.AddAsync(woodcarving);

//            return (true, woodcarverList, woodTypeList);
//        }

//        public async Task<bool> EditWoodcarvingAsync(WoodcarvingEditViewModel model)
//        {
//            throw new NotImplementedException();

//        }

//        public async Task<WoodcarvingDetailsViewModel?> GetWoodcarvingDetailsByIdAsync(Guid id)
//        {
//            var woodcarving = await woodcarvingRepository
//                .GetAllAttached()
//                .Include(w => w.Woodcarver)
//                .Include(w => w.WoodType)
//                .Include(w => w.WoodcarvingExhibitions)
//                    .ThenInclude(we => we.Exhibition)
//                .Where(w => w.Id == id && !w.IsDeleted)
//                .FirstOrDefaultAsync();

//            if (woodcarving == null)
//            {
//                return null;
//            }

//            return new WoodcarvingDetailsViewModel
//            {
//                Id = woodcarving.Id,
//                Title = woodcarving.Title,
//                Description = woodcarving.Description,
//                WoodcarverName = $"{woodcarving.Woodcarver.FirstName} {woodcarving.Woodcarver.LastName}",
//                WoodTypeName = woodcarving.WoodType.WoodTypeName,
//                ImageUrl = woodcarving.ImageUrl,
//                IsAvailable = woodcarving.IsAvailable,
//                Exhibitions = woodcarving.WoodcarvingExhibitions
//                    .Where(we => !we.Exhibition.IsDeleted)
//                    .Select(we => new ExhibitionViewModel
//                    {
//                        Id = we.Exhibition.Id,
//                        ExhibitionName = we.Exhibition.ExhibitionName,
//                        StartDate = we.Exhibition.StartDate,
//                        EndDate = we.Exhibition.EndDate
//                    })
//                    .ToList()
//            };
//        }

//        public async Task<WoodcarvingEditViewModel?> GetWoodcarvingForEditByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();

//        }

//        public async Task<WoodcarvingDeleteViewModel?> GetWoodcarvingForDeleteByIdAsync(Guid id)
//        {
//            var woodcarving = await woodcarvingRepository
//                .GetAllAttached()
//                .Where(w => w.Id == id && !w.IsDeleted)
//                .FirstOrDefaultAsync();

//            if (woodcarving == null)
//            {
//                return null;
//            }

//            return new WoodcarvingDeleteViewModel
//            {
//                Id = woodcarving.Id,
//                Title = woodcarving.Title,
//                ImageUrl = woodcarving.ImageUrl
//            };
//        }

//        public async Task<bool> SoftDeleteCinemaAsync(Guid id)
//        {
//            var woodcarving = await woodcarvingRepository.GetByIdAsync(id);

//            if (woodcarving == null || woodcarving.IsDeleted)
//            {
//                return false;
//            }

//            woodcarving.IsDeleted = true;

//            woodcarvingRepository.Update(woodcarving);
//            await woodcarvingRepository.SaveChangesAsync();
//            return true;
//        }

//        public async Task<IEnumerable<Woodcarving>> GetAllIndexAsync()
//        {
//            IEnumerable<Woodcarving> allWoodcarvings = await woodcarvingRepository
//                .GetAllAttached()
//                .Where(w => !w.IsDeleted)
//                .ToListAsync();

//            return allWoodcarvings;
//        }

//        public async Task<(IEnumerable<SelectListItem> woodcarvers, IEnumerable<SelectListItem> woodTypes)> GetDropdownListsAsync()
//        {

//            throw new NotImplementedException();
//        }

//        public Task<WoodcarvingEditViewModel?> GetWoodcarvingForCreateAsync()
//        {
//            throw new NotImplementedException();
//        }
//    }

//}
