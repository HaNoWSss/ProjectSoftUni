using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Seeding.DataTransferObjects
{
    public class CitySeedDto
    {
        [Required]
        [MinLength(IdMinLength)]
        [MaxLength(IdMaxLength)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CityName { get; set; } = null!;

        [Url]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}
