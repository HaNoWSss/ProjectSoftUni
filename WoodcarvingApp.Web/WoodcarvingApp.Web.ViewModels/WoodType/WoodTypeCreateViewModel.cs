using System.ComponentModel.DataAnnotations;

namespace WoodcarvingApp.Web.ViewModels.WoodType
{
    public class WoodTypeCreateViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The name must not exceed 20 characters.")]
        public string WoodTypeName { get; set; } = null!;

        [Required]
        [StringLength(200, ErrorMessage = "The description must not exceed 200 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(20, ErrorMessage = "The hardness must not exceed 20 characters.")]
        public string Hardness { get; set; } = null!;

        [Required]
        [StringLength(20, ErrorMessage = "The color must not exceed 20 characters.")]
        public string Color { get; set; } = null!;

        [Url(ErrorMessage = "The image URL must be a valid URL.")]
        public string? ImageUrl { get; set; }
    }
}
