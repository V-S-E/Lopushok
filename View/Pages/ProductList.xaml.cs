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
                foreach (Product r in LV.SelectedItems)
                {
                    r.MinCostForAgent = w.Price;
                }
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
