using Microsoft.EntityFrameworkCore;
using Book.Models;
using BookStoreAPI.Repository;
using BookStoreWebApi.Models;


namespace BookStoreAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly masterContext _context;

        public BookRepository(masterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book.Models.Bookstore>> GetBooksSortedByPublisherAsync()
        {
            // Fetch the list of Book_Detail from the database
            var bookPublisherDetails = await _context.Book_Details
                .FromSqlRaw("EXEC GetBooksSortedByPublisherAuthorTitle")
                .ToListAsync();

            // Map Book_Detail to Bookstore
            var bookSortedByPublisher = bookPublisherDetails.Select(b => new Bookstore
            {
                // Map properties from Book_Detail to Bookstore
                Publisher = b.Publisher,
                AuthorFirstName = b.Author_FirstName,
                AuthorLastName = b.Author_LastName,
                Title = b.Title
            }).ToList();

            return bookSortedByPublisher;
        }

        public async Task<IEnumerable<Bookstore>> GetBooksSortedByAuthorAsync()
        {
            var bookAuthorDetails = await _context.Book_Details
                          .FromSqlRaw("EXEC GetBooksSortedByAuthorTitle")
                          .ToListAsync();

            var BooksSortedByAuthor = bookAuthorDetails.Select(b => new Bookstore
            {
                // Map properties from Book_Detail to bookAuthorDetails
                AuthorFirstName = b.Author_FirstName,
                AuthorLastName = b.Author_LastName,
                Title = b.Title
            }).ToList();

            return BooksSortedByAuthor;
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            return (decimal)await _context.Book_Details.SumAsync(b => b.Price);
        }

        public async Task AddBooksAsync(IEnumerable<Bookstore> books)
        {
            Book_Detail bookDetail = new Book_Detail
            {
                Publisher = books.ToList().First().Publisher,
                Author_LastName = books.ToList().First().AuthorLastName,
                Author_FirstName = books.ToList().First().AuthorFirstName,
                Title = books.ToList().First().Title,
                Price = books.ToList().First().Price
            };
            await _context.Book_Details.AddAsync(bookDetail);
            await _context.SaveChangesAsync();
        }
    }
}