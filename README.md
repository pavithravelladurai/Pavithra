
## PreRequest :

* Visual Studio 2022 – > .Net Core 8
* SQL Server Management Studio 2022
* Entity Framework


## BookStoreController

 **1. Overview**
 >The BookStoreController is an ASP.NET Core Web API controller designed to manage book-related operations in a bookstore application. It uses dependency injection to interact with a service layer for bookstore operations.
 
 **Dependencies**
 
* **IBookService:** Service interface providing methods to manage books.

* **Bookstore:** Model representing a book in the bookstore.

**Attributes:**
* **[Route("api/[controller]")]:** Defines the base route for the controller, replacing [controller] with BookStore.

* **[ApiController]:** Indicates that this class is an API controller, enabling automatic model validation and response formatting.

**Constructor:**
* **BookStoreController(IBookService bookService):** Initializes the controller with an IBookService instance for accessing book data.

**Methods:**

**1. GetBooksSortedByPublisher()**

**Route:** api/bookstore/SortedListByPublisher

**HTTP Method:** GET

**Description:** Retrieves a list of books sorted by publisher.

**Returns:** ActionResult<IEnumerable<Bookstore>>

**Service Call:** GetBooksSortedByPublisherAsync()

**2.  GetBooksSortedByAuthor()**

**Route:** api/bookstore/SortedListByAuthor

**HTTP Method:** GET

**Description:** Retrieves a list of books sorted by author.

**Returns:** ActionResult<IEnumerable<Bookstore>>

**Service Call:** GetBooksSortedByAuthorAsync()

**3. GetTotalPrice()**

**Route:** api/bookstore/TotalPriceDetails

**HTTP Method:** GET

**Description:** Retrieves the total price of all books.

**Returns:** ActionResult<decimal>

**Service Call:** GetTotalPriceAsync() 


**4. AddBooks()**

**Route:** api/bookstore

**HTTP Method:** POST

**Description:** Adds a collection of books to the system.

**Parameters:** IEnumerable<Bookstore> (from request body)

**Returns:** IActionResult
Service Call: AddBooksAsync()

## IBookService  :
  IBookService interface defines the essential operations required to manage books within a bookstore application. It abstracts the underlying implementation, meaning that different implementations can exist for this service, such as one that interacts with a database, another that interacts with a web API, or even a mock implementation for testing.

* **Separation of Concerns:** By defining these operations in an interface, the business logic (like sorting books and calculating total price) is separated from the controller logic, promoting cleaner, more maintainable code.
* **Dependency Injection:** This interface allows for dependency injection, where the concrete implementation of IBookService can be injected into other components (like controllers) that depend on these operations. This promotes flexibility and testability in the application.

```
public interface IBookService
{ 
Task<IEnumerable<Bookstore>> GetBooksSortedByPublisherAsync();
Task<IEnumerable<Bookstore>> GetBooksSortedByAuthorAsync();
Task<decimal> GetTotalPriceAsync();
Task AddBooksAsync(IEnumerable<Bookstore> books); 
} 
```
## BookService :
**Initialize BookService**

* Input: IBookRepository instance injected into BookService
* Operation: The constructor sets the _bookRepository field with the provided repository instance

**Get Books Sorted by Publisher (Method Call)**
* Method: GetBooksSortedByPublisherAsync
* Operation: Calls _bookRepository.GetBooksSortedByPublisherAsync()
* Output: A collection of books sorted by publisher.
  
**Get Books Sorted by Author (Method Call)**
* Method: GetBooksSortedByAuthorAsync
* Operation: Calls _bookRepository.GetBooksSortedByAuthorAsync()
* Output: A collection of books sorted by author

**Get Total Price (Method Call)**
* Method: GetTotalPriceAsync
* Operation: Calls _bookRepository.GetTotalPriceAsync()
* Output: A decimal value representing the total price of all books
  
**Add Books (Method Call)**
* Method: AddBooksAsync
* Input: A collection of Bookstore objects
* Operation: Calls _bookRepository.AddBooksAsync(books)
* Output: Books are added to the repository

