using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [StringLength(NameMaxLength)]
        public string CityName { get; set; } = null!;

        [Column(TypeName = "NVARCHAR(255)")]
        [StringLength(UrlMaxLength)]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; } = new HashSet<Exhibition>();
        public virtual ICollection<Woodcarver> Woodcarvers { get; set; } = new HashSet<Woodcarver>();
    }
}
