﻿using System;
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
        #region Конструктор и обновление объекта
        public Product_VM(Product p = null)
        {
            Product = p;
            Build();
        }

        public void Build()
        {
            context = new LopushokEntities();
            if (Product != null)
            {
                Product = context.Product.Include(x => x.ProductMaterial)
                                     .First(y => y.ID == Product.ID);
            }
            else
            {
                Product = context.Product.Add(new Product());
            }
            Materials = context.ProductMaterial.Local.ToBindingList();
        }
        #endregion

        #region Поля и свойства
        private LopushokEntities context;
        private Product _product;
        public Product Product { get { return _product; } set { _product = value; OnPropertyChanged(); } }
        public BindingList<ProductMaterial> Materials { get; set; }
        public List<ProductType> ProductTypes => context.ProductType.ToList();
        #endregion
      
        #region Методы CRUD
        public bool AddMaterial(Material material)
        {
            //try
            //{
            var productMaterial = new ProductMaterial
            {
                Material = material,
                Count = 0
            };
            Product.ProductMaterial.Add(productMaterial);
                context.Entry(productMaterial).State = EntityState.Added;
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public bool DeleteMaterial(ProductMaterial productMaterial)
        {
            try
            {
                Product.ProductMaterial.Remove(productMaterial);
                context.Entry(productMaterial).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }

        }

        public string Save()
        {
            try
            {
                string errors = Product.Validation();
                if (!string.IsNullOrEmpty(errors)) return errors;
                context.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
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

        public void SetImageFolder(string StringFolder)
        {
            Product.Image = StringFolder;
            OnPropertyChanged(nameof(Product));
        }
        #endregion

    }
}
