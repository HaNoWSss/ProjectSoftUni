using WoodcarvingApp.Web.ViewModels.Woodcarving;

namespace WoodcarvingApp.Services.Data.Interfaces
{
    public interface IWoodcarvingService
    {
        Task<IEnumerable<WoodcarvingIndexViewModel>> GetAllIndexAsync();
        Task<WoodcarvingCreateViewModel?> GetWoodcarvingForCreateAsync();
        Task<bool> CreateWoodcarvingAsync(WoodcarvingCreateViewModel inputModel);
        Task<WoodcarvingDetailsViewModel?> GetWoodcarvingDetailsByIdAsync(Guid id);
        Task<WoodcarvingEditViewModel?> GetWoodcarvingForEditByIdAsync(Guid id);
        Task<bool> EditWoodcarvingAsync(WoodcarvingEditViewModel model);
        Task<WoodcarvingDeleteViewModel?> GetWoodcarvingForDeleteByIdAsync(Guid id);
        Task<bool> SoftDeleteWoodcarvingAsync(Guid id);

    }
}
