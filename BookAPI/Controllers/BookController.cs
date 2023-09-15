using BookAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        
        [Route("GetBookDetails")]
        public async Task<List<Book>> GetBookDetails()
        {
            return await _bookService.GetBookDetails();
        }
        [HttpGet]
        [Route("GetBookDetailsOrderByAuther")]
        public async Task<List<Book>> GetBookDetailsOrderByAuther()
        {
            return await _bookService.GetBookDetails();
        }

        [HttpGet]
        [Route("TotalBookPrice")]

        public decimal TotalBookPrice()
        { 
        return _bookService.TotalBookPrice();
        }

        [HttpPost]
        [Route("SaveBookDetails")]

        public async Task<ActionResult> SaveBookDetails(List<BookVM> books)
        {
            await _bookService.SaveBookDetails(books);
            return Ok();
        }
    }
}
