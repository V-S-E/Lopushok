using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Interfaces;
using WpfApp1.Model;
using WpfApp1.ViewModel;

namespace WpfApp1.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        private Products_VM vm;
        public ProductList(Products_VM vm)
        {
            this.vm = vm;
            InitializeComponent();
            this.DataContext = vm;
        }

        private void EditPrice(object sender, RoutedEventArgs e)
        {
            ModalWindows.PriceChange w = new ModalWindows.PriceChange();
            w.ShowDialog();
            if (w.IsUpdate == true && w.Price != 0)
            {
                var l = LV.SelectedItems.Cast<Product>().Select(x=>x.ID).ToArray();
                var cnt = vm.Context.Product.Where(x=>l.Any(n=>n==x.ID)).ToList();
                cnt.ForEach(x=>x.MinCostForAgent = w.Price);
                vm.Context.SaveChanges();
                vm.OnPropertyChanged("ProductList");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigateClass.Go(new CRUDProduct(new Product_VM()), UpdateList);
            
        }

        private void LV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Page p = new CRUDProduct(new Product_VM((Product)LV.SelectedItem));
            FrameNavigateClass.Go(new CRUDProduct(new Product_VM((Product)LV.SelectedItem)),UpdateList);
        }

        private void UpdateList()
        {
            vm.OnPropertyChanged("ProductList");
        }

    }
}
