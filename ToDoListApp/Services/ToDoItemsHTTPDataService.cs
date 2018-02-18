using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;
using ToDoListApp.Services.HttpService;

namespace ToDoListApp.Services
{
    public class ToDoItemsHTTPDataService : IDataService
    {
        private readonly HttpRequestService _httpRequestService;
        private List<ToDoItem> _toDoItems;

        public ToDoItemsHTTPDataService()
        {
            _httpRequestService = new HttpRequestService();
        }

        public async Task<List<ToDoItem>> GetToDoItemsAsync()
        {
            _toDoItems = null;
            string commandUrl = "GetToDoItems";

            var result = await _httpRequestService.Get<ToDoItemsResult>(commandUrl);
            _toDoItems = result?.ToDoItems;
            return _toDoItems;
        }

        public async void SaveOrUpdateToDoItemsAsync(List<ToDoItem> toDoItems)
        {
            _toDoItems = null;
            _toDoItems = toDoItems;
            string commandUrl = "SaveOrUpdateToDoItems";
            var result = await _httpRequestService.Post<ToDoItemsResult>(commandUrl, toDoItems);
        }
    }
}
