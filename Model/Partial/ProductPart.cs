using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;
using WpfApp1.Properties;

namespace WpfApp1.Model
{
    public partial class Product : IDataErrorInfo
    {
        public string FullImagePath
        { get { return string.IsNullOrEmpty(Image) ? null : String.Concat(Environment.CurrentDirectory, Image); } }

        public decimal? Cost { get => ProductMaterial.Sum(x => x.Material.Cost * (decimal)x.Count); set { OnPropertyChanged(); } }

        // ВАЛИДАЦИЯ
        public string this[string columnName] 
        {
            get
            {
                foreach (var item in _validList)
                {
                    if (item.CheckDelegate()) return item.error_message;
                }
                return string.Empty;
            }
        }
        public string Error => throw new NotImplementedException();
        private List<ValidClass> _validList;
        // Необходимо выполнить в конструкторе ->
        private void initValidation()
        {
            _validList = new List<ValidClass>();
            _validList.Add(new ValidClass { propertyName = nameof(Title), error_message="ашипка!!!", CheckDelegate = () => { return ProductionPersonCount is int?; } });
        }
    }

     class ValidClass
    {
        public delegate bool _chk();
        public _chk CheckDelegate;
        public string propertyName;
        public string error_message;

    }

}
