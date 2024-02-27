using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public abstract record BookDTOValidation
    {
        [Required (ErrorMessage = "Title field is required.")]
        [MinLength(2,ErrorMessage ="Minumum 2 characters.")]
        [MaxLength(50,ErrorMessage ="Maximum 50 Characters.")]
        public string Title { get; init; }
        [Required (ErrorMessage = "price field is required.")]
        [Range(10,500, ErrorMessage ="Price must between 10 and 500")]
        public Decimal Price { get; init; }
    }
}
