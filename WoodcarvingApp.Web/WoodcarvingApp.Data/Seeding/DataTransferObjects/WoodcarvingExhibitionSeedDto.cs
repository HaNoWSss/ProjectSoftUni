using System.ComponentModel.DataAnnotations;

namespace WoodcarvingApp.Data.Seeding.DataTransferObjects
{
    public class WoodcarvingExhibitionSeedDto
    {
        [Required]
        public Guid WoodcarvingId { get; set; }

        [Required]
        public Guid ExhibitionId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
