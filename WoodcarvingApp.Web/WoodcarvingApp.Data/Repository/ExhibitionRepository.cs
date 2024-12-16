using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Web.Data;

namespace WoodcarvingApp.Data.Repository
{
    public class ExhibitionRepository : BaseRepository<Exhibition, Guid>, IExhibitionRepository
    {
        private readonly WoodcarvingDbContext _dbContext;

        public ExhibitionRepository(WoodcarvingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<City>> GetAvailableCitiesAsync()
        {
            return await _dbContext.Cities
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Woodcarving>> GetAvailableWoodcarvingsAsync()
        {
            return await _dbContext.Woodcarvings
                .Include(w => w.Woodcarver)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
        }
    }
}
