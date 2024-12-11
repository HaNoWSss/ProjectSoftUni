using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Models
{
    public class WoodType
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [StringLength(NameMaxLength)]
        public string WoodTypeName { get; set; } = null!;


        [Column(TypeName = "NVARCHAR(1000)")]
        [StringLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        [StringLength(HardnessMaxLength)]
        public string Hardness { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        [StringLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Woodcarving> Woodcarvings { get; set; } = new HashSet<Woodcarving>();
    }
}
