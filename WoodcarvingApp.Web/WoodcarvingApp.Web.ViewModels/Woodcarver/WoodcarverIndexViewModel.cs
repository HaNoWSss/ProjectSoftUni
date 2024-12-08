namespace WoodcarvingApp.Web.ViewModels.Woodcarver
{
    public class WoodcarverIndexViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
