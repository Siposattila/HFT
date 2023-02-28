using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HFT.Models
{
    [Table("owners")]
    public class Owner : Entity
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        public Owner()
        {
            this.Cars = new HashSet<Car>();
        }
    }
}
