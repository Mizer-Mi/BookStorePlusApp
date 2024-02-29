using Entities.DTO;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IBookService _bookService;
        public ServiceManager(IRepositoryManager repositoryManager, IBookService bookService,ILoggerService logger, IDataShaper<BookDto> shaper)
        {
            _bookService = bookService;
        }
        public IBookService BookService => _bookService;

    }
}
