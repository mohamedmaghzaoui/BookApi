using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using DataRequests;

namespace Controllers
{
    [ApiController]

    public class BookController : ControllerBase
    {
        private readonly Repository<Media> bookRepo;

        public BookController(Repository<Media> bookRepository)
        {
            bookRepo = bookRepository;
        }
        //get all the books from the bd
        [HttpGet("livres")]
        public ActionResult<List<Media>> GetAllBooks([FromQuery] string? author, [FromQuery] string? title, [FromQuery] string? sort)
        {
            var books = bookRepo.GetAll();

            if (!string.IsNullOrEmpty(author))
                books = books.Where(b => b.Author == author).ToList();

            if (!string.IsNullOrEmpty(title))
                books = books.Where(b => b.Title == title).ToList();
            //sort the bookss
            if (sort == "author")
                books = books.OrderBy(b => b.Author).ToList();
            else if (sort == "title")
                books = books.OrderBy(b => b.Title).ToList();

            return Ok(books);
        }
        //return one book with id from bd

        [HttpGet("livres/{id}")]
        public ActionResult<Media> GetBookById(int id)
        {
            var book = bookRepo.GetWithId(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // create a new book
        [HttpPost("livres")]
        public ActionResult CreateBook([FromBody] BookRequest bookRequestJson)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Media book;

            if (bookRequestJson.MediaType == "paperbook")
                book = new PaperBook { Title = bookRequestJson.Title, Author = bookRequestJson.Author };
            else if (bookRequestJson.MediaType == "ebook")
                book = new Ebook { Title = bookRequestJson.Title, Author = bookRequestJson.Author };
            else
                return BadRequest("Invalid mediaType. Use 'ebook' or 'paperbook'.");

            bookRepo.Add(book);
            return Ok("book created");
        }

       
        //delete a book
        [HttpDelete("livres/{id}")]
        public ActionResult DeleteBook(int id)
        {
            bookRepo.Delete(id);
            return Ok("book deleted");
        }
        //modify a book

        [HttpPut("livres/{id}")]
        public ActionResult UpdateBook(int id, [FromBody] BookRequest bookRequestJson)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = bookRepo.GetWithId(id);
            if (book == null) return NotFound();

            book.Title = bookRequestJson.Title;
            book.Author = bookRequestJson.Author;

            bookRepo.Update(book);
            return Ok("Book modified successfully.");
        }
    }
}
