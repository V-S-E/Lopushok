using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void InitialValidationList()
        {
            ValidList.Add(
            new ValidItem
            {
                _name = "Title",
                _message = "Заголовок не должен быть больше 50-ти символов и не должен содержать лишних символов.",
                _check = delegate
                {

                    if (Title is null || Title.Length > 50
                       || Title.Length < 5)
                        return true;
                    return false;
                }
            });
            ValidList.Add(
            new ValidItem
            {
                _name = "ArticleNumber",
                _message = "Артикул должен быть числом.",
                _check = delegate
                {
                    if (ArticleNumber is null || Regex.IsMatch(ArticleNumber, @"\D")) return true;
                    return false;
                }
            });
            ValidList.Add(
                new ValidItem
                {
                    _name = "MinCostForAgent",
                    _message = "Значение цены для агента должно быть больше нуля.",
                    _check = delegate
                    {
                        if (MinCostForAgent <= 0) return true;
                        return false;
                    }
                });
            ValidList.Add(
                new ValidItem
                {
                    _name = "ProductType",
                    _message = "Необходимо указать тип продукции.",
                    _check = delegate
                    {
                        if (ProductType is null) return true;
                        return false;
                    }
                });
            ValidList.Add(
                new ValidItem
                {
                    _name = "ProductionPersonCount",
                    _message = "Количество человек для производства должно быть больше нуля.",
                    _check = delegate
                    {
                        if (ProductionPersonCount is null || ProductionPersonCount <= 0) return true;
                        return false;
                    }
                });
        }
    }

    //public class ProductMetadata
    //{
    //    public int ID { get; set; }
    //    public string Title { get; set; }
    //    public decimal MinCostForAgent { get; set; }
    //    public string Image { get; set; }
    //    public string ArticleNumber { get; set; }
    //}
}
