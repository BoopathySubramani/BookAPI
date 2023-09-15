using BookAPI.Model;

namespace BookAPI
{
    public interface IBookService
    {
        Task<List<Book>> GetBookDetails();
        Task<List<Book>> GetBookDetailsOrderByAuther();
        Decimal TotalBookPrice();
        Task<int> SaveBookDetails(List<BookVM> books);
    }
}
