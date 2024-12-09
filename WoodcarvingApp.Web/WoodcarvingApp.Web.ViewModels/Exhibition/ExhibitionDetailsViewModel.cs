namespace WoodcarvingApp.Web.ViewModels.Exhibition
{
    public class ExhibitionDetailsViewModel
    {
        public Guid Id { get; set; }

        public string ExhibitionName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string CityName { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? ImageUrl { get; set; }

        public List<ExhibitionWoodcarvingViewModel> Woodcarvings { get; set; } = new();
    }

    public class ExhibitionWoodcarvingViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
