namespace WoodcarvingApp.Web.ViewModels.Exhibition
{
    public class ExhibitionIndexViewModel
    {
        public Guid Id { get; set; }
        public string ExhibitionName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
