using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Woodcarver;

namespace WoodcarvingApp.Web.Controllers
{
	public class WoodcarverController(WoodcarvingDbContext dbContext) : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<Woodcarver> allWoodcarvers = await dbContext
				.Woodcarvers
				.Where(w => !w.IsDeleted)
				.ToListAsync();

			return View(allWoodcarvers);
		}

		[HttpGet]
		[Authorize]
		public IActionResult Create()
		{
			var cities = dbContext.Cities;

			var model = new WoodcarverCreateViewModel()
			{
				CityList = new SelectList(cities, nameof(City.Id), nameof(City.CityName))
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create(WoodcarverCreateViewModel inputModel)
		{
			if (ModelState.ContainsKey(nameof(WoodcarverCreateViewModel.CityList)))
			{
				ModelState.Remove(nameof(WoodcarverCreateViewModel.CityList));
			}
			inputModel.CityList = new SelectList(await dbContext.Cities.ToListAsync(), nameof(City.Id), nameof(City.CityName));

			if (!ModelState.IsValid)
			{
				return View(inputModel);
			}

			if (!dbContext.Cities.Any(c => c.Id == inputModel.CityId))
			{
				ModelState.AddModelError(nameof(inputModel.CityId), "Invalid city selected.");
				return View(inputModel);
			}

			var woodcarver = new Woodcarver
			{
				FirstName = inputModel.FirstName,
				LastName = inputModel.LastName,
				CityId = inputModel.CityId,
				Age = inputModel.Age,
				PhoneNumber = inputModel.PhoneNumber,
				ImageUrl = inputModel.ImageUrl,
				IsDeleted = false
			};

			await dbContext.Woodcarvers.AddAsync(woodcarver);
			await dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var woodcarver = await dbContext.Woodcarvers
				.Include(w => w.City)
				.FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted);

			if (woodcarver == null)
			{
				return NotFound();
			}

			var model = new WoodcarverDetailsViewModel
			{
				Id = woodcarver.Id,
				FullName = $"{woodcarver.FirstName} {woodcarver.LastName}",
				ImageUrl = woodcarver.ImageUrl,
				CityId = woodcarver.CityId,
				CityName = woodcarver.City.CityName,
				Age = woodcarver.Age,
				PhoneNumber = woodcarver.PhoneNumber
			};

			return View(model);
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(Guid id)
		{
			var woodcarver = await dbContext.Woodcarvers
				.Where(w => !w.IsDeleted && w.Id == id)
				.Select(w => new WoodcarverEditViewModel
				{
					Id = w.Id,
					FirstName = w.FirstName,
					LastName = w.LastName,
					Age = w.Age,
					PhoneNumber = w.PhoneNumber,
					CityId = w.CityId,
					ImageUrl = w.ImageUrl,
					CityList = new SelectList(dbContext.Cities.Where(c => !c.IsDeleted).ToList(), nameof(City.Id), nameof(City.CityName))
				})
				.FirstOrDefaultAsync();

			if (woodcarver == null)
			{
				return NotFound();
			}

			return View(woodcarver);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(WoodcarverEditViewModel inputModel)
		{
			if (ModelState.ContainsKey(nameof(WoodcarverEditViewModel.CityList)))
			{
				ModelState.Remove(nameof(WoodcarverEditViewModel.CityList));
			}
			inputModel.CityList = new SelectList(await dbContext.Cities.ToListAsync(), nameof(City.Id), nameof(City.CityName));

			if (!ModelState.IsValid)
			{
				return View(inputModel);
			}

			var woodcarver = await dbContext.Woodcarvers
				.Where(w => !w.IsDeleted && w.Id == inputModel.Id)
				.FirstOrDefaultAsync();

			if (woodcarver == null)
			{
				return NotFound();
			}

			woodcarver.FirstName = inputModel.FirstName;
			woodcarver.LastName = inputModel.LastName;
			woodcarver.Age = inputModel.Age;
			woodcarver.PhoneNumber = inputModel.PhoneNumber;
			woodcarver.CityId = inputModel.CityId;
			woodcarver.ImageUrl = inputModel.ImageUrl;

			dbContext.Woodcarvers.Update(woodcarver);
			await dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Delete(Guid id)
		{
			var woodcarver = await dbContext.Woodcarvers
				.Where(w => !w.IsDeleted && w.Id == id)
				.Select(w => new WoodcarverDeleteViewModel
				{
					Id = w.Id,
					FullName = $"{w.FirstName} {w.LastName}",
					ImageUrl = w.ImageUrl
				})
				.FirstOrDefaultAsync();

			if (woodcarver == null)
			{
				return NotFound();
			}

			return View(woodcarver);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Delete(WoodcarverDeleteViewModel inputModel)
		{
			var woodcarver = await dbContext.Woodcarvers
				.Where(w => !w.IsDeleted && w.Id == inputModel.Id)
				.FirstOrDefaultAsync();

			if (woodcarver == null)
			{
				return NotFound();
			}

			woodcarver.IsDeleted = true;
			dbContext.Woodcarvers.Update(woodcarver);
			await dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

	}
}
