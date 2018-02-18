using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Services.HttpService
{
    public class ToDoItemsResult
    {
        public object ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "Result")]
        public List<ToDoItem> ToDoItems { get; set; }

        public bool Success { get; set; }
    }
}
