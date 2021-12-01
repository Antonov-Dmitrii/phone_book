namespace phones_book.Models
{
    public class Users
    {
        public int id { get; set; }

        public string first_name { get; set; }
        
        public string last_name { get; set; }

        public int phone { get; set; }

        public string email { get; set; }

        public int id_department { get; set; }
    }
}
