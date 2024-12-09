using Microsoft.AspNetCore.Mvc.Rendering;

namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class EditWoodcarvingViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public Guid WoodcarverId { get; set; }
        public SelectList WoodcarverList { get; set; } = null!;

        public Guid WoodTypeId { get; set; }
        public SelectList WoodTypeList { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }
    }
}
