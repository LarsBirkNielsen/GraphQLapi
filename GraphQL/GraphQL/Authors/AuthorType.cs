using GraphQL.Data;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace GraphQL.GraphQL.Authors
{
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {

            descriptor
                .Field(author => author.Id)
                .Description("Represents the unique ID for the author.");

            descriptor
                .Field(author => author.Name)
                .Description("Represents the name for the author.");

            descriptor
                .Field(author => author.Age)
                .Description("Represents the age of the author.");

            //descriptor
            //    .Field(author => author.BookId)
            //    .Description("Represents the unique ID of the book which the author belongs.");

            descriptor
                .Field(author => author.Books)
                .ResolveWith<Resolvers>(author => author.GetAuthor(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("");

        }

        private class Resolvers
        {
            public Author GetAuthor(Book book, [ScopedService] AppDbContext context)
            {
                return context.Authors.FirstOrDefault(author => author.Id == book.AuthorId);
            }
        }
    }
}