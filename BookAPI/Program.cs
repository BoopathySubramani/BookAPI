using BookAPI.BookService;
using BookAPI;
using BookAPI.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("BookDB");
builder.Services.AddDbContext<BookContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<IBookService, BookService>();
//builder.Services.AddDbContext<BookContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
