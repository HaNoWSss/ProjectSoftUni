using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Data.Models
{
    public class Woodcarver
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [StringLength(NameMaxLength)]

        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [StringLength(NameMaxLength)]
        public string LastName { get; set; } = null!;

        public Guid CityId { get; set; }

        [Required]
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; } = null!;

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        [Column(TypeName = "VARCHAR(255)")]
        [StringLength(UrlMaxLength)]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Woodcarving> Woodcarvings { get; set; } = new HashSet<Woodcarving>();
    }
}
