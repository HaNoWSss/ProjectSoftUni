using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Data.Models
{
    public class Exhibition
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string ExhibitionName { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Address { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CityId { get; set; }

        [Required]
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<WoodcarvingExhibition> ExhibitionWoodcarvings { get; set; } = new HashSet<WoodcarvingExhibition>();
    }
}
