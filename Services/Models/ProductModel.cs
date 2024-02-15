using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ProductModel : INotifyPropertyChanged
    {
        private int _id;
        private string _productName;
        private int? _categoryId;
        private string _categoryName;
        private int? _colorId;
        private string _colorName;
        private int? _brandId;
        private string _brandName;
        private decimal? _price;
        private int? _quantity;
        private string _description;
        private string _image;
        private bool _productInCart;
        private bool _productInWishList;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("id"); }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged("ProductName"); }
        }
        public int? CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; OnPropertyChanged("CategoryId"); }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; OnPropertyChanged("CategoryName"); }
        }
        public int? ColorId
        {
            get { return _colorId; }
            set { _colorId = value; OnPropertyChanged("ColorId"); }
        }
        public string ColorName
        {
            get { return _colorName; }
            set { _colorName = value; OnPropertyChanged("ColorName"); }
        }
        public int? BrandId
        {
            get { return _brandId; }
            set { _brandId = value; OnPropertyChanged("BrandId"); }
        }
        public string BrandName
        {
            get { return _brandName; }
            set { _brandName = value; OnPropertyChanged("BrandName"); }
        }
        public decimal? Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged("Quantity"); }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }
        public bool ProductInCart
        {
            get { return _productInCart; }
            set { _productInCart = value; OnPropertyChanged("ProductInCart"); }
        }
        public bool ProductInWishList
        {
            get { return _productInWishList; }
            set { _productInWishList = value; OnPropertyChanged("ProductInWishList"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
