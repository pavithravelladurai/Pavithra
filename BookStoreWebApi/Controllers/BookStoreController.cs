using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Services;
using Book.Models;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookStoreController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("SortedListByPublisher")]
        public async Task<ActionResult<IEnumerable<Bookstore>>> GetBooksSortedByPublisher()
        {
            var books = await _bookService.GetBooksSortedByPublisherAsync();
            return Ok(books);
        }

        [HttpGet("SortedListByAuthor")]
        public async Task<ActionResult<IEnumerable<Bookstore>>> GetBooksSortedByAuthor()
        {
            var books = await _bookService.GetBooksSortedByAuthorAsync();
            return Ok(books);
        }

        [HttpGet("TotalPriceDetails")]
        public async Task<ActionResult<decimal>> GetTotalPrice()
        {
            var totalPrice = await _bookService.GetTotalPriceAsync();
            return Ok(totalPrice);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooks([FromBody] IEnumerable<Bookstore> books)
        {
            await _bookService.AddBooksAsync(books);
            return CreatedAtAction(nameof(GetBooksSortedByPublisher), null);
        }
    }
}
