using Microsoft.AspNetCore.Mvc.Rendering;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.ViewModels.Woodcarving;

namespace WoodcarvingApp.Services.Data.Interfaces
{
    public interface IWoodcarvingService
    {
        Task<IEnumerable<Woodcarving>> GetAllIndexAsync();
        Task AddWoodcarvingAsync(WoodcarvingCreateViewModel model);
        Task<WoodcarvingDetailsViewModel?> GetWoodcarvingDetailsByIdAsync(Guid id);
        Task<WoodcarvingEditViewModel?> GetWoodcarvingForEditByIdAsync(Guid id);
        Task<WoodcarvingEditViewModel?> GetWoodcarvingForCreateAsync();
        Task<bool> EditWoodcarvingAsync(WoodcarvingEditViewModel model);
        Task<WoodcarvingDeleteViewModel?> GetWoodcarvingForDeleteByIdAsync(Guid id);
        Task<bool> SoftDeleteCinemaAsync(Guid id);
        Task<(IEnumerable<SelectListItem> woodcarvers, IEnumerable<SelectListItem> woodTypes)> GetDropdownListsAsync();

        Task<(bool isValid, SelectList woodcarvers, SelectList woodTypes)> PrepareCreateViewModelAsync(WoodcarvingCreateViewModel inputModel);

    }
}
