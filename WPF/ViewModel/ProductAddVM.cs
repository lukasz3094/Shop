using Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModel
{
    public class ProductAddVM : INotifyPropertyChanged
    {
        public ProductAddVM()
        {
            Categories = new List<CategoryModel>();
            Colors = new List<ColorModel>();
            Brands = new List<BrandModel>();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    _name = value;
                OnPropertyChanged("Name");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                    _price = value;
                OnPropertyChanged("Price");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                    _description = value;
                OnPropertyChanged("Description");
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                    _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                    _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        private ColorModel _selectedColor;
        public ColorModel SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                if (_selectedColor != value)
                    _selectedColor = value;
                OnPropertyChanged("SelectedColor");
            }
        }

        private BrandModel _selectedBrand;
        public BrandModel SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                if (_selectedBrand != value)
                    _selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
            }
        }

        private List<CategoryModel> _categories;
        public List<CategoryModel> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                    _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private List<ColorModel> _colors;
        public List<ColorModel> Colors
        {
            get { return _colors; }
            set
            {
                if (_colors != value)
                    _colors = value;
                OnPropertyChanged("Colors");
            }
        }

        private List<BrandModel> _brands;
        public List<BrandModel> Brands
        {
            get { return _brands; }
            set
            {
                if (_brands != value)
                    _brands = value;
                OnPropertyChanged("Brands");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
