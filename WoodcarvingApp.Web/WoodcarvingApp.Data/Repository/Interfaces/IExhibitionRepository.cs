using WoodcarvingApp.Data.Models;

namespace WoodcarvingApp.Data.Repository.Interfaces
{
    public interface IExhibitionRepository : IRepository<Exhibition, Guid>
    {
        Task<IEnumerable<City>> GetAvailableCitiesAsync();
        Task<IEnumerable<Woodcarving>> GetAvailableWoodcarvingsAsync();
    }
}
