using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using static Entities.Exceptions.NotFoundException;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")] int id)
        {
            var book = _manager.BookService.GetOneBookById(id, false);
            return Ok(book);

        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDtoForInsertion book)
        {
            if (book is null)
                return BadRequest();
            _manager.BookService.CreateOneBook(book);
            return StatusCode(201, book);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate book)
        {
            if (book is null)
                return BadRequest();
            _manager.BookService.UpdateOneBook(id, book, false);
            return StatusCode(200, book);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            _manager.BookService.DeleteOneBook(id, false);
            return StatusCode(204);
        }
        [HttpDelete]
        public IActionResult DeleteBooks()
        {
            /*
            _context.Database.ExecuteSqlRaw(@"TRUNCATE TABLE ""Books""");
            _context.SaveChanges();
            */
            return Ok();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();
            var result = _manager.BookService.GetOneBookForPatch(id,true);
            bookPatch.ApplyTo(result.bookDtoForUpdate);
            TryValidateModel(result.bookDtoForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate,result.book);
            //_manager.BookService.UpdateOneBook(id, entity, true);
            return NoContent();
        }


    }
}
