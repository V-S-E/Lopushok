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
using WpfApp1.Interfaces;
using WpfApp1.View.Pages;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для ProductsList.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            FrameNavigateClass.SetFrame(MainFrame);
            FrameNavigateClass.Go(new ProductList(new ViewModel.Products_VM()));
        }

        public void ClickBack(object sender, RoutedEventArgs e)
        {
            FrameNavigateClass.Back();
        }
        public void SetTitle(string s)
        {
            this.Title = s;
        }
    }
}
