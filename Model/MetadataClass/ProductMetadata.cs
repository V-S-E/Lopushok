using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;

namespace WpfApp1.Model.MetadataClass
{
    [MetadataType (typeof(Product))]
    public class ProductMetadata
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public decimal MinCostForAgent { get; set; }
    }
}
