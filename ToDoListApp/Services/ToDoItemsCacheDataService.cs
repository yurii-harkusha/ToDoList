using Akavache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;
using ToDoListApp.Services.HttpService;
using System.Reactive.Linq;
using ToDoListApp.Interfaces;

namespace ToDoListApp.Services
{
    public class ToDoItemsCacheDataService : IDataService
    {
        private List<ToDoItem> _toDoItems;
        public ToDoItemsCacheDataService()
        {
            
        }

        public async Task<List<ToDoItem>> GetToDoItemsAsync()
        {
            _toDoItems = null;
            _toDoItems = await BlobCache.UserAccount.GetObject<List<ToDoItem>>("toDoItems");
            return _toDoItems;
        }

        public void SaveOrUpdateToDoItemsAsync(List<ToDoItem> toDoItems)
        {
            _toDoItems = null;
            _toDoItems = toDoItems;
            BlobCache.UserAccount.InsertObject("toDoItems", _toDoItems);
        }
    }
}
