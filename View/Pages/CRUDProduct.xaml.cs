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
using WpfApp1.View.ModalWindows;
using WpfApp1.ViewModel;

namespace WpfApp1.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CRUDProduct.xaml
    /// </summary>
    public partial class CRUDProduct : Page
    {
        Product_VM vm;
        public CRUDProduct(Product_VM vm)
        {
            this.vm = vm;
            InitializeComponent();
            DataContext = vm;
        }

        private void Add_Material(object sender, RoutedEventArgs e)
        {
            var w = new SelectMaterial();
            w.ShowDialog();
            if (w.Selected != null && !w.IsClosing)
            {
                vm.AddMaterial(w.Selected);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.Save();
            FrameNavigateClass.Back();
        }

        private void Delete_Material(object sender, RoutedEventArgs e)
        {
            vm.DeleteMaterial((ProductMaterial)DG.SelectedItem);
        }

        private void DG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            vm.OnPropertyChanged("Product");
        }
    }
}
