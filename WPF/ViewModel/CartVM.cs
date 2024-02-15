using Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModel
{
    public class CartVM : INotifyPropertyChanged
    {
        public CartVM()
        {
            Cart = new ObservableCollection<CartModel>();
            Products = new ObservableCollection<ProductModel>();
            WishList = new ObservableCollection<WishListModel>();
        }

        private ObservableCollection<CartModel> _cart;
        public ObservableCollection<CartModel> Cart
        {
            get { return _cart; }
            set
            {
                if (_cart != value)
                    _cart = value;
                OnPropertyChanged("Cart");
            }
        }

        private ObservableCollection<ProductModel> _products;
        public ObservableCollection<ProductModel> Products
        {
            get { return _products; }
            set
            {
                if (value != _products)
                {
                    _products = value;
                    OnPropertyChanged("Products");
                }
            }
        }

        private ObservableCollection<WishListModel> _wishList;
        public ObservableCollection<WishListModel> WishList
        {
            get { return _wishList; }
            set
            {
                if (value != _wishList)
                {
                    _wishList = value;
                    OnPropertyChanged("WishList");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
