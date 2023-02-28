using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HFT.Models
{
    [Table("cars")]
    public class Car : Entity
    {
        [MaxLength(100)]
        [MinLength(3)]
        [Required]
        public string Model { get; set; }

        public int? BasePrice { get; set; }

        [NotMapped]
        public virtual Brand Brand { get; set; }

        [NotMapped]
        public virtual Owner Owner { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
    }
}
