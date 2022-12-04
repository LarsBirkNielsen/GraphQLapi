using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQL.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        //[NotMapped]
        //public DateTime ValidFrom { get; set; }

        //[NotMapped]
        //public DateTime ValidTo { get; set; }


    }
}