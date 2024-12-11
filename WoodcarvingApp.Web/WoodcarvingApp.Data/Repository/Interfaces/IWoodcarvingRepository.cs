namespace WoodcarvingApp.Data.Repository.Interfaces
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Threading.Tasks;
    using WoodcarvingApp.Data.Models;

    public interface IWoodcarvingRepository : IRepository<Woodcarving, Guid>
    {
        Task<SelectList> GetWoodcarverListAsync();
        Task<SelectList> GetWoodTypeListAsync();
        Task<bool> WoodcarverExistsAsync(Guid woodcarverId);
        Task<bool> WoodTypeExistsAsync(Guid woodTypeId);

    }
}
