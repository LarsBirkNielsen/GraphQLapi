using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQL.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        //[NotMapped]
        //public DateTime ValidFrom { get; set; }
        //[NotMapped]
        //public DateTime ValidTo { get; set; }


        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}