using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<SelectList> GetWoodcarverListAsync()
    {
        var woodcarvers = await dbContext.Woodcarvers.ToListAsync();
        return new SelectList(woodcarvers, nameof(Woodcarver.Id), nameof(Woodcarver.FirstName));
    }

    public async Task<SelectList> GetWoodTypeListAsync()
    {
        var woodTypes = await dbContext.WoodTypes.ToListAsync();
        return new SelectList(woodTypes, nameof(WoodType.Id), nameof(WoodType.WoodTypeName));
    }

    public async Task<bool> WoodcarverExistsAsync(Guid woodcarverId)
    {
        return await dbContext.Woodcarvers.AnyAsync(w => w.Id == woodcarverId);
    }

    public async Task<bool> WoodTypeExistsAsync(Guid woodTypeId)
    {
        return await dbContext.WoodTypes.AnyAsync(w => w.Id == woodTypeId);
    }
}
