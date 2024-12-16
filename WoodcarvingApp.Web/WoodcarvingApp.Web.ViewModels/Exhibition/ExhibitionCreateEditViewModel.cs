namespace WoodcarvingApp.Web.ViewModels.Exhibition
{
    public class ExhibitionCreateEditViewModel
    {
        public Guid? Id { get; set; }

        public string ExhibitionName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CityId { get; set; }
        public virtual IEnumerable<CityViewModel>? Cities { get; set; }
        public string? ImageUrl { get; set; }

        public List<WoodcarvingCheckboxViewModel> Woodcarvings { get; set; }
    }
}
