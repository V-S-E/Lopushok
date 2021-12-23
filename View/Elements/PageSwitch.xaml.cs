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

namespace WpfApp1.View.Elements
{
    /// <summary>
    /// Логика взаимодействия для PageSwitch.xaml
    /// </summary>
    public partial class PageSwitch : UserControl
    {

        public static readonly DependencyProperty PageSeparatorProperty = DependencyProperty.Register("PageSep", typeof(PageSeparator), typeof(PageSwitch), new UIPropertyMetadata());
        public static readonly DependencyProperty SelectedPageProperty = DependencyProperty.Register("SelectedPage", typeof(int), typeof(PageSwitch), new PropertyMetadata(0));

        public PageSeparator PageSep
        {
            get { return (PageSeparator)GetValue(PageSeparatorProperty); }
            set { SetValue(PageSeparatorProperty, value); }
        }

        public int SelectedPage
        {
            get { return (int)GetValue(SelectedPageProperty); }
            set { SetValue(SelectedPageProperty, value); }
        }
        public PageSwitch()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LB.SelectedIndex = LB.SelectedIndex > 0 ? LB.SelectedIndex - 1 : 0;
            LB.ScrollIntoView(LB.SelectedItem);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LB.SelectedIndex = LB.SelectedIndex < LB.Items.Count ? LB.SelectedIndex + 1 : LB.Items.Count;
            LB.ScrollIntoView(LB.SelectedItem);
        }
    }
}
