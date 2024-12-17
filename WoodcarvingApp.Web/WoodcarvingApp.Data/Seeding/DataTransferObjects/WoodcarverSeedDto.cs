using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Seeding.DataTransferObjects
{
    public class WoodcarverSeedDto
    {
        [Required]
        [MinLength(IdMinLength)]
        [MaxLength(IdMaxLength)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required]
        public Guid CityId { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^\+?[0-9]{10,15}$")] // Adjust based on ApplicationConstants.PhoneNumberRegex
        public string PhoneNumber { get; set; } = null!;

        [Url]
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
