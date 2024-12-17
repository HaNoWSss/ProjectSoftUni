namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class WoodcarvingSearchFilterIndexViewModel
    {
        public IEnumerable<WoodcarvingIndexViewModel>? Woodcarvings { get; set; }

        public string? SearchQuery { get; set; }

        public string? WoodcarverFilter { get; set; }

        public IEnumerable<string>? AllWoodcarvers { get; set; }

        public int? CurrentPage { get; set; } = 1;

        public int? EntitiesPerPage { get; set; } = 5;

        public int? TotalPages { get; set; }
    }
}
