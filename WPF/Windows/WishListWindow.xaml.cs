using Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF.ApiServices;
using WPF.ViewModel;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for WishListWindow.xaml
    /// </summary>
    public partial class WishListWindow : Window
    {
        public WishListWindow()
        {
            InitializeComponent();
        }

        private async void ButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            if (!(button.Tag is ProductModel product))
                return;

            ProductDetailsWindow window = new ProductDetailsWindow();
            window.DataContext = new ProductDetailsVM(product);
            window.ShowDialog();

            if (DataContext is WishListVM vm)
            {
                ProductDetailsVM productDetailsVM = window.DataContext as ProductDetailsVM;
                ProductModel productInVm = productDetailsVM.Product;

                ProductModel productModel = vm.Products.First(p => p.Id == productInVm.Id);

                productModel = productInVm;

                if (productDetailsVM.Product != null && productDetailsVM.Product.ProductInWishList == false)
                {
                    vm.WishList.Remove(productDetailsVM.WishList);
                    vm.Products.Remove(productDetailsVM.Product);
                }
            }

            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                if (!(DataContext is WishListVM vm))
                    return;

                var apiService = new ProductService();
                var products = await apiService.GetAll();
                List<ProductModel> productsList = new List<ProductModel>();

                foreach (ProductModel product in vm.Products)
                {
                    ProductModel productModel = products.First(p => p.Id == product.Id);
                    productsList.Add(productModel);
                }

                products = productsList;

                var random = new Random();

                foreach (var product in products)
                {
                    var number = random.Next(1, 10);
                    var imageText = $"image{number}";
                    product.Image = $"pack://application:,,,/Images/{imageText}.jpg";

                    if (vm.Cart.Any(c => c.ProductId == product.Id))
                        product.ProductInCart = true;

                    if (vm.WishList.Any(w => w.ProductId == product.Id))
                        product.ProductInWishList = true;
                }

                vm.Products = new ObservableCollection<ProductModel>(products);
            }
            catch
            {
                MessageBox.Show("Error while loading data from db");
            }
        }

        private async void ButtonAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is Button button))
                    return;

                if (!(button.Tag is ProductModel product))
                    return;

                e.Handled = true;

                if (product.ProductInCart)
                {
                    MessageBox.Show("Product already in cart");
                    return;
                }

                CartService cartService = new CartService();
                CartModel cart = new CartModel
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    CustomerId = 1
                };

                CartModel result = await cartService.Add(cart);

                if (DataContext is CartVM vm)
                {
                    vm.Cart.Add(result);

                    ProductModel productInVm = vm.Products.First(p => p.Id == product.Id);

                    productInVm.ProductInCart = true;
                    vm.OnPropertyChanged("Products");
                }

                product.Quantity--;
                product.ProductInCart = true;

                MessageBox.Show("Product added to cart");
            }
            catch
            {
                MessageBox.Show("Error while adding product to cart");
            }
        }

        private async void ButtonAddToWishlist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is Button button))
                    return;

                if (!(button.Tag is ProductModel product))
                    return;

                e.Handled = true;

                if (product.ProductInWishList)
                {
                    MessageBox.Show("Product already in wishlist");
                    return;
                }

                WishListService wishListService = new WishListService();
                WishListModel wishList = new WishListModel
                {
                    ProductId = product.Id,
                    CustomerId = 1
                };

                WishListModel result = await wishListService.Add(wishList);

                if (DataContext is CartVM vm)
                {
                    vm.WishList.Add(result);

                    product.ProductInWishList = true;
                    vm.OnPropertyChanged("Products");
                }

                MessageBox.Show("Product added to wishlist");
            }
            catch
            {
                MessageBox.Show("Error while adding product to wishlist");
            }
        }
    }
}
