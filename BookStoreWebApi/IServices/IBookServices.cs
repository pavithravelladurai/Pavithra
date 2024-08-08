using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Models;

namespace BookStoreAPI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Bookstore>> GetBooksSortedByPublisherAsync();
        Task<IEnumerable<Bookstore>> GetBooksSortedByAuthorAsync();
        Task<decimal> GetTotalPriceAsync();
        Task AddBooksAsync(IEnumerable<Bookstore> books);
    }
}

