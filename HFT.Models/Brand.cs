using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HFT.Models
{
    [Table("brands")]
    public class Brand : Entity
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        public Brand()
        {
            this.Cars = new HashSet<Car>();
        }
    }
}
