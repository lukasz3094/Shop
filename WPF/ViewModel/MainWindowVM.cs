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
    public class MainWindowVM : INotifyPropertyChanged
    {
        public MainWindowVM()
        {

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

        private ObservableCollection<CartModel> _cartList;
        public ObservableCollection<CartModel> CartList
        {
            get { return _cartList; }
            set
            {
                if (value != _cartList)
                {
                    _cartList = value;
                    OnPropertyChanged("CartList");
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
