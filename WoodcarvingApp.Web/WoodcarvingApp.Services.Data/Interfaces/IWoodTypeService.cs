using WoodcarvingApp.Web.ViewModels.WoodType;

namespace WoodcarvingApp.Services.Data.Interfaces
{
    public interface IWoodTypeService
    {
        Task<IEnumerable<WoodTypeIndexViewModel>> GetAllWoodTypesAsync();
        Task<bool> CreateWoodTypeAsync(WoodTypeCreateViewModel model);
        Task<WoodTypeDetailsViewModel?> GetWoodTypeDetailsAsync(Guid id);
        Task<WoodTypeEditViewModel?> GetWoodTypeForEditAsync(Guid id);
        Task<bool> UpdateWoodTypeAsync(WoodTypeEditViewModel model);
        Task<WoodTypeDeleteViewModel?> GetWoodTypeForDeleteAsync(Guid id);
        Task<bool> DeleteWoodTypeAsync(Guid id);
    }
}
