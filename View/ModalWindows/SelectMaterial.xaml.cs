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
using System.Windows.Shapes;
using WpfApp1.Model;
using WpfApp1.ViewModel;

namespace WpfApp1.View.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для SelectMaterial.xaml
    /// </summary>
    public partial class SelectMaterial : Window
    {
        private SelectMaterial_VM vm;
        private bool _isClosing = true;
        public bool IsClosing => _isClosing;
        public SelectMaterial()
        {
            vm = new SelectMaterial_VM();
            InitializeComponent();
            DataContext = vm;
        }

        public Material Selected { get { return vm.Selected; } set { vm.Selected = value; } }


        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            _isClosing = false;
            Close();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var a = sender as TreeView;
            if (a.SelectedItem != null && a.SelectedItem is Material)
            {
                Selected = (Material)a.SelectedItem;
            }
        }
    }
}
