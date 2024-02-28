using AutoMapper;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Exceptions.BadRequestException;
using static Entities.Exceptions.NotFoundException;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger,IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book)
        {
            _manager.Book.CreateOneBook(_mapper.Map<Book>(book));
           await _manager.SaveAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookAndCheckExits(id, trackChanges);
            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
        {
            var result = await _manager.Book.GetAllBooksAsync(trackChanges);
            return _mapper.Map <IEnumerable<BookDto>>(result);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookAndCheckExits(id, trackChanges);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookAndCheckExits(id, trackChanges);
            var bookDtokForUpdate = _mapper.Map<BookDtoForUpdate>(book);
                return (bookDtokForUpdate, book);

        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtokForUpdate, Book book)
        {
            _mapper.Map(bookDtokForUpdate, book);
           await _manager.SaveAsync();
        }

        public async Task<BookDto> UpdateOneBookAsync(int id, BookDtoForUpdate book,bool trackChanges)
        {
            var entity = await GetOneBookAndCheckExits(id, trackChanges); //update için izlemek gereksizdir,   
               if (book is null || entity is null)
                throw new BookNotFoundException(id);
            if (!book.Id.Equals(id))
                throw new BookBadRequestException(id);
            var result= _mapper.Map<Book>(book);
            _manager.Book.Update(result);
           await _manager.SaveAsync();
            return _mapper.Map<BookDto>(result);
        }
        private async Task<Book> GetOneBookAndCheckExits (int id, bool trackChanges)
        {
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            return entity;
        }
    }
}
