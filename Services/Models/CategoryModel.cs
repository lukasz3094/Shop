using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class CategoryModel : INotifyPropertyChanged
    {
        public CategoryModel() { }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                    _id = value;
                OnPropertyChanged("Id");
            }
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

        private int _parentId;
        public int ParentId
        {
            get { return _parentId; }
            set
            {
                if (_parentId != value)
                    _parentId = value;
                OnPropertyChanged("ParentId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
