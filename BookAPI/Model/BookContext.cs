using Microsoft.EntityFrameworkCore;

namespace BookAPI.Model
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DbConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
