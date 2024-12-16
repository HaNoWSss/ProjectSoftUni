using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Web.Data;

public class WoodcarvingRepository : BaseRepository<Woodcarving, Guid>, IWoodcarvingRepository
{
    private readonly WoodcarvingDbContext dbContext;

    public WoodcarvingRepository(WoodcarvingDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<IEnumerable<Woodcarver>> GetWoodcarverListAsync()
    {
        var woodcarvers = await dbContext.Woodcarvers
            .Where(w => !w.IsDeleted)
            .ToListAsync();

        return woodcarvers;
    }

    public async Task<IEnumerable<WoodType>> GetWoodTypeListAsync()
    {
        var woodTypes = await dbContext.WoodTypes
            .Where(w => !w.IsDeleted)
            .ToListAsync();

        return woodTypes;
    }
}
