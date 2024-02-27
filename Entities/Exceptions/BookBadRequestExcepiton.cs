using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{

    public abstract partial class BadRequestException
    {
        public sealed class BookBadRequestException : BadRequestException
        {
            public BookBadRequestException(int id) : base($"The book id's not match.")
            {
            }
        }
    }
}
