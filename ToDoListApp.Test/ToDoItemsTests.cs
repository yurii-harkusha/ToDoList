using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListApp.Helpers;
using ToDoListApp.Models;

namespace ToDoListApp.Test
{
    [TestClass]
    public class ToDoItemsTests
    {
        [TestMethod]
        public void CheckIfToDoItemsSortByAlphabetIsCorrect()
        {
            var toDoItemSortHelper = new ToDoItemSortHelper();
            var toDoItems = GetTestToDoItemsCollection();
            var sortedByAlphabetToDoItems = toDoItemSortHelper.SortByAlphabet(toDoItems);

            var expectedFirstItem = toDoItems.Where(x => x.Title == "A Item").FirstOrDefault();
            var expectedSecondItem = toDoItems.Where(x => x.Title == "B Item").FirstOrDefault();
            var expectedThirdItem = toDoItems.Where(x => x.Title == "C Item").FirstOrDefault();
            var expectedFourthItem = toDoItems.Where(x => x.Title == "N Item").FirstOrDefault();
            var expectedFifthItem = toDoItems.Where(x => x.Title == "O Item").FirstOrDefault();
            var expectedSixthItem = toDoItems.Where(x => x.Title == "Z Item").FirstOrDefault();

            Assert.AreEqual(expectedFirstItem, sortedByAlphabetToDoItems[0]);
            Assert.AreEqual(expectedSecondItem, sortedByAlphabetToDoItems[1]);
            Assert.AreEqual(expectedThirdItem, sortedByAlphabetToDoItems[2]);
            Assert.AreEqual(expectedFourthItem, sortedByAlphabetToDoItems[3]);
            Assert.AreEqual(expectedFifthItem, sortedByAlphabetToDoItems[4]);
            Assert.AreEqual(expectedSixthItem, sortedByAlphabetToDoItems[5]);
        }

        [TestMethod]
        public void CheckIfToDoItemsSortByCreatedDateIsCorrect()
        {
            var toDoItemSortHelper = new ToDoItemSortHelper();
            var toDoItems = GetTestToDoItemsCollection();
            var sortedByAlphabetToDoItems = toDoItemSortHelper.SortByCreatedDate(toDoItems);

            var expectedFirstItemWithDateTimeNow = toDoItems.Where(x => x.Title == "B Item").FirstOrDefault();
            var expectedSecondItemWithDateTimeNowPlus1Hour = toDoItems.Where(x => x.Title == "C Item").FirstOrDefault();
            var expectedThirdItemWithDateTimeNowPlus1Day = toDoItems.Where(x => x.Title == "A Item").FirstOrDefault();
            var expectedFourthItemWithDateTimeNowPlus1Month = toDoItems.Where(x => x.Title == "Z Item").FirstOrDefault();
            var expectedFifthItemWithDateTimeNowPlus2Months = toDoItems.Where(x => x.Title == "O Item").FirstOrDefault();
            var expectedSixthItemWithDateTimeNowPlus3Years = toDoItems.Where(x => x.Title == "N Item").FirstOrDefault();

            Assert.AreEqual(expectedFirstItemWithDateTimeNow, sortedByAlphabetToDoItems[0]);
            Assert.AreEqual(expectedSecondItemWithDateTimeNowPlus1Hour, sortedByAlphabetToDoItems[1]);
            Assert.AreEqual(expectedThirdItemWithDateTimeNowPlus1Day, sortedByAlphabetToDoItems[2]);
            Assert.AreEqual(expectedFourthItemWithDateTimeNowPlus1Month, sortedByAlphabetToDoItems[3]);
            Assert.AreEqual(expectedFifthItemWithDateTimeNowPlus2Months, sortedByAlphabetToDoItems[4]);
            Assert.AreEqual(expectedSixthItemWithDateTimeNowPlus3Years, sortedByAlphabetToDoItems[5]);
        }

        public ObservableCollection<ToDoItem> GetTestToDoItemsCollection()
        {
            var toDoItems = new ObservableCollection<ToDoItem>();
            
            var item1 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "B Item",
                Text = "Test To Do Item 1",
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };

            var item2 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "A Item",
                Text = "Test To Do Item 2",
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now.AddDays(1)
            };

            var item3 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "C Item",
                Text = "Test To Do Item 3",
                ModifiedDate = DateTime.Now,
                IsDone = true,
                CreatedDate = DateTime.Now.AddHours(1)
            };
            var item4 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "N Item",
                Text = "Test To Do Item 4",
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now.AddYears(3)
            };

            var item5 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "Z Item",
                Text = "Test To Do Item 5",
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now.AddMonths(1),
                IsDone = true
            };

            var item6 = new ToDoItem()
            {
                AuthorId = "80F23C33-85A2-42DB-AD2A-7C9C0C3513C0",
                Title = "O Item",
                Text = "Test To Do Item 6",
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now.AddMonths(2),
                IsDone = true
            };

            toDoItems.Add(item1);
            toDoItems.Add(item2);
            toDoItems.Add(item3);
            toDoItems.Add(item4);
            toDoItems.Add(item5);
            toDoItems.Add(item6);

            return toDoItems;
        }  
    }
}
