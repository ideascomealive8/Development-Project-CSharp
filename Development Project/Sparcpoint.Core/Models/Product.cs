using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sparcpoint.Models
{
    public class Product: IHasTimestamp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Description { get; set; }
        //ignoring for brevity for now
        /*public string ProductImageUris { get; set; }
        public string ValidSkus { get; set; }*/
        public DateTime CreatedTimestamp { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
    }
}