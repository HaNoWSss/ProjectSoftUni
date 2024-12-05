using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodcarvingApp.Data.Models
{
    [PrimaryKey(nameof(WoodcarvingId), nameof(ExhibitionId))]
    public class WoodcarvingExhibition
    {
        public Guid WoodcarvingId { get; set; }

        [ForeignKey(nameof(WoodcarvingId))]
        public virtual Woodcarving Woodcarving { get; set; } = null!;

        public Guid ExhibitionId { get; set; }

        [ForeignKey(nameof(ExhibitionId))]
        public virtual Exhibition Exhibition { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
