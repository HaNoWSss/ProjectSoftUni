using WoodcarvingApp.Web.ViewModels.Exhibition;

namespace WoodcarvingApp.Services.Data.Interfaces
{
    public interface IExhibitionService
    {
        Task<IEnumerable<ExhibitionIndexViewModel>> GetAllExhibitionsAsync();
        Task<ExhibitionCreateEditViewModel> GetCreateExhibitionDataAsync();
        Task<bool> CreateExhibitionAsync(ExhibitionCreateEditViewModel model);
        Task<ExhibitionCreateEditViewModel> GetEditExhibitionDataAsync(Guid id);
        Task<bool> EditExhibitionAsync(ExhibitionCreateEditViewModel model);
        Task<ExhibitionDetailsViewModel> GetExhibitionDetailsAsync(Guid id);
        Task<ExhibitionDeleteViewModel> GetExhibitionDeleteDataAsync(Guid id);
        Task<bool> DeleteExhibitionAsync(Guid id);
    }
}
