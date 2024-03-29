﻿using AutoMapper;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private readonly IDataShaper<BookDto> _shaper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<BookDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
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

        public async Task<(IEnumerable<ExpandoObject>, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            if (!bookParameters.ValidPriceRange)
                throw new PriceOutofRangeBadRequestException();

            var booksWithMetaData = await _manager.Book.GetAllBooksAsync(bookParameters,trackChanges);

            var booksDto= _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            var shapedData = _shaper.ShapeData(booksDto, bookParameters.Fields);
            return (books: shapedData, metaData: booksWithMetaData.MetaData);
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
