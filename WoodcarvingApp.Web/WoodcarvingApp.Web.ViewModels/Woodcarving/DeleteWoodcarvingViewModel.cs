namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class DeleteWoodcarvingViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
