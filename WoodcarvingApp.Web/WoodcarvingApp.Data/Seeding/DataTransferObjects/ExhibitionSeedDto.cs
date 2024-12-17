using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Seeding.DataTransferObjects
{
    public class ExhibitionSeedDto
    {
        [Required]
        [MinLength(IdMinLength)]
        [MaxLength(IdMaxLength)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ExhibitionName { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Address { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
