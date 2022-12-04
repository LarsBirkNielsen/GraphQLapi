using GraphQL.AuditLibrary;
using GraphQL.Data;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace GraphQL.GraphQL
{

    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable book.")]
        public IQueryable<Book> GetBook([ScopedService] AppDbContext context)
        {

            return context.Books;
        }

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable platform.")]
        //public IQueryable<Book> GetBookHistory([ScopedService] AppDbContext context)
        //{

        //    var allBooks = context.Books.TemporalAll()
        //        .Select(x => new Book
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Genre = x.Genre,
        //            ValidFrom = EF.Property<DateTime>(x, "PeriodStart"),
        //            ValidTo = EF.Property<DateTime>(x, "PeriodEnd"),
        //        })
        //        .AsQueryable();


        //    var booksHistory = allBooks.OrderByDescending(book => book.ValidTo).Where(x => x.ValidTo < DateTime.MaxValue);

        //    //foreach (var book in booksHistory)
        //    //{
        //    //    Console.WriteLine(book.ValidFrom.ToString("G"));
        //    //    //book.ValidTo.ToString("G");
        //    //}
        //    return booksHistory;

        //}



        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable platform.")]
        //public IQueryable<Book> GetBookHistoryById([ScopedService] AppDbContext context, int id)
        //{

        //    var allBooks = context.Books.TemporalAll()
        //        .Where(x => x.Id == id)
        //        .Select(x => new Book
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Genre = x.Genre,
        //            ValidFrom = EF.Property<DateTime>(x, "PeriodStart"),
        //            ValidTo = EF.Property<DateTime>(x, "PeriodEnd"),
        //        })
        //        .AsQueryable();


        //    var booksHistory = allBooks.OrderByDescending(book => book.ValidTo).Where(x => x.ValidTo < DateTime.MaxValue);


        //    return booksHistory;

        //}



        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the last changes in book historic values.")]
        //public IQueryable<Book> ListLastXBookChangesByID([ScopedService] AppDbContext context, int id, int number)
        //{

        //    var allBooks = context.Books.TemporalAll()
        //        .Where(x => x.Id == id)
        //        .Select(x => new Book
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Genre = x.Genre,
        //            ValidFrom = EF.Property<DateTime>(x, "PeriodStart"),
        //            ValidTo = EF.Property<DateTime>(x, "PeriodEnd"),
        //        })
        //        .AsQueryable();


        //    var booksHistory = allBooks.OrderByDescending(book => book.ValidTo).Where(x => x.ValidTo < DateTime.MaxValue).Take(number);


        //    return booksHistory;

        //}

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable author.")]
        public IQueryable<Author> GetAuthor([ScopedService] AppDbContext context)
        {
            return context.Authors;
        }


        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable author.")]
        //public Author GetAuthor(int Id, AppDbContext context)
        //{
        //    return context.Authors.FirstOrDefault(x => x.Id == Id);
        //}

        ///**************************/
        ///* AUDIT AS OBJECTS => BOOKS */
        ///**************************/

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Book from the auditlog.")]
        //public IList<Book> GetBookHistoryAudit([ScopedService] AppDbContext context)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.TableName == "Book").OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Book> bookHistoryList = new List<Book>();



        //    foreach (var book in auditBookLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Book>(book.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(book.PrimaryKey);
        //        var timeOfUpdateColum = book.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditBookId = dezerilizedPKColum.Id;
        //        var activeBook = context.Books.FirstOrDefault(x => x.Id == auditBookId);

        //        //Creating a Book Object             
        //        Book auditBook = new Book();


        //        // Setting the props for audit-book object
        //        auditBook.Id = auditBookId;

        //        if (activeBook.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditBook.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditBook.Name = activeBook.Name; }
        //        if (activeBook.Genre != dezerilizedOldValueColum.Genre && dezerilizedOldValueColum.Genre is not null)
        //        {
        //            auditBook.Genre = dezerilizedOldValueColum.Genre;
        //        }
        //        else { auditBook.Genre = activeBook.Genre; }
        //        if (activeBook.AuthorId != dezerilizedOldValueColum.AuthorId && dezerilizedOldValueColum.AuthorId != 0)
        //        {
        //            auditBook.AuthorId = dezerilizedOldValueColum.AuthorId;
        //        }
        //        else
        //        {
        //            auditBook.AuthorId = activeBook.AuthorId;
        //        }

        //        bookHistoryList.Add(auditBook);
        //    }

        //    return bookHistoryList;
        //}



        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Book from the auditlog.")]
        //public IList<Book> GetBookHistoryAuditByUserId([ScopedService] AppDbContext context, string userId)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.TableName == "Book" && x.UserId == userId).OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Book> bookHistoryList = new List<Book>();



        //    foreach (var book in auditBookLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Book>(book.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(book.PrimaryKey);
        //        var timeOfUpdateColum = book.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditBookId = dezerilizedPKColum.Id;
        //        var activeBook = context.Books.FirstOrDefault(x => x.Id == auditBookId);

        //        //Creating a Book Object             
        //        Book auditBook = new Book();


        //        // Setting the props for audit-book object
        //        auditBook.Id = auditBookId;

        //        if (activeBook.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditBook.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditBook.Name = activeBook.Name; }
        //        if (activeBook.Genre != dezerilizedOldValueColum.Genre && dezerilizedOldValueColum.Genre is not null)
        //        {
        //            auditBook.Genre = dezerilizedOldValueColum.Genre;
        //        }
        //        else { auditBook.Genre = activeBook.Genre; }
        //        if (activeBook.AuthorId != dezerilizedOldValueColum.AuthorId && dezerilizedOldValueColum.AuthorId != 0)
        //        {
        //            auditBook.AuthorId = dezerilizedOldValueColum.AuthorId;
        //        }
        //        else
        //        {
        //            auditBook.AuthorId = activeBook.AuthorId;
        //        }

        //        bookHistoryList.Add(auditBook);
        //    }

        //    return bookHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Book from the auditlog.")]
        //public IList<Book> GetBookHistoryAuditByClickBatchId([ScopedService] AppDbContext context, int clickBatchId)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.TableName == "Book" && x.ClickBatchId == clickBatchId).OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Book> bookHistoryList = new List<Book>();



        //    foreach (var book in auditBookLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Book>(book.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(book.PrimaryKey);
        //        var timeOfUpdateColum = book.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditBookId = dezerilizedPKColum.Id;
        //        var activeBook = context.Books.FirstOrDefault(x => x.Id == auditBookId);

        //        //Creating a Book Object             
        //        Book auditBook = new Book();


        //        // Setting the props for audit-book object
        //        auditBook.Id = auditBookId;

        //        if (activeBook.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditBook.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditBook.Name = activeBook.Name; }
        //        if (activeBook.Genre != dezerilizedOldValueColum.Genre && dezerilizedOldValueColum.Genre is not null)
        //        {
        //            auditBook.Genre = dezerilizedOldValueColum.Genre;
        //        }
        //        else { auditBook.Genre = activeBook.Genre; }
        //        if (activeBook.AuthorId != dezerilizedOldValueColum.AuthorId && dezerilizedOldValueColum.AuthorId != 0)
        //        {
        //            auditBook.AuthorId = dezerilizedOldValueColum.AuthorId;
        //        }
        //        else
        //        {
        //            auditBook.AuthorId = activeBook.AuthorId;
        //        }

        //        bookHistoryList.Add(auditBook);
        //    }

        //    return bookHistoryList;
        //}


        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Book from the auditlog.")]
        //public IList<Book> GetBookHistoryAuditByClickBatchIdAndUserId([ScopedService] AppDbContext context, int clickBatchId, string userId)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.TableName == "Book" && x.ClickBatchId == clickBatchId && x.UserId == userId).OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Book> bookHistoryList = new List<Book>();



        //    foreach (var book in auditBookLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Book>(book.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(book.PrimaryKey);
        //        var timeOfUpdateColum = book.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditBookId = dezerilizedPKColum.Id;
        //        var activeBook = context.Books.FirstOrDefault(x => x.Id == auditBookId);

        //        //Creating a Book Object             
        //        Book auditBook = new Book();


        //        // Setting the props for audit-book object
        //        auditBook.Id = auditBookId;

        //        if (activeBook.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditBook.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditBook.Name = activeBook.Name; }
        //        if (activeBook.Genre != dezerilizedOldValueColum.Genre && dezerilizedOldValueColum.Genre is not null)
        //        {
        //            auditBook.Genre = dezerilizedOldValueColum.Genre;
        //        }
        //        else { auditBook.Genre = activeBook.Genre; }
        //        if (activeBook.AuthorId != dezerilizedOldValueColum.AuthorId && dezerilizedOldValueColum.AuthorId != 0)
        //        {
        //            auditBook.AuthorId = dezerilizedOldValueColum.AuthorId;
        //        }
        //        else
        //        {
        //            auditBook.AuthorId = activeBook.AuthorId;
        //        }

        //        bookHistoryList.Add(auditBook);
        //    }

        //    return bookHistoryList;
        //}

        ///**************************/
        ///* AUDIT AS OBJECTS => AUTHORS */
        ///**************************/


        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Authors from auditlog.")]
        //public IList<Author> GetAuthorHistoryAudit([ScopedService] AppDbContext context)
        //{

        //    var auditAuthorLog = context.AuditLogs.Where(x => x.TableName == "Author").OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Author> authorHistoryList = new List<Author>();



        //    foreach (var auhtor in auditAuthorLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Author>(auhtor.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(auhtor.PrimaryKey);
        //        var timeOfUpdateColum = auhtor.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditAuthorId = dezerilizedPKColum.Id;
        //        var activeAuthor = context.Authors.FirstOrDefault(x => x.Id == auditAuthorId);

        //        //Creating a Book Object             
        //        Author auditAuthor = new Author();


        //        // Setting the props for audit-author object
        //        auditAuthor.Id = auditAuthorId;

        //        if (activeAuthor.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditAuthor.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditAuthor.Name = activeAuthor.Name; }
        //        if (activeAuthor.Age != dezerilizedOldValueColum.Age /*&& dezerilizedOldValueColum.Age is not null*/)
        //        {
        //            auditAuthor.Age = dezerilizedOldValueColum.Age;
        //        }
        //        else { auditAuthor.Age = activeAuthor.Age; }


        //        authorHistoryList.Add(auditAuthor);
        //    }

        //    return authorHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Authors from auditlog.")]
        //public IList<Author> GetAuthorHistoryAuditUserId([ScopedService] AppDbContext context, string userId)
        //{

        //    var auditAuthorLog = context.AuditLogs.Where(x => x.TableName == "Author" && x.UserId == userId).OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Author> authorHistoryList = new List<Author>();



        //    foreach (var auhtor in auditAuthorLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Author>(auhtor.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(auhtor.PrimaryKey);
        //        var timeOfUpdateColum = auhtor.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditAuthorId = dezerilizedPKColum.Id;
        //        var activeAuthor = context.Authors.FirstOrDefault(x => x.Id == auditAuthorId);

        //        //Creating a Book Object             
        //        Author auditAuthor = new Author();


        //        // Setting the props for audit-author object
        //        auditAuthor.Id = auditAuthorId;

        //        if (activeAuthor.Name != dezerilizedOldValueColum.Name /*&& dezerilizedOldValueColum.Name is not null*/)
        //        {
        //            auditAuthor.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditAuthor.Name = activeAuthor.Name; }
        //        if (activeAuthor.Age != dezerilizedOldValueColum.Age /*&& dezerilizedOldValueColum.Age is not null*/)
        //        {
        //            auditAuthor.Age = dezerilizedOldValueColum.Age;
        //        }
        //        else { auditAuthor.Age = activeAuthor.Age; }


        //        authorHistoryList.Add(auditAuthor);
        //    }

        //    return authorHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Authors from auditlog.")]
        //public IList<Author> GetAuthorHistoryAuditClickBatchId([ScopedService] AppDbContext context, int clickBatchId)
        //{

        //    var auditAuthorLog = context.AuditLogs.Where(x => x.TableName == "Author" && x.ClickBatchId == clickBatchId).OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Author> authorHistoryList = new List<Author>();



        //    foreach (var auhtor in auditAuthorLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Author>(auhtor.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(auhtor.PrimaryKey);
        //        var timeOfUpdateColum = auhtor.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditAuthorId = dezerilizedPKColum.Id;
        //        var activeAuthor = context.Authors.FirstOrDefault(x => x.Id == auditAuthorId);

        //        //Creating a Book Object             
        //        Author auditAuthor = new Author();


        //        // Setting the props for audit-author object
        //        auditAuthor.Id = auditAuthorId;

        //        if (activeAuthor.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditAuthor.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditAuthor.Name = activeAuthor.Name; }
        //        if (activeAuthor.Age != dezerilizedOldValueColum.Age /*&& dezerilizedOldValueColum.Age is not null*/)
        //        {
        //            auditAuthor.Age = dezerilizedOldValueColum.Age;
        //        }
        //        else { auditAuthor.Age = activeAuthor.Age; }


        //        authorHistoryList.Add(auditAuthor);
        //    }

        //    return authorHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the Authors from auditlog.")]
        //public IList<Author> GetAuthorHistoryAuditClickBatchIdAndUserId([ScopedService] AppDbContext context, int clickBatchId, string userId)
        //{

        //    var auditAuthorLog = context.AuditLogs.Where(x => x.TableName == "Author" && x.ClickBatchId == clickBatchId && x.UserId == userId).OrderByDescending(x => x.TimeOfUpdate);

        //    IList<Author> authorHistoryList = new List<Author>();



        //    foreach (var auhtor in auditAuthorLog)
        //    {
        //        //Dezerlizing Json String
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<Author>(auhtor.OldValues);
        //        var dezerilizedPKColum = JsonConvert.DeserializeObject<Book>(auhtor.PrimaryKey);
        //        var timeOfUpdateColum = auhtor.TimeOfUpdate;

        //        //Getting the matching book from Auditlog/Book-table
        //        var auditAuthorId = dezerilizedPKColum.Id;
        //        var activeAuthor = context.Authors.FirstOrDefault(x => x.Id == auditAuthorId);

        //        //Creating a Book Object             
        //        Author auditAuthor = new Author();


        //        // Setting the props for audit-author object
        //        auditAuthor.Id = auditAuthorId;

        //        if (activeAuthor.Name != dezerilizedOldValueColum.Name && dezerilizedOldValueColum.Name is not null)
        //        {
        //            auditAuthor.Name = dezerilizedOldValueColum.Name;
        //        }
        //        else { auditAuthor.Name = activeAuthor.Name; }
        //        if (activeAuthor.Age != dezerilizedOldValueColum.Age /*&& dezerilizedOldValueColum.Age is not null*/)
        //        {
        //            auditAuthor.Age = dezerilizedOldValueColum.Age;
        //        }
        //        else { auditAuthor.Age = activeAuthor.Age; }


        //        authorHistoryList.Add(auditAuthor);
        //    }

        //    return authorHistoryList;
        //}




        ///**************************/
        ///* AUDIT */
        ///**************************/

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable audit from auditlog.")]
        //public IList<Audit> GetAllAudit([ScopedService] AppDbContext context)
        //{

        //    var auditLog = context.AuditLogs.OrderByDescending(x => x.TimeOfUpdate);

        //    Console.WriteLine("HEEER COUNT " + auditLog.Count());

        //    IList<Audit> auditHistoryList = new List<Audit>();



        //    foreach (var item in auditLog)
        //    {

        //        Audit auditObj = new Audit();

        //        var jsonString = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.NewValues);
        //        int i = 0;
        //        foreach (KeyValuePair<string, string> kvp in jsonString)
        //        {
        //            i++;
        //            Console.WriteLine($"Key {i} is : {kvp.Key} while value {i} is : {kvp.Value}");
        //            auditObj.AffectedColumns = kvp.Key;
        //            auditObj.NewValues = kvp.Value;
        //        }

        //        //Setting the props for audit - book object

        //       auditObj.Id = item.Id;
        //       auditObj.Type = item.Type;
        //       auditObj.TableName = item.TableName;
        //        auditObj.TimeOfUpdate = item.TimeOfUpdate;
        //        //auditObj.OldValues = dezerilizedOldValueColum.OldValues;
        //       // auditObj.NewValues = item.NewValues;
        //       // auditObj.AffectedColumns = item.AffectedColumns;
        //        auditObj.PrimaryKey = item.PrimaryKey;
        //        auditObj.UserId = item.UserId;

        //        auditHistoryList.Add(auditObj);
        //    }

        //    return auditHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable audit from auditlog.")]
        //public IList<AuditChange> GetAllAudit([ScopedService] AppDbContext context)
        //{

        //    var auditLog = context.AuditLogs.OrderByDescending(x => x.TimeOfUpdate);

        //    Console.WriteLine("HEEER COUNT " + auditLog.Count());

        //    IList<AuditChange> auditHistoryList = new List<AuditChange>();



        //    foreach (var item in auditLog)
        //    {

        //        AuditChange auditObj = new AuditChange();

        //        var jsonString = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.NewValues);
        //        int i = 0;
        //        foreach (KeyValuePair<string, string> kvp in jsonString)
        //        {
        //            i++;
        //            Console.WriteLine($"Key {i} is : {kvp.Key} while value {i} is : {kvp.Value}");
        //            auditObj.AffectedColumns.Add(kvp.Key);
        //            auditObj.NewValues.Add(kvp.Value);
        //        }

        //        auditObj.Id = item.Id;
        //        auditObj.ActionType = item.Type;
        //        auditObj.TableName = item.TableName;
        //        auditObj.TimeOfUpdate = item.TimeOfUpdate;
        //        auditObj.PrimaryKey = item.PrimaryKey;
        //        auditObj.UserId = item.UserId;
        //        auditObj.TimeOfUpdate = item.TimeOfUpdate;
        //        auditObj.PrimaryKey = item.PrimaryKey;


        //        auditHistoryList.Add(auditObj);
        //    }

        //    return auditHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable audit from auditlog.")]
        //public IList<AuditChange> GetAllAudit([ScopedService] AppDbContext context)
        //{

        //    var auditLog = context.AuditLogs.Where(x => x.Type.Equals("Update")).OrderByDescending(x => x.TimeOfUpdate).ToList();



        //    IList<AuditChange> auditHistoryList = new List<AuditChange>();



        //    foreach (var item in auditLog)
        //    {
        //        var dezerilizedOldValueColum = JsonConvert.DeserializeObject<dynamic>(item.AffectedColumns);
        //        Console.WriteLine("HEEER " + dezerilizedOldValueColum);

        //        var audit = new AuditChange();
        //        foreach(var cw in dezerilizedOldValueColum)
        //        {
        //            audit.AffectedColumnsChanges.Add(cw);

        //        }

        //        audit.DateTimeStamp = item.TimeOfUpdate.ToString();
        //        //audit.AffectedColumnsChanges.AddRange(dezerilizedOldValueColum);
        //        //audit.UserId = item.UserId;
        //        //audit.ClickBatchId = item.ClickBatchId;
        //        //audit.Type = item.Type.ToString();
        //        //audit.TableName = item.TableName;
        //        //audit.TimeOfUpdate = DateTime.Now;
        //        //audit.PrimaryKey = item.PrimaryKey;
        //        //audit.NewValues = JsonConvert.DeserializeObject<Audit>(item.NewValues);
        //        //audit.AffectedColumns = JsonConvert.DeserializeObject<Audit>(item.AffectedColumns);
        //        auditHistoryList.Add(audit);


        //    }

        //    return auditHistoryList;
        //}

        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable sorted audit from auditlog.")]
        //public IList<Audit> GetAuditByUserId([ScopedService] AppDbContext context, string userId)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.UserId == userId);
        //    IList<Audit> bookHistoryList = new List<Audit>();



        //    foreach (var item in auditBookLog)
        //    {

        //        Audit auditObj = new Audit();


        //        // Setting the props for audit-book object
        //        auditObj.Id = item.Id;
        //        auditObj.Type = item.Type;
        //        auditObj.TableName = item.TableName;
        //        auditObj.TimeOfUpdate = item.TimeOfUpdate;
        //        auditObj.OldValues = item.OldValues;
        //        auditObj.NewValues = item.NewValues;
        //        auditObj.AffectedColumns = item.AffectedColumns;
        //        auditObj.PrimaryKey = item.PrimaryKey;
        //        auditObj.UserId = item.UserId;
        //        auditObj.ClickBatchId = item.ClickBatchId;



        //        bookHistoryList.Add(auditObj);
        //    }


        //    foreach (var book in auditBookLog)
        //    {
        //        Console.WriteLine("ID: " + book.UserId);
        //    }
        //    return bookHistoryList;
        //}


        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable sorted audit from auditlog.")]
        //public IList<Audit> GetAuditByClickBatchId([ScopedService] AppDbContext context, int clickBatchid)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.ClickBatchId == clickBatchid);
        //    IList<Audit> bookHistoryList = new List<Audit>();



        //    foreach (var item in auditBookLog)
        //    {

        //        Audit auditObj = new Audit();


        //        // Setting the props for audit-book object
        //        auditObj.Id = item.Id;
        //        auditObj.Type = item.Type;
        //        auditObj.TableName = item.TableName;
        //        auditObj.TimeOfUpdate = item.TimeOfUpdate;
        //        auditObj.OldValues = item.OldValues;
        //        auditObj.NewValues = item.NewValues;
        //        auditObj.AffectedColumns = item.AffectedColumns;
        //        auditObj.PrimaryKey = item.PrimaryKey;
        //        auditObj.UserId = item.UserId;
        //        auditObj.ClickBatchId = item.ClickBatchId;


        //        bookHistoryList.Add(auditObj);
        //    }

        //    return bookHistoryList;
        //}



        //[UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription("Gets the queryable sorted audit from auditlog.")]
        //public IList<Audit> GetAuditByClickBatchIdAndUserId([ScopedService] AppDbContext context, int clickBatchid, string userId)
        //{

        //    var auditBookLog = context.AuditLogs.Where(x => x.ClickBatchId == clickBatchid && x.UserId == userId);
        //    IList<Audit> bookHistoryList = new List<Audit>();



        //    foreach (var item in auditBookLog)
        //    {

        //        Audit auditObj = new Audit();


        //        // Setting the props for audit-book object
        //        auditObj.Id = item.Id;
        //        auditObj.Type = item.Type;
        //        auditObj.TableName = item.TableName;
        //        auditObj.TimeOfUpdate = item.TimeOfUpdate;
        //        auditObj.OldValues = item.OldValues;
        //        auditObj.NewValues = item.NewValues;
        //        auditObj.AffectedColumns = item.AffectedColumns;
        //        auditObj.PrimaryKey = item.PrimaryKey;
        //        auditObj.UserId = item.UserId;
        //        auditObj.ClickBatchId = item.ClickBatchId;


        //        bookHistoryList.Add(auditObj);
        //    }

        //    return bookHistoryList;
        //}
    }
}
