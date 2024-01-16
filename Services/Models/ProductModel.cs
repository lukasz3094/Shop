using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ProductModel
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

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        public int? CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
        public int? ColorId
        {
            get { return _colorId; }
            set { _colorId = value; }
        }
        public string ColorName
        {
            get { return _colorName; }
            set { _colorName = value; }
        }
        public int? BrandId
        {
            get { return _brandId; }
            set { _brandId = value; }
        }
        public string BrandName
        {
            get { return _brandName; }
            set { _brandName = value; }
        }
        public decimal? Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}
