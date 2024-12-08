namespace WoodcarvingApp.Web.ViewModels.City
{
    public class CityDetailsViewModel
    {
        public Guid Id { get; set; }

        public string CityName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public List<CityWoodcarverViewModel> Woodcarvers { get; set; } = new();

        public List<CityExhibitionViewModel> Exhibitions { get; set; } = new();
    }

    public class CityWoodcarverViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }

    public class CityExhibitionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
