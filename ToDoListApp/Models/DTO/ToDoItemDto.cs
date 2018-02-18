using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models.DTO
{
    public class ToDoItemDto : BaseDto
    {
        public ToDoItemDto()
        {

        }

        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
