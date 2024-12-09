namespace WoodcarvingApp.Web.ViewModels.WoodType
{
    public class WoodTypeDetailsViewModel
    {
        public Guid Id { get; set; }

        public string WoodTypeName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Hardness { get; set; } = null!;

        public string Color { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
