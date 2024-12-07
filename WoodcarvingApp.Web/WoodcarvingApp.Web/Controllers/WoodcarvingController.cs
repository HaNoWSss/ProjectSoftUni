﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.ViewModels.Woodcarving;

namespace WoodcarvingApp.Web.Controllers
{
    public class WoodcarvingController(WoodcarvingDbContext dbContext) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Woodcarving> allWoodcarving = await dbContext
                .Woodcarvings
                .ToListAsync();

            return View(allWoodcarving);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var woodcarvers = dbContext.Woodcarvers;
            var woodTypes = dbContext.WoodTypes;

            var model = new WoodcarvingCreateViewModel()
            {
                WoodcarverList = new SelectList(woodcarvers, nameof(Woodcarver.Id), nameof(Woodcarver.FirstName)),
                WoodTypeList = new SelectList(woodTypes, nameof(WoodType.Id), nameof(WoodType.WoodTypeName))
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WoodcarvingCreateViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));
                return View(inputModel);
            }

            if (!dbContext.Woodcarvers.Any(w => w.Id == inputModel.WoodcarverId))
            {
                ModelState.AddModelError(nameof(inputModel.WoodcarverId), "Invalid woodcarver selected.");
                inputModel.WoodcarverList = new SelectList(await dbContext.Woodcarvers.ToListAsync(), nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));
                return View(inputModel);
            }

            if (!dbContext.WoodTypes.Any(w => w.Id == inputModel.WoodTypeId))
            {
                ModelState.AddModelError(nameof(inputModel.WoodTypeId), "Invalid wood type selected.");
                inputModel.WoodTypeList = new SelectList(await dbContext.WoodTypes.ToListAsync(), nameof(WoodType.Id), nameof(WoodType.WoodTypeName));
                return View(inputModel);
            }

            var woodcarving = new Woodcarving
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                ImageUrl = inputModel.ImageUrl,
                IsAvailable = inputModel.IsAvailable,
                WoodcarverId = inputModel.WoodcarverId,
                WoodTypeId = inputModel.WoodTypeId,
                IsDeleted = false
            };

            await dbContext.Woodcarvings.AddAsync(woodcarving);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
