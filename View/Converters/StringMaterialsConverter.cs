using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfApp1.Model;

namespace WpfApp1.View.Converters
{
    public class StringMaterialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var materials = (HashSet<ProductMaterial>)value;
            return materials.Count>0 ? String.Join(", ", materials.Select(x=>x.Material.Title).ToArray()) : "-- не указано --";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
