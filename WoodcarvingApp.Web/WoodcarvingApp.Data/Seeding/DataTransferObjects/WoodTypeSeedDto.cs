using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Seeding.DataTransferObjects
{
    public class WoodTypeSeedDto
    {
        [Required]
        [MinLength(IdMinLength)]
        [MaxLength(IdMaxLength)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string WoodTypeName { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Hardness { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Color { get; set; } = null!;

        [Url]
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
