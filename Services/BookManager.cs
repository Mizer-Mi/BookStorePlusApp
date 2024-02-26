using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
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

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book= _manager.Book.GetOneBookById(id,trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return book;
        }
        public void UpdateOneBook(int id, BookDtoForUpdate book,bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, false); //update için izlemek gereksizdir,   
               if (book is null || entity is null)
                throw new BookNotFoundException(id);
            entity= _mapper.Map<BookDtoForUpdate,Book>(book);
            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
