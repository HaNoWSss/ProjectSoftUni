namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class WoodcarvingDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string WoodcarverName { get; set; } = null!;

        public string WoodTypeName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }

        public List<ExhibitionViewModel> Exhibitions { get; set; } = new();
    }

    public class ExhibitionViewModel
    {
        public Guid Id { get; set; }

        public string ExhibitionName { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
