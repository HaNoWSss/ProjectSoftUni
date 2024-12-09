using System.ComponentModel.DataAnnotations;

namespace WoodcarvingApp.Web.ViewModels.City
{
    public class CityEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "City name cannot exceed 20 characters.")]
        public string CityName { get; set; } = null!;

        [Url(ErrorMessage = "Please provide a valid image URL.")]
        public string? ImageUrl { get; set; }
    }
}
