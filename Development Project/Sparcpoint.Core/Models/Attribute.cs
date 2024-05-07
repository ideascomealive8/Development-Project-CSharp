using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sparcpoint.Models
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string Description { get; set; }
       
        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}