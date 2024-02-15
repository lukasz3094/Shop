using Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModel
{
    public class ProductDetailsVM : INotifyPropertyChanged
    {
        public ProductDetailsVM(ProductModel product)
        {
            Product = product;
        }

        private ProductModel _product;
        public ProductModel Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        private CartModel _cart;
        public CartModel Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                OnPropertyChanged("Cart");
            }
        }

        private WishListModel _wishList;
        public WishListModel WishList
        {
            get { return _wishList; }
            set
            {
                _wishList = value;
                OnPropertyChanged("WishList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
