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

namespace WpfApp1.View.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для PriceChange.xaml
    /// </summary>
    public partial class PriceChange : Window
    {
        public decimal Price { get; set; }
        public bool IsUpdate { get; private set; }
        public PriceChange()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            IsUpdate = true;
            Close();
        }
    }
}
