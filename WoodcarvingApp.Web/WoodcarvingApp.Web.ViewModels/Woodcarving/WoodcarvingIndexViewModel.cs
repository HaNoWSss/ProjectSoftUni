namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class WoodcarvingIndexViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
