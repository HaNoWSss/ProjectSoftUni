using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Web.ViewModels.Woodcarver
{
    public class WoodcarverCreateViewModel
    {
        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string LastName { get; set; } = null!;

        [Required]
        public Guid CityId { get; set; }

        [BindNever]
        public SelectList CityList { get; set; } = null!;

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^\+(\d{1,3})[-.\s]?(\d{1,4}[-.\s]?)*\d{4,15}$")]
        public string PhoneNumber { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
