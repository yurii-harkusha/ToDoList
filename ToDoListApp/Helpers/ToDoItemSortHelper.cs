using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Helpers
{
    public class ToDoItemSortHelper
    {
        public ToDoItemSortHelper()
        {

        }
        
        public ObservableCollection<ToDoItem> SortByAlphabet(ObservableCollection<ToDoItem> toDoItems)
        {
            var toDoItemsSortedByAlphabet = new ObservableCollection<ToDoItem>(toDoItems.OrderBy(x => x.Title).ToList());
            return toDoItemsSortedByAlphabet;
        }

        public ObservableCollection<ToDoItem> SortByCreatedDate(ObservableCollection<ToDoItem> toDoItems)
        {
            var toDoItemsSortedByAlphabet = new ObservableCollection<ToDoItem>(toDoItems.OrderBy(x => x.CreatedDate).ToList());
            return toDoItemsSortedByAlphabet;
        }
    }
}
