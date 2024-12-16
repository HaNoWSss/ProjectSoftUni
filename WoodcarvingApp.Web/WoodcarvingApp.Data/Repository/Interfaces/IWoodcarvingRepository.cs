namespace WoodcarvingApp.Data.Repository.Interfaces
{
    using System.Threading.Tasks;
    using WoodcarvingApp.Data.Models;

    public interface IWoodcarvingRepository : IRepository<Woodcarving, Guid>
    {
        Task<IEnumerable<Woodcarver>> GetWoodcarverListAsync();
        Task<IEnumerable<WoodType>> GetWoodTypeListAsync();

    }
}