## IBookRepository :
1. **Client Request:**

* The client (e.g., service) calls one of the methods defined in the IBookRepository interface, such as GetBooksSortedByPublisherAsync().
2. **Interface Method Call:**
* The client does not interact directly with the database but through the methods defined in the IBookRepository interface.
3. **Concrete Implementation (BookRepository):**
* BookRepository which implements the IBookRepository interface handles the logic for data retrieval or manipulation.
* It interacts with the database using the appropriate method logic (like executing stored procedures or queries).
4. **Database Interaction:**
* The BookRepository class interacts with the database to execute SQL commands, retrieve sorted lists, calculate total prices, or insert new records.
5. **Return Data to Client:**
* The processed data is returned back to the client through the IBookRepository method implementation.
```
public interface IBookRepository
{ 
       Task<IEnumerable<Bookstore>> GetBooksSortedByPublisherAsync();
        Task<IEnumerable<Bookstore>> GetBooksSortedByAuthorAsync();
        Task<decimal> GetTotalPriceAsync();
        Task AddBooksAsync(IEnumerable<Bookstore> books);
} 
```
## BookRepository
1. **Client Request:**
* The client ( a controller) calls one of the methods in the BookRepository, such as GetBooksSortedByPublisherAsync(), GetBooksSortedByAuthorAsync(), GetTotalPriceAsync(), AddBooksAsync().
2. **BookRepository Method Execution:**
* **Fetching Data:**
     * The repository method executes a stored procedure using FromSqlRaw to retrieve sorted data from the database.
  * The data retrieved from the database (Book_Detail) is mapped to the Bookstore model.
3. **Adding Data:**
 * When adding books, the method maps the Bookstore model to Book_Detail and inserts the new record into the database.
4. **Entity Framework Core:**
* The repository uses Entity Framework Core to execute SQL queries, manage transactions, and map data between the database and the application models.
5. **Database Interaction:**
* The database stores the Book_Detail records. It responds to queries and commands initiated by the repository, such as fetching sorted lists or saving new records.
6. **Response to Client:**
 * The repository returns the processed data back to the client (e.g., the controller), which then sends the appropriate HTTP response.
## DIResolver 
* **DIResolver Class:** Contains a static method ConfigureServices that registers various services and repositories into the DI container.
* **IServiceCollection:** The collection where services and repositories are registered. This is passed as an argument to the ConfigureServices method.
* **HttpContextAccessor:** Registered as a transient service. This allows access to the HTTP context at the view level.
* **IBookService:** A service interface for handling business logic related to books. It is registered as a scoped service with its implementation BookService.
* **IBookRepository:** A repository interface for handling data access related to books. It is also registered as a scoped service with its implementation BookRepository.

## Entity Framework :
Entity Framework (EF) is an ORM (Object-Relational Mapper) that allows developers to work with databases using .NET objects. It abstracts the database interactions, making it easier to connect, query, and manage data in a relational database. 
1. **Application Layer:** This includes controllers, services, Repositories that interacts with the data layer.
2. **DbContext:** The core class in Entity Framework that manages the database connection, configures the model, and handles querying and saving data.
3. **DbSet:** Represents a collection of entities (tables) in the database. It is used to query and save instances of the entity type.
4. **Database:** The actual relational database where data is stored.
```
CREATE PROCEDURE GetBooksSortedByPublisherAuthorTitle
AS
BEGIN
    SELECT * FROM Book_Details
    ORDER BY Publisher, Author_LastName, Author_FirstName, Title;
END
------------------------------------------------
CREATE PROCEDURE GetBooksSortedByAuthorTitle
AS
BEGIN
    SELECT * FROM Book_Details
    ORDER BY Author_LastName, Author_FirstName, Title;
END
------------------------------------------------
CREATE PROCEDURE GetTotalPrice
AS
BEGIN
    SELECT SUM(Price) AS TotalPrice FROM Book_Details;
END
```
