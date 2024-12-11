using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Models
{
    public class Exhibition
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [StringLength(NameMaxLength)]
        public string ExhibitionName { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        [StringLength(AddressMaxLength)]
        public string Address { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CityId { get; set; }

        [Required]
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; } = null!;

        [Column(TypeName = "NVARCHAR(255)")]
        [StringLength(UrlMaxLength)]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<WoodcarvingExhibition> ExhibitionWoodcarvings { get; set; } = new HashSet<WoodcarvingExhibition>();
    }
}
