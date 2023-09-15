using BookAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nito.AsyncEx;
using System.Data;
using System.Reflection.Metadata;

namespace BookAPI.BookService
{
    public class BookService : IBookService
    {
        private readonly BookContext _bookDB;
        public BookService(BookContext _db)
        {
            _bookDB = _db;
        }

        public async Task<List<Book>> GetBookDetails()
        {
            List<Book> book = new List<Book>();
            string procedureName = "SP_Get_bookDdetails";
            //book = await _bookDB.Add<Book>().FromSql(procedureName).ToListAsync();
            using (var command = _bookDB.Database.GetDbConnection().CreateCommand())
            {
                if (_bookDB.Database.GetDbConnection().State != ConnectionState.Open)
                    _bookDB.Database.GetDbConnection().Open();

                command.CommandText = procedureName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                command.CommandTimeout = 600;
                try
                {
                    var _book = await command.ExecuteReaderAsync();

                    while (_book.Read())
                    {
                        Book Book = new Book();
                        Book.BookID = Convert.ToInt32(_book["BookID"]);
                        Book.Title = Convert.ToString(_book["Title"]);
                        Book.Publisher = Convert.ToString(_book["Publisher"]);
                        Book.AutherLastName = Convert.ToString(_book["AutherLastName"]);
                        Book.AutherFirstName = Convert.ToString(_book["AutherFirstName"]);
                        Book.price = Convert.ToDecimal(_book["Price"]);
                        Book.yearofPublication = Convert.ToInt32(_book["yearofPublication"]);
                        Book.Edition = Convert.ToInt32(_book["Edition"]);
                        Book.MLACitiation = Convert.ToString(_book["MLACitiation"]);
                        Book.PlaceofPublication = Convert.ToString(_book["PlaceofPublication"]);
                        Book.URLs = Convert.ToString(_book["SiteURL"]);
                        book.Add(Book);
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return book;
        }

        public async Task<List<Book>> GetBookDetailsOrderByAuther()
        {
            List<Book> book = new List<Book>();
            string procedureName = "SP_Get_bookDdetailsOrderByAuther";
            
            using (var command = _bookDB.Database.GetDbConnection().CreateCommand())
            {
                if (_bookDB.Database.GetDbConnection().State != ConnectionState.Open)
                    _bookDB.Database.GetDbConnection().Open();

                command.CommandText = procedureName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 600;
                
                try
                {
                    var _book = await command.ExecuteReaderAsync();

                    while (_book.Read())
                    {
                        Book Book = new Book();
                        Book.BookID = Convert.ToInt32(_book["BookID"]);
                        Book.Title = Convert.ToString(_book["Title"]);
                        Book.Publisher = Convert.ToString(_book["Publisher"]);
                        Book.AutherLastName = Convert.ToString(_book["AutherLastName"]);
                        Book.AutherFirstName = Convert.ToString(_book["AutherFirstName"]);
                        Book.price = Convert.ToDecimal(_book["Price"]);
                        Book.yearofPublication = Convert.ToInt32(_book["yearofPublication"]);
                        Book.Edition = Convert.ToInt32(_book["Edition"]);
                        Book.MLACitiation = Convert.ToString(_book["MLACitiation"]);
                        Book.PlaceofPublication = Convert.ToString(_book["PlaceofPublication"]);
                        Book.URLs = Convert.ToString(_book["SiteURL"]);
                        book.Add(Book);
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return book;
        }

        public decimal TotalBookPrice()
        {
            decimal total = 0;

            try
            {
                var result = AsyncContext.Run(GetBookDetails);
                List<Book> _book = result;

                if (_book != null)
                {
                    total = _book.Sum(x => Convert.ToDecimal(x.price));
                }
            }
            catch (Exception ex)
            {

            }
            return total;
        }

        public async Task<int> SaveBookDetails(List<BookVM> books)
        {
            int result = 0;
            string procedureName = "SP_SaveBookDetaiils";

            using (var command = _bookDB.Database.GetDbConnection().CreateCommand())
            {
                if (_bookDB.Database.GetDbConnection().State != ConnectionState.Open)
                    _bookDB.Database.GetDbConnection().Open();

                command.CommandText = procedureName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 600;

                try
                {
                    foreach (var book in books)
                    {
                        command.Parameters.Add(new SqlParameter("@Title", book.Title));
                        command.Parameters.Add(new SqlParameter("@AutherID", book.AutherID));
                        command.Parameters.Add(new SqlParameter("@PublisherID", book.PublisherID));
                        command.Parameters.Add(new SqlParameter("@Price", book.price));
                        command.Parameters.Add(new SqlParameter("@yearofPublication", book.yearofPublication));
                        command.Parameters.Add(new SqlParameter("@PlaceofPublication", book.PlaceofPublication));
                        command.Parameters.Add(new SqlParameter("@Edition", book.Edition));
                        command.Parameters.Add(new SqlParameter("@MLACitiation", book.MLACitiation));
                        command.Parameters.Add(new SqlParameter("@SiteURL", book.URLs));

                        result = await command.ExecuteNonQueryAsync();
                        command.Parameters.Clear();
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }

            return result;
        }
    }
}
