using Microsoft.AspNetCore.Mvc.Rendering;

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

        public string? ImageUrl { get; set; }

        public IEnumerable<Guid>? SelectedWoodcarvings { get; set; }

        public SelectList? Cities { get; set; }

        public SelectList? Woodcarvings { get; set; }
    }
}
