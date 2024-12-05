using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Data.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string CityName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; } = new HashSet<Exhibition>();
    }
}
