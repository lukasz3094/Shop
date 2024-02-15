using Services.Models;
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
using WPF.ApiServices;
using WPF.ViewModel;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        public ProductDetailsWindow()
        {
            InitializeComponent();
        }

        private async void ButtonCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(DataContext is ProductDetailsVM vm))
                    return;

                if (vm.Product.ProductInCart)
                    await RemoveFromCart(vm.Product);
                else
                    await AddToCart(vm.Product);
            }
            catch
            {
                MessageBox.Show("Error while processing");
            }
        }

        private async void ButtonWishlist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(DataContext is ProductDetailsVM vm))
                    return;

                if (vm.Product.ProductInWishList)
                    await RemoveFromWishList(vm.Product);
                else
                    await AddToWishList(vm.Product);
            }
            catch
            {
                MessageBox.Show("Error while processing");
            }
        }

        private async Task AddToCart(ProductModel product)
        {
            try
            {
                CartService cartService = new CartService();
                CartModel cart = new CartModel { ProductId = product.Id, Quantity = 1, CustomerId = 1 };
                CartModel result = await cartService.Add(cart);
                product.ProductInCart = true;
                product.Quantity--;

                if (result != null && DataContext is ProductDetailsVM vm)
                {
                    vm.Cart = result;
                    vm.OnPropertyChanged("Cart");
                    vm.OnPropertyChanged("Product");
                }

                MessageBox.Show("Product added to cart");
            }
            catch
            {
                MessageBox.Show("Error while adding product to cart");
            }
        }

        private async Task RemoveFromCart(ProductModel product)
        {
            try
            {
                CartService cartService = new CartService();

                if (DataContext is ProductDetailsVM vm)
                {
                    await cartService.Delete(vm.Cart.Id);
                    product.ProductInCart = false;
                    product.Quantity++;
                    vm.Cart = null;
                    vm.OnPropertyChanged("Cart");
                }
                MessageBox.Show("Product removed from cart");
            }
            catch
            {
                MessageBox.Show("Error while removing product from cart");
            }
        }

        private async Task AddToWishList(ProductModel product)
        {
            try
            {
                WishListService wishListService = new WishListService();
                WishListModel result = await wishListService.Add(new WishListModel { ProductId = product.Id, CustomerId = 1 });
                product.ProductInWishList = true;

                if (result != null && DataContext is ProductDetailsVM vm)
                { 
                    vm.WishList = result;
                    vm.OnPropertyChanged("WishList");
                }

                MessageBox.Show("Product added to wishlist");
            }
            catch
            {
                MessageBox.Show("Error while adding product to wishlist");
            }
        }

        private async Task RemoveFromWishList(ProductModel product)
        {
            try
            {
                WishListService wishListService = new WishListService();

                if (DataContext is ProductDetailsVM vm)
                {
                    await wishListService.Delete(vm.WishList.Id);
                    product.ProductInWishList = false;

                    vm.WishList = null;
                    vm.OnPropertyChanged("WishList");
                }

                MessageBox.Show("Product removed from wishlist");
            }
            catch
            {
                MessageBox.Show("Error while removing product from wishlist");
            }
        }

        private async Task LoadData()
        {
            try
            {
                if (DataContext is ProductDetailsVM vm)
                {
                    CartService cartService = new CartService();
                    List<CartModel> cart = await cartService.GetById(1);
                    CartModel cartItem = cart.FirstOrDefault(x => x.ProductId == vm.Product.Id);

                    vm.Cart = cartItem;

                    WishListService wishListService = new WishListService();
                    List<WishListModel> wishList = await wishListService.GetById(1);
                    WishListModel wishListItem = wishList.FirstOrDefault(x => x.ProductId == vm.Product.Id);

                    vm.WishList = wishListItem;
                }
            }
            catch
            {
                MessageBox.Show("Error while loading product details");
            }
        }

        private async void ProductDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }
    }
}
