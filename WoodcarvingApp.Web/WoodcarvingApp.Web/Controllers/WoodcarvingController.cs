using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Web.ViewModels.Woodcarving;

namespace WoodcarvingApp.Web.Controllers
{
	public class WoodcarvingController(IWoodcarvingService woodcarvingService) : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> Index(WoodcarvingSearchFilterIndexViewModel inputModel)
		{
			// Fetch filtered woodcarvings
			IEnumerable<WoodcarvingIndexViewModel> woodcarvings = await woodcarvingService.GetAllWoodcarvingsAsync(inputModel);

			// Count matching woodcarvings
			int allWoodcarvingsCount = await woodcarvingService.GetWoodcarvingsCountByFilterAsync(inputModel);

			// Prepare the view model
			WoodcarvingSearchFilterIndexViewModel viewModel = new WoodcarvingSearchFilterIndexViewModel
			{
				Woodcarvings = woodcarvings,
				SearchQuery = inputModel.SearchQuery,
				WoodcarverFilter = inputModel.WoodcarverFilter,
				AllWoodcarvers = await woodcarvingService.GetAllWoodcarversAsync(),
				CurrentPage = inputModel.CurrentPage ?? 1,
				EntitiesPerPage = inputModel.EntitiesPerPage ?? 10,
				TotalPages = (int)Math.Ceiling((double)allWoodcarvingsCount / (inputModel.EntitiesPerPage ?? 10))
			};

			return View(viewModel);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{

			WoodcarvingCreateViewModel model = await woodcarvingService.GetWoodcarvingForCreateAsync();

			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(WoodcarvingCreateViewModel inputModel)
		{
			if (!ModelState.IsValid)
			{
				var model = await woodcarvingService.GetWoodcarvingForCreateAsync();
				inputModel.Woodcarvers = model?.Woodcarvers;
				inputModel.WoodTypes = model?.WoodTypes;

				return View(inputModel);
			}

			bool isCreated = await woodcarvingService.CreateWoodcarvingAsync(inputModel);

			if (!isCreated)
			{
				ModelState.AddModelError("", "An error occurred while creating the woodcarving.");
				return View(inputModel);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var model = await woodcarvingService.GetWoodcarvingDetailsByIdAsync(id);

			if (model == null)
			{
				return NotFound();
			}

			return View(model);
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(Guid id)
		{
			var model = await woodcarvingService.GetWoodcarvingForEditByIdAsync(id);

			if (model == null)
			{
				return NotFound();
			}

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(WoodcarvingEditViewModel inputModel)
		{
			if (!ModelState.IsValid)
			{
				await woodcarvingService.EditWoodcarvingAsync(inputModel);
				return View(inputModel);
			}

			var success = await woodcarvingService.EditWoodcarvingAsync(inputModel);

			if (!success)
			{
				return NotFound();
			}

			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(Guid id)
		{
			var model = await woodcarvingService.GetWoodcarvingForDeleteByIdAsync(id);

			if (model == null)
			{
				return NotFound();
			}

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var success = await woodcarvingService.SoftDeleteWoodcarvingAsync(id);

			if (!success)
			{
				return NotFound();
			}

			return RedirectToAction(nameof(Index));
		}

	}
}
