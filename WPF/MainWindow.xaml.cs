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
using WPF.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // prevent from infinite loading
            Task.Delay(2000).Wait();

            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCartAndWishlist();
            await LoadProducts();
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

            if (DataContext is MainWindowVM vm)
            {
                ProductDetailsVM productDetailsVM = window.DataContext as ProductDetailsVM;
                ProductModel productInVm = productDetailsVM.Product;

                ProductModel productModel = vm.Products.First(p => p.Id == productInVm.Id);

                productModel = productInVm;
            }

            await LoadCartAndWishlist();
            await LoadProducts();
        }

        private async Task LoadCartAndWishlist()
        {
            try
            {
                if (!(DataContext is MainWindowVM vm))
                    return;

                var cartService = new CartService();
                var cart = await cartService.GetById(1);
                vm.CartList = new ObservableCollection<CartModel>(cart);

                var wishListService = new WishListService();
                var wishList = await wishListService.GetById(1);
                vm.WishList = new ObservableCollection<WishListModel>(wishList);
            }
            catch
            {
                MessageBox.Show("Error while loading data from db");
            }
        }

        private async Task LoadProducts()
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
                    product.Image = $"pack://application:,,,/Images/{imageText}.jpg";

                    if (vm.CartList.Any(c => c.ProductId == product.Id))
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

        private async void ButtonDodajPrzedmiot_Click(object sender, RoutedEventArgs e)
        {
            ProductAddWindow window = new ProductAddWindow();
            window.ShowDialog();

            await LoadCartAndWishlist();
            await LoadProducts();
        }

        private async void ButtonWishList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(DataContext is MainWindowVM vm))
                    return;

                List<WishListModel> wishList = vm.WishList.Where(x => x.CustomerId == 1).ToList();
                List<ProductModel> productsInCart = new List<ProductModel>();
                List<CartModel> cart = vm.CartList.Where(x => x.CustomerId == 1).ToList();

                foreach (WishListModel wishListItem in wishList)
                    productsInCart.Add(vm.Products.FirstOrDefault(p => p.Id == wishListItem.ProductId));

                WishListWindow window = new WishListWindow
                {
                    DataContext = new WishListVM()
                    {
                        WishList = new ObservableCollection<WishListModel>(wishList),
                        Products = new ObservableCollection<ProductModel>(productsInCart),
                        Cart = new ObservableCollection<CartModel>(cart),                        
                    }
                };

                window.ShowDialog();

                await LoadCartAndWishlist();
                await LoadProducts();
            }
            catch
            {
                MessageBox.Show("Error while opening wishlist");
            }
        }

        private async void ButtonCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(DataContext is MainWindowVM vm))
                    return;

                List<CartModel> cart = vm.CartList.Where(x => x.CustomerId == 1).ToList();
                List<ProductModel> productsInCart = new List<ProductModel>();
                List<WishListModel> wishList = vm.WishList.Where(x => x.CustomerId == 1).ToList();

                foreach (CartModel cartItem in cart)
                    productsInCart.Add(vm.Products.FirstOrDefault(p => p.Id == cartItem.ProductId));

                CartWindow window = new CartWindow
                {
                    DataContext = new CartVM()
                    {
                        Cart = new ObservableCollection<CartModel>(cart),
                        Products = new ObservableCollection<ProductModel>(productsInCart),
                        WishList = new ObservableCollection<WishListModel>(wishList)
                    }
                };

                window.ShowDialog();

                await LoadCartAndWishlist();
                await LoadProducts();
            }
            catch
            {
                MessageBox.Show("Error while opening cart");
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

                if (DataContext is MainWindowVM vm)
                {
                    vm.CartList.Add(result);

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

                if (DataContext is MainWindowVM vm)
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
