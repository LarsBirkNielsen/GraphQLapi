using Configuration;
using GraphQL.AuditLibrary;
using GraphQL.AuditLibrary.Service;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data
{
    public class AppDbContext : AuditableIdentityContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options, IUserService userService)
            : base(options, userService)
        {

        }

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<Author> Authors { get; set; } = default!;

    }
}
