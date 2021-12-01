using System.Collections.Generic;

namespace phones_book.Models
{
    public class DepartmentInfo
    {
        public Department Department { get; set; }
        public List<DepartmentInfo> Childs { get; set; }
    }
}
