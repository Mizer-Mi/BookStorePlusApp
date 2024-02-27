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

        public BookDto CreateOneBook(BookDtoForInsertion book)
        {
            _manager.Book.CreateOneBook(_mapper.Map<Book>(book));
            _manager.Save();
            return _mapper.Map<BookDto>(book);
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var result = _manager.Book.GetAllBooks(trackChanges);
            return _mapper.Map <IEnumerable<BookDto>>(result);
        }

        public BookDto GetOneBookById(int id, bool trackChanges)
        {
            var book= _manager.Book.GetOneBookById(id,trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return _mapper.Map<BookDto>(book);
        }

        public (BookDtoForUpdate bookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(id,trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            var bookDtokForUpdate = _mapper.Map<BookDtoForUpdate>(book);
                return (bookDtokForUpdate, book);

        }

        public void SaveChangesForPatch(BookDtoForUpdate bookDtokForUpdate, Book book)
        {
            _mapper.Map(bookDtokForUpdate, book);
            _manager.Save();
        }

        public BookDto UpdateOneBook(int id, BookDtoForUpdate book,bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges); //update için izlemek gereksizdir,   
               if (book is null || entity is null)
                throw new BookNotFoundException(id);
            if (!book.Id.Equals(id))
                throw new BookBadRequestException(id);
            var result= _mapper.Map<Book>(book);
            _manager.Book.Update(result);
            _manager.Save();
            return _mapper.Map<BookDto>(result);
        }
    }
}
