using System.ComponentModel.DataAnnotations;

namespace WoodcarvingApp.Web.ViewModels.WoodType
{
    public class WoodTypeEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Wood type name cannot exceed 20 characters.")]
        public string WoodTypeName { get; set; } = null!;

        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(20, ErrorMessage = "Hardness cannot exceed 20 characters.")]
        public string Hardness { get; set; } = null!;

        [Required]
        [StringLength(20, ErrorMessage = "Color cannot exceed 20 characters.")]
        public string Color { get; set; } = null!;

        [Url(ErrorMessage = "Please provide a valid image URL.")]
        public string? ImageUrl { get; set; }
    }
}
