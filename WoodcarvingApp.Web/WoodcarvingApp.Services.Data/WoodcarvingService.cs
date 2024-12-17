using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Woodcarving;

namespace WoodcarvingApp.Services.Data
{
	public class WoodcarvingService : BaseService, IWoodcarvingService
	{
		private readonly IWoodcarvingRepository woodcarvingRepository;

		public WoodcarvingService(IWoodcarvingRepository woodcarvingRepository)
		{
			this.woodcarvingRepository = woodcarvingRepository;
		}
		public async Task<WoodcarvingCreateViewModel?> GetWoodcarvingForCreateAsync()
		{
			IEnumerable<Woodcarver> woodcarvers = await woodcarvingRepository.GetWoodcarverListAsync();
			IEnumerable<WoodType> woodTypes = await woodcarvingRepository.GetWoodTypeListAsync();

			return new WoodcarvingCreateViewModel
			{
				Woodcarvers = woodcarvers.Select(wc => new WoodcarverViewModel()
				{
					Id = wc.Id,
					FullName = $"{wc.FirstName} {wc.LastName}"
				}),
				WoodTypes = woodTypes.Select(wt => new WoodTypeViewModel()
				{
					Id = wt.Id,
					WoodTypeName = wt.WoodTypeName
				})
			};
		}

		public async Task<bool> CreateWoodcarvingAsync(WoodcarvingCreateViewModel inputModel)
		{
			if (inputModel == null) return false;

			var woodcarvers = await woodcarvingRepository.GetWoodcarverListAsync();
			inputModel.Woodcarvers = woodcarvers.Select(w => new WoodcarverViewModel
			{
				Id = w.Id,
				FullName = $"{w.FirstName} {w.LastName}"
			});

			var woodTypes = await woodcarvingRepository.GetWoodTypeListAsync();
			inputModel.WoodTypes = woodTypes.Select(wt => new WoodTypeViewModel
			{
				Id = wt.Id,
				WoodTypeName = wt.WoodTypeName
			});

			if (string.IsNullOrWhiteSpace(inputModel.ImageUrl))
			{
				inputModel.ImageUrl = "/images/woodcarving-image-not-added.png";
			}

			var woodcarving = new Woodcarving
			{
				Id = inputModel.Id,
				Title = inputModel.Title,
				Description = inputModel.Description,
				WoodcarverId = inputModel.WoodcarverId ?? throw new InvalidOperationException("WoodcarverId is required."),
				WoodTypeId = inputModel.WoodTypeId ?? throw new InvalidOperationException("WoodTypeId is required."),
				ImageUrl = inputModel.ImageUrl,
				IsAvailable = inputModel.IsAvailable
			};

			await woodcarvingRepository.AddAsync(woodcarving);
			await woodcarvingRepository.SaveChangesAsync();

			return true;
		}

