using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class WoodcarvingCreateViewModel()
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please enter the title of the woodcarving")]
        [StringLength(40, ErrorMessage = "Title cannot exceed 40 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a woodcarver.")]
        public Guid WoodcarverId { get; set; }

        public SelectList WoodcarverList { get; set; } = null!;

        [Required(ErrorMessage = "Please select a wood type.")]
        public Guid WoodTypeId { get; set; }

        public SelectList WoodTypeList { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
