using Services.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.ApiServices;
using WPF.ViewModel;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for ProductAddWindow.xaml
    /// </summary>
    public partial class ProductAddWindow : Window
    {
        private readonly ColorService _colorService;
        private readonly BrandService _brandService;
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;

        public ProductAddWindow()
        {
            InitializeComponent();
            DataContext = new ProductAddVM();

            _colorService = new ColorService();
            _brandService = new BrandService();
            _categoryService = new CategoryService();
            _productService = new ProductService();
        }

        private async void ProductAddWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            if (DataContext is ProductAddVM vm)
            {
                vm.Colors = await _colorService.GetAll();
                vm.Brands = await _brandService.GetAll();
                vm.Categories = await _categoryService.GetAll();
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is ProductAddVM vm)
                {
                    if (string.IsNullOrEmpty(vm.Name))
                    {
                        MessageBox.Show("Name is required");
                        return;
                    }
                    else if (decimal.TryParse(vm.Price.ToString(), out decimal price) == false)
                    {
                        MessageBox.Show("Check price field");
                        return;
                    }
                    else if (string.IsNullOrEmpty(vm.Description))
                    {
                        MessageBox.Show("Description is required");
                        return;
                    }
                    else if (int.TryParse(vm.Quantity.ToString(), out int quantity) == false)
                    {
                        MessageBox.Show("Check quantity field");
                        return;
                    }
                    else if (vm.SelectedBrand == null)
                    {
                        MessageBox.Show("Brand is required");
                        return;
                    }
                    else if (vm.SelectedCategory == null)
                    {
                        MessageBox.Show("Category is required");
                        return;
                    }
                    else if (vm.SelectedColor == null)
                    {
                        MessageBox.Show("Color is required");
                        return;
                    }   

                    ProductModel product = new ProductModel
                    {
                        ProductName = vm.Name,
                        Price = decimal.Parse(vm.Price.ToString()),
                        Description = vm.Description,
                        Quantity = int.Parse(vm.Quantity.ToString()),
                        BrandId = vm.SelectedBrand.Id,
                        CategoryId = vm.SelectedCategory.Id,
                        ColorId = vm.SelectedColor.Id
                    };

                    await _productService.Add(product);

                    MessageBox.Show("Product added successfully");

                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Error while adding new product");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);

            if (sender is TextBox textBox && (e.Text.Contains(".") && textBox.Text.Contains(".")))
                e.Handled = true;
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
                ParseInput(textBox.Text, textBox);
        }

        private void ParseInput(string input, TextBox textBox)
        {
            input = input.TrimStart('0');

            decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue);
            int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out int intValue);

            if (intValue > 0)
                textBox.Text = intValue.ToString();
            else if (decimalValue > 0)
                textBox.Text = decimalValue.ToString("F2", CultureInfo.InvariantCulture);
            else
                textBox.Text = "0";
        }
    }
}
