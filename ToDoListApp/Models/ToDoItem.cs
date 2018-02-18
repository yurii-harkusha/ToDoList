using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models.DTO;
using ToDoListApp.Models.Enums;

namespace ToDoListApp.Models
{
    public class ToDoItem : ToDoItemDto, INotifyPropertyChanged
    {
        private bool _isDone;

        public ToDoItem()
        {
            
        }

        public new bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
                OnPropertyChanged(nameof(StatusImage));
                OnPropertyChanged(nameof(Status));
            }
        }

        [JsonIgnore]
        public string StatusImage
        {
            get
            {
                if (IsDone)
                {
                    return "to_do_item_done.png";
                }
                else
                {
                    return "to_do_item_active.png";
                }
            }
        }

        [JsonIgnore]
        public string Status
        {
            get
            {
                if (IsDone)
                {
                    return Enum.GetName(typeof(ToDoItemStatuses), ToDoItemStatuses.Done);
                }
                else
                {
                    return Enum.GetName(typeof(ToDoItemStatuses), ToDoItemStatuses.Active);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
