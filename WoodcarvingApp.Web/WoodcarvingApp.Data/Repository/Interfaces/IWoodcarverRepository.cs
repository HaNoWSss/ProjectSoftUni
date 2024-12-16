using WoodcarvingApp.Data.Models;

namespace WoodcarvingApp.Data.Repository.Interfaces
{
    public interface IWoodcarverRepository : IRepository<Woodcarver, Guid>
    {
        Task<IEnumerable<City>> GetCityListAsync();

    }
}
