using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class FakeDataService
    {
        public ObservableCollection<ToDoItem> GetToDoItemsObservableCollection()
        {
            var toDoItems = new ObservableCollection<ToDoItem>();
            var item1 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "Item 1",
                Text = "Test To Do Item 1",
                ItemCreatedDate = DateTime.Now,
                ItemModifiedDate = DateTime.Now
            };
            var item2 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "Item 2",
                Text = "Test To Do Item 2",
                ItemCreatedDate = DateTime.Now,
                ItemModifiedDate = DateTime.Now
            };
            var item3 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "Item 3",
                Text = "Test To Do Item 3",
                ItemCreatedDate = DateTime.Now,
                ItemModifiedDate = DateTime.Now,
                IsDone = true
            };
            var item4 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "Item 4",
                Text = "Test To Do Item 4",
                ItemCreatedDate = DateTime.Now,
                ItemModifiedDate = DateTime.Now
            };
            var item5 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "Item 5",
                Text = "Test To Do Item 5",
                ItemCreatedDate = DateTime.Now,
                ItemModifiedDate = DateTime.Now,
                IsDone = true
            };
            toDoItems.Add(item1);
            toDoItems.Add(item2);
            toDoItems.Add(item3);
            toDoItems.Add(item4);
            toDoItems.Add(item5);

            return toDoItems;
        }
    }
}
