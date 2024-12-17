using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Seeding.DataTransferObjects
{
    public class WoodcarvingSeedDto
    {
        [Required]
        [MinLength(IdMinLength)]
        [MaxLength(IdMaxLength)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public Guid WoodcarverId { get; set; }

        [Required]
        public Guid WoodTypeId { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
    }
}
