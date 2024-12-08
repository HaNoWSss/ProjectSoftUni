namespace WoodcarvingApp.Web.ViewModels.Woodcarver
{
    public class WoodcarverDetailsViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public Guid CityId { get; set; }

        public string CityName { get; set; } = null!;

        public int Age { get; set; }

        public string PhoneNumber { get; set; } = null!;
    }
}
