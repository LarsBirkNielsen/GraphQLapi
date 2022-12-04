using GraphQL.Data;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace GraphQL.GraphQL.Books
{
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Description("Represents Book props.");

            descriptor
                .Field(book => book.Id)
                .Description("Represents the unique ID for the book.");

            descriptor
                .Field(book => book.Name)
                .Description("Represents name of the book.");

            descriptor
                .Field(book => book.Genre)
                .Description("Represents the genre of the book.");

            descriptor
                .Field(book => book.AuthorId)
                .Description("Represents the unique ID of the author which the book is written by.");

            descriptor
                .Field(book => book.Author)
                .ResolveWith<Resolvers>(book => book.GetBooks(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the book the author has written.");

        }

        private class Resolvers
        {
            //public Book GetBook(Author author, [ScopedService] AppDbContext context)
            //{
            //    return context.Books.FirstOrDefault(book => book.AuthorId == author.Id);
            //}

            public IQueryable<Book> GetBooks(Author author, [ScopedService] AppDbContext context)
            {
                return context.Books.Where(book => book.AuthorId == author.Id);
            }
        }
    }
}