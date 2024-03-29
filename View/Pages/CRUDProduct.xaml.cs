﻿using System;
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
using Microsoft.Win32;

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
            var result = MessageBox.Show("Вы действительно хотите сохранить изменения?", "Сохранение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                string saveResult = vm.Save();
                if (string.IsNullOrEmpty(saveResult))
                {
                    MessageBox.Show("Данные успешно сохранены!");
                    FrameNavigateClass.Back();
                }
                else MessageBox.Show(saveResult, "Ошибка сохранения");
            }
        }

        private void Delete_Material(object sender, RoutedEventArgs e)
        {
            vm.DeleteMaterial((ProductMaterial)DG.SelectedItem);
        }

        private void DG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            vm.OnPropertyChanged(nameof(vm.Product));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show("Удалить запись?", "Оповещение", MessageBoxButton.YesNo);
            if (mr == MessageBoxResult.Yes) vm.Delete();
            FrameNavigateClass.Back();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory+"\\products";
            ofd.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == false || !ofd.FileName.Contains(Environment.CurrentDirectory))
            {
                MessageBox.Show("Выбрана другая директория");
            }
            else
            {
                vm.SetImageFolder(ofd.FileName.Replace(Environment.CurrentDirectory,""));
            }
        }

        private void ClearImageButton_Click(object sender, RoutedEventArgs e)
        {
            vm.SetImageFolder(String.Empty);
        }
    }
}
