using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Web.ViewModels.Woodcarving
{
    public class WoodcarvingEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter the title of the woodcarving")]
        [StringLength(TitleMaxLength, ErrorMessage = $"Title cannot exceed 50 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(DescriptionMaxLength, ErrorMessage = $"Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a woodcarver.")]
        public Guid? WoodcarverId { get; set; }

        public virtual IEnumerable<WoodcarverViewModel>? Woodcarvers { get; set; }

        [Required(ErrorMessage = "Please select a wood type.")]
        public Guid? WoodTypeId { get; set; }

        public virtual IEnumerable<WoodTypeViewModel>? WoodTypes { get; set; }

        [StringLength(UrlMaxLength, ErrorMessage = $"Url cannot exceed 255 characters.")]
        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
