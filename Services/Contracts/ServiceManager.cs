using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public class ServiceManager : IServiceManager
    {
        private readonly IBookService _bookService;
        public IBookService BookService => _bookService;
        public ServiceManager(IRepositoryManager repositoryManager,IBookService bookService)
        {
            _bookService = bookService;
        }

    }
}
