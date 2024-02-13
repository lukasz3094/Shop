using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class WishListModel : INotifyPropertyChanged
    {
        private int _id;
        private int _customerId;
        private int _productId;

        public int Id { get => _id; set { _id = value; OnPropertyChanged("Id"); } }
        public int CustomerId { get => _customerId; set { _customerId = value; OnPropertyChanged("CustomerId"); } }
        public int ProductId { get => _productId; set { _productId = value; OnPropertyChanged("ProductId"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
