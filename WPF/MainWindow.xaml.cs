using Entities.Data;
using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Collections.ObjectModel;
using WPF.ApiServices;
using WPF.ViewModel;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(DataContext is MainWindowVM vm))
                    return;

                var apiService = new ProductService();
                var products = await apiService.GetAll();
                var random = new Random();

                foreach (var product in products)
                {
                    var number = random.Next(1, 10);
                    var imageText = $"image{number}";
                    product.Image = $"Images/{imageText}.jpg";
                }

                vm.Products = new ObservableCollection<ProductModel>(products);
            }
            catch
            {
                MessageBox.Show("Error while loading data from db");
            }
        }
    }
}