		public async Task<bool> EditWoodcarvingAsync(WoodcarvingEditViewModel inputModel)
		{
			if (inputModel == null) return false;

			var woodcarvers = await woodcarvingRepository.GetWoodcarverListAsync();
			inputModel.Woodcarvers = woodcarvers.Select(w => new WoodcarverViewModel
			{
				Id = w.Id,
				FullName = $"{w.FirstName} {w.LastName}"
			});

			var woodTypes = await woodcarvingRepository.GetWoodTypeListAsync();
			inputModel.WoodTypes = woodTypes.Select(wt => new WoodTypeViewModel
			{
				Id = wt.Id,
				WoodTypeName = wt.WoodTypeName
			});

			if (string.IsNullOrWhiteSpace(inputModel.ImageUrl))
			{
				inputModel.ImageUrl = "/images/woodcarving-image-not-added.png";
			}

			var woodcarving = await woodcarvingRepository
				.GetAllAttached()
				.FirstOrDefaultAsync(w => w.Id == inputModel.Id && !w.IsDeleted);

			if (woodcarving == null)
			{
				return false;
			}

			woodcarving.Title = inputModel.Title;
			woodcarving.Description = inputModel.Description;
			woodcarving.WoodcarverId = inputModel.WoodcarverId ?? throw new InvalidOperationException("WoodcarverId is required.");
			woodcarving.WoodTypeId = inputModel.WoodTypeId ?? throw new InvalidOperationException("WoodTypeId is required.");
			woodcarving.ImageUrl = inputModel.ImageUrl;
			woodcarving.IsAvailable = inputModel.IsAvailable;

			await woodcarvingRepository.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<WoodcarvingIndexViewModel>> GetAllIndexAsync()
		{
			return await woodcarvingRepository
				.GetAllAttached()
				.Where(w => !w.IsDeleted)
				.Select(w => new WoodcarvingIndexViewModel
				{
					Id = w.Id,
					Title = w.Title,
					Description = w.Description,
					ImageUrl = w.ImageUrl
				})
				.ToListAsync();
		}

		public async Task<WoodcarvingDetailsViewModel?> GetWoodcarvingDetailsByIdAsync(Guid id)
		{
			var woodcarving = await woodcarvingRepository
				.GetAllAttached()
				.Include(w => w.Woodcarver)
				.Include(w => w.WoodType)
				.Include(w => w.WoodcarvingExhibitions)
					.ThenInclude(we => we.Exhibition)
				.Where(w => !w.IsDeleted && w.Id == id)
				.FirstOrDefaultAsync();

			if (woodcarving == null)
			{
				return null;
			}

			var model = new WoodcarvingDetailsViewModel
			{
				Id = woodcarving.Id,
				Title = woodcarving.Title,
				Description = woodcarving.Description,
				WoodcarverName = $"{woodcarving.Woodcarver.FirstName} {woodcarving.Woodcarver.LastName}",
				WoodTypeName = woodcarving.WoodType.WoodTypeName,
				ImageUrl = woodcarving.ImageUrl,
				IsAvailable = woodcarving.IsAvailable,
				Exhibitions = woodcarving.WoodcarvingExhibitions
					.Where(we => !we.Exhibition.IsDeleted)
					.Select(we => new ExhibitionViewModel
					{
						Id = we.Exhibition.Id,
						ExhibitionName = we.Exhibition.ExhibitionName,
						StartDate = we.Exhibition.StartDate,
						EndDate = we.Exhibition.EndDate
					})
					.ToList()
			};

			return model;
		}

		public async Task<WoodcarvingDeleteViewModel?> GetWoodcarvingForDeleteByIdAsync(Guid id)
		{
			var woodcarving = await woodcarvingRepository
				.GetAllAttached()
				.Where(w => !w.IsDeleted && w.Id == id)
				.Select(w => new
				{
					w.Id,
					w.Title,
					w.ImageUrl
				})
				.FirstOrDefaultAsync();

			if (woodcarving == null)
			{
				return null;
			}

			return new WoodcarvingDeleteViewModel
			{
				Id = woodcarving.Id,
				Title = woodcarving.Title,
				ImageUrl = woodcarving.ImageUrl
			};
		}

		public async Task<WoodcarvingEditViewModel?> GetWoodcarvingForEditByIdAsync(Guid id)
		{
			var woodcarvers = await woodcarvingRepository.GetWoodcarverListAsync();
			var woodcarverViewModels = woodcarvers.Select(w => new WoodcarverViewModel
			{
				Id = w.Id,
				FullName = $"{w.FirstName} {w.LastName}"
			}).ToList();

			var woodTypes = await woodcarvingRepository.GetWoodTypeListAsync();
			var woodTypeViewModels = woodTypes.Select(wt => new WoodTypeViewModel
			{
				Id = wt.Id,
				WoodTypeName = wt.WoodTypeName
			}).ToList();

			var woodcarving = await woodcarvingRepository
				.GetAllAttached()
				.Where(w => w.Id == id)
				.FirstOrDefaultAsync();

			if (woodcarving == null)
			{
				return null;
			}

			var model = new WoodcarvingEditViewModel
			{
				Id = woodcarving.Id,
				Title = woodcarving.Title,
				Description = woodcarving.Description,
				WoodcarverId = woodcarving.WoodcarverId,
				Woodcarvers = woodcarverViewModels,
				WoodTypeId = woodcarving.WoodTypeId,
				WoodTypes = woodTypeViewModels,
				ImageUrl = woodcarving.ImageUrl,
				IsAvailable = woodcarving.IsAvailable
			};

			return model;
		}

		public async Task<bool> SoftDeleteWoodcarvingAsync(Guid id)
		{
			var woodcarving = await woodcarvingRepository
				.GetAllAttached()
				.FirstOrDefaultAsync(w => !w.IsDeleted && w.Id == id);

			if (woodcarving == null)
			{
				return false;
			}

			woodcarving.IsDeleted = true;

			woodcarvingRepository.Update(woodcarving);
			await woodcarvingRepository.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<string>> GetAllWoodcarversAsync()
		{
			var woodcarvers = woodcarvingRepository.GetWoodcarverListStringAsync();
			return woodcarvers.Distinct();
		}
		public async Task<IEnumerable<WoodcarvingIndexViewModel>> GetAllWoodcarvingsAsync(WoodcarvingSearchFilterIndexViewModel inputModel)
		{
			IQueryable<Woodcarving> allWoodcarvingsQuery = woodcarvingRepository
				.GetAllAttached();

			if (!String.IsNullOrWhiteSpace(inputModel.SearchQuery))
			{
				allWoodcarvingsQuery = allWoodcarvingsQuery
					.Where(m => m.Title.ToLower().Contains(inputModel.SearchQuery.ToLower()));
			}

			if (!String.IsNullOrWhiteSpace(inputModel.WoodcarverFilter))
			{
				allWoodcarvingsQuery = allWoodcarvingsQuery
					.Where(m => m.Woodcarver.FirstName.ToLower() == inputModel.WoodcarverFilter.ToLower()
					|| m.Woodcarver.LastName.ToLower() == inputModel.WoodcarverFilter.ToLower());
			}

			if (inputModel.CurrentPage.HasValue &&
				inputModel.EntitiesPerPage.HasValue)
			{
				allWoodcarvingsQuery = allWoodcarvingsQuery
					.Skip(inputModel.EntitiesPerPage.Value * (inputModel.CurrentPage.Value - 1))
					.Take(inputModel.EntitiesPerPage.Value);
			}
			return await allWoodcarvingsQuery
				.Where(w => !w.IsDeleted)
				.Select(w => new WoodcarvingIndexViewModel
				{
					Id = w.Id,
					Title = w.Title,
					Description = w.Description,
					ImageUrl = w.ImageUrl
				})
				.ToListAsync();
		}

		public async Task<int> GetWoodcarvingsCountByFilterAsync(WoodcarvingSearchFilterIndexViewModel inputModel)
		{
			WoodcarvingSearchFilterIndexViewModel inputModelCopy = new WoodcarvingSearchFilterIndexViewModel()
			{
				CurrentPage = null,
				EntitiesPerPage = null,
				SearchQuery = inputModel.SearchQuery,
				WoodcarverFilter = inputModel.WoodcarverFilter
			};

			int moviesCount = (await GetAllWoodcarvingsAsync(inputModelCopy))
				.Count();
			return moviesCount;
		}

	}
}
