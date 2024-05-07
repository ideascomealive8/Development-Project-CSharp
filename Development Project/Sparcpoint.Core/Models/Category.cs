using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sparcpoint.Models
{
    public class Category: IHasTimestamp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        
        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}