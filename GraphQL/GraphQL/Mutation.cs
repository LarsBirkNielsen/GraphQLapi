using GraphQL.Data;
using GraphQL.GraphQL.Authors;
using GraphQL.GraphQL.Books;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GraphQL.GraphQL
{

    [GraphQLDescription("Represents the mutations available.")]
    public class Mutation
    {

        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Adds a book.")]
        public async Task<AddBookPayload> AddBookAsync(
            AddBookInput input,
            [ScopedService] AppDbContext context, int clickBatchId, string userId)
        {
            var book = new Book
            {
                Name = input.Name,
                Genre = input.Genre,
                AuthorId = input.authorId
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();


            return new AddBookPayload(book);
        }
        [UseDbContext(typeof(AppDbContext))]
        public async Task<UpdateBookPayload> UpdateBookAsync(
                    UpdateBookInput input,
                    [ScopedService] AppDbContext context, string userId, int clickBatchId)
        {
            if (input.Name.Length != 0 && input.Name is null)
            {
                return null;
            }

            Book? updatedBook = await context.Books.FindAsync(input.id);

            if (updatedBook is null)
            {
                return null;
            }
            if (input.Genre.Length == 0)
            {
                return null;
            }


            if (input.Name.Length != 0)
            {
                updatedBook.Name = input.Name;
            }

            if (input.Genre.Length != 0)
            {
                updatedBook.Genre = input.Genre;
            }

            if (input.authorId != updatedBook.AuthorId)
            {
                updatedBook.AuthorId = input.authorId;
            }
            await context.SaveChangesAsync();

            return new UpdateBookPayload(updatedBook);
        }


        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddAuthorPayload> AddAuthorAsync(
            AddAuthorInput input,
            [ScopedService] AppDbContext context, string userId, int clickBatchId)
        {
            var author = new Author
            {
                Name = input.Name,
                Age = input.Age
            };

            context.Add(author);
            await context.SaveChangesAsync();

            return new AddAuthorPayload(author);
        }


        [UseDbContext(typeof(AppDbContext))]
        public async Task<UpdateAuthorPayload> UpdateAuthorAsync(
                    UpdateAuthorInput input,
                    [ScopedService] AppDbContext context, string userId, int clickBatchId)
        {
            if (input.Name.Length != 0 && input.Name is null)
            {
                return null;
            }

            Author? updatedAuthor = await context.Authors.FindAsync(input.id);

            if (updatedAuthor is null)
            {
                return null;
            }
            if (input.Age <= 0)
            {
                return null;
            }
            if (input.Age > 0)
            {
                updatedAuthor.Age = input.Age;
            }

            if (input.Name.Length != 0)
            {
                updatedAuthor.Name = input.Name;
            }

            if (input.id == updatedAuthor.Id)
            {
                updatedAuthor.Id = input.id;
            }

            await context.SaveChangesAsync();

            return new UpdateAuthorPayload(updatedAuthor);
        }


        //[UseDbContext(typeof(AppDbContext))]
        //public async Task<UpdateAuthorPayload> RestoreAuthor(
        //    [ScopedService] AppDbContext context, int clickBatchId, string userId, int auditId)
        //{

        //    //Sorting The AuditLog so we only get authors
        //    var auditAuthorLog = context.AuditLogs.Where(x => x.TableName == "Author").OrderByDescending(x => x.TimeOfUpdate);
        //    //Getting the Specific AuditLog we want to roll back
        //    Audit? oldAuthor = auditAuthorLog.FirstOrDefault(auditAuthorLog => auditAuthorLog.Id == auditId);


        //    //Dezerlizing props
        //    var dezerilizedPK = JsonConvert.DeserializeObject<Author>(oldAuthor.PrimaryKey);
        //    var dezerilizedOlValues = JsonConvert.DeserializeObject<Author>(oldAuthor.OldValues);

        //    //Getting the Matching Author that relates to the Audit-Author
        //    Author? updatedAuthor = await context.Authors.FindAsync(dezerilizedPK.Id);


        //    //Converting The Audit obj To an Author obj
        //    Author obj = new Author();
        //    obj.Id = dezerilizedPK.Id;
        //    obj.Name = dezerilizedOlValues.Name;
        //    obj.Age = dezerilizedOlValues.Age;

        //    //Checking for null values
        //    if (obj.Name is null)
        //    {
        //        obj.Name = updatedAuthor.Name;
        //    }
        //    else if (obj.Age == 0)
        //    {
        //        obj.Age = updatedAuthor.Age;
        //    }

        //    context.Entry(updatedAuthor).CurrentValues.SetValues(obj);
        //    await context.SaveChangesAsync(userId, clickBatchId);

        //    return new UpdateAuthorPayload(obj);
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //public async Task<UpdateBookPayload> RestoreBook(
        //    [ScopedService] AppDbContext context, int clickBatchId, string userId, int auditId)
        //{

        //    //Sorting The AuditLog so we only get books
        //    var auditBookLog = context.AuditLogs.Where(x => x.TableName == "Book").OrderByDescending(x => x.TimeOfUpdate);
        //    //Getting the Specific AuditLog we want to roll back
        //    Audit? oldBook = auditBookLog.FirstOrDefault(auditBookLog => auditBookLog.Id == auditId);


        //    //Dezerlizing props
        //    var dezerilizedPK = JsonConvert.DeserializeObject<Book>(oldBook.PrimaryKey);
        //    var dezerilizedOlValues = JsonConvert.DeserializeObject<Book>(oldBook.OldValues);

        //    //Getting the Matching Author that relates to the Audit-Book
        //    Book? updatedBook = await context.Books.FindAsync(dezerilizedPK.Id);


        //    //Converting The Audit obj To a Book obj
        //    Book obj = new Book();
        //    obj.Id = dezerilizedPK.Id;
        //    obj.Name = dezerilizedOlValues.Name;
        //    obj.Genre = dezerilizedOlValues.Genre;

        //    //Checking for null values
        //    if (obj.Name is null)
        //    {
        //        obj.Name = updatedBook.Name;
        //    }
        //    else if (obj.Genre is null)
        //    {
        //        obj.Genre = updatedBook.Genre;
        //    }

        //    context.Entry(updatedBook).CurrentValues.SetValues(obj);
        //    await context.SaveChangesAsync(userId, clickBatchId);

        //    return new UpdateBookPayload(obj);
        //}
    }
}


