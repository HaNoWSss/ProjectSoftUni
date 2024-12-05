using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Data.Models
{
    public class WoodType
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string WoodTypeName { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string Hardness { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string Color { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Woodcarving> Woodcarvings { get; set; } = new HashSet<Woodcarving>();
    }
}
