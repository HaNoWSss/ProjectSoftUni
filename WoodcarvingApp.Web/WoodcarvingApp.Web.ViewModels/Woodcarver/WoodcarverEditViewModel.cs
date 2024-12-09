using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace WoodcarvingApp.Web.ViewModels.Woodcarver
{
    public class WoodcarverEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Range(1, 120)]
        public int Age { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Guid CityId { get; set; }

        public string? ImageUrl { get; set; }
        public SelectList CityList { get; set; } = null!;

    }

}
