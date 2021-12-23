using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;
using WpfApp1.Model;
using System.Windows;

namespace WpfApp1.ViewModel
{
    public class Product_VM : Notify
    {
        public Product_VM(Product p = null)
        {
            context = new LopushokEntities();
            if (p != null)
            {
                Product = context.Product.Include(x => x.ProductMaterial)
                                     .First(y => y.ID == p.ID);
            }
            else
            {
                Product = context.Product.Add(new Product());
            }
            Materials = context.ProductMaterial.Local.ToBindingList();
        }

        #region Поля и свойства
        private LopushokEntities context;
        private Product _product;
        public Product Product { get { return _product; } set { _product = value; OnPropertyChanged(); } }

        public BindingList<ProductMaterial> Materials { get; set; }
        public List<ProductType> ProductTypes => context.ProductType.ToList();
        #endregion

        #region Методы
        public bool AddMaterial(Material material)
        {
            try
            {
                var productMaterial = new ProductMaterial();
                productMaterial.Material = material;
                productMaterial.Count = 0;
                Product.ProductMaterial.Add(productMaterial);
                context.Entry(productMaterial).State = EntityState.Added;
                OnPropertyChanged(nameof(Product));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMaterial(ProductMaterial productMaterial)
        {
            try
            {
                context.ProductMaterial.Remove(productMaterial);
                context.Entry(productMaterial).State = EntityState.Deleted;
                OnPropertyChanged(nameof(Product));
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                context.Product.Remove(Product);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
