using WoodcarvingApp.Web.ViewModels.City;

namespace WoodcarvingApp.Services.Data.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityIndexViewModel>> GetAllCitiesAsync();
        Task CreateCityAsync(CityCreateViewModel inputModel);
        Task<CityDetailsViewModel?> GetCityDetailsAsync(Guid id);
        Task<CityEditViewModel?> GetCityEditModelAsync(Guid id);
        Task<bool> EditCityAsync(CityEditViewModel inputModel);
        Task<CityDeleteViewModel?> GetCityDeleteModelAsync(Guid id);
        Task<bool> DeleteCityAsync(Guid id);
    }
}
