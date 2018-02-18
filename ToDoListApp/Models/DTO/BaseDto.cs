using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models.DTO
{
    public class BaseDto : INotifyPropertyChanged
    {
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        public BaseDto()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
        public string Id { get;}

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set
            {
                _createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set
            {
                _modifiedDate = value;
                OnPropertyChanged(nameof(ModifiedDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
