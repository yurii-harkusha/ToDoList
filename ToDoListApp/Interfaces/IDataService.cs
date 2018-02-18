using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Interfaces
{
    public interface IDataService
    {
        Task<List<ToDoItem>> GetToDoItemsAsync();
        void SaveOrUpdateToDoItemsAsync(List<ToDoItem> toDoItems);
    }
}
