using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Web.ViewModels.City
{
    public class CityCreateViewModel
    {
        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string CityName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
