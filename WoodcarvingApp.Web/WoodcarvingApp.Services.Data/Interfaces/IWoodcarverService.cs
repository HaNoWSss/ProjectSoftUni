using WoodcarvingApp.Web.ViewModels.Woodcarver;

namespace WoodcarvingApp.Services.Data.Interfaces
{
    public interface IWoodcarverService
    {
        Task<IEnumerable<WoodcarverIndexViewModel>> GetAllWoodcarversAsync();
        Task<WoodcarverCreateViewModel> GetWoodcarverCreateModelAsync();
        Task<bool> CreateWoodcarverAsync(WoodcarverCreateViewModel inputModel);
        Task<WoodcarverDetailsViewModel> GetWoodcarverDetailsAsync(Guid id);
        Task<WoodcarverEditViewModel> GetWoodcarverForEditAsync(Guid id);
        Task<bool> UpdateWoodcarverAsync(WoodcarverEditViewModel inputModel);
        Task<WoodcarverDeleteViewModel?> GetWoodcarverForDeleteAsync(Guid id);
        Task<bool> DeleteWoodcarverAsync(Guid id);
    }
}
