using Library_magmt.DTO;
using Library_magmt.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public string URI = Environment.GetEnvironmentVariable("cosmos-url");
        public string PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
        public string DatabaseName = Environment.GetEnvironmentVariable("database-name");
        public string ContainerName = Environment.GetEnvironmentVariable("container-name");

        public readonly Container _container;
        public BookController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            try
            {
                Book bookEntity=ToBookEntity(bookModel);

                Book response = await _container.CreateItemAsync(bookEntity);

                bookModel = ToBookModel(response);

                return Ok("Book Added Successfull !!! ");

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpGet]
        public IActionResult GetBookByBookName(string bookName)
        {
            Book book=_container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookName == bookName).AsEnumerable().FirstOrDefault();
            
            if (book != null)
            {
                BookModel bookModel = ToBookModel(book);
                return Ok(bookModel);
            }
            else
            {
                return Ok("No Book Found !!!");
            }       
        }

        [HttpGet]
        public IActionResult GetBooksByAuthor(string author)
        {
            try
            {
                var bookList = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookAuthor == author).AsEnumerable().ToList();
               
                if (bookList != null)
                {
                    List<BookModel> books = new List<BookModel>();
                    foreach (var book in bookList)
                    {
                        BookModel bookModel = ToBookModel(book);
                        books.Add(bookModel);
                    }

                    return Ok(books);
                }
                else
                {
                    return Ok("No Books Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed "+ex);
            }
        }

        [HttpGet]
        public IActionResult GetBooksByDomain(string domain)
        {
            try
            {
                var bookList = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookDomain==domain).AsEnumerable().ToList();
                if (bookList != null)
                {
                    List<BookModel> books = new List<BookModel>();
                    foreach (var book in bookList)
                    {
                        BookModel bookModel = ToBookModel(book);
                        books.Add(bookModel);
                    }

                    return Ok(books);
                }
                else
                {
                    return Ok("No Books Found !!!");
                };
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed " +ex);
            }
        }

        [HttpGet]
        public IActionResult ShowAllBooks()
        {
            try
            {
                var bookList=_container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.Active == true).AsEnumerable().ToList();
                if (bookList != null)
                {
                    List<BookModel> books = new List<BookModel>();
                    foreach (var book in bookList)
                    {
                        BookModel bookModel = ToBookModel(book);
                        books.Add(bookModel);
                    }

                    return Ok(books);
                }
                else
                {
                    return Ok("No Books Found !!!" );
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed "+ex);
            }
        }

        [HttpGet]
        public IActionResult ShowAvailableBooks()
        {
            try
            {
                var bookList = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.Active==true && q.Archieved==false).AsEnumerable().ToList();
                if (bookList != null)
                {
                    List<BookModel> books = new List<BookModel>();
                    foreach (var book in bookList)
                    {
                        BookModel bookModel = ToBookModel(book);
                        books.Add(bookModel);
                    }

                    return Ok(books);
                }
                else
                {
                    return Ok("No Books Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed " +ex);
            }
        }


        [HttpGet]
        public IActionResult ShowBorrowedBooks()
        {
            try
            {
                var bookList = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.Active == true && q.Archieved == true).AsEnumerable().ToList();
                if (bookList != null)
                {
                    List<BookModel> books = new List<BookModel>();
                    foreach (var book in bookList)
                    {
                        BookModel bookModel = ToBookModel(book);
                        books.Add(bookModel);
                    }

                    return Ok(books);
                }
                else
                {
                    return Ok("No Books Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed "+ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookModel bookModel)
        {
            var existingBook=_container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookId == bookModel.BookId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            if (existingBook != null)
            {
                existingBook.BookName = bookModel.BookName;
                existingBook.BookAuthor = bookModel.BookAuthor;
                existingBook.BookDomain = bookModel.BookDomain;
                existingBook.Version++;
                existingBook.UpdatedOn = DateTime.Now;
                existingBook.UpdatedByName = "Kumar";
                existingBook.UpdatedBy = "Kumar's UId";

                try
                {
                    var responseBook = await _container.UpsertItemAsync(existingBook);
                    if (responseBook.StatusCode == System.Net.HttpStatusCode.OK || responseBook.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return Ok("Book Issued Successfully !!!");
                    }
                    else
                    {
                        return BadRequest("Failed to Issue Book !!!");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            return BadRequest("Book Not Found !!!");
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            var existingBook = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookId == bookId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            if (existingBook != null)
            {
                try
                {
                    existingBook.Active = false;
                    await _container.ReplaceItemAsync(existingBook, existingBook.Id);
                    return Ok("Book Deleted Successfully !!!");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex);
                }
                
            }
            else
            {
                return Ok("Book Not Found !!!");
            }
        }

        [HttpGet]
        public IActionResult ShowAllBooksTransaction()
        {
            try
            {
                var historyList = _container.GetItemLinqQueryable<BookIssue>(true).Where(q => q.DocumentType == "bookReturn" && q.Active == true).AsEnumerable().ToList();
                if (historyList != null)
                {
                    List<BookReturnModel> history = new List<BookReturnModel>();
                    foreach (var book in historyList)
                    {
                        BookReturnModel bookReturnModel = ToBookReturnModel(book);
                        history.Add(bookReturnModel);
                    }

                    return Ok(history);
                }
                else
                {
                    return Ok("No Transaction Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed " + ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> IssueBook(string PRN , string bookId)
        {
            try
            {
                var student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.PRN == PRN && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                var book = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookId == bookId && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                
                if (student != null && book != null && book.Archieved == false)
                {
                    BookIssue bookIssue = new BookIssue();

                    bookIssue.BookId = bookId;
                    bookIssue.PRN = PRN;
                    bookIssue.IssueBook=false;
                    bookIssue.IssueDate=DateTime.Now;
                    bookIssue.ReturnBook = true;
                    bookIssue.ReturnDate = DateTime.Now.AddDays(30);

                    bookIssue.Id = Guid.NewGuid().ToString();
                    bookIssue.UId=bookIssue.Id;
                    bookIssue.DocumentType="bookReturn";
                    
                    bookIssue.CreatedOn = DateTime.Now;
                    bookIssue.CreatedByName = "Kumar";
                    bookIssue.CreatedBy = "Kumar's UId";

                    bookIssue.UpdatedOn = DateTime.Now;
                    bookIssue.UpdatedByName = "";
                    bookIssue.UpdatedBy = "";

                    bookIssue.Version = 1;
                    bookIssue.Active = true;
                    bookIssue.Archieved = false;



                    var response = await _container.CreateItemAsync(bookIssue);
                    if (response != null)
                    {
                        book.Archieved = true;
                        await _container.ReplaceItemAsync(book, book.Id);
                        return Ok("Book Issued Successfully !!!");
                    }
                    else
                    {
                        return BadRequest("Failed to Issue Book !!!");
                    }
                }
                else
                {
                    return BadRequest("Failed to Issue Book !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ReturnBook(string PRN, string bookId)
        {
            try
            {
                var student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.PRN == PRN && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                var book = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.BookId == bookId && q.Archieved == true && q.Active == true).AsEnumerable().FirstOrDefault();
                var bookIssue = _container.GetItemLinqQueryable<BookIssue>(true).Where(q => q.DocumentType == "bookReturn" && q.BookId == bookId && q.PRN == PRN && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                if (student != null && book != null && bookIssue != null && bookIssue.ReturnBook == true)
                {
                   
                    bookIssue.IssueBook = true;
                    bookIssue.ReturnBook = false;
                    bookIssue.ReturnDate = DateTime.Now;
                    bookIssue.Archieved = true;

                    var response = await _container.ReplaceItemAsync(bookIssue,bookIssue.Id);
                    if (response != null)
                    {
                        book.Archieved = false;
                        await _container.ReplaceItemAsync(book, book.Id);
                        return Ok("Book Returned Successfully !!!");
                    }
                    else
                    {
                        return BadRequest("Failed to Return Book !!!");
                    }
                }
                else
                {
                    return BadRequest("Failed to Return Book !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

        private Book ToBookEntity(BookModel bookModel)
        {
            Book bookEntity = new Book();

            bookEntity.BookId = bookModel.BookId;
            bookEntity.BookName = bookModel.BookName;
            bookEntity.BookAuthor = bookModel.BookAuthor;
            bookEntity.BookDomain = bookModel.BookDomain;

            bookEntity.Id = Guid.NewGuid().ToString();
            bookEntity.UId = bookEntity.Id;
            bookEntity.DocumentType = "book";

            bookEntity.CreatedOn = DateTime.Now;
            bookEntity.CreatedByName = "Kumar";
            bookEntity.CreatedBy = "Kumar's UId";

            bookEntity.UpdatedOn = DateTime.Now;
            bookEntity.UpdatedByName = "";
            bookEntity.UpdatedBy = "";

            bookEntity.Version = 1;
            bookEntity.Active = true;
            bookEntity.Archieved = false;
            return bookEntity;
        }

        private BookModel ToBookModel(Book response)
        {
            var bookModel = new BookModel();

            bookModel.UId = response.UId;
            bookModel.BookId = response.BookId;
            bookModel.BookName = response.BookName;
            bookModel.BookAuthor = response.BookAuthor;
            bookModel.BookDomain = response.BookDomain;

            return bookModel;
        }

        private BookReturnModel ToBookReturnModel(BookIssue response)
        {
            var bookReturnModel = new BookReturnModel();

            bookReturnModel.UId = response.UId;
            bookReturnModel.BookId = response.BookId;
            bookReturnModel.PRN = response.PRN;
            bookReturnModel.IssueDate = response.IssueDate;
            bookReturnModel.IssueBook = response.IssueBook;
            bookReturnModel.ReturnDate = response.ReturnDate;
            bookReturnModel.ReturnBook = response.ReturnBook;
            return bookReturnModel;
        }
    }



    /*
     feature :

   1-  user : [ I can be able to login and singup ] 
          steps : 1 students must able to singup ( add student )
          steps : 2 Stundets must be able to log in ( request : username and pass .... ) return (uid):

    need : student (dtype)   - CRUD
           librarian      - CRUD  
       
    Deadline : Tomarrow (3 Jan ) 


    2 - Book  
Features : 

    1- Add Book , Delete , Update , Issue book , return book , Request (xyz) 
    2 -  search by book-name , author , subject (get)


    librarian : 
    show books in library 
    show borrowed books 
    show total books 
     
    Need : Book CRUD
     
     */
}
