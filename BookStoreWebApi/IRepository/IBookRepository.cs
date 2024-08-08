using Book.Models;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Bookstore>> GetBooksSortedByPublisherAsync();
        Task<IEnumerable<Bookstore>> GetBooksSortedByAuthorAsync();
        Task<decimal> GetTotalPriceAsync();
        Task AddBooksAsync(IEnumerable<Bookstore> books);
    }
}

