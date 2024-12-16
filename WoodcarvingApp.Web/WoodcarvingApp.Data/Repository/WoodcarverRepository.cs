using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Web.Data;

namespace WoodcarvingApp.Data.Repository
{
    public class WoodcarverRepository : BaseRepository<Woodcarver, Guid>, IWoodcarverRepository
    {
        private readonly WoodcarvingDbContext dbContext;

        public WoodcarverRepository(WoodcarvingDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<City>> GetCityListAsync()
        {
            return await dbContext.Cities.ToListAsync();
        }

    }
}
