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
using System.Diagnostics;

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
            try
            {
                _toDoItems = await BlobCache.UserAccount.GetObject<List<ToDoItem>>("toDoItems");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return _toDoItems;
        }

        public void SaveOrUpdateToDoItemsAsync(List<ToDoItem> toDoItems)
        {
            _toDoItems = null;
            _toDoItems = toDoItems;
            try
            {
                BlobCache.UserAccount.InsertObject("toDoItems", _toDoItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
