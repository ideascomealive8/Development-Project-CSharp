using System.Collections.Generic;

namespace Sparcpoint.Models
{
    public class FilterProduct
    {
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Attributes { get; set; } = new List<string>();
    }
}