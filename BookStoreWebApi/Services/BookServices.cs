using Book.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Bookstore>> GetBooksSortedByPublisherAsync()
        {
            return await _bookRepository.GetBooksSortedByPublisherAsync();
        }

        public async Task<IEnumerable<Bookstore>> GetBooksSortedByAuthorAsync()
        {
            return await _bookRepository.GetBooksSortedByAuthorAsync();
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            return await _bookRepository.GetTotalPriceAsync();
        }

        public async Task AddBooksAsync(IEnumerable<Bookstore> books)
        {
            await _bookRepository.AddBooksAsync(books);
        }
    }
}
