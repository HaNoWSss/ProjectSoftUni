using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Data.Models
{
    public class Woodcarver
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string LastName { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^\+(\d{1,3})[-.\s]?(\d{1,4}[-.\s]?)*\d{4,15}$")]
        public string PhoneNumber { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Woodcarving> Woodcarvings { get; set; } = new HashSet<Woodcarving>();
    }
}
