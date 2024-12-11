using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Models
{
    public class Woodcarving
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength)]
        [Column(TypeName = "NVARCHAR(1000)")]
        public string? Description { get; set; }

        public Guid WoodcarverId { get; set; }

        [Required]
        [ForeignKey(nameof(WoodcarverId))]
        public virtual Woodcarver Woodcarver { get; set; } = null!;

        public Guid WoodTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(WoodTypeId))]
        public virtual WoodType WoodType { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<WoodcarvingExhibition> WoodcarvingExhibitions { get; set; } = new HashSet<WoodcarvingExhibition>();
    }
}
