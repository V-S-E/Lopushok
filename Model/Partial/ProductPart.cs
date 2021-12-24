using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;
using WpfApp1.Properties;

namespace WpfApp1.Model
{
    public partial class Product : ValidatorBase
    {
        public string FullImagePath
        { get { return string.IsNullOrEmpty(Image) ? null : String.Concat(Environment.CurrentDirectory, Image); } }

        public decimal? Cost => ProductMaterial.Sum(x => x.Material.Cost * (decimal)x.Count);
    }

    public class ProductMetadata
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:N2}")]
        public decimal MinCostForAgent { get; set; }
        public string Image { get; set; }
        [Required]
        public string ArticleNumber { get; set; }
    }
}
