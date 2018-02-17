using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models.DTO
{
    public class BaseDto
    {
        public BaseDto()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
        }
        public string Id { get;}

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }
    }
}
