using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;
using static Entities.Exceptions.NotFoundException;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LogFilterAttiribute))]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] BookParameters bookParameters)
        {
            var pagedResult = await _manager.BookService.GetAllBooksAsync(bookParameters,false);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.Item1);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);
            return Ok(book);

        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDtoForInsertion book)
        {
            var result = await _manager.BookService.CreateOneBookAsync(book);
            return StatusCode(201, result);
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate book)
        {
            await _manager.BookService.UpdateOneBookAsync(id, book, false);
            return StatusCode(200, book);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return StatusCode(204);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();
            var result = await _manager.BookService.GetOneBookForPatchAsync(id,true);
            bookPatch.ApplyTo(result.bookDtoForUpdate);
            TryValidateModel(result.bookDtoForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
           await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate,result.book);
            //_manager.BookService.UpdateOneBook(id, entity, true);
            return Ok(result.book);
        }


    }
}
