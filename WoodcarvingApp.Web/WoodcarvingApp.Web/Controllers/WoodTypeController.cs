using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.WoodType;

namespace WoodcarvingApp.Web.Controllers
{
	public class WoodTypeController(WoodcarvingDbContext dbContext) : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var woodTypes = await dbContext.WoodTypes
				.Where(w => !w.IsDeleted)
				.Select(w => new WoodTypeIndexViewModel
				{
					Id = w.Id,
					WoodTypeName = w.WoodTypeName,
					ImageUrl = w.ImageUrl
				})
				.ToListAsync();

			return View(woodTypes);
		}

		[HttpGet]
		[Authorize]
		public IActionResult Create()
		{
			var model = new WoodTypeCreateViewModel();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create(WoodTypeCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var woodType = new WoodType
			{
				WoodTypeName = model.WoodTypeName,
				Description = model.Description,
				Hardness = model.Hardness,
				Color = model.Color,
				ImageUrl = model.ImageUrl
			};

			await dbContext.WoodTypes.AddAsync(woodType);
			await dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var woodType = await dbContext.WoodTypes
				.Where(w => !w.IsDeleted && w.Id == id)
				.FirstOrDefaultAsync();

			if (woodType == null)
			{
				return NotFound();
			}

			var model = new WoodTypeDetailsViewModel
			{
				Id = woodType.Id,
				WoodTypeName = woodType.WoodTypeName,
				Description = woodType.Description,
				Hardness = woodType.Hardness,
				Color = woodType.Color,
				ImageUrl = woodType.ImageUrl
			};

			return View(model);
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(Guid id)
		{
			var woodType = await dbContext.WoodTypes
				.Where(w => !w.IsDeleted && w.Id == id)
				.FirstOrDefaultAsync();

			if (woodType == null)
			{
				return NotFound();
			}

			var viewModel = new WoodTypeEditViewModel
			{
				Id = woodType.Id,
				WoodTypeName = woodType.WoodTypeName,
				Description = woodType.Description,
				Hardness = woodType.Hardness,
				Color = woodType.Color,
				ImageUrl = woodType.ImageUrl
			};

			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(WoodTypeEditViewModel inputModel)
		{
			if (!ModelState.IsValid)
			{
				return View(inputModel);
			}

			var woodType = await dbContext.WoodTypes
				.Where(w => !w.IsDeleted && w.Id == inputModel.Id)
				.FirstOrDefaultAsync();

			if (woodType == null)
			{
				return NotFound();
			}

			woodType.WoodTypeName = inputModel.WoodTypeName;
			woodType.Description = inputModel.Description;
			woodType.Hardness = inputModel.Hardness;
			woodType.Color = inputModel.Color;
			woodType.ImageUrl = inputModel.ImageUrl;

			dbContext.WoodTypes.Update(woodType);
			await dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(Guid id)
		{
			var woodType = await dbContext.WoodTypes
				.Where(w => !w.IsDeleted && w.Id == id)
				.Select(w => new WoodTypeDeleteViewModel
				{
					Id = w.Id,
					WoodTypeName = w.WoodTypeName,
					ImageUrl = w.ImageUrl
				})
				.FirstOrDefaultAsync();

			if (woodType == null)
			{
				return NotFound();
			}

			return View(woodType);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Delete(WoodTypeDeleteViewModel inputModel)
		{
			var woodType = await dbContext.WoodTypes
				.Where(w => !w.IsDeleted && w.Id == inputModel.Id)
				.FirstOrDefaultAsync();

			if (woodType == null)
			{
				return NotFound();
			}

			woodType.IsDeleted = true;
			dbContext.WoodTypes.Update(woodType);
			await dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

	}
}
