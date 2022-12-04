using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using GraphQL.AuditLibrary.Service;
using GraphQL.Data;
using GraphQL.GraphQL;
using GraphQL.GraphQL.Authors;
using GraphQL.GraphQL.Books;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;




var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
var config = ConfigurationModel.InitializeConfig();
builder.Services.AddSingleton(config);
builder.Services.AddEntityFrameworkSqlServer();

//builder.Services.AddControllers();

//Add Application Db Context options
builder.Services.AddDbContextFactory<AppDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("SqlServer")));





// Register custom services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services

    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<BookType>()
    .AddType<AuthorType>()
    .AddSorting()
    .AddFiltering();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();
