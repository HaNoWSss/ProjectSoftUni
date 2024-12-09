using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Exhibition;

public class ExhibitionsController(WoodcarvingDbContext dbContext) : Controller
{

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var exhibitions = await dbContext.Exhibitions
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

		return View(exhibitions);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Create()
	{
		var model = new ExhibitionCreateEditViewModel
		{
			Cities = new SelectList(await dbContext.Cities.Where(c => !c.IsDeleted).ToListAsync(), nameof(City.Id), nameof(City.CityName)),
			Woodcarvings = new SelectList(await dbContext.Woodcarvings.Where(w => !w.IsDeleted).ToListAsync(), nameof(Woodcarving.Id), nameof(Woodcarving.Title))
		};

		return View("CreateEdit", model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> Create(ExhibitionCreateEditViewModel inputModel)
	{
		if (!ModelState.IsValid)
		{
			inputModel.Cities = new SelectList(await dbContext.Cities.Where(c => !c.IsDeleted).ToListAsync(), nameof(City.Id), nameof(City.CityName));
			inputModel.Woodcarvings = new SelectList(await dbContext.Woodcarvings.Where(w => !w.IsDeleted).ToListAsync(), nameof(Woodcarving.Id), nameof(Woodcarving.Title));
			return View("CreateEdit", inputModel);
		}

		var exhibition = new Exhibition
		{
			ExhibitionName = inputModel.ExhibitionName,
			Address = inputModel.Address,
			StartDate = inputModel.StartDate,
			EndDate = inputModel.EndDate,
			CityId = inputModel.CityId,
			ImageUrl = inputModel.ImageUrl
		};

		dbContext.Exhibitions.Add(exhibition);

		if (inputModel.SelectedWoodcarvings != null)
		{
			foreach (var woodcarvingId in inputModel.SelectedWoodcarvings)
			{
				dbContext.WoodcarvingExhibitions.Add(new WoodcarvingExhibition
				{
					ExhibitionId = exhibition.Id,
					WoodcarvingId = woodcarvingId
				});
			}
		}

		await dbContext.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Edit(Guid id)
	{
		var exhibition = await dbContext.Exhibitions
			.Include(e => e.ExhibitionWoodcarvings)
			.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

		if (exhibition == null)
		{
			return NotFound();
		}

		var model = new ExhibitionCreateEditViewModel
		{
			Id = exhibition.Id,
			ExhibitionName = exhibition.ExhibitionName,
			Address = exhibition.Address,
			StartDate = exhibition.StartDate,
			EndDate = exhibition.EndDate,
			CityId = exhibition.CityId,
			ImageUrl = exhibition.ImageUrl,
			SelectedWoodcarvings = exhibition.ExhibitionWoodcarvings.Select(w => w.WoodcarvingId).ToList(),
			Cities = new SelectList(await dbContext.Cities.Where(c => !c.IsDeleted).ToListAsync(), nameof(City.Id), nameof(City.CityName)),
			Woodcarvings = new SelectList(await dbContext.Woodcarvings.Where(w => !w.IsDeleted).ToListAsync(), nameof(Woodcarving.Id), nameof(Woodcarving.Title))
		};

		return View("CreateEdit", model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> Edit(ExhibitionCreateEditViewModel inputModel)
	{
		if (!ModelState.IsValid)
		{
			inputModel.Cities = new SelectList(await dbContext.Cities.Where(c => !c.IsDeleted).ToListAsync(), nameof(City.Id), nameof(City.CityName));
			inputModel.Woodcarvings = new SelectList(await dbContext.Woodcarvings.Where(w => !w.IsDeleted).ToListAsync(), nameof(Woodcarving.Id), nameof(Woodcarving.Title));
			return View("CreateEdit", inputModel);
		}

		var exhibition = await dbContext.Exhibitions
			.Include(e => e.ExhibitionWoodcarvings)
			.FirstOrDefaultAsync(e => e.Id == inputModel.Id && !e.IsDeleted);

		if (exhibition == null)
		{
			return NotFound();
		}

		exhibition.ExhibitionName = inputModel.ExhibitionName;
		exhibition.Address = inputModel.Address;
		exhibition.StartDate = inputModel.StartDate;
		exhibition.EndDate = inputModel.EndDate;
		exhibition.CityId = inputModel.CityId;
		exhibition.ImageUrl = inputModel.ImageUrl;

		var existingWoodcarvings = exhibition.ExhibitionWoodcarvings.ToList();
		dbContext.WoodcarvingExhibitions.RemoveRange(existingWoodcarvings);
		if (inputModel.SelectedWoodcarvings != null)
		{
			foreach (var woodcarvingId in inputModel.SelectedWoodcarvings)
			{
				dbContext.WoodcarvingExhibitions.Add(new WoodcarvingExhibition
				{
					ExhibitionId = exhibition.Id,
					WoodcarvingId = woodcarvingId
				});
			}
		}

		await dbContext.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}


	public async Task<IActionResult> Details(Guid id)
	{
		var exhibition = await dbContext.Exhibitions
			.Include(e => e.City)
			.Include(e => e.ExhibitionWoodcarvings)
			.ThenInclude(ew => ew.Woodcarving)
			.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

		if (exhibition == null)
		{
			return NotFound();
		}

		var model = new ExhibitionDetailsViewModel
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

		return View(model);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Delete(Guid id)
	{
		var exhibition = await dbContext.Exhibitions
			.Include(e => e.City)
			.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

		if (exhibition == null)
		{
			return NotFound();
		}

		var model = new ExhibitionDeleteViewModel
		{
			Id = exhibition.Id,
			ExhibitionName = exhibition.ExhibitionName,
			CityName = exhibition.City.CityName,
			StartDate = exhibition.StartDate,
			EndDate = exhibition.EndDate
		};

		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var exhibition = await dbContext.Exhibitions
			.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

		if (exhibition == null)
		{
			return NotFound();
		}

		exhibition.IsDeleted = true;
		await dbContext.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
}
